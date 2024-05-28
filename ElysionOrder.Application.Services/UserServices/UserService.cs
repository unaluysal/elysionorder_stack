using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RoleRightServices;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ElysionOrder.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly IRoleRightService _roleRightService;
       
        
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IRoleRightService roleRightService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRightService = roleRightService;
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            
              var user = _mapper.Map<User>(userDto);
              
              await  _unitOfWork.GetRepository<User>().AddAsync(user);
              await  _unitOfWork.SaveChangesAsync();
                
        }

        public async Task AddUserTypeAsync(UserTypeDto userTypeDto)
        {
            var t = _mapper.Map<UserType>(userTypeDto);
     
            await _unitOfWork.GetRepository<UserType>().AddAsync(t);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var u =await _unitOfWork.GetRepository<User>().GetByIdAsync(id);
            u.Status = false;
            _unitOfWork.GetRepository<User>().Update(u);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserTypeAsync(Guid id)
        {
            var u = await _unitOfWork.GetRepository<UserType>().GetByIdAsync( id);
            u.Status = false;
            _unitOfWork.GetRepository<UserType>().Update(u);

            var l =await _unitOfWork.GetRepository<User>().GetWhere(x => x.Status && x.UserTypeId == id).ToListAsync();
            l.ForEach(x => x.Status = false);
            _unitOfWork.GetRepository<User>().UpdateRange(l);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var l =await _unitOfWork.GetRepository<User>().GetAll().Include(x => x.UserType).Where(x => x.Status && x.UserType.Status).ToListAsync();
            var ml = _mapper.Map<List<UserDto>>(l);
    
            return ml;
        }

        public async Task<List<UserTypeDto>> GetAllUserTypesAsync()
        {
            var l = await _unitOfWork.GetRepository<UserType>().GetWhere(x => x.Status).ToListAsync();
            var ml = _mapper.Map<List<UserTypeDto>>(l);
            return ml;
        }

        public async Task<UserDto> GetUserLoginAsync(UserDto userDto)
        {
            var user =await _unitOfWork.GetRepository<User>().GetAll().Include(x=>x.UserType).FirstOrDefaultAsync(x => x.LoginName == userDto.LoginName && x.Password == userDto.Password && x.Status);
            if (user!=null)
            {
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }

        public async Task<UserTypeDto> GetUserTypeWithIdAsync(Guid id)
        {
            var u =await _unitOfWork.GetRepository<UserType>().GetByIdAsync(id);
            var mu = _mapper.Map<UserTypeDto>(u);

            return mu;
        }

        public async Task<UserDto> GetUserWithIdAsync(Guid id)
        {
            var u = await _unitOfWork.GetRepository<User>().GetAll().Include(x=>x.UserType).FirstOrDefaultAsync(x => x.Status && x.UserType.Status && x.Id == id);
            var mu = _mapper.Map<UserDto>(u);

            return mu;
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var u = _mapper.Map<User>(userDto);
            u.Status = true;
            _unitOfWork.GetRepository<User>().Update(u);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserTypeAsync(UserTypeDto userTypeDto)
        {
            var u = _mapper.Map<UserType>(userTypeDto);
            u.Status = true;
            _unitOfWork.GetRepository<UserType>().Update(u);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<RightDto>> GetUserAllRightsAsync(Guid id)
        {
            List<RightDto> rights = new List<RightDto>();
            var rolerights =await _roleRightService.GetAllRoleRightByUserIdAsync(id);
            if (rolerights!=null)
            {
                foreach (var item in rolerights)
                {
                    if (rights.FirstOrDefault(x=>x.Id==item.RightId)==null)
                    {
                        rights.Add(item.RightDto);
                    }
                   
                }

            }
            return rights;
        }
    }
}
