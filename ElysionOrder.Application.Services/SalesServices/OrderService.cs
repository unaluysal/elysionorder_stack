using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.ProductServices;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.SalesServices
{
    public class OrderService : IOrderService
    {
        readonly IUnitOfWork _unifOfWork;
        readonly IMapper _mapper;
        readonly IStockService _stockService;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IStockService stockService)
        {

            _unifOfWork = unitOfWork;
            _mapper = mapper;
            _stockService = stockService;


        }
        public async Task AddOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.Product = await _unifOfWork.GetRepository<Product>().GetFirstWhereAsync(x=>x.Id == orderDto.ProductId);
            

            if (orderDto.SpecialPrice != 0)
            {

                var a = order.Product.Price - orderDto.SpecialPrice;
                var disc = (a / order.Product.Price) * 100;
                order.Discount = disc;

            }


           
            await _unifOfWork.GetRepository<Order>().AddAsync(order);
            await _unifOfWork.SaveChangesAsync();
            await _stockService.DropStockAsync(orderDto);
        }



        public async Task DeleteAllOrderWithSalesIdAsync(Guid id)
        {
            var order = await _unifOfWork.GetRepository<Order>().GetAll().Where(x => x.Status && x.SalesId == id).ToListAsync();
            order.ForEach(x => x.Status = false);
            _unifOfWork.GetRepository<Order>().UpdateRange(order);
            await _unifOfWork.SaveChangesAsync();
            foreach (var item in order)
            {

                await _stockService.CancelDropStockAsync(_mapper.Map<OrderDto>(item));

            }

        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _unifOfWork.GetRepository<Order>().GetFirstWhereAsync(x=>x.Id==id);
            order.Status = false;
            _unifOfWork.GetRepository<Order>().Update(order);
            await _unifOfWork.SaveChangesAsync();
            await _stockService.CancelDropStockAsync(_mapper.Map<OrderDto>(order));
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unifOfWork.GetRepository<Order>().GetAll().Include(x => x.Product).Include(x => x.Sales).Include(x => x.Sales.Customer).Where(x => x.Status && x.Sales.Status && x.Sales.Customer.Status).ToListAsync();
            var mo = _mapper.Map<List<OrderDto>>(orders);
            return mo;
        }

        public async Task<List<OrderDto>> GetOrdersWithSalesIdAsync(Guid id)
        {
            var list = await _unifOfWork.GetRepository<Order>().GetAll()
                .Include(x => x.Product).ThenInclude(x => x.Currency).Include(x => x.Product.Tax).Where(x => x.Status && x.SalesId == id).ToListAsync();
            var mlist = _mapper.Map<List<OrderDto>>(list);
            foreach (var item in mlist)
            {

                if (item.Discount != 0)
                {
                    Math.Round(item.SpecialPrice = item.ProductDto.Price - (item.ProductDto.Price * item.Discount / 100), 2);

                }
            }

            return _mapper.Map<List<OrderDto>>(list);

        }

        public async Task<OrderDto> GetOrderWithIdAsync(Guid id)
        {
            var order = await _unifOfWork.GetRepository<Order>().GetAll().Include(x => x.Product).ThenInclude(x=>x.Currency).Include(x=>x.Product.Tax)
                .FirstOrDefaultAsync(x => x.Id == id);
            var mor = _mapper.Map<OrderDto>(order);
            return mor;
        }

     

       

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            

            var or = _mapper.Map<Order>(orderDto);
            or.Status = true;
            _unifOfWork.GetRepository<Order>().Update(or);
            await _unifOfWork.SaveChangesAsync();

        }
    }
}
