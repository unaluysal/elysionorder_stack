using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.CustomerServices
{
    public interface ICustomerService
    {
        public Task<List<CustomerTypeDto>> GetAllCustomerTypesAsync();
        public Task<CustomerTypeDto> GetCustomerTypeWithIdAsync(Guid id);
        public Task AddCustomerTypeAsync(CustomerTypeDto CustomerTypeDto);
        public Task UpdateCustomerTypeAsync(CustomerTypeDto CustomerTypeDto);
        public Task DeleteCustomerTypeAsync(Guid id);



        public Task<List<CustomerDto>> GetAllCustomersAsync();
        public Task<CustomerDto> GetCustomerWithIdAsync(Guid id);
        public Task<CustomerDto> GetCustomerWithDetailByIdAsync(Guid id);
        public Task AddCustomerAsync(CustomerDto CustomerDto);
        public Task UpdateCustomerAsync(CustomerDto CustomerDto);
        public Task DeleteCustomerAsync(Guid id);


        public Task<bool> CustomerCanBeDeleteAsync(Guid id);

    }
}
