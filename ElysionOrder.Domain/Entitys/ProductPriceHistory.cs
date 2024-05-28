namespace ElysionOrder.Domain.Entitys
{
    public class ProductPriceHistory : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public double Price { get; set; }
    }
}
