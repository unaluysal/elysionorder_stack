using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    public class ExpenseController : Controller
    {

        private IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {

            _expenseService=expenseService;

        }

        [Authorize(Roles = "Expense_View")]
        public async Task<IActionResult> Index()
        {
            var list = await _expenseService.GetAllExpensesAsync();
            return View(list);
        }
        
        [Authorize(Roles = "Expense_Add")]
        public async Task<IActionResult> Add()
        {

            var list = await _expenseService.GetAllExpenseTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;
            return View();
        }


        [Authorize(Roles = "Expense_Add")]
        [HttpPost]
        public async Task<ActionResult> Add(ExpenseDto expenseDto)
        {
            await _expenseService.AddExpenseAsync(expenseDto);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Expense_Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var et = await _expenseService.GetExpenseWithIdAsync(id);

            return View(et);
        }

        [Authorize(Roles = "Expense_Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(ExpenseDto expenseDto)
        {
            await _expenseService.DeleteExpenseAsync(expenseDto.Id);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Expense_Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var list = await _expenseService.GetAllExpenseTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;
            var et = await _expenseService.GetExpenseWithIdAsync(id);

            return View(et);
        }

        [Authorize(Roles = "Expense_Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseDto expenseDto)
        {
            await _expenseService.UpdateExpenseAsync(expenseDto);

            return RedirectToAction("Index");
        }





        [Authorize(Roles = "ExpenseType_View")]
        public async Task<IActionResult> Types()
        {
            var list = await _expenseService.GetAllExpenseTypesAsync();
            return View(list);
        }


        [Authorize(Roles = "ExpenseType_Add")]
        public IActionResult AddType()
        {

            
            return View();
        }


        [Authorize(Roles = "ExpenseType_Add")]
        [HttpPost]
        public async Task<ActionResult> AddType(ExpenseTypeDto expenseTypeDto)
        {
            await _expenseService.AddExpenseTypeAsync(expenseTypeDto);

            return RedirectToAction("Types");
        }

        [Authorize(Roles = "ExpenseType_Delete")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var et = await _expenseService.GetExpenseTypeWithIdAsync(id);

            return View(et);
        }

        [Authorize(Roles = "ExpenseType_Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteType(ExpenseTypeDto expenseTypeDto)
        {
             await _expenseService.DeleteExpenseTypeAsync(expenseTypeDto.Id);

            return RedirectToAction("Types");
        }

        [Authorize(Roles = "ExpenseType_Edit")]
        public async Task<IActionResult> EditType(Guid id)
        {
            var et = await _expenseService.GetExpenseTypeWithIdAsync(id);

            return View(et);
        }

        [Authorize(Roles = "ExpenseType_Edit")]
        [HttpPost]
        public async Task<IActionResult> EditType(ExpenseTypeDto expenseTypeDto)
        {
            await _expenseService.UpdateExpenseTypeAsync(expenseTypeDto);

            return RedirectToAction("Types");
        }
    }
}
