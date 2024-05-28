namespace ElysionOrder.Application.Services.Dtos
{
    public class SalesDto : BaseDto
    {
        public SalesDto()
        {
            CreateUserDto = new UserDto();
            UpdateUserDto = new UserDto();
            CustomerDto = new CustomerDto();
            SalesStatusDto = new SalesStatusDto();
            OrderDtos = new List<OrderDto>();
        }
        public Guid CustomerId { get; set; }
        public virtual CustomerDto CustomerDto { get;set; }

        public Guid SalesStatusId { get; set; }
        public virtual SalesStatusDto SalesStatusDto { get; set; }

        public Guid BackStatuesId { get; set; }
        public virtual SalesStatusDto BackSalesStatusDto { get; set; }

        public Guid NextStatuesId { get; set; }
        public virtual SalesStatusDto NextSalesStatusDto { get; set; }


        public List<OrderDto> OrderDtos { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderTotalTax { get; set; }
        public double OrderTotalTaxFreePrice { get; set; }

        public bool Bill { get; set; }

        public virtual UserDto CreateUserDto { get; set; }
        public virtual UserDto UpdateUserDto { get; set; }
    }
}
