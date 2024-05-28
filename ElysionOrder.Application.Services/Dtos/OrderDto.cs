namespace ElysionOrder.Application.Services.Dtos
{
    public class OrderDto : BaseDto
        {

        
        public Guid SalesId { get; set; }
        public virtual SalesDto SalesDto { get; set; }
        public int Piece { get; set; }
        public double Discount { get; set; }
        public double SpecialPrice { get; set; }
        public Guid ProductId { get; set; }
        public virtual ProductDto ProductDto { get; set; }


        public double OrderTotalPrice { get; set; }
        public double OrderTotalTax { get; set; }
        public double OrderTotalPriceWithTax { get; set; }
        public double OrderTotalTaxFreePrice { get; set; }

    }
   
}
