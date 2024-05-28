using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.ProductServices
{
    public interface IStoreService
    {
        #region StoreType
        public Task<List<StoreTypeDto>> GetAllStoreTypesAsync();
        public Task<StoreTypeDto> GetStoreTypeWithIdAsync(Guid id);
        public Task AddStoreTypeAsync(StoreTypeDto storeTypeDto);
        public Task UpdateStoreTypeAsync(StoreTypeDto storeTypeDto);
        public Task DeleteStoreTypeAsync(Guid id);
        #endregion

        #region Store

        public Task<List<StoreDto>> GetAllStoresAsync();
        public Task<StoreDto> GetStoreWithIdAsync(Guid id);
        public Task AddStoreAsync(StoreDto storeDto);
        public Task UpdateStoreAsync(StoreDto storeDto);
        public Task DeleteStoreAsync(Guid id);
        #endregion
    }
}
