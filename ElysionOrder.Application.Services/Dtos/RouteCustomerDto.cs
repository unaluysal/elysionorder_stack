namespace ElysionOrder.Application.Services.Dtos
{
    public class RouteCustomerDto : BaseDto
    {
        public Guid RouteId { get; set; }
        public virtual RouteDto RouteDto { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerDto CustomerDto { get; set; }
    
    }
}
