using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.SalesServices
{
    public interface ISalesService
    {
        public Task<List<SalesDto>> GetAllSalesesAsync();
        public Task<List<SalesDto>> GetAllActiveSalesesAsync();
        public Task<List<SalesDto>> GetAllPassiveSalesesAsync();
        public Task<SalesDto> GetSalesWithIdAsync(Guid id);
        public Task<SalesDto> CreateSalesAsync(SalesDto salesDto);
        public Task<SalesDto> CreateBasicSalesAsync(BasicOrderDto basicOrderDto);
        public Task<List<SalesDto>> GetSalesWithStatusIdAsync(Guid id);
        public Task<List<SalesDto>> GetSalesesWithCustomerIdAsync(Guid id);
        public Task<List<SalesDto>> GetActiveSalesesWithCustomerIdAsync(Guid id);
        public Task AddSalesAsync(SalesDto salesDto);
        public Task UpdateSalesAsync(SalesDto salesDto);
        public Task DeleteSalesAsync(Guid id);



        public Task<List<SalesStatusDto>> GetAllSalesStatusesAsync();
        public Task<SalesStatusDto> GetSalesStatusWithIdAsync(Guid id);
        public Task AddSalesStatusAsync(SalesStatusDto salesStatusDto);
        public Task UpdateSalesStatusAsync(SalesStatusDto salesStatusDto);
        public Task DeleteSalesStatusAsync(Guid id);

        public Task<bool> CustomerHaveActiveSalesAsync(Guid id);
        public Task<int> SetSalesNextStepAsync(SalesDto salesDto);
        public Task<int> SetSalesBackStepAsync(SalesDto salesDto);
        public Task<SalesDto> GetSalesNextStepAsync(Guid id);
        public Task<SalesDto> GetSalesBackStepAsync(Guid id);
        public Task<SalesDto> GetSalesByIdWithStepsAsync(Guid id);
    }
}
