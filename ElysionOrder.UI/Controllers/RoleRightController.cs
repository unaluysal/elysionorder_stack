using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RoleRightServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
   
    public class RoleRightController : Controller
    {
        readonly IRoleRightService _roleRightService;

        public RoleRightController(IRoleRightService roleRightService)
        {
            _roleRightService = roleRightService;

        }

        #region RoleRight

        [Authorize(Roles ="RoleRight_View")]
        public async Task<IActionResult> Index()
        {
            var list = await _roleRightService.GetAllRoleRightsAsync();

            return View(list);
        }
        [Authorize(Roles = "RoleRight_Add")]
        public async Task<IActionResult> Add()
        {
            var role =await _roleRightService.GetAllRolesAsync();
            var right =await _roleRightService.GetAllRightsAsync();
            
            List<SelectListItem> roles = new List<SelectListItem>();

            foreach (var item in role)
            {
                roles.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Roles = roles;


            List<SelectListItem> rights = new List<SelectListItem>();

            foreach (var item in right)
            {
                rights.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Rights = rights;

            return View();
        }

        [Authorize(Roles = "RoleRight_Add")]
        [HttpPost]
        public async Task<IActionResult> Add(RoleRightDto roleRightDto)
        {
            await _roleRightService.AddRoleRightAsync(roleRightDto);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "RoleRight_Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var roleright =await _roleRightService.GetRoleRightByIdAsync(id);

            return View(roleright);
        }

        [Authorize(Roles = "RoleRight_Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(RoleRightDto roleRightDto)
        {
            await _roleRightService.DeleteRoleRightAsync(roleRightDto.Id);

            return RedirectToAction("Index");
        }




        #endregion

        #region Right
        [Authorize(Roles = "Right_View")]
        public async Task<IActionResult> Right()
        {
            var list = await _roleRightService.GetAllRightsAsync();

            return View(list);
        }

        [Authorize(Roles = "Right_Add")]
        public  IActionResult AddRight()
        {
            

            return View();
        }

        [Authorize(Roles = "Right_Add")]
        [HttpPost]
        public async Task<IActionResult> AddRight(RightDto rightDto)
        {
            await _roleRightService.AddRightAsync(rightDto);

            return RedirectToAction("Right");
        }
        [Authorize(Roles = "Right_Delete")]
        public async Task<IActionResult> DeleteRight(Guid id)
        {
            var r = await _roleRightService.GetRightByIdAsync(id);

            return View(r);
        }
        [Authorize(Roles = "Right_Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteRight(RightDto rightDto)
        {
           await _roleRightService.DeleteRightAsync(rightDto.Id);

            return RedirectToAction("Right");
        }


        #endregion

        #region Role

        [Authorize(Roles = "Role_View")]
        public async Task<IActionResult> Role()
        {
            var list = await _roleRightService.GetAllRolesAsync();

            return View(list);
        }
        [Authorize(Roles = "Role_Add")]
        public IActionResult AddRole()
        {


            return View();
        }
        [Authorize(Roles = "Role_Add")]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDto roleDto)
        {
            await _roleRightService.AddRoleAsync(roleDto);

            return RedirectToAction("Role");
        }
        [Authorize(Roles = "Role_Delete")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var r = await _roleRightService.GetRoleByIdAsync(id);

            return View(r);
        }
        [Authorize(Roles = "Role_Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(RoleDto roleDto)
        {
            await _roleRightService.DeleteRoleAsync(roleDto.Id);

            return RedirectToAction("Role");
        }

        #endregion
    }
}
