namespace ElysionOrder.Application.Services.Dtos
{
    public class IbanDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IbanAdres { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual CurrencyDto CurrencyDto { get; set; }

    }
}
