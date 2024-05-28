namespace ElysionOrder.Domain.Entitys
{
    public class Iban : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IbanAdres { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

    }
}
