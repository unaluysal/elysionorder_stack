namespace ElysionOrder.Application.Services.Dtos
{
    public class PaymentDto : BaseDto
    {
        public double Total { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime PaymentDay { get; set; }
        public virtual CustomerDto CustomerDto { get; set; }
        public Guid PaymentTypeId { get; set; }
        public virtual PaymentTypeDto PaymentTypeDto { get; set; }

        public Guid PaymentWayId { get; set; }
        public virtual PaymentWayDto PaymentWayDto { get; set; }
    }
}
