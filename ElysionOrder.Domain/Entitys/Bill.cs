using System.ComponentModel.DataAnnotations.Schema;

namespace ElysionOrder.Domain.Entitys
{


    public class Bill : BaseEntity
    {
        public string BillNumber { get; set; }
        public string WayBillNumber { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? BillDate { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? WayBillDate { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? ShipmentDate { get; set; }

        public Guid InvoicerId { get; set; }
        public Guid CustomerId { get; set; }

        public double TotalDiscount { get; set; }
        public double Total { get; set;  }
        public Guid BillTypeId { get; set; }
        public virtual BillType BillType { get; set; }
    }
}
