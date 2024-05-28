using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddBrandAsync(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);

            await _unitOfWork.GetRepository<Brand>().AddAsync(brand);
            await _unitOfWork.SaveChangesAsync();
        }



        public async Task AddProductAsync(ProductDto productDto)
        {
            var p = _mapper.Map<Product>(productDto);

            await _unitOfWork.GetRepository<Product>().AddAsync(p);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task AddProductTypeAsync(ProductTypeDto productTypeDto)
        {
            var p = _mapper.Map<ProductType>(productTypeDto);

            await _unitOfWork.GetRepository<ProductType>().AddAsync(p);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddSubProductTypeAsync(SubProductTypeDto subProductTypeDto)
        {
            var p = _mapper.Map<SubProductType>(subProductTypeDto);

            await _unitOfWork.GetRepository<SubProductType>().AddAsync(p);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            var brand = await _unitOfWork.GetRepository<Brand>().GetByIdAsync(id);
            brand.Status = false;
            _unitOfWork.GetRepository<Brand>().Update(brand);
            await _unitOfWork.SaveChangesAsync();

            var list = await _unitOfWork.GetRepository<Product>().GetWhere(x => x.Status && x.BrandId == id).ToListAsync();
            foreach (var item in list.Select(x => x.Id))
            {
                await DeleteProductAsync(item);
            }

        }



        public async Task DeleteProductAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            p.Status = false;
            _unitOfWork.GetRepository<Product>().Update(p);
            await _unitOfWork.SaveChangesAsync();


        }

        public async Task DeleteProductTypeAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<ProductType>().GetByIdAsync(id);
            p.Status = false;
            _unitOfWork.GetRepository<ProductType>().Update(p);
            await _unitOfWork.SaveChangesAsync();
            var pl = await _unitOfWork.GetRepository<SubProductType>().GetWhere(x => x.Status && x.ProductTypeId == p.Id).Select(x => x.Id).ToListAsync();
            foreach (var item in pl)
            {
                await DeleteSubProductTypeAsync(item);
            }


        }

        public async Task DeleteSubProductTypeAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<SubProductType>().GetByIdAsync(id);
            p.Status = false;
            _unitOfWork.GetRepository<SubProductType>().Update(p);
            await _unitOfWork.SaveChangesAsync();
            var pr = await _unitOfWork.GetRepository<Product>().GetWhere(x => x.Status && x.SubProductTypeId == p.Id).Select(x => x.Id).ToListAsync();

            foreach (var item in pr)
            {
                await DeleteProductAsync(item);
            }

        }

        public async Task<List<BrandDto>> GetAllBrandsAsync()
        {
            var list = await _unitOfWork.GetRepository<Brand>().GetWhere(x => x.Status).ToListAsync();
            var mlist = _mapper.Map<List<BrandDto>>(list);
            return mlist;
        }



        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var pl = await _unitOfWork.GetRepository<Product>().GetAll().Include(x => x.Currency).Include(x => x.Tax).Include(x => x.SubProductType).ThenInclude(x => x.ProductType).Include(x => x.Brand)
                .Where(x => x.Status && x.SubProductType.Status && x.SubProductType.ProductType.Status && x.Brand.Status
                && x.Tax.Status && x.Currency.Status).OrderBy(x => x.Name).ToListAsync();
            var mpl = _mapper.Map<List<ProductDto>>(pl);
            return mpl;
        }

        public async Task<List<ProductDto>> GetAllProductsIfHaveStockAsync()
        {
            var pl = await _unitOfWork.GetRepository<Product>().GetAll()
                   .Include(x => x.Currency).Include(x => x.Tax).Include(x => x.SubProductType).ThenInclude(x => x.ProductType).Include(x => x.Brand)
                 .Where(x => x.Status && x.SubProductType.Status && x.SubProductType.ProductType.Status && x.Brand.Status
                 && x.Tax.Status && x.Currency.Status).OrderBy(x => x.Name).ToListAsync();
            var mpl = _mapper.Map<List<ProductDto>>(pl);
            var rlist = new List<ProductDto>();
            foreach ( var item in mpl)
            {
                var response =await _unitOfWork.GetRepository<Stock>().GetFirstWhereAsync(x => x.Status && x.Count > 0 && x.ProductId == item.Id);
                if (response!=null)
                {
                    rlist.Add(item);
                }
            }

            return rlist;

        }

        public async Task<List<ProductTypeDto>> GetAllProductTypesAsync()
        {
            var pl = await _unitOfWork.GetRepository<ProductType>()
               .GetWhere(x => x.Status).OrderBy(x => x.Name).ToListAsync();
            var mpl = _mapper.Map<List<ProductTypeDto>>(pl);
            return mpl;
        }

        public async Task<List<SubProductTypeDto>> GetAllSubProductTypesAsync()
        {
            var pl = await _unitOfWork.GetRepository<SubProductType>().GetAll().Include(x => x.ProductType)
              .Where(x => x.Status && x.ProductType.Status).OrderBy(x => x.Name).ToListAsync();
            var mpl = _mapper.Map<List<SubProductTypeDto>>(pl);
            return mpl;
        }

        public async Task<BrandDto> GetBrandWithIdAsync(Guid id)
        {
            var brand = await _unitOfWork.GetRepository<Brand>().GetByIdAsync(id);
            var mb = _mapper.Map<BrandDto>(brand);
            return mb;

        }




        public async Task<ProductTypeDto> GetProductTypeWithIdAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<ProductType>().GetByIdAsync(id);
            var mp = _mapper.Map<ProductTypeDto>(p);

            return mp;
        }

        public async Task<ProductDto> GetProductWithIdAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<Product>().GetAll().Include(x => x.Currency).Include(x => x.Tax)
                .Include(x => x.SubProductType).ThenInclude(x => x.ProductType).FirstOrDefaultAsync(x => x.Status && x.SubProductType.Status && x.SubProductType.ProductType.Status && x.Id == id);
            var mp = _mapper.Map<ProductDto>(p);

            return mp;
        }

        public async Task<ProductDto> GetProductWithTypeIdAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<Product>().GetAll().Include(x => x.SubProductType).ThenInclude(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Status && x.SubProductType.Status && x.SubProductType.ProductType.Status && x.SubProductTypeId == id);
            var mp = _mapper.Map<ProductDto>(p);

            return mp;
        }

        public async Task<SubProductTypeDto> GetSubProductTypeWithIdAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<SubProductType>().GetAll().Include(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Status && x.ProductType.Status && x.Id == id);
            var mp = _mapper.Map<SubProductTypeDto>(p);

            return mp;
        }

        public async Task<SubProductTypeDto> GetSubProductTypeWithTypeIdAsync(Guid id)
        {
            var p = await _unitOfWork.GetRepository<SubProductType>().GetAll().Include(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Status && x.ProductType.Status && x.ProductTypeId == id);
            var mp = _mapper.Map<SubProductTypeDto>(p);
            return mp;
        }

        public async Task UpdateBrandAsync(BrandDto brandDto)
        {
            var mb = _mapper.Map<Brand>(brandDto);
            mb.Status = true;
            _unitOfWork.GetRepository<Brand>().Update(mb);
            await _unitOfWork.SaveChangesAsync();
        }



        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var p = _mapper.Map<Product>(productDto);
            p.Status = true;
            _unitOfWork.GetRepository<Product>().Update(p);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProductTypeAsync(ProductTypeDto productTypeDto)
        {
            var p = _mapper.Map<ProductType>(productTypeDto);
            p.Status = true;
            _unitOfWork.GetRepository<ProductType>().Update(p);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSubProductTypeAsync(SubProductTypeDto subProductTypeDto)
        {
            var p = _mapper.Map<SubProductType>(subProductTypeDto);
            p.Status = true;
            _unitOfWork.GetRepository<SubProductType>().Update(p);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
