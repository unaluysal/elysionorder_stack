using System.ComponentModel.Design;

namespace ElysionOrder.Domain.Entitys
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }        
      
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public Guid SubProductTypeId { get; set; }
        public virtual SubProductType SubProductType { get; set; }

        public double Price { get; set; }

        public Guid TaxId { get; set; }
        public virtual Tax Tax { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
       

    }
}
