namespace ElysionOrder.Domain.Entitys
{
    public class RouteUser : BaseEntity
    {
        public Guid RouteId { get; set; }
        public virtual Route Route { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }


    }
}
