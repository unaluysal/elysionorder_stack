﻿namespace ElysionOrder.Domain.Entitys
{
    public class PaymentType : BaseEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
