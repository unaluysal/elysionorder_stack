using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.SalesServices;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.ProductServices
{
    public class PriceService : IPriceService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly IOrderService _orderService;
        public PriceService(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService )
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderService = orderService;

        }
        


        #region Tax

        public async Task<List<TaxDto>> GetAllTaxesAsync()
        {
            var list =await _unitOfWork.GetRepository<Tax>().GetWhere(x => x.Status).ToListAsync();
            var ml = _mapper.Map<List<TaxDto>>(list);
            return ml;
        }

        public async Task<TaxDto> GetTaxWithIdAsync(Guid id)
        {
            var t =await _unitOfWork.GetRepository<Tax>().GetByIdAsync(id);
            var mt = _mapper.Map<TaxDto>(t);
            return mt;
        }

        public async Task AddTaxAsync(TaxDto taxDto)
        {
            var t = _mapper.Map<Tax>(taxDto);
        
           await _unitOfWork.GetRepository<Tax>().AddAsync(t);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaxAsync(TaxDto taxDto)
        {
            var t = _mapper.Map<Tax>(taxDto);
            t.Status = true;
            _unitOfWork.GetRepository<Tax>().Update(t);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTaxAsync(Guid id)
        {
            var t = await _unitOfWork.GetRepository<Tax>().GetByIdAsync(id);
            t.Status = false;
            _unitOfWork.GetRepository<Tax>().Update(t);
              await _unitOfWork.SaveChangesAsync();
            
        }


        #endregion

        #region Currency

        public async Task<List<CurrencyDto>> GetAllCurrenciesAsync()
        {
            var list =await _unitOfWork.GetRepository<Currency>().GetWhere(x => x.Status).ToListAsync();
            var mlist =_mapper.Map<List<CurrencyDto>>(list);
            return mlist;
        }

        public async Task<CurrencyDto> GetCurrencyWithIdAsync(Guid id)
        {
            var cu =await _unitOfWork.GetRepository<Currency>().GetByIdAsync(id);
            var mc =_mapper.Map<CurrencyDto>(cu);
            return mc;
        }

        public async Task AddCurrencyAsync(CurrencyDto currencyDto)
        {
            var cu = _mapper.Map<Currency>(currencyDto);
           
           await _unitOfWork.GetRepository<Currency>().AddAsync(cu);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCurrencyAsync(CurrencyDto currencyDto)
        {
            var c = _mapper.Map<Currency>(currencyDto);
            c.Status = true;
            _unitOfWork.GetRepository<Currency>().Update(c);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCurrencyAsync(Guid id)
        {
            var cu = await _unitOfWork.GetRepository<Currency>().GetByIdAsync( id);
            cu.Status = false;
            _unitOfWork.GetRepository<Currency>().Update(cu);
           await _unitOfWork.SaveChangesAsync();
            
           


        }


        #endregion

        #region Iban

        public async Task<List<IbanDto>> GetAllIbansAsync()
        {
            var list =await _unitOfWork.GetRepository<Iban>().GetAll().Include(x => x.Currency).Where(x=>x.Status && x.Currency.Status).ToListAsync();
            var ml =_mapper.Map<List<IbanDto>>(list);
            return ml;
        }

        public async Task<IbanDto> GetIbanWithIdAsync(Guid id)
        {
            var mi = new IbanDto();
            var i = await _unitOfWork.GetRepository<Iban>().GetAll().Include(x=>x.Currency).FirstOrDefaultAsync(x=>x.Status && x.Currency.Status && x.Id==id);
            if (i!=null)
            {
                mi = _mapper.Map<IbanDto>(i);
            }
            
            return mi;
        }

        public async Task AddIbanAsync(IbanDto ibanDto)
        {
            var i = _mapper.Map<Iban>(ibanDto);
         
          await  _unitOfWork.GetRepository<Iban>().AddAsync(i);
          await  _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateIbanAsync(IbanDto ibanDto)
        {
            var i = _mapper.Map<Iban>(ibanDto);
            i.Status = true;
            _unitOfWork.GetRepository<Iban>().Update(i);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteIbanAsync(Guid id)
        {
           var i =await _unitOfWork.GetRepository<Iban>().GetByIdAsync(id);
            i.Status = false;
            _unitOfWork.GetRepository<Iban>().Update(i);
          await  _unitOfWork.SaveChangesAsync();
        }


        #endregion
    }
}
