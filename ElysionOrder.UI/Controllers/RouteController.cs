using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.RouteServices;
using ElysionOrder.Application.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        readonly IRouteService _routeService;
        readonly IUserService _userService;
        readonly ICustomerService _customerService;

        public RouteController(IRouteService routeService, IUserService userService, ICustomerService customerService)
        {

            _routeService = routeService;
            _userService = userService;
            _customerService = customerService;
        }




        [Authorize(Roles ="Route_View")]
        public async Task<IActionResult> Index()
        {
            var list =await _routeService.GetAllRoutesAsync();
            return View(list);
        }

        [Authorize(Roles = "Route_Add")]
        public async Task<IActionResult> Add()
        {
            var days =await _routeService.GetAllDaysAsync();
            List<SelectListItem> day = new List<SelectListItem>();

            foreach (var item in days)
            {
                day.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Days = day;

            return View();
        }
        [Authorize(Roles = "Route_Add")]
        [HttpPost]
        public async Task<ActionResult> Add(RouteDto routeDto)
        {
            await _routeService.AddRouteAsnyc(routeDto);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Route_Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var r =await _routeService.GetRouteByIdAsync(id);

            return View(r);
        }

        [Authorize(Roles = "Route_Delete")]
        [HttpPost]
        public async Task<ActionResult> Delete(RouteDto routeDto)
        {
            await _routeService.DeleteRouteAsnyc(routeDto.Id);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Route_Detail")]
        public async Task<ActionResult> Detail(Guid id)
        {
            var r = await _routeService.GetRouteByIdAsync(id);

            return View(r);
        }

        [Authorize(Roles = "RouteUser_View")]
        public async Task<ActionResult> RouteUsers(Guid id)
        {

            var list =await _routeService.GetAllRouteUsersWithRouteIdAsync(id);
            return View(list);

        }

        [Authorize(Roles = "RouteUser_Add")]
        public async Task<IActionResult> AddRouteUser()
            {
            var routes = await _routeService.GetAllRoutesAsync();
            List<SelectListItem> rt = new List<SelectListItem>();

            foreach (var item in routes)
            {
                rt.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Routes = rt;

            var users = await _userService.GetAllUsersAsync();
            List<SelectListItem> ut = new List<SelectListItem>();

            foreach (var item in users)
            {
                ut.Add(new SelectListItem { Text = item.Name +" " +item.LastName, Value = item.Id.ToString() });
            }
            ViewBag.Users = ut;

            return View();
        }
        [Authorize(Roles = "RouteUser_Add")]
        [HttpPost]
        public async Task<ActionResult> AddRouteUser(RouteUserDto routeUserDto)
        {
            await _routeService.AddRouteUserAsync(routeUserDto);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "RouteCustomer_View")]
        public async Task<ActionResult> RouteCustomers(Guid id)
        {

            var list = await _routeService.GetAllRouteCustomersWithRouteIdAsync(id);
            return View(list);

        }

        [Authorize(Roles = "RouteCustomer_Add")]
        public async Task<IActionResult> AddRouteCustomer()
        {
            var routes = await _routeService.GetAllRoutesAsync();
            List<SelectListItem> rt = new List<SelectListItem>();

            foreach (var item in routes)
            {
                rt.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Routes = rt;

            var customers = await _customerService.GetAllCustomersAsync();
            List<SelectListItem> ut = new List<SelectListItem>();

            foreach (var item in customers)
            {
                ut.Add(new SelectListItem { Text = item.Name , Value = item.Id.ToString() });
            }
            ViewBag.Customers = ut;

            return View();
        }
        [Authorize(Roles = "RouteCustomer_Add")]
        [HttpPost]
        public async Task<ActionResult> AddRouteCustomer(RouteCustomerDto routeCustomerDto)
        {
            await _routeService.AddRouteCustomerAsync(routeCustomerDto);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "RouteUser_Delete")]
        public async Task<IActionResult> DeleteRouteUser(Guid id)
        {

            var ru =await _routeService.GetRouteUserByIdAsync(id);
            return View(ru);
        }
        [Authorize(Roles = "RouteUser_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteRouteUser(RouteUserDto routeUserDto)
        {

           await _routeService.DeleteRouteUserAsync(routeUserDto.Id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "RouteCustomer_Delete")]
        public async Task<IActionResult> DeleteRouteCustomer(Guid id)
        {

            var ru = await _routeService.GetRouteCustomerByIdAsync(id);
            return View(ru);
        }
        [Authorize(Roles = "RouteCustomer_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteRouteCustomer(RouteCustomerDto routeCustomerDto)
        {

            await _routeService.DeleteRouteCustomerAsync(routeCustomerDto.Id);
            return RedirectToAction("Index");
        }



    }
}
