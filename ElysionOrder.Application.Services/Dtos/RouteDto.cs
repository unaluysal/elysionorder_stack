namespace ElysionOrder.Application.Services.Dtos
{
    public class RouteDto :BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid DayOfWeekId { get; set; }


        public virtual  DayOfWeekDto DayOfWeekDto { get; set; }

        public List<UserDto> RouteUsers { get; set; }
        public List<CustomerDto> RouteCustomers { get; set; }
        
       
    }
}
