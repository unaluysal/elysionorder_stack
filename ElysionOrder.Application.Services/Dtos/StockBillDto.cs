namespace ElysionOrder.Application.Services.Dtos
{
    public class StockBillDto : BaseDto
    {
        public DateTime BillDate { get; set; }
        public string BillNumber { get; set; }
        public string IRSNumber { get; set; }

        public DateTime? ShipmentDate { get; set; }
        public string Description { get; set; }
        public Guid CustomerId { get; set; }
       
      
    }
}
