using ElysionOrder.Application.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.UserServices
{
    public interface IUserService
    {

        public Task<List<UserTypeDto>> GetAllUserTypesAsync();
        public Task<UserTypeDto> GetUserTypeWithIdAsync(Guid id);
        public Task AddUserTypeAsync(UserTypeDto userTypeDto);
        public Task UpdateUserTypeAsync(UserTypeDto userTypeDto);
        public Task DeleteUserTypeAsync(Guid id);



        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<UserDto> GetUserWithIdAsync(Guid id);
        public Task AddUserAsync(UserDto userDto);
        public Task UpdateUserAsync(UserDto userDto);
        public Task DeleteUserAsync(Guid id);
        public Task<UserDto> GetUserLoginAsync(UserDto userDto);
        public Task<List<RightDto>> GetUserAllRightsAsync(Guid id);

    }
}
