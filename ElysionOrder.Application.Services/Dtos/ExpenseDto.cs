namespace ElysionOrder.Application.Services.Dtos
{
    public class ExpenseDto : BaseDto
    {
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime ExpenseDate { get; set; }

        public Guid ExpenseTypeId { get; set; }
        public virtual ExpenseTypeDto ExpenseTypeDto { get; set; }
    }
}
