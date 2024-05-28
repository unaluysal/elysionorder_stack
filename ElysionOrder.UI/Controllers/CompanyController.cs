using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.SalesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElysionOrder.UI.Controllers
{
    public class CompanyController : Controller
    {

        readonly private IBillService _billService;

        public CompanyController(IBillService billService)
        {

            _billService = billService;
        }

        [Authorize(Roles = "Company_View")]
        public async Task<IActionResult> Index()
        {

            var list =await _billService.GetAllCompanysAsync();
            return View(list);
        }
        
        [Authorize(Roles = "Company_Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var list = await _billService.GetCompanyWithAsync(id);
            return View(list);
        }

        [Authorize(Roles = "Company_Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(CompanyDto companyDto)
        {

            await _billService.EditCompanyAsync(companyDto);
            return RedirectToAction("Index", "Company");
        }

        [Authorize(Roles = "EBillSetting_View")]
        public async Task<IActionResult> EBillSettingIndex()
        {
            
            var list = await _billService.GetAllEbillSettingsAsync();
            return View(list);
        }

        [Authorize(Roles = "EBillSetting_Edit")]
        public async Task<IActionResult> EBillSettingEdit(Guid id)
        {

            var e = await _billService.GetEBillSettingWithIdAsync(id);
            return View(e);
        }

        [Authorize(Roles = "EBillSetting_Edit")]
        [HttpPost]
        public async Task<IActionResult> EBillSettingEdit(EBillSettingDto eBillSettingDto)
        {

           await _billService.EditEBillSettingAsync(eBillSettingDto);
            return RedirectToAction("EBillSettingIndex", "Company");
        }
    }
}
