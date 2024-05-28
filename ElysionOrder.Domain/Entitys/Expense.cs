using System.ComponentModel.DataAnnotations.Schema;

namespace ElysionOrder.Domain.Entitys
{
    public class Expense :BaseEntity
    {
        public string Description { get; set; }
        public double Cost { get; set; }
        public Guid ExpenseTypeId { get; set; }


        [Column(TypeName = "timestamp")]
        public DateTime ExpenseDate { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
