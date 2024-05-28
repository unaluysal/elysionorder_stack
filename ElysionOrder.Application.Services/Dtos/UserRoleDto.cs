namespace ElysionOrder.Application.Services.Dtos
{
    public class UserRoleDto: BaseDto
    {
        public Guid UserId { get; set; }
        public virtual UserDto UserDto { get; set; }

        public Guid RoleId { get; set; }
        public virtual RoleDto RoleDto { get; set; }


    }
}
