using AutoMapper;
using EFaturaEDMTest;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.Helpers;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.SalesServices
{
    public class BillService : IBillService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
     
        public BillService(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService) {


            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _orderService = orderService;
        }

        //public async Task AddBillAfterSalesAsync(Guid id)
        //{
        //  //  var sales  =await _unitOfWork.GetRepository<Sales>().GetByIdAsync ( id);

        //  //  if (sales!=null)
        //  //  {
        //  //      Bill bill = new Bill();
        //  //      bill.SalesId= sales.Id;
        //  //      bill.BillNumber = 1;

        //  //     await _unitOfWork.GetRepository<Bill>().AddAsync(bill);
        //  //  }
        //  //await  _unitOfWork.SaveChangesAsync();
        //}

        public async Task AddBillAsync(BillDto billDto)
        {
           var b = _mapper.Map<Bill>(billDto);
          await  _unitOfWork.GetRepository<Bill>().AddAsync(b);
           await _unitOfWork.SaveChangesAsync();
        }

        public Task AddCompanyAsync(CompanyDto companyDto)
        {
            throw new NotImplementedException();
        }

        public async Task AddCustomerDebtAsync(SalesDto salesDto)
        {
            var debt = new Payment();
            debt.CustomerId= salesDto.CustomerId;
            debt.Total = salesDto.OrderTotalPrice;

            var debttype =await _unitOfWork.GetRepository<PaymentType>().GetWhere(x => x.Status && x.Type == "Negatif").FirstOrDefaultAsync();
            if (debttype!=null)
            {

                debt.PaymentTypeId=debttype.Id;
            }

            var pt =await _unitOfWork.GetRepository<PaymentWay>().GetFirstWhereAsync(x => x.Status && x.Name == "Diğer");
            if (pt!=null)
            {
                debt.PaymentWayId = pt.Id;
                
            }
            debt.PaymentDay= DateTime.Now;
          await  _unitOfWork.GetRepository<Payment>().AddAsync(debt);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task AddDebtAsync(PaymentDto paymentDto)
        {
            var p = _mapper.Map<Payment>(paymentDto);
            p.Id = Guid.NewGuid();
            var debttype = await _unitOfWork.GetRepository<PaymentType>().GetWhere(x => x.Status && x.Type == "Negatif").FirstOrDefaultAsync();
            if (debttype != null)
            {

                p.PaymentTypeId = debttype.Id;
            }
            await _unitOfWork.GetRepository<Payment>().AddAsync(p);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddPaymentAsync(PaymentDto paymentDto)
        {
            var p = _mapper.Map<Payment>(paymentDto);
            p.Id = Guid.NewGuid();
            var debttype = await _unitOfWork.GetRepository<PaymentType>().GetWhere(x => x.Status && x.Type == "Pozitif").FirstOrDefaultAsync();
            if (debttype != null)
            {

                p.PaymentTypeId = debttype.Id;
            }
            await _unitOfWork.GetRepository<Payment>().AddAsync(p);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task AddSupplierBillAsync(BillDto billDto)
        {
            
            var b =_mapper.Map<Bill>(billDto);
            var bt = await _unitOfWork.GetRepository<BillType>().GetFirstWhereAsync(x => x.Status && x.Name =="Gelen Fatura");
            b.BillTypeId = bt.Id;


        }

        public async Task DeleteBillAsync(Guid id)
        {
            var b =await _unitOfWork.GetRepository<Bill>().GetByIdAsync(id);
            b.Status = false;
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
           var c  = await _unitOfWork.GetRepository<Company>().GetByIdAsync(id);
            c.Status = false;
            _unitOfWork.GetRepository<Company>().Update(c);
           await _unitOfWork.SaveChangesAsync();
        }

        public Task DeletePaymentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task EditBillSettingAsync(BillSettingDto billSettingDto)
        {
            var b = _mapper.Map<BillSetting>(billSettingDto);
            b.Status = true;
            _unitOfWork.GetRepository<BillSetting>().Update(b);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task EditCompanyAsync(CompanyDto companyDto)
        {
            var c =_mapper.Map<Company>(companyDto);
            c.Status = true;
            _unitOfWork.GetRepository<Company>().Update(c);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditEBillSettingAsync(EBillSettingDto eBillSettingDto)
        {
           var e =_mapper.Map<EBillSetting>(eBillSettingDto);
            e.Status = true;
            _unitOfWork.GetRepository<EBillSetting>().Update(e);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<BillDto>> GetAllBillsAsync()
        {
            var bill = await _unitOfWork.GetRepository<Bill>().GetAll().Include(x => x.BillType)
                .Where(x => x.Status && x.BillType.Status).ToListAsync();
            var mb = _mapper.Map<List<BillDto>>(bill);
            foreach (var b in mb)
            {

               var items = await _unitOfWork.GetRepository<BillItem>().GetAll().Where(x=>x.Status && x.BillId==b.Id).ToListAsync();
                b.BillItems=_mapper.Map<List<BillItemDto>>(items);
                var k = await _unitOfWork.GetRepository<Customer>().GetFirstWhereAsync(x => x.Id == b.InvoicerId);
                b.InvoicerDto = _mapper.Map<CustomerDto>(k);
                var kk = await _unitOfWork.GetRepository<Customer>().GetFirstWhereAsync(x => x.Id == b.CustomerId);
                b.RecipientDto = _mapper.Map<CustomerDto>(kk);

                b.Total = items.Sum(x=>x.Total);

            }

            return mb;
        }

        public async Task<List<BillSettingDto>> GetAllBillSettingsAsync()
        {
            var list =await _unitOfWork.GetRepository<BillSetting>().GetWhere(x => x.Status).ToListAsync();
            var mlist = _mapper.Map<List<BillSettingDto>>(list);
            return mlist;
        }

        //public async Task<List<BillDto>> GetAllBillsWithCustomerIdAsync(Guid id)
        //{
        //    var bill = await _unitOfWork.GetRepository<Bill>().GetAll().Include(x => x.Sales).ThenInclude(x => x.Customer).Include(x => x.Sales.SalesStatus)
        //        .Where(x => x.Status && x.Sales.Status && x.Sales.CustomerId==id ).ToListAsync();
        //    var mb = _mapper.Map<List<BillDto>>(bill);
        //    return mb;
        //}

        public async Task<List<CompanyDto>> GetAllCompanysAsync()
        {
            var list = await _unitOfWork.GetRepository<Company>().GetWhere(x => x.Status).ToListAsync();
            var ml = _mapper.Map<List<CompanyDto>>(list);
            return ml;
        }

        public async Task<List<PaymentDto>> GetAllCustomerPaymentsWithIdAsync(Guid id)
        {
            var payments = await _unitOfWork.GetRepository<Payment>().GetAll().Include(x=>x.PaymentWay).Include(x => x.Customer).Include(x => x.PaymentType)
                 .Where(x => x.Status && x.Customer.Status && x.CustomerId == id).OrderByDescending(x=>x.CreateTime).ToListAsync();

            var mpayments = _mapper.Map<List<PaymentDto>>(payments);

            return mpayments;
        }

        public async Task<List<EBillSettingDto>> GetAllEbillSettingsAsync()
        {
            var bl =await _unitOfWork.GetRepository<EBillSetting>().GetWhere(x => x.Status).ToListAsync();
            var bld=_mapper.Map<List<EBillSettingDto>>(bl);
            return bld;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var list = await   _unitOfWork.GetRepository<Payment>().GetAll().Include(x => x.PaymentType).Include(x => x.Customer).Where(x => x.Status && x.Customer.Status).ToListAsync();

            var mlist =_mapper.Map<List<PaymentDto>>(list);
            return mlist;
        }

        public async Task<List<PaymentWayDto>> GetAllPaymentWaysAsync()
        {
            var list =await _unitOfWork.GetRepository<PaymentWay>().GetAll().Where(x=>x.Status).ToListAsync();

            var mlist =_mapper.Map<List<PaymentWayDto>>(list);
            return mlist;
        }

        public async Task<BillSettingDto> GetBillSettingWithIdAsync(Guid id)
        {
            var bil =await _unitOfWork.GetRepository<BillSetting>().GetByIdAsync(id);
            var mb=_mapper.Map<BillSettingDto>(bil);
            return mb;
        }

        //public async Task<BillDto> GetBillWithIdAsync(Guid id)
        //{
        //    var bill = await _unitOfWork.GetRepository<Bill>().GetByIdAsync(id);

        //    if (bill==null)
        //    {
        //        return null;
        //    }

        //    var mbox = _mapper.Map<BillDto>(bill);
           
           

        //    var s = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == bill.SalesId);
        //    var ms = _mapper.Map<SalesDto>(s);
        //    ms.OrderDtos = await _orderService.GetOrdersWithSalesIdAsync(s.Id);

        //    foreach (var item in ms.OrderDtos)
        //    {
        //        item.OrderTotalPrice = Math.Round(item.Piece * item.PriceListDto.Price, 2);
        //        if (item.Discount > 0)
        //        {
        //            var fark = item.OrderTotalPrice * (item.Discount / 100);
        //            item.OrderTotalPrice -= fark;
        //        }
        //        ms.OrderTotalTaxFreePrice = Math.Round(ms.OrderTotalTaxFreePrice + item.OrderTotalPrice, 2);
        //        item.OrderTotalTax = Math.Round(item.OrderTotalPrice * item.PriceListDto.TaxDto.Rate / 100, 2);
        //        item.OrderTotalPriceWithTax = Math.Round(item.OrderTotalPrice + item.OrderTotalTax, 2);
        //        ms.OrderTotalPrice = Math.Round(ms.OrderTotalPrice + item.OrderTotalPriceWithTax, 2);
        //        ms.OrderTotalTax = Math.Round(ms.OrderTotalTax + item.OrderTotalTax, 2);

        //    }


        //    mbox.SalesDto = ms;


        //    return mbox;
        //}

        //public async Task<BillDto> GetBillWithSalesIdAsync(Guid id)
        //{
        //    var bill = await _unitOfWork.GetRepository<Bill>().GetWhere(x=>x.SalesId==id).FirstOrDefaultAsync();
        //    if (bill==null)
        //    {
        //        return null;
        //    }
        //    var mbox = _mapper.Map<BillDto>(bill);

        //    var s = await _unitOfWork.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == bill.SalesId);
        //    var ms = _mapper.Map<SalesDto>(s);
        //    ms.OrderDtos = await _orderService.GetOrdersWithSalesIdAsync(s.Id);

        //    foreach (var item in ms.OrderDtos)
        //    {
        //        item.OrderTotalPrice = Math.Round(item.Piece * item.PriceListDto.Price, 2);
        //        if (item.Discount > 0)
        //        {
        //            var fark = item.OrderTotalPrice * (item.Discount / 100);
        //            item.OrderTotalPrice -= fark;
        //        }

        //        ms.OrderTotalTaxFreePrice = Math.Round(ms.OrderTotalTaxFreePrice + item.OrderTotalPrice, 2);

        //        item.OrderTotalTax = Math.Round(item.OrderTotalPrice * item.PriceListDto.TaxDto.Rate / 100, 2);
        //        item.OrderTotalPriceWithTax = Math.Round(item.OrderTotalPrice + item.OrderTotalTax, 2);
        //        ms.OrderTotalPrice = Math.Round(ms.OrderTotalPrice + item.OrderTotalPriceWithTax, 2);
        //        ms.OrderTotalTax = Math.Round(ms.OrderTotalTax + item.OrderTotalTax, 2);

        //    }

        //    mbox.SalesDto = ms;

           


        //    return mbox;
        //}

        public async Task<CompanyDto> GetCompanyWithAsync(Guid id)
        {
            var c =await _unitOfWork.GetRepository<Company>().GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(c);
        }

        public async Task<EBillSettingDto> GetEBillSettingWithIdAsync(Guid id)
        {
           var ebil =await _unitOfWork.GetRepository<EBillSetting>().GetFirstWhereAsync(x=>x.Status && x.Id==id);
            var me =_mapper.Map<EBillSettingDto>(ebil);
            return me;
        }

        public async Task<PaymentDto> GetPaymentWithIdAsync(Guid id)
        {
           var payment =await _unitOfWork.GetRepository<Payment>().GetAll().Include(x=>x.Customer).Include(x=>x.PaymentType).FirstOrDefaultAsync(x=>x.Id== id);
            var mp = _mapper.Map<PaymentDto>(payment);
            return mp;
        }

        //public async Task<string> SendEBillingAsync(Guid id)
        //{

        //    var bil =await _unitOfWork.GetRepository<Bill>().GetAll().Include(x=>x.Sales).ThenInclude(x=>x.Customer).FirstOrDefaultAsync(x=>x.Id== id && x.Status);

        //    EBillHelper eBillHelper = new EBillHelper(_unitOfWork);

        //      var res =  await eBillHelper.EdmSendInvoiceAsync(bil);

        //    return res;
        //}

        //public async Task WriteBillAsync(BillDto billDto)
        //{
        //    var bilsetting =await _unitOfWork.GetRepository<BillSetting>().GetFirstAsync();
        //    PrintHelper printHelper = new PrintHelper();
        //    printHelper.PrintBill(billDto,_mapper.Map<BillSettingDto>(bilsetting));
           
        //}
    }
}
