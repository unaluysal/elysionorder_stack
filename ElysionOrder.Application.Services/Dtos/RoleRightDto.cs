namespace ElysionOrder.Application.Services.Dtos
{
    public class RoleRightDto :BaseDto
    {
        public Guid RoleId { get; set; }
        public virtual RoleDto RoleDto { get; set; }

        public Guid RightId { get; set; }
        public virtual RightDto RightDto { get; set; }

    }
}
