namespace ElysionOrder.Domain.Entitys
{
    public class SalesStatus : BaseEntity
    {
        public int LineNumber { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
