namespace ElysionOrder.Application.Services.Dtos
{
    public class SubProductTypeDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public Guid ProductTypeId { get; set; }
        public virtual ProductTypeDto ProductTypeDto { get; set; }
    }
}
