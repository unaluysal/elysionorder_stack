namespace ElysionOrder.Domain.Entitys
{
    public class Route :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid DayOfWeekId { get; set; }


       
        public virtual DayOfWeek DayOfWeek { get; set; }

       
    }
}
