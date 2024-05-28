using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.Context;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using DayOfWeek = ElysionOrder.Domain.Entitys.DayOfWeek;

namespace ElysionOrder.Application.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        readonly IUnitOfWork _unifOfWork;
        readonly IMapper _mapper;
        public RouteService(IUnitOfWork unitOfWork, IMapper mapper) {

            _mapper=mapper;
            _unifOfWork=unitOfWork;


        }

        #region Route
        public async Task AddRouteAsnyc(RouteDto routeDto)
        {
            var r =_mapper.Map<Route>(routeDto);
          
           await _unifOfWork.GetRepository<Route>().AddAsync(r);
           await _unifOfWork.SaveChangesAsync();
           
        }

        public async Task DeleteRouteAsnyc(Guid id)
        {
            var r =await _unifOfWork.GetRepository<Route>().GetByIdAsync( id);
            if (r != null)
            {
                r.Status= false;
                _unifOfWork.GetRepository<Route>().Update(r);
              await  _unifOfWork.SaveChangesAsync();
            }
            var rl =await _unifOfWork.GetRepository<RouteUser>().GetWhere(x => x.Status == true && x.RouteId == r.Id).ToListAsync ();
            rl.ForEach(x => x.Status = false);
            var ml = await _unifOfWork.GetRepository<RouteCustomer>().GetWhere(x => x.Status == true && x.RouteId == r.Id).ToListAsync();
            ml.ForEach(x => x.Status = false);
            _unifOfWork.GetRepository<RouteUser>().UpdateRange(rl);
            _unifOfWork.GetRepository<RouteCustomer>().UpdateRange(ml);
           await _unifOfWork.SaveChangesAsync();
        }

       
        public async Task<List<RouteDto>> GetAllRoutesAsync()
        {
            var list = await _unifOfWork.GetRepository<Route>().GetAll(). Include(x => x.DayOfWeek).Where(x => x.Status && x.DayOfWeek.Status).ToListAsync();
            var mlist = _mapper.Map<List<RouteDto>>(list);
            return mlist;
        }

        public async Task<RouteDto> GetRouteByIdAsync(Guid id)
        {
          var r = await _unifOfWork.GetRepository<Route>().GetAll().Include(x=>x.DayOfWeek).FirstOrDefaultAsync(x => x.Id == id);
          var mr = _mapper.Map<RouteDto>(r);
            List<UserDto> users = new List<UserDto>();
          var ulist =await _unifOfWork.GetRepository<RouteUser>().GetAll().Include(x=>x.User).ThenInclude(x=>x.UserType).Where(x=>x.Status && x.RouteId== id).ToListAsync();
            if (ulist != null)
            {
                foreach (var user in ulist)
                {
                    var c = users.FirstOrDefault(x => x.Id == user.UserId);
                    if (c == null)
                    {

                        users.Add(_mapper.Map<UserDto>(user.User));
                    }

                }
               

            }

            List<CustomerDto> custoemrs = new List<CustomerDto>();
            var clist = await _unifOfWork.GetRepository<RouteCustomer>().GetAll().Include(x => x.Customer).Where(x => x.Status && x.RouteId == id).ToListAsync();
            if (clist != null)
            {
                foreach (var customer in clist)
                {
                    var c = custoemrs.FirstOrDefault(x => x.Id == customer.CustomerId);
                    if (c == null)
                    {

                        custoemrs.Add(_mapper.Map<CustomerDto>(customer.Customer));
                    }

                }


            }
            mr.RouteUsers= users;
            mr.RouteCustomers=custoemrs;
           return mr;
            
          
        }

        public async Task UpdateRouteAsnyc(RouteDto routeDto)
        {
           var r =_mapper.Map<Route>(routeDto);
            r.Status = true;
            _unifOfWork.GetRepository<Route>().Update(r);
           await _unifOfWork.SaveChangesAsync();
        }
        #endregion

        #region Days

        public async Task<List<DayOfWeekDto>> GetAllDaysAsync()
        {
            var l =await _unifOfWork.GetRepository<DayOfWeek>().GetWhere(x => x.Status).ToListAsync();
            return _mapper.Map<List<DayOfWeekDto>>(l);

        }



        #endregion

        #region RouteCustomerUser
        public async Task<List<RouteCustomerDto>> GetAllRouteCustomersAsync()
        {
            var list =await _unifOfWork.GetRepository<RouteCustomer>().GetAll().Include(x => x.Route).Include(x => x.Customer).Where(x => x.Status && x.Customer.Status && x.Route.Status).ToListAsync();
            var mlist = _mapper.Map<List<RouteCustomerDto>>(list);
            return mlist;
        }

        public async Task<List<RouteCustomerDto>> GetAllRouteCustomersWithRouteIdAsync(Guid id)
        {
            var list = await _unifOfWork.GetRepository<RouteCustomer>().GetAll().Include(x => x.Route).Include(x => x.Customer).Where(x => x.Status && x.Customer.Status && x.Route.Status && x.RouteId==id).ToListAsync();
            var mlist = _mapper.Map<List<RouteCustomerDto>>(list);
            return mlist;
        }

        public async Task AddRouteCustomerAsync(RouteCustomerDto routeCustomerDto)
        {
            var check =await _unifOfWork.GetRepository<RouteCustomer>().GetAll().FirstOrDefaultAsync(x=>x.Status && x.RouteId== routeCustomerDto.RouteId && x.CustomerId==routeCustomerDto.Id);
            if (check == null)
            {
                var r = _mapper.Map<RouteCustomer>(routeCustomerDto);
               
                await _unifOfWork.GetRepository<RouteCustomer>().AddAsync(r);
                await _unifOfWork.SaveChangesAsync();

            }
           
        }

        public async Task DeleteRouteCustomerAsync(Guid id)
        {
           var r =await _unifOfWork.GetRepository<RouteCustomer>().GetByIdAsync(id);
            r.Status = false;
            _unifOfWork.GetRepository<RouteCustomer>().Update(r);
          await  _unifOfWork.SaveChangesAsync();
        }

        public async Task<List<RouteUserDto>> GetAllRouteUsersAsync()
        {
            var list = await _unifOfWork.GetRepository<RouteUser>().GetAll().Include(x => x.Route).Include(x => x.User).Where(x => x.Status && x.User.Status && x.Route.Status).ToListAsync();
            var mlist = _mapper.Map<List<RouteUserDto>>(list);
            return mlist;
        }

        public async Task<List<RouteUserDto>> GetAllRouteUsersWithRouteIdAsync(Guid id)
        {
            var list = await _unifOfWork.GetRepository<RouteUser>().GetAll().Include(x => x.Route).Include(x => x.User).Where(x => x.Status && x.User.Status && x.Route.Status && x.RouteId==id).ToListAsync();
            var mlist = _mapper.Map<List<RouteUserDto>>(list);
            return mlist;
        }

        public async Task AddRouteUserAsync(RouteUserDto routeUserDto)
        {

            var check =await _unifOfWork.GetRepository<RouteUser>().GetAll().FirstOrDefaultAsync(x => x.Status && x.UserId == routeUserDto.UserId && x.RouteId == routeUserDto.RouteId);
            if (check ==null)
            {
                var r = _mapper.Map<RouteUser>(routeUserDto);
              
                await _unifOfWork.GetRepository<RouteUser>().AddAsync(r);
                await _unifOfWork.SaveChangesAsync();


            }
           
        }

        public async Task DeleteRouteUserAsync(Guid id)
        {
            var r =await _unifOfWork.GetRepository<RouteUser>().GetByIdAsync (id);
            r.Status = false;
            _unifOfWork.GetRepository<RouteUser>().Update(r);
          await  _unifOfWork.SaveChangesAsync();
        }

        public async Task<RouteUserDto> GetRouteUserByIdAsync(Guid id)
        {
            var r =await _unifOfWork.GetRepository<RouteUser>().GetAll().Include(x=>x.Route).Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==id);
            return _mapper.Map<RouteUserDto>(r);
        }

        public async Task<RouteCustomerDto> GetRouteCustomerByIdAsync(Guid id)
        {
            var r = await _unifOfWork.GetRepository<RouteCustomer>().GetAll().Include(x => x.Route).Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<RouteCustomerDto>(r);
        }

        public async Task DeleteCustomerRoutesAsync(CustomerDto customerDto)
        {
            var list = await   _unifOfWork.GetRepository<RouteCustomer>().GetWhere(x => x.CustomerId == customerDto.Id).ToListAsync();
            list.ForEach(x=>x.Status= false);
            _unifOfWork.GetRepository<RouteCustomer>().UpdateRange(list);
          await  _unifOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
