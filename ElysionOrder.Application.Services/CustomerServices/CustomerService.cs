using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RouteServices;
using ElysionOrder.Application.Services.SalesServices;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.CustomerServices
{

    public class CustomerService : ICustomerService
    {

       
        readonly IMapper _mapper;
        readonly IRouteService _routeService;
        readonly ISalesService _salesService;
        readonly IUnitOfWork _unitOfWork;
        readonly IBillService _billService;
        public CustomerService( IMapper mapper, IRouteService routeService, ISalesService salesService, IUnitOfWork unitOfWork, IBillService billService)
        {
           
            _mapper = mapper;
            _routeService = routeService;
            _salesService = salesService;
            _unitOfWork = unitOfWork;
            _billService = billService;
        }


        public async Task AddCustomerAsync(CustomerDto CustomerDto)
        {
            
            var Customer = _mapper.Map<Customer>(CustomerDto);
         
            await _unitOfWork.GetRepository<Customer>().AddAsync(Customer);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task AddCustomerTypeAsync(CustomerTypeDto CustomerTypeDto)
        {
            var t = _mapper.Map<CustomerType>(CustomerTypeDto);
        
            await _unitOfWork.GetRepository<CustomerType>().AddAsync(t);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CustomerCanBeDeleteAsync(Guid id)
        {
            var l =await _salesService.GetActiveSalesesWithCustomerIdAsync(id);
            if (l!=null)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
          var res = await _salesService.CustomerHaveActiveSalesAsync(id);
            if (res!=true)
            {
                var u = await _unitOfWork.GetRepository<Customer>().GetByIdAsync(id);
                u.Status = false;
                _unitOfWork.GetRepository<Customer>().Update(u);
                await _unitOfWork.SaveChangesAsync();
                await _routeService.DeleteCustomerRoutesAsync(_mapper.Map<CustomerDto>(u));

            }
          
           
           
        }

        public async Task DeleteCustomerTypeAsync(Guid id)
        {
            var u = await _unitOfWork.GetRepository<CustomerType>().GetByIdAsync(id);
            u.Status = false;
            _unitOfWork.GetRepository<CustomerType>().Update(u);

            var l = await _unitOfWork.GetRepository<Customer>().GetWhere(x => x.Status && x.CustomerTypeId == id).ToListAsync();
            l.ForEach(x => x.Status = false);
            _unitOfWork.GetRepository<Customer>().UpdateRange(l);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var l = await _unitOfWork.GetRepository<Customer>().GetAll().Include(x => x.CustomerType).Where(x => x.Status && x.CustomerType.Status).OrderBy(x=>x.Name).ToListAsync();
            var ml = _mapper.Map<List<CustomerDto>>(l);
            return ml;
        }

        public async Task<List<CustomerTypeDto>> GetAllCustomerTypesAsync()
        {
            var l = await _unitOfWork.GetRepository<CustomerType> ().GetWhere(x => x.Status).OrderBy(x => x.Name).ToListAsync();
            var ml = _mapper.Map<List<CustomerTypeDto>>(l);
            return ml;
        }

        public async Task<CustomerTypeDto> GetCustomerTypeWithIdAsync(Guid id)
        {
            var u = await _unitOfWork.GetRepository<CustomerType>() .GetByIdAsync(id);
            var mu = _mapper.Map<CustomerTypeDto>(u);

            return mu;
        }

        public async Task<CustomerDto> GetCustomerWithDetailByIdAsync(Guid id)
        {
            var customer =await _unitOfWork.GetRepository<Customer>().GetAll().Include(x => x.CustomerType).FirstOrDefaultAsync(x => x.Id == id);
            var cm = _mapper.Map<CustomerDto>(customer);
            cm.ActiveSales =await _salesService.GetActiveSalesesWithCustomerIdAsync(id);
            cm.AllSales =await _salesService.GetSalesesWithCustomerIdAsync(id);
            cm.AllSales = cm.AllSales.OrderBy(x => x.SalesStatusDto.Name).ToList();
            cm.Payments = await _billService.GetAllCustomerPaymentsWithIdAsync(id);
            var payment = cm.Payments.Where(x => x.PaymentTypeDto.Type == "Pozitif").ToList().Sum(x => x.Total);
            var deb = cm.Payments.Where(x => x.PaymentTypeDto.Type == "Negatif").ToList().Sum(x => x.Total);
            cm.Debt = payment - deb;


            return cm;
        }

        public async Task<CustomerDto> GetCustomerWithIdAsync(Guid id)
        {
            var u = await _unitOfWork.GetRepository<Customer>().GetAll().Include(x => x.CustomerType).FirstOrDefaultAsync(x => x.Status && x.CustomerType.Status && x.Id == id);
            var mu = _mapper.Map<CustomerDto>(u);

            return mu;
        }

        public async Task UpdateCustomerAsync(CustomerDto CustomerDto)
        {
           
            var u = _mapper.Map<Customer>(CustomerDto);
           
            _unitOfWork.GetRepository<Customer>().Update(u);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCustomerTypeAsync(CustomerTypeDto CustomerTypeDto)
        {
            var u = _mapper.Map<CustomerType>(CustomerTypeDto);
           
            _unitOfWork.GetRepository<CustomerType>().Update(u);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
