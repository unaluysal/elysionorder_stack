using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.RouteServices
{
    public interface IRouteService
    {
        public Task<List<RouteDto>> GetAllRoutesAsync();
        public Task<RouteDto> GetRouteByIdAsync(Guid id);
        public Task AddRouteAsnyc(RouteDto routeDto);
        public Task UpdateRouteAsnyc(RouteDto routeDto);
        public Task DeleteRouteAsnyc(Guid id);


        public Task<List<DayOfWeekDto>> GetAllDaysAsync();


        public Task<List<RouteCustomerDto>> GetAllRouteCustomersAsync();
        public Task<List<RouteCustomerDto>> GetAllRouteCustomersWithRouteIdAsync(Guid id);
        public Task<RouteCustomerDto> GetRouteCustomerByIdAsync(Guid id);
       
        public Task AddRouteCustomerAsync(RouteCustomerDto routeCustomerDto);
        public Task DeleteRouteCustomerAsync(Guid id);
        public Task<List<RouteUserDto>> GetAllRouteUsersAsync();
        public Task<List<RouteUserDto>> GetAllRouteUsersWithRouteIdAsync(Guid id);
        public Task<RouteUserDto> GetRouteUserByIdAsync(Guid id);
        public Task AddRouteUserAsync(RouteUserDto routeUserDto);
        public Task DeleteRouteUserAsync(Guid id);
        public Task DeleteCustomerRoutesAsync(CustomerDto customerDto);


    }
}
