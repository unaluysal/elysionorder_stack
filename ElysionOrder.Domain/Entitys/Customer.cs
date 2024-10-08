﻿namespace ElysionOrder.Domain.Entitys
{
    public class Customer :BaseEntity
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string BillingAddRess { get; set; }
        public string Description { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }

        //public string City { get; set; }
        //public string District { get; set; }    

        public int CustomerNumber { get; set; }
        public Guid CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }

    }
}
