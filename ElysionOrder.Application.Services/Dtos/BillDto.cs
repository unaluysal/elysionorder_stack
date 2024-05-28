using ElysionOrder.Domain.Entitys;

namespace ElysionOrder.Application.Services.Dtos
{
    public class BillDto : BaseDto
    {
        public string BillNumber { get; set; }
        public string WayBillNumber { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? WayBillDate { get; set; }
        public DateTime? ShipmentDate { get; set; }

        public Guid InvoicerId { get; set; }
        public Guid CustomerId { get; set; }

        public double TotalDiscount { get; set; }
        public double Total { get; set; }
        public Guid BillTypeId { get; set; }
        public virtual BillTypeDto BillTypeDto { get; set; }

        public virtual CustomerDto InvoicerDto { get; set; }
        public virtual CustomerDto RecipientDto { get; set; }

        public List<BillItemDto> BillItems { get; set; }
    }
}
