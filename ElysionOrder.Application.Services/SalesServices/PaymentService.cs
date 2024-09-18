using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.SalesServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task AddCustomerDebtAsync(SalesDto salesDto)
        {
            var debt = new Payment();
            debt.CustomerId = salesDto.CustomerId;
            debt.Total = salesDto.OrderTotalPrice;

            var debttype = await _unitOfWork.GetRepository<PaymentType>().GetWhere(x => x.Status && x.Type == "Negatif").FirstOrDefaultAsync();
            if (debttype != null)
            {

                debt.PaymentTypeId = debttype.Id;
            }

            var pt = await _unitOfWork.GetRepository<PaymentWay>().GetFirstWhereAsync(x => x.Status && x.Name == "Diğer");
            if (pt != null)
            {
                debt.PaymentWayId = pt.Id;

            }
            debt.PaymentDay = DateTime.Now;
            await _unitOfWork.GetRepository<Payment>().AddAsync(debt);
            await _unitOfWork.SaveChangesAsync();

            
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
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var list = await _unitOfWork.GetRepository<Payment>().GetAll().Include(x => x.PaymentType).Include(x => x.Customer).Where(x => x.Status && x.Customer.Status).ToListAsync();

            var mlist = _mapper.Map<List<PaymentDto>>(list);
            return mlist;
        }

        public async Task<List<PaymentWayDto>> GetAllPaymentWaysAsync()
        {
            var list = await _unitOfWork.GetRepository<PaymentWay>().GetAll().Where(x => x.Status).ToListAsync();

            var mlist = _mapper.Map<List<PaymentWayDto>>(list);
            return mlist;
        }

        public async Task<PaymentDto> GetPaymentWithIdAsync(Guid id)
        {
            var payment = await _unitOfWork.GetRepository<Payment>().GetAll().Include(x => x.Customer).Include(x => x.PaymentType).FirstOrDefaultAsync(x => x.Id == id);
            var mp = _mapper.Map<PaymentDto>(payment);
            return mp;
        }

        public async Task<List<PaymentDto>> GetAllCustomerPaymentsWithIdAsync(Guid id)
        {
            var payments = await _unitOfWork.GetRepository<Payment>().GetAll().Include(x => x.PaymentWay).Include(x => x.Customer).Include(x => x.PaymentType)
                 .Where(x => x.Status && x.Customer.Status && x.CustomerId == id).OrderByDescending(x => x.CreateTime).ToListAsync();

            var mpayments = _mapper.Map<List<PaymentDto>>(payments);

            return mpayments;
        }

        public Task DeletePaymentAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
