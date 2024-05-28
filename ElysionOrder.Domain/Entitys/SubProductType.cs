namespace ElysionOrder.Domain.Entitys
{
    public class SubProductType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public Guid ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
