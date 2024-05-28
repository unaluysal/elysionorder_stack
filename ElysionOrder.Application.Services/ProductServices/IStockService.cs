using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.ProductServices
{
    public interface IStockService
    {
        #region Store

        public Task<List<StockDto>> GetAllStocksAsync();
        public Task<StockDto> GetStockWithIdAsync(Guid id);
        public Task<List<StockDto>> GetStockWithProductIdAsync(Guid id);
        public Task AddStockAsync(StockDto stockDto);
        public Task UpdateStockAsync(StockDto stockDto);
        public Task DeleteStockAsync(Guid id);
        public Task<bool> CanBeOrderAsync(Guid pricelistId, int piece);
        public Task DropStockAsync(OrderDto orderDto);
        public Task CancelDropStockAsync(OrderDto orderDto);
        public Task AddStokBillAsync(BillDto billDto);
        public Task<BillDto> GetStokBillWithIdAsync(Guid id);
        public Task AddStokBillItemAsync(BillItemDto billItemDto);
        public Task DeleteStokBillItemAsync(Guid id);
        public Task DeleteStokBillAsync(Guid id);
        public Task BillToStockAsync(Guid id);
        public Task<BillItemDto> GetStokBillItemAsync(Guid id);
        #endregion
    }
}
