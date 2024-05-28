using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.SalesServices
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetAllOrdersAsync();
        public Task<OrderDto> GetOrderWithIdAsync(Guid id);
        public Task DeleteAllOrderWithSalesIdAsync(Guid id);
        public Task<List<OrderDto>> GetOrdersWithSalesIdAsync(Guid id);
        public Task AddOrderAsync(OrderDto orderDto);
        public Task UpdateOrderAsync(OrderDto orderDto);
        public Task DeleteOrderAsync(Guid id);
       
    }
}
