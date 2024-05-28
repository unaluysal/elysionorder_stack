using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Domain.Entitys
{
    public class BillItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Piece { get; set;}   
        public Guid TaxId { get; set; }
        public virtual Tax Tax { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
       
        public double Total { get; set; }

        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
        public Guid BillId { get; set; }
        public virtual Bill Bill { get; set; }
        
    }
}
