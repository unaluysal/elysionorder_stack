namespace ElysionOrder.Domain.Entitys
{
    public class RouteCustomer : BaseEntity
    {
        public Guid RouteId { get; set; }
        public virtual Route Route { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
