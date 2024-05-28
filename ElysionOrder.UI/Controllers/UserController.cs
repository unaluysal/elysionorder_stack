using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RoleRightServices;
using ElysionOrder.Application.Services.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Principal;

namespace ElysionOrder.UI.Controllers
{

    public class UserController : Controller
    {
        readonly IUserService _userService;
        readonly IRoleRightService _roleRightService;
       
        public UserController(IUserService userService, IRoleRightService roleRightService)
        {

            _userService = userService;
            _roleRightService = roleRightService;
        
        }

        #region User

        [Authorize(Roles = "User_View")]
        public async Task<IActionResult> Index()
        {
            var list =await _userService.GetAllUsersAsync();
            return View(list);
        }

        [Authorize(Roles = "User_Add")]
        public async Task<ActionResult> Add()
        {
            var list =await _userService.GetAllUserTypesAsync();

            List<SelectListItem> types = new List<SelectListItem>();
           
            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;
            return View();
        }

        [Authorize(Roles = "User_Add")]
        [HttpPost]
        public async Task<ActionResult> Add(UserDto userDto)
        {
            await _userService.AddUserAsync(userDto);


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UserType()
        {
            var list =await _userService.GetAllUserTypesAsync();

            return View(list);
        }


        [Authorize(Roles = "UserType_Add")]
        public ActionResult AddType()
        {
            

            return View();
        }

        [Authorize(Roles = "UserType_Add")]
        [HttpPost]
        public async Task<ActionResult> AddType(UserTypeDto userTypeDto)
        {
           await _userService.AddUserTypeAsync(userTypeDto);

            return RedirectToAction("UserType");
        }


        [Authorize(Roles = "UserType_Delete")]
        public async Task<ActionResult> DeleteType(Guid id)
        {
            var type =await _userService.GetUserTypeWithIdAsync(id);


            return View(type);
        }
        [Authorize(Roles = "UserType_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteType(UserTypeDto userTypeDto)
        {
            await _userService.DeleteUserTypeAsync(userTypeDto.Id);


            return RedirectToAction("UserType");
        }

        [Authorize(Roles = "User_Edit")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _userService.GetUserWithIdAsync(id);
            List<SelectListItem> types = new List<SelectListItem>();
            var list = await _userService.GetAllUserTypesAsync();
            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;

            return View(user);
        }

        [Authorize(Roles = "User_Edit")]
        [HttpPost]
        public async Task<ActionResult> Edit(UserDto userDto)
        {
            await _userService.UpdateUserAsync(userDto);


            return RedirectToAction("Index");
        }


        [Authorize(Roles = "User_Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetUserWithIdAsync(id);


            return View(user);
        }

        [Authorize(Roles = "User_Delete")]
        [HttpPost]
        public async Task<ActionResult> Delete(UserDto userDto)
        {
            await _userService.DeleteUserAsync(userDto.Id);


            return RedirectToAction("Index");
        }

        #endregion


        [Authorize(Roles = "UserRole_View")]
        public async Task<IActionResult> UserRole()
        {
            var list = await _roleRightService.GetAllUserRolesAsync();
            return View(list);
        }


        [Authorize(Roles = "UserRole_Add")]
        public async Task<IActionResult> AddUserRole()
        {
            var list = await _userService.GetAllUsersAsync();

            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Users = types;


            var lis = await _roleRightService.GetAllRolesAsync();

            List<SelectListItem> tys = new List<SelectListItem>();

            foreach (var item in lis)
            {
                tys.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Roles = tys;


            return View();
        }

        [Authorize(Roles = "UserRole_Add")]
        [HttpPost]

        public async Task<IActionResult> AddUserRole(UserRoleDto userRoleDto)
        {
         await   _roleRightService.AddUserRoleAsync(userRoleDto);


            return RedirectToAction("Index");
        }


        [Authorize(Roles = "UserRole_Delete")]
        public async Task<IActionResult> DeleteUserRole(Guid id)
        {
           
            var r =await _roleRightService.GetUserRoleByIdAsync(id);

            return View(r);
        }

        [Authorize(Roles = "UserRole_Delete")]
        [HttpPost]

        public async Task<IActionResult> DeleteUserRole(UserRoleDto userRoleDto)
        {
            await _roleRightService.DeleteUserRoleAsync(userRoleDto.Id);


            return RedirectToAction("UserRole");
        }

        [AllowAnonymous]
        public  IActionResult Login()
        {
            return View();


        }
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login(UserDto userDto)
        {
      
            
            ViewBag.Result = null;
            var user =  await _userService.GetUserLoginAsync(userDto);
         
            if (user!=null)
            {
                var clms = await _userService.GetUserAllRightsAsync(user.Id);
                List<Claim> claims = new List<Claim> {
                        new Claim("Id",user.Id.ToString()),
                        new Claim("Name",user.Name),
                        new Claim("LastName",user.LastName.ToString()),
                        new Claim("UserType",user.UserTypeDto.Name)
                    };
                foreach (var item in clms)
                {

                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
               
                var identity = new ClaimsIdentity(claims,
                      CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Result = "Kullanıcı Adı Şifre Hatalı";
                return View();
            }

           
        }



        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login","User");


        }
       
     

        public IActionResult AccessDenied()
        {

            return View();
        }

    }
}
