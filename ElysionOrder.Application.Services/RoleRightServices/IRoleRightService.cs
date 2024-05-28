using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.RoleRightServices
{
    public interface IRoleRightService
    {
        public Task<List<RoleDto>> GetAllRolesAsync();
        public Task<RoleDto> GetRoleByIdAsync(Guid Id);
        public Task AddRoleAsync(RoleDto roleDto);
        public Task DeleteRoleAsync(Guid Id);
        public Task UpdateRoleAsync(RoleDto roleDto);

        public Task<List<RightDto>> GetAllRightsAsync();
        public Task<RightDto> GetRightByIdAsync(Guid Id);
        public Task AddRightAsync(RightDto rightDto);
        public Task DeleteRightAsync(Guid Id);
        public Task UpdateRightAsync(RightDto rightDto);

        public Task<List<RoleRightDto>> GetAllRoleRightsAsync();
        public Task<RoleRightDto> GetRoleRightByIdAsync(Guid Id);        
        public Task AddRoleRightAsync(RoleRightDto rightDto);
        public Task DeleteRoleRightAsync(Guid Id);

        public Task<List<UserRoleDto>> GetAllUserRolesAsync();
        public Task<UserRoleDto> GetUserRoleByIdAsync(Guid Id);
        public Task AddUserRoleAsync(UserRoleDto userRoleDto);
        public Task DeleteUserRoleAsync(Guid Id);
        public Task<List<RoleRightDto>> GetAllRoleRightByUserIdAsync(Guid Id);


    }
}
