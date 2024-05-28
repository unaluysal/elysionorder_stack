namespace ElysionOrder.Application.Services.Dtos
{
    public class StoreDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StoreTypeId { get; set; }
        public virtual StoreTypeDto StoreTypeDto { get; set; }

    }
}
