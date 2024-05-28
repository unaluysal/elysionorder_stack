namespace ElysionOrder.Domain.Entitys
{
    public class RoleRight :BaseEntity
    {
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public Guid RightId { get; set; }
        public virtual Right Right { get; set; }

    }
}
