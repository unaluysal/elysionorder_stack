using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.ProductServices
{
    public class StoreService : IStoreService
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper) {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #region Store


        public async Task AddStoreAsync(StoreDto storeDto)
        {
           var  s=_mapper.Map<Store>(storeDto);
          
          await  _unitOfWork.GetRepository<Store>().AddAsync(s);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            var s =await _unitOfWork.GetRepository<Store>().GetByIdAsync( id);
            s.Status = false;
            _unitOfWork.GetRepository<Store>().Update(s);
          await  _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<StoreDto>> GetAllStoresAsync()
        {
            var s =await _unitOfWork.GetRepository<Store>().GetAll().Include(x => x.StoreType).Where(x => x.Status && x.StoreType.Status).ToListAsync();
            var ms =_mapper.Map<List<StoreDto>>(s);
            return ms;
        }
        public async Task<StoreDto> GetStoreWithIdAsync(Guid id)
        {
           var s =await _unitOfWork.GetRepository<Store>().GetAll().Include(x=>x.StoreType).FirstOrDefaultAsync(x=>x.Id== id);
            var ms =_mapper.Map<StoreDto>(s);
            return ms;
        }

        public async Task UpdateStoreAsync(StoreDto storeDto)
        {
            var s =_mapper.Map<Store>(storeDto);
            s.Status = true;
            _unitOfWork.GetRepository<Store>().Update(s);
          await  _unitOfWork.SaveChangesAsync();
        }
        #endregion
        #region StoreType
        public async Task AddStoreTypeAsync(StoreTypeDto storeTypeDto)
        {
            var r =_mapper.Map<StoreType>(storeTypeDto);
          
          await  _unitOfWork.GetRepository<StoreType>().AddAsync(r);
          await  _unitOfWork.SaveChangesAsync();
        }

       

        public async Task DeleteStoreTypeAsync(Guid id)
        {
           var r =await _unitOfWork.GetRepository<StoreType>().GetByIdAsync( id);
            r.Status = false;
            _unitOfWork.GetRepository<StoreType>().Update(r);
          await  _unitOfWork.SaveChangesAsync();
        }

      

        public async Task<List<StoreTypeDto>> GetAllStoreTypesAsync()
        {
            var list =await _unitOfWork.GetRepository<StoreType>().GetWhere(x => x.Status).ToListAsync();
            var mlist =_mapper.Map<List<StoreTypeDto>>(list);
            return mlist;
        }

        public async Task<StoreTypeDto> GetStoreTypeWithIdAsync(Guid id)
        {
            var r = await _unitOfWork.GetRepository<StoreType>().GetByIdAsync( id);
           var mr= _mapper.Map<StoreTypeDto>(r);

            return mr;
        }

      

        public async Task UpdateStoreTypeAsync(StoreTypeDto storeTypeDto)
        {
            var r = _mapper.Map<StoreType>(storeTypeDto);
            r.Status = true;
            _unitOfWork.GetRepository<StoreType>().Update(r);
           await _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
