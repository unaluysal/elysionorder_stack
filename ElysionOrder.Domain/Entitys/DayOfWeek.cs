namespace ElysionOrder.Domain.Entitys
{
    public class DayOfWeek : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int DayNumber { get; set; }

    }
}
