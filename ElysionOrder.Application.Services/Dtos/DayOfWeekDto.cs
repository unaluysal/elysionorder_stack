namespace ElysionOrder.Application.Services.Dtos
{
    public class DayOfWeekDto: BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int DayNumber { get; set; }

    }
}
