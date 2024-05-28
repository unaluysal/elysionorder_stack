namespace ElysionOrder.Application.Services.Dtos
{
    public class SalesStatusDto : BaseDto
    {
        public int LineNumber { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
