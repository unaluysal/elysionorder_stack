using System.ComponentModel.DataAnnotations.Schema;

namespace ElysionOrder.Domain.Entitys
{
    public class Payment : BaseEntity
    {
        public double Total { get; set; }   
        public Guid CustomerId { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime PaymentDay { get; set; }    
        public virtual Customer Customer { get; set; }
        public Guid PaymentTypeId { get; set; }
        public virtual  PaymentType PaymentType { get; set; }

        public Guid PaymentWayId { get; set; }
        public virtual PaymentWay PaymentWay { get; set; }
    }
}
