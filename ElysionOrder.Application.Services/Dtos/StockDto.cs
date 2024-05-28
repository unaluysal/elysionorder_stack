namespace ElysionOrder.Application.Services.Dtos
{
    public class StockDto : BaseDto
    {
        public int Count { get; set; }

        public double BuyedPrice { get; set; }
        public DateTime DateOfEntry { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductDto ProductDto { get; set; }
        public Guid StoreId { get; set; }
        public virtual StoreDto StoreDto { get; set; }

    }
}
