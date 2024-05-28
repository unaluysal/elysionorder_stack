using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Domain.Entitys
{
    public class PaymentWay : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
