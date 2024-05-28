namespace ElysionOrder.Domain.Entitys
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }    
        public string Description { get; set; }
        public Guid StoreTypeId { get; set; }
        public virtual StoreType StoreType { get; set; }


    }
}
