using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.ProductServices
{
    public interface IPriceService
    {

       



        public Task<List<TaxDto>> GetAllTaxesAsync();
        public Task<TaxDto> GetTaxWithIdAsync(Guid id);       
        public Task AddTaxAsync(TaxDto taxDto);
        public Task UpdateTaxAsync(TaxDto taxDto);
        public Task DeleteTaxAsync(Guid id);


        public Task<List<CurrencyDto>> GetAllCurrenciesAsync();
        public Task<CurrencyDto> GetCurrencyWithIdAsync(Guid id);
        public Task AddCurrencyAsync(CurrencyDto currencyDto);
        public Task UpdateCurrencyAsync(CurrencyDto currencyDto);
        public Task DeleteCurrencyAsync(Guid id);

        public Task<List<IbanDto>> GetAllIbansAsync();
        public Task<IbanDto> GetIbanWithIdAsync(Guid id);
        public Task AddIbanAsync(IbanDto ibanDto);
        public Task UpdateIbanAsync(IbanDto ibanDto);
        public Task DeleteIbanAsync(Guid id);
    }
}
