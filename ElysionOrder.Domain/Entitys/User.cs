namespace ElysionOrder.Domain.Entitys
{
    public class User :BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }

    
    }
}
