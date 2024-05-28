namespace ElysionOrder.Domain.Entitys
{
    public class Order : BaseEntity
    {
        
        public Guid SalesId { get; set; }
        public virtual Sales Sales { get; set; }
        public int Piece { get; set; }
        public double Discount { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
