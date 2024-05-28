namespace ElysionOrder.Application.Services.Dtos
{
    public class TaxDto :BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }

    }
}
