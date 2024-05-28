namespace ElysionOrder.Domain.Entitys
{
    public class Tax: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }



    }
}
