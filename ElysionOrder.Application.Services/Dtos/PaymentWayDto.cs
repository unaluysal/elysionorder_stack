using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.Dtos
{
    public class PaymentWayDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
