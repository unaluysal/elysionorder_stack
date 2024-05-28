using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Domain.Entitys
{
    public class BillSetting : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public string PrinterName { get; set; } 
        public double PaperWidth { get; set; }
        public double PaperWeight { get; set;}
        public int NextPageNumber { get; set; }
    }
}
