using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.SalesServices
{
    public interface IPaymentService
    {

        public Task AddPaymentAsync(PaymentDto paymentDto);
        public Task AddDebtAsync(PaymentDto paymentDto);
        public Task AddCustomerDebtAsync(SalesDto salesDto);
        public Task DeletePaymentAsync(Guid id);
        public Task<List<PaymentDto>> GetAllPaymentsAsync();
        public Task<PaymentDto> GetPaymentWithIdAsync(Guid id);
        public Task<List<PaymentDto>> GetAllCustomerPaymentsWithIdAsync(Guid id);
        public Task<List<PaymentWayDto>> GetAllPaymentWaysAsync();
    }
}
