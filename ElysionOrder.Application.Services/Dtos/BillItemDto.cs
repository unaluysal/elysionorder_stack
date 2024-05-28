using ElysionOrder.Domain.Entitys;

namespace ElysionOrder.Application.Services.Dtos
{
    public class BillItemDto:BaseDto
    {
        public Guid ProductId { get; set; }
        public virtual ProductDto ProductDto { get; set; }
        public int Piece { get; set; }
        public Guid TaxId { get; set; }
        public virtual TaxDto TaxDto { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public double Total { get; set; }
        public Guid StoreId { get; set; }
        public virtual StoreDto StoreDto { get; set; }
        public Guid BillId { get; set; }
        public virtual BillDto BillDto { get; set; }
    }
}
