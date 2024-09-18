using EFaturaEdm;
using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.SalesServices
{
    public interface IBillService
    {

        public Task<List<BillDto>> GetAllBillsAsync();

        public Task AddBillAsync(BillDto billDto);
        public Task AddSupplierBillAsync(BillDto billDto);
        public Task DeleteBillAsync(Guid id);

        public Task AddCompanyAsync(CompanyDto companyDto);
        public Task EditCompanyAsync(CompanyDto companyDto);
        public Task DeleteCompanyAsync(Guid id);
        public Task<List<CompanyDto>> GetAllCompanysAsync();
        public Task<CompanyDto> GetCompanyWithAsync(Guid id);

        public Task<List<EBillSettingDto>> GetAllEbillSettingsAsync();
        public Task EditEBillSettingAsync(EBillSettingDto eBillSettingDto);
        public Task<EBillSettingDto> GetEBillSettingWithIdAsync(Guid id);

        public Task<List<BillSettingDto>> GetAllBillSettingsAsync();
        public Task<BillSettingDto> GetBillSettingWithIdAsync(Guid id);
        public Task EditBillSettingAsync(BillSettingDto billSettingDto);

        public Task<string> LoginEdmAsync();
        public Task LogOutEdmAsync(string sessionId);
        public Task SendInvoiceAsync(Guid salesId);
        public Task<EDM_CheckUserResponse> GetCustomerBillTypeAsync(Guid customerId);


    }
}
