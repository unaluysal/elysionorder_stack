using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.ProductServices
{
    public class StockService : IStockService


    {

        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;


        public StockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _mapper= mapper;


        }

        public async Task AddStockAsync(StockDto stockDto)
        {
            var s = _mapper.Map<Stock>(stockDto);
       
           await _unitOfWork.GetRepository<Stock>().AddAsync(s);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStockAsync(Guid id)
        {
           var s =await _unitOfWork.GetRepository<Stock>().GetByIdAsync( id);
            s.Status = false;
            _unitOfWork.GetRepository<Stock>().Update(s);
          await  _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<StockDto>> GetAllStocksAsync()
        {
            var list =await _unitOfWork.GetRepository<Stock>().GetAll().Include(x => x.Store).ThenInclude(x => x.StoreType).Include(x => x.Product).ThenInclude(x => x.SubProductType).ThenInclude(x => x.ProductType).Include(x=>x.Product.Brand)
                .Where(x => x.Status && x.Store.Status && x.Store.StoreType.Status && x.Product.Status && x.Product.SubProductType.Status && x.Product.SubProductType.ProductType.Status).ToListAsync();
            var mlist =_mapper.Map<List<StockDto>>(list);
            return mlist;
        }

        public async Task<StockDto> GetStockWithIdAsync(Guid id)
        {
            var s = await _unitOfWork.GetRepository<Stock>().GetAll().Include(x => x.Store).ThenInclude(x => x.StoreType).Include(x => x.Product).ThenInclude(x => x.SubProductType).ThenInclude(x => x.ProductType).Include(x => x.Product.Brand)
                 .FirstOrDefaultAsync(x => x.Id == id && x.Status && x.Store.Status && x.Store.StoreType.Status && x.Product.Status && x.Product.SubProductType.Status && x.Product.SubProductType.ProductType.Status);

            var ms = _mapper.Map<StockDto>(s);
            return ms;
        }

        public async Task<List<StockDto>> GetStockWithProductIdAsync(Guid id)
        {
            var list = await _unitOfWork.GetRepository<Stock>().GetAll().Include(x => x.Store).ThenInclude(x => x.StoreType).Include(x => x.Product).ThenInclude(x => x.SubProductType).ThenInclude(x => x.ProductType).Include(x => x.Product.Brand)
                 .Where(x => x.Status && x.Store.Status && x.Store.StoreType.Status && x.Product.Status && x.Product.SubProductType.Status && x.Product.SubProductType.ProductType.Status && x.ProductId==id).ToListAsync();
            var mlist = _mapper.Map<List<StockDto>>(list);
            return mlist;
        }

        public async Task UpdateStockAsync(StockDto stockDto)
        {
           var s =_mapper.Map<Stock>(stockDto);
            s.Status = true;
            _unitOfWork.GetRepository<Stock>().Update(s);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CanBeOrderAsync(Guid pricelistId, int piece)
        {
           var list = await _unitOfWork.GetRepository<Stock>().GetWhere(x=>x.Status&& x.Count>=piece).ToListAsync();
            if (list!=null)
            {
                return true;
            }
            return false;
        }

        public async Task DropStockAsync(OrderDto orderDto)
        {
          
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelDropStockAsync(OrderDto orderDto)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddStokBillAsync(BillDto billDto)
        {
            var bill = _mapper.Map<Bill>(billDto);
            var biltype = await _unitOfWork.GetRepository<BillType>().GetFirstWhereAsync(x => x.Status && x.Name == "Gelen Fatura");
            bill.BillTypeId=biltype.Id;
           await _unitOfWork.GetRepository<Bill>().AddAsync(bill);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BillDto> GetStokBillWithIdAsync(Guid id)
        {
            var bill =await _unitOfWork.GetRepository<Bill>().GetAll().Include(x=>x.BillType).FirstOrDefaultAsync(x=>x.Status && x.Id==id);
            var mbil =_mapper.Map<BillDto>(bill);
            var cust =await _unitOfWork.GetRepository<Customer>().GetAll().Include(x => x.CustomerType).FirstOrDefaultAsync(x => x.Id == bill.InvoicerId);
            mbil.InvoicerDto=_mapper.Map<CustomerDto>(cust);
            var p =await _unitOfWork.GetRepository<BillItem>().GetAll().Include(x => x.Tax).Include(x=>x.Product).Include(x=>x.Store).Where(x => x.Status && x.BillId == id).ToListAsync();
            mbil.BillItems= _mapper.Map<List<BillItemDto>>(p);
            foreach (var item in mbil.BillItems)
            {
                mbil.Total = mbil.Total + item.Total;
                mbil.TotalDiscount = mbil.TotalDiscount + ((item.Price- (item.Price * item.Discount/100)) * item.Piece );
            }
            
            return mbil;
        }

        public async Task AddStokBillItemAsync(BillItemDto billItemDto)
        {
            var tax =await _unitOfWork.GetRepository<Tax>().GetFirstWhereAsync(x=>x.Id==billItemDto.TaxId);
            var item = _mapper.Map<BillItem>(billItemDto);
            item.Status = true;
            double rp = item.Price - (item.Price * item.Discount/100);
            double prt = rp * item.Piece;
            item.Total = (prt * tax.Rate / 100) + prt ;
           // item.Total = (item.Discount * item.Piece)+ (item.Discount * item.Piece)* tax.Rate/100;
           await _unitOfWork.GetRepository<BillItem>().AddAsync(item);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BillItemDto> GetStokBillItemAsync(Guid id)
        {
           var bi =await _unitOfWork.GetRepository<BillItem>().GetAll().Include(x=>x.Product).Include(x=>x.Store).FirstOrDefaultAsync(x=>x.Id==id);
            var mbi =_mapper.Map<BillItemDto>(bi);
            return mbi;
        }

        public async Task DeleteStokBillItemAsync(Guid id)
        {
            var bi = await _unitOfWork.GetRepository<BillItem>().GetAll().FirstOrDefaultAsync(x => x.Id == id);
            bi.Status = false;
             _unitOfWork.GetRepository<BillItem>().Update(bi);
          await  _unitOfWork.SaveChangesAsync();

          
        }

        public async Task DeleteStokBillAsync(Guid id)
        {
            var bi = await _unitOfWork.GetRepository<Bill>().GetAll().FirstOrDefaultAsync(x => x.Id == id);
            bi.Status = false;
            _unitOfWork.GetRepository<Bill>().Update(bi);
            await _unitOfWork.SaveChangesAsync();
            var blist =await _unitOfWork.GetRepository<BillItem>().GetAll().Where(x => x.Status && x.BillId == id).ToListAsync();
            blist.ForEach(x=>x.Status=false);
            _unitOfWork.GetRepository<BillItem>().UpdateRange(blist);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task BillToStockAsync(Guid id)
        {
            var list =await _unitOfWork.GetRepository<BillItem>().GetAll().Where(x => x.Status && x.BillId == id).ToListAsync();

            foreach (var item in list)
            {

                await AddStockAsync(new StockDto() { ProductId=item.ProductId,Count=item.Piece,StoreId=item.StoreId,BuyedPrice= item.Discount,DateOfEntry= DateTime.Now});

            }
        }
    }

}
