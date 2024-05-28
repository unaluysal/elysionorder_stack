namespace ElysionOrder.Domain.Entitys
{
    public class Sales : BaseEntity
    {
      
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid SalesStatusId { get; set; }
        public virtual SalesStatus SalesStatus { get;set; }

        public bool Bill { get; set; }

    }
}
