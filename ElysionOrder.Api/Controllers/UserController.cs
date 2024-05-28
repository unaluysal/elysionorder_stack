using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace ElysionOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpGet("GetAllUserTypes")]
        public async Task<ActionResult> GetAllUserTypes()
        {


            var list = await _userService.GetAllUserTypesAsync();


            return Ok(list);

        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {


            var list = await _userService.GetAllUsersAsync();


            return Ok(list);

        }


        [HttpPost("GetUserTypeById")]
        public async Task<ActionResult> GetUserTypeById(Guid guid)
        {


            var u = await _userService.GetUserTypeWithIdAsync(guid);


            return Ok(u);

        }

        [HttpPost("GetUserById")]
        public async Task<ActionResult> GetUserById(Guid guid)
        {


            var u = await _userService.GetUserWithIdAsync(guid);


            return Ok(u);

        }


        [HttpPost("AddUserType")]
        public async Task<ActionResult> AddUserType(UserTypeDto userTypeDto)
        {


             await _userService.AddUserTypeAsync(userTypeDto);


            return Ok();

        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser(UserDto userDto)
        {


             await _userService.AddUserAsync(userDto);


            return Ok();

        }

        [HttpPost("UpdateUserType")]
        public async Task<ActionResult> UpdateUserType(UserTypeDto userTypeDto)
        {


            await _userService.UpdateUserTypeAsync(userTypeDto);


            return Ok();

        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UserDto userDto)
        {


            await _userService.UpdateUserAsync(userDto);


            return Ok();

        }


        [HttpPost("DeleteUserType")]
        public async Task<ActionResult> DeleteUserType(Guid guid)
        {


            await _userService.DeleteUserTypeAsync(guid);


            return Ok();

        }

        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(Guid guid)
        {


            await _userService.DeleteUserAsync(guid);


            return Ok();

        }

    }
}
