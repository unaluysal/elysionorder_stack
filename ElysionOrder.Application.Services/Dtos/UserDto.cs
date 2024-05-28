namespace ElysionOrder.Application.Services.Dtos
{
    public class UserDto :BaseDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid UserTypeId { get; set; }
        public virtual UserTypeDto UserTypeDto { get; set; }


    }
}
