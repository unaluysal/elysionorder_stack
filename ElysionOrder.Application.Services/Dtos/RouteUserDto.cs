namespace ElysionOrder.Application.Services.Dtos
{
    public class RouteUserDto : BaseDto
    {
        public Guid RouteId { get; set; }
        public virtual RouteDto RouteDto { get; set; }
        public Guid UserId { get; set; }

        public virtual UserDto UserDto { get; set; }

 
    }
}
