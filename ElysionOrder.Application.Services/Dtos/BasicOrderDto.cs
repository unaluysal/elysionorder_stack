using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.Dtos
{
    public class BasicOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<BOrderDto> Orders { get; set; } = new List<BOrderDto>();
        public decimal OrderTotal { get; set; }
    }

    public class BOrderDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
