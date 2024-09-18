using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.UserServices;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.SalesServices
{

    public class SalesService : ISalesService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly IOrderService _orderService;
        readonly IUserService _userService;
        readonly IPaymentService _paymentService;

        public SalesService(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService, IUserService userService, IPaymentService paymentService)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderService = orderService;
            _userService = userService;
            _paymentService = paymentService;

        }
        public async Task AddSalesAsync(SalesDto salesDto)
        {
            var sales = _mapper.Map<Sales>(salesDto);

            sales.Id = Guid.NewGuid();
            await _unitOfWork.GetRepository<Sales>().AddAsync(sales);
            await _unitOfWork.SaveChangesAsync();

            //if (salesDto.Bill != false)
            //{
            //    await _billService.AddBillAfterSalesAsync(sales.Id);
            //}


        }

        public async Task AddSalesStatusAsync(SalesStatusDto salesStatusDto)
        {
            var sales = _mapper.Map<SalesStatus>(salesStatusDto);

            await _unitOfWork.GetRepository<SalesStatus>().AddAsync(sales);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CustomerHaveActiveSalesAsync(Guid id)
        {
            var activesales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus)
                .FirstOrDefaultAsync(x => x.Status && x.CustomerId == id && (x.SalesStatus.Name != "İptal" || x.SalesStatus.Name != "Ürünler Teslim Edildi"));
            if (activesales != null)
            {
                return true;
            }
            return false;
        }

        public async Task<SalesDto> CreateSalesAsync(SalesDto salesDto)
        {
            var s = _mapper.Map<Sales>(salesDto);
            s.Id = Guid.NewGuid();

            var status = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.Name == "Başladı").FirstOrDefaultAsync();
            s.SalesStatusId = status.Id;
            await _unitOfWork.GetRepository<Sales>().AddAsync(s);
            await _unitOfWork.SaveChangesAsync();

            //if (salesDto.Bill != false)
            //{
            //    await _billService.AddBillAfterSalesAsync(s.Id);
            //}
            var gs = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Include(x => x.Customer).ThenInclude(x => x.CustomerType).FirstOrDefaultAsync(x => x.Id == s.Id);
            return _mapper.Map<SalesDto>(gs);
        }

        public async Task DeleteSalesAsync(Guid id)
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetByIdAsync(id);
            sales.Status = false;
            _unitOfWork.GetRepository<Sales>().Update(sales);
            await _unitOfWork.SaveChangesAsync();
            await _orderService.DeleteAllOrderWithSalesIdAsync(id);
        }

        public async Task DeleteSalesStatusAsync(Guid id)
        {
            var sales = await _unitOfWork.GetRepository<SalesStatus>().GetByIdAsync(id);
            sales.Status = false;
            _unitOfWork.GetRepository<SalesStatus>().Update(sales);
            await _unitOfWork.SaveChangesAsync();
            var saleses = await _unitOfWork.GetRepository<Sales>().GetWhere(x => x.Status && x.SalesStatusId == id).ToListAsync();
            foreach (var item in saleses.Select(x => x.Id))
            {
                await DeleteSalesAsync(item);
            }

        }

        public async Task<List<SalesDto>> GetAllSalesesAsync()
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Include(x => x.Customer).Where(x => x.Status && x.SalesStatus.Status).ToListAsync();
            var msales = _mapper.Map<List<SalesDto>>(sales);
            foreach (var item in msales)
            {
                item.OrderDtos = await _orderService.GetOrdersWithSalesIdAsync(item.Id);
                foreach (var tem in item.OrderDtos)
                {
                    tem.OrderTotalPrice = tem.Piece * tem.ProductDto.Price;
                    tem.OrderTotalTax = tem.OrderTotalPrice * tem.ProductDto.TaxDto.Rate / 100;
                    tem.OrderTotalPriceWithTax = tem.OrderTotalPrice + tem.OrderTotalTax;
                    item.OrderTotalPrice = item.OrderTotalPrice + tem.OrderTotalPriceWithTax;
                }


            }
            return msales;
        }

        public async Task<List<SalesStatusDto>> GetAllSalesStatusesAsync()
        {
            var sales = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.Status).ToListAsync();
            var msales = _mapper.Map<List<SalesStatusDto>>(sales);
            return msales;
        }

        public async Task<SalesStatusDto> GetSalesStatusWithIdAsync(Guid id)
        {
            var ss = await _unitOfWork.GetRepository<SalesStatus>().GetByIdAsync(id);
            var mss = _mapper.Map<SalesStatusDto>(ss);
            return mss;
        }

        public async Task<SalesDto> GetSalesWithIdAsync(Guid id)
        {
            var s = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            var ms = _mapper.Map<SalesDto>(s);
            ms.OrderDtos = await _orderService.GetOrdersWithSalesIdAsync(id);
            ms.CreateUserDto = await _userService.GetUserWithIdAsync(s.CreateUserId);
            if (ms.UpdateUserId != null)
            {
                ms.UpdateUserDto = await _userService.GetUserWithIdAsync(Guid.Parse(s.UpdateUserId.ToString()));
            }
            foreach (var item in ms.OrderDtos)
            {
                item.OrderTotalPrice = Math.Round(item.Piece * item.ProductDto.Price, 2);
                if (item.Discount > 0)
                {
                    var fark = item.OrderTotalPrice * (item.Discount / 100);
                    item.OrderTotalPrice -= fark;
                }
                ms.OrderTotalTaxFreePrice = Math.Round(ms.OrderTotalTaxFreePrice + item.OrderTotalPrice, 2);
                item.OrderTotalTax = Math.Round(item.OrderTotalPrice * item.ProductDto.TaxDto.Rate / 100, 2);
                item.OrderTotalPriceWithTax = Math.Round(item.OrderTotalPrice + item.OrderTotalTax, 2);
                ms.OrderTotalPrice = Math.Round(ms.OrderTotalPrice + item.OrderTotalPriceWithTax, 2);
                ms.OrderTotalTax = Math.Round(ms.OrderTotalTax + item.OrderTotalTax, 2);

            }
            return ms;
        }

        public async Task<List<SalesDto>> GetSalesWithStatusIdAsync(Guid id)
        {
            var s = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.SalesStatusId == id && x.Status && x.SalesStatus.Status).ToListAsync();
            var ms = _mapper.Map<List<SalesDto>>(s);
            return ms;
        }

        public async Task UpdateSalesAsync(SalesDto salesDto)
        {
            var s = _mapper.Map<Sales>(salesDto);
            s.Status = true;
            _unitOfWork.GetRepository<Sales>().Update(s);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSalesStatusAsync(SalesStatusDto salesStatusDto)
        {
            var s = _mapper.Map<SalesStatus>(salesStatusDto);
            s.Status = true;
            _unitOfWork.GetRepository<SalesStatus>().Update(s);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<SalesDto>> GetSalesesWithCustomerIdAsync(Guid id)
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.CustomerId == id).ToListAsync();
            var ms = _mapper.Map<List<SalesDto>>(sales);
            return ms;
        }

        public async Task<List<SalesDto>> GetActiveSalesesWithCustomerIdAsync(Guid id)
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.CustomerId == id && x.SalesStatus.Name != "Ürünler Teslim Edildi" && x.SalesStatus.Name != "Iptal").ToListAsync();
            var ms = _mapper.Map<List<SalesDto>>(sales);
            return ms;
        }

        public async Task<List<SalesDto>> GetAllActiveSalesesAsync()
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.Customer).Include(x => x.SalesStatus).Where(x => x.Status && x.Customer.Status && x.SalesStatus.Status
            && (x.SalesStatus.LineNumber != 0 && x.SalesStatus.LineNumber != 5)).ToListAsync();

            var ms = _mapper.Map<List<SalesDto>>(sales);
            return ms;
        }



        public async Task<List<SalesDto>> GetAllPassiveSalesesAsync()
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.Customer).Include(x => x.SalesStatus).Where(x => x.Status && x.Customer.Status && x.SalesStatus.Status
          && (x.SalesStatus.LineNumber == 0 || x.SalesStatus.LineNumber == 5)).ToListAsync();

            var ms = _mapper.Map<List<SalesDto>>(sales);
            return ms;
        }

        public async Task<int> SetSalesNextStepAsync(SalesDto salesDto)
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.Id == salesDto.Id).FirstOrDefaultAsync();
            if (sales != null)
            {
                var salesstatues = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.Status && x.LineNumber == sales.SalesStatus.LineNumber + 1).FirstOrDefaultAsync();

                sales.SalesStatusId = salesstatues.Id;
                _unitOfWork.GetRepository<Sales>().Update(sales);
                if (salesstatues.LineNumber == 5)
                {
                    var detail = await GetSalesWithIdAsync(sales.Id);
                    await _paymentService.AddCustomerDebtAsync(detail);
                    

                }
                await _unitOfWork.SaveChangesAsync();

                return salesstatues.LineNumber;
            }
            return -1;

        }

        public async Task<int> SetSalesBackStepAsync(SalesDto salesDto)
        {
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.Id == salesDto.Id).FirstOrDefaultAsync();
            if (sales != null)
            {
                var salesstatues = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.Status && x.LineNumber == sales.SalesStatus.LineNumber - 1).FirstOrDefaultAsync();
                if (salesstatues.LineNumber == 0)
                {
                    await _orderService.DeleteAllOrderWithSalesIdAsync(sales.Id);
                }

                sales.SalesStatusId = salesstatues.Id;
                _unitOfWork.GetRepository<Sales>().Update(sales);
                await _unitOfWork.SaveChangesAsync();
                return sales.SalesStatus.LineNumber;
            }


            return -1;

        }

        public async Task<SalesDto> GetSalesNextStepAsync(Guid id)
        {
            var salesdto = new SalesDto();
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.Id == id).FirstOrDefaultAsync();

            if (sales != null)
            {
                salesdto = _mapper.Map<SalesDto>(sales);
                var ss = _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.LineNumber == x.LineNumber + 1).FirstOrDefaultAsync();

                if (ss != null)
                {
                    salesdto.SalesStatusDto = _mapper.Map<SalesStatusDto>(ss);
                }

            }

            return salesdto;
        }

        public async Task<SalesDto> GetSalesBackStepAsync(Guid id)
        {
            var salesdto = new SalesDto();
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.Id == id).FirstOrDefaultAsync();

            if (sales != null)
            {
                salesdto = _mapper.Map<SalesDto>(sales);
                var ss = _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.LineNumber == x.LineNumber - 1).FirstOrDefaultAsync();

                if (ss != null)
                {
                    salesdto.SalesStatusDto = _mapper.Map<SalesStatusDto>(ss);
                }

            }

            return salesdto;
        }

        public async Task<SalesDto> GetSalesByIdWithStepsAsync(Guid id)
        {
            var salesdto = new SalesDto();
            var sales = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.Id == id).FirstOrDefaultAsync();

            if (sales != null)
            {
                salesdto = _mapper.Map<SalesDto>(sales);
                var sb = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.LineNumber == sales.SalesStatus.LineNumber - 1).FirstOrDefaultAsync();

                if (sb != null)
                {
                    salesdto.BackSalesStatusDto = _mapper.Map<SalesStatusDto>(sb);
                }
                var sn = await _unitOfWork.GetRepository<SalesStatus>().GetWhere(x => x.LineNumber == sales.SalesStatus.LineNumber + 1).FirstOrDefaultAsync();

                if (sn != null)
                {
                    salesdto.NextSalesStatusDto = _mapper.Map<SalesStatusDto>(sn);
                }
            }

            return salesdto;
        }

        public async Task<SalesDto> CreateBasicSalesAsync(BasicOrderDto basicOrderDto)
        {


            var sales = new Sales();
            sales.Id = Guid.NewGuid();
            sales.CustomerId = basicOrderDto.CustomerId;
            var salesstatus = await _unitOfWork.GetRepository<SalesStatus>().GetFirstWhereAsync(x => x.Status && x.Name == "Başladı");
            sales.SalesStatusId = salesstatus.Id;
            await _unitOfWork.GetRepository<Sales>().AddAsync(sales);
            await _unitOfWork.SaveChangesAsync();

            foreach (var item in basicOrderDto.Orders.Where(x=>x.Quantity>0).ToList())
            {
                Order order = new Order();
                order.SalesId = sales.Id;
                order.ProductId = item.ProductId;
                order.Piece = item.Quantity;
                order.Discount = Convert.ToDouble(item.Discount);
                order.Status = true;

                await _unitOfWork.GetRepository<Order>().AddAsync(order);

            }

            await _unitOfWork.SaveChangesAsync();

            var s = await _unitOfWork.GetRepository<Sales>().GetByIdAsync(sales.Id);
            var ms = _mapper.Map<SalesDto>(s);
            return ms;

        }
    }
}