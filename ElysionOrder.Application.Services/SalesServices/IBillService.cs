using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.SalesServices
{
    public interface IBillService
    {

        public Task<List<BillDto>> GetAllBillsAsync ();
      //  public Task<List<BillDto>> GetAllBillsWithCustomerIdAsync (Guid id);
      //  public Task<BillDto> GetBillWithIdAsync (Guid id);
      //  public Task<BillDto> GetBillWithSalesIdAsync(Guid id);
        public Task AddBillAsync (BillDto billDto);
        public Task AddSupplierBillAsync (BillDto billDto);
       // public Task AddBillAfterSalesAsync (Guid id);
        public Task DeleteBillAsync (Guid id);

       // public Task WriteBillAsync(BillDto billDto);

        public Task AddCompanyAsync (CompanyDto companyDto);
        public Task EditCompanyAsync (CompanyDto companyDto);
        public Task DeleteCompanyAsync (Guid id);
        public Task<List<CompanyDto>> GetAllCompanysAsync ();
        public Task<CompanyDto> GetCompanyWithAsync (Guid id);

        public Task<List<EBillSettingDto>> GetAllEbillSettingsAsync();
        public Task EditEBillSettingAsync(EBillSettingDto eBillSettingDto);
        public Task<EBillSettingDto> GetEBillSettingWithIdAsync(Guid id);



        public Task AddPaymentAsync(PaymentDto paymentDto);
        public Task AddDebtAsync(PaymentDto paymentDto);
        public Task AddCustomerDebtAsync(SalesDto salesDto);
        public Task DeletePaymentAsync(Guid id);
        public Task<List<PaymentDto>> GetAllPaymentsAsync();
        public Task<PaymentDto> GetPaymentWithIdAsync(Guid id);
        public Task<List<PaymentDto>> GetAllCustomerPaymentsWithIdAsync(Guid id);

        public Task<List<PaymentWayDto>> GetAllPaymentWaysAsync();



       
        public Task<List<BillSettingDto>> GetAllBillSettingsAsync();
        public Task<BillSettingDto> GetBillSettingWithIdAsync(Guid id);
        public Task  EditBillSettingAsync(BillSettingDto billSettingDto);

      //  public Task<string> SendEBillingAsync(Guid id);
            

    }
}
