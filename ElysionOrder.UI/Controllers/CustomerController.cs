using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {

        readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) { 
        
        _customerService= customerService;

        }
        [Authorize(Roles = "Customer_View")]
        public async Task<IActionResult> Index()
        {

            var customer =await _customerService.GetAllCustomersAsync();
            return View(customer);
        }

        [Authorize(Roles = "Customer_Add")]
        public async Task<IActionResult> Add()
        {
            var type = await _customerService.GetAllCustomerTypesAsync();
            List<SelectListItem> tyy = new List<SelectListItem>();

            foreach (var item in type)
            {
                tyy.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = tyy;
            return View();
        }

        [Authorize(Roles = "Customer_Add")]
        [HttpPost]
        public async Task<ActionResult> Add(CustomerDto customerDto)
        {
            await _customerService.AddCustomerAsync(customerDto);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Customer_Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var t = await _customerService.GetCustomerWithIdAsync(id);

            ViewBag.CanBeDelete = await _customerService.CustomerCanBeDeleteAsync(id);

            

            return View(t);
        }

        [Authorize(Roles = "Customer_Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(CustomerDto customerDto)
        {
            await _customerService.DeleteCustomerAsync(customerDto.Id);

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Customer_Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var c = await _customerService.GetCustomerWithIdAsync(id);
            var type = await _customerService.GetAllCustomerTypesAsync();
            List<SelectListItem> tyy = new List<SelectListItem>();

            foreach (var item in type)
            {
                tyy.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = tyy;

            return View(c);
        }

        [Authorize(Roles = "Customer_Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDto customerDto)
        {
            await _customerService.UpdateCustomerAsync(customerDto);

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Customer_View")]
        public async Task<IActionResult> Detail(Guid id)
        {

            var customer = await _customerService.GetCustomerWithDetailByIdAsync(id);

            return View(customer);


        }


        #region CustomerType

        [Authorize(Roles = "CustomerType_View")]
        public async Task<IActionResult> Type()
        {

            var list =await _customerService.GetAllCustomerTypesAsync();
            return View(list);
        }

        [Authorize(Roles = "CustomerType_Add")]
        public  IActionResult AddType()
        {
           

            return View();
        }

        [Authorize(Roles = "CustomerType_Add")]
        [HttpPost]
        public async Task<IActionResult> AddType(CustomerTypeDto customerTypeDto)
        {
           await _customerService.AddCustomerTypeAsync(customerTypeDto);

            return RedirectToAction("Type");

        }

        [Authorize(Roles = "CustomerType_Delete")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var t =await _customerService.GetCustomerTypeWithIdAsync(id);

            return View(t);
        }
        [Authorize(Roles = "CustomerType_Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteType(CustomerTypeDto customerTypeDto)
        {
           await _customerService.DeleteCustomerTypeAsync(customerTypeDto.Id);

            return RedirectToAction("Type");

        }


        [Authorize(Roles = "CustomerType_Edit")]
        public async Task<IActionResult> EditType(Guid id)
        {
            var t = await _customerService.GetCustomerTypeWithIdAsync(id);

            return View(t);
        }

        [Authorize(Roles = "CustomerType_Edit")]
        [HttpPost]
        public async Task<IActionResult> EditType(CustomerTypeDto customerTypeDto)
        {
            await _customerService.UpdateCustomerTypeAsync(customerTypeDto);

            return RedirectToAction("Type");

        }

        #endregion
    }
}
