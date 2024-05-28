using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RoleRightServices;
using Microsoft.AspNetCore.Mvc;

namespace ElysionOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleRightController : ControllerBase
    {
        readonly IRoleRightService _roleRightService;
        public RoleRightController(IRoleRightService roleRightService) {

            _roleRightService = roleRightService;
        }


        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {


            var list = await _roleRightService.GetAllRolesAsync();


            return Ok(list);

        }

        [HttpGet("GetAllRights")]
        public async Task<ActionResult> GetAllRights()
        {


            var list = await _roleRightService.GetAllRightsAsync();


            return Ok(list);

        }


        [HttpGet("GetAllRoleRights")]
        public async Task<ActionResult> GetAllRoleRights()
        {


            var list = await _roleRightService.GetAllRoleRightsAsync();


            return Ok(list);

        }


        [HttpPost("GetRoleById")]
        public async Task<ActionResult> GetRoleById(Guid guid)
        {


            var r = await _roleRightService.GetRoleByIdAsync(guid);


            return Ok(r);

        }

        [HttpPost("GetRightById")]
        public async Task<ActionResult> GetRightById(Guid id)
        {


            var r = await _roleRightService.GetRightByIdAsync(id);


            return Ok(r);

        }


        [HttpGet("GetAllRoleRightById")]
        public async Task<ActionResult> GetAllRoleRightById(Guid id)
        {


            var r = await _roleRightService.GetRoleRightByIdAsync(id);


            return Ok(r);

        }


        [HttpPost("AddRole")]
        public async Task<ActionResult> AddRole(RoleDto roleDto)
        {


            await _roleRightService.AddRoleAsync(roleDto);


            return Ok();

        }

        [HttpPost("AddRight")]
        public async Task<ActionResult> AddRight(RightDto rightDto)
        {


            await _roleRightService.AddRightAsync(rightDto);


            return Ok();

        }


        [HttpPost("AddRoleRight")]
        public async Task<ActionResult> AddRoleRight(RoleRightDto rightDto)
        {


            await _roleRightService.AddRoleRightAsync(rightDto);


            return Ok();

        }



        [HttpPost("UpdateRole")]
        public async Task<ActionResult> UpdateRole(RoleDto roleDto)
        {


            await _roleRightService.UpdateRoleAsync(roleDto);


            return Ok();

        }

        [HttpPost("DeleteRole")]
        public async Task<ActionResult> DeleteRole(Guid guid)
        {


            await _roleRightService.DeleteRoleAsync(guid);


            return Ok();

        }

        [HttpPost("UpdateRight")]
        public async Task<ActionResult> UpdateRight(RightDto rightDto)
        {


            await _roleRightService.UpdateRightAsync(rightDto);


            return Ok();

        }


        [HttpPost("DeleteRight")]
        public async Task<ActionResult> DeleteRight(Guid guid)
        {


            await _roleRightService.DeleteRightAsync(guid);


            return Ok();

        }

        [HttpPost("DeleteRoleRight")]
        public async Task<ActionResult> DeleteRoleRight(Guid guid)
        {


            await _roleRightService.DeleteRoleRightAsync(guid);


            return Ok();

        }



    }
}
