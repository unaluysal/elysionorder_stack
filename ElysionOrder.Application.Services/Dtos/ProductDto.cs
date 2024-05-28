namespace ElysionOrder.Application.Services.Dtos
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Guid BrandId { get; set; }
        public virtual BrandDto BrandDto { get; set; }

        public Guid SubProductTypeId { get; set; }
        public virtual SubProductTypeDto SubProductTypeDto { get; set; }

        public double Price { get; set; }

        public Guid TaxId { get; set; }
        public virtual TaxDto TaxDto { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual CurrencyDto CurrencyDto { get; set; }

    }
}
