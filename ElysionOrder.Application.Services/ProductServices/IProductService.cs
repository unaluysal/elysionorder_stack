using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.ProductServices
{
    public interface IProductService
    {
        public Task<List<ProductTypeDto>> GetAllProductTypesAsync();
        public Task<ProductTypeDto> GetProductTypeWithIdAsync(Guid id);
        public Task AddProductTypeAsync(ProductTypeDto productTypeDto);
        public Task UpdateProductTypeAsync(ProductTypeDto productTypeDto);
        public Task DeleteProductTypeAsync(Guid id);



        public Task<List<SubProductTypeDto>> GetAllSubProductTypesAsync();
        public Task<SubProductTypeDto> GetSubProductTypeWithIdAsync(Guid id);
        public Task<SubProductTypeDto> GetSubProductTypeWithTypeIdAsync(Guid id);
        public Task AddSubProductTypeAsync(SubProductTypeDto subProductTypeDto);
        public Task UpdateSubProductTypeAsync(SubProductTypeDto subProductTypeDto);
        public Task DeleteSubProductTypeAsync(Guid id);



        public Task<List<ProductDto>> GetAllProductsAsync();
        public Task<ProductDto> GetProductWithIdAsync(Guid id);
        public Task<ProductDto> GetProductWithTypeIdAsync(Guid id);
        public Task AddProductAsync(ProductDto productDto);
        public Task UpdateProductAsync(ProductDto productDto);
        public Task DeleteProductAsync(Guid id);
        public Task<List<ProductDto>> GetAllProductsIfHaveStockAsync();


        public Task<List<BrandDto>> GetAllBrandsAsync();
        public Task<BrandDto> GetBrandWithIdAsync(Guid id);
       
        public Task AddBrandAsync(BrandDto brandDto);
        public Task UpdateBrandAsync(BrandDto brandDto);
        public Task DeleteBrandAsync(Guid id);






    }
}
