using System.ComponentModel.DataAnnotations.Schema;

namespace ElysionOrder.Domain.Entitys
{
    public class Stock : BaseEntity
    {
        public int Count { get; set; }
        public double BuyedPrice { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateOfEntry { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
}
