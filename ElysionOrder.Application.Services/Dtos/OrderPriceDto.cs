namespace ElysionOrder.Application.Services.Dtos
{
    public class OrderPriceDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public virtual ProductDto ProductDto { get; set; }
        public double Price { get; set; }
        public Guid CurrencyId { get; set; }
        public virtual CurrencyDto CurrencyDto { get; set; }
  
    }
}
