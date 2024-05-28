using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ElysionOrder.Application.Services.RoleRightServices
{
    public class RoleRightService : IRoleRightService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public RoleRightService(IUnitOfWork unitOfWork, IMapper mapper) {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task AddRightAsync(RightDto rightDto)
        {
            var right =_mapper.Map<Right>(rightDto);
           
            await  _unitOfWork.GetRepository<Right>().AddAsync(right);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddRoleAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
         
            await _unitOfWork.GetRepository<Role>().AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddRoleRightAsync(RoleRightDto roleRightDto)
        {
            var roleright = _mapper.Map<RoleRight>(roleRightDto);
           
            await _unitOfWork.GetRepository<RoleRight>().AddAsync(roleright);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddUserRoleAsync(UserRoleDto userRoleDto)
        {
            var ur =_mapper.Map<UserRole>(userRoleDto);
           
           await _unitOfWork.GetRepository<UserRole>().AddAsync(ur);
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRightAsync(Guid Id)
        {
            var r =await _unitOfWork.GetRepository<Right>().GetByIdAsync ( Id);
            r.Status = false;
            _unitOfWork.GetRepository<Right>().Update(r);
            await _unitOfWork.SaveChangesAsync();
            var rr =await _unitOfWork.GetRepository<RoleRight>().GetWhere(x => x.RightId == Id && x.Status).ToListAsync();
            if (rr!=null)
            {
                foreach (var item in rr.Select(x=>x.Id))
                {
                  await  DeleteRoleRightAsync(item);
                }
            }
           
        }

        public async Task DeleteRoleAsync(Guid Id)
        {
            var r = await _unitOfWork.GetRepository<Role>().GetByIdAsync( Id);
            r.Status = false;
            _unitOfWork.GetRepository<Role>().Update(r);
            await _unitOfWork.SaveChangesAsync();
            var rr = await _unitOfWork.GetRepository<RoleRight>().GetWhere(x => x.RoleId == Id).ToListAsync();
            if (rr != null)
            {
                rr.ForEach(x => x.Status = false);
            }
            _unitOfWork.GetRepository<RoleRight>().UpdateRange(rr);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRoleRightAsync(Guid Id)
        {
            var r = await _unitOfWork.GetRepository<RoleRight>().GetByIdAsync(Id);
            r.Status = false;
            _unitOfWork.GetRepository<RoleRight>().Update(r);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(Guid Id)
        {
           var ur =await _unitOfWork.GetRepository<UserRole>().GetByIdAsync( Id);
            if (ur!=null)
            {
                ur.Status = false;
                  _unitOfWork.GetRepository<UserRole>().Update(ur);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<List<RightDto>> GetAllRightsAsync()
        {
            var list =await _unitOfWork.GetRepository<Right>().GetWhere(x => x.Status).ToListAsync();
            var mlist =_mapper.Map<List<RightDto>>(list);

            return mlist.OrderBy(x=>x.Name).ToList();

        }

        public async Task<List<RoleRightDto>> GetAllRoleRightByUserIdAsync(Guid Id)
        {
            var rr = new    List<RoleRightDto>();

            var list =await _unitOfWork.GetRepository<UserRole>().GetAll().Include(x => x.User).Include(x => x.Role).Where(x => x.Status && x.User.Status && x.Role.Status && x.UserId == Id).ToListAsync();
            foreach (var r in list)
            {
                var rolerights =await _unitOfWork.GetRepository<RoleRight>().GetAll().Include(x=>x.Right).Where(x=>x.Right.Status&& x.Status && x.RoleId== r.RoleId).ToListAsync();

                foreach (var item in rolerights)
                {
                    var check = rr.FirstOrDefault(x => x.Id == item.Id);
                    if (check == null)
                    {
                        rr.Add(_mapper.Map<RoleRightDto>(item));

                    }
                }

                

            }

            return rr;
        }

        public async Task<List<RoleRightDto>> GetAllRoleRightsAsync()
        {
            var rr = new List<RoleRightDto>();
            var r =await _unitOfWork.GetRepository<RoleRight>().GetAll().Include(x => x.Role).Include(x => x.Right).Where(x => x.Status && x.Role.Status && x.Right.Status).ToListAsync();
            if (r!=null)
            {
                rr = _mapper.Map<List<RoleRightDto>>(r);
            }

            return rr;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var list = await _unitOfWork.GetRepository<Role>().GetWhere(x => x.Status).ToListAsync();
            var mlist = _mapper.Map<List<RoleDto>>(list);

            return mlist.OrderBy(x=>x.Name).ToList();
        }

        public async Task<List<UserRoleDto>> GetAllUserRolesAsync()
        {
            var list =await _unitOfWork.GetRepository<UserRole>().GetAll().Include(x => x.User).Include(x => x.Role).Where(x => x.Status && x.User.Status && x.Role.Status).ToListAsync();
            var ml =_mapper.Map<List<UserRoleDto>>(list);
            return ml;
        }

        public async Task<RightDto> GetRightByIdAsync(Guid Id)
        {
            var rr = new RightDto();
            var r =await _unitOfWork.GetRepository<Right>().GetByIdAsync(Id);
            if (r!=null)
            {
               rr= _mapper.Map<RightDto>(r);
            }
            return rr;
        }

        public async Task<RoleDto> GetRoleByIdAsync(Guid Id)
        {
            var rr = new RoleDto();
            var r = await _unitOfWork.GetRepository<Role>().GetByIdAsync( Id );
            if (r != null)
            {
                rr = _mapper.Map<RoleDto>(r);
            }
            return rr;
        }

        public async Task<RoleRightDto> GetRoleRightByIdAsync(Guid Id)
        {
            var r = new RoleRightDto();
            var rr =await _unitOfWork.GetRepository<RoleRight>().GetAll().Include(x => x.Role).Include(x => x.Right)
                .FirstOrDefaultAsync(x => x.Status && x.Role.Status && x.Right.Status && x.Id == Id);
            if (rr!=null)
            {
                r=_mapper.Map<RoleRightDto>(rr);
            }

            return r;
        }

        public async Task<UserRoleDto> GetUserRoleByIdAsync(Guid Id)
        {
           var r =await _unitOfWork.GetRepository<UserRole>().GetAll().Include(x=>x.User).Include(x=>x.Role).FirstOrDefaultAsync(x=>x.Id== Id);

            var mr =_mapper.Map<UserRoleDto>(r);

            return mr;
        }

        public async Task UpdateRightAsync(RightDto rightDto)
        {
           var r = _mapper.Map<Right>(rightDto);
            r.Status = true;
            _unitOfWork.GetRepository<Right>().Update(r);
             await   _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RoleDto roleDto)
        {
            var r = _mapper.Map<Role>(roleDto);
            r.Status = true;
            _unitOfWork.GetRepository<Role>().Update(r);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
