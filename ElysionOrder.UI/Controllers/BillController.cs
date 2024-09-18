using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.SalesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    public class BillController : Controller
    {

        private readonly IBillService _billService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentService _paymentService;

        public BillController(IBillService billService, ICustomerService customerService, IPaymentService paymentService)
        {

            _billService = billService;
            _customerService = customerService;
            _paymentService = paymentService;   
        }


        [Authorize(Roles = "Bill_View")]
        public async Task<IActionResult> Index()
        {
            var list = await _billService.GetAllBillsAsync();
            return View(list);
        }

        //[Authorize(Roles = "Bill_Add")]
        //public async Task<IActionResult> Add(Guid id)
        //{
        //     await _billService.AddBillAfterSalesAsync(id);
        //    return View();
        //}

        [Authorize(Roles = "Bill_View")]
        public async Task<IActionResult> Detail(Guid id)
        {

            var b = await _billService.GetBillSettingWithIdAsync(id);
            return View(b);
        }


        [Authorize(Roles = "BillSettings_View")]
        public async Task<IActionResult> BillSettings()
        {
            var l = await _billService.GetAllBillSettingsAsync();
            return View(l);
        }


        [Authorize(Roles = "BillSettings_Edit")]
        public async Task<IActionResult> EditSetting(Guid id)
        {
            var l = await _billService.GetBillSettingWithIdAsync(id);
            return View(l);
        }


        [Authorize(Roles = "BillSettings_Edit")]
        [HttpPost]
        public async Task<IActionResult> EditSetting(BillSettingDto billSettingDto)
        {
            await _billService.EditBillSettingAsync(billSettingDto);
            return RedirectToAction("BillSettings", "Bill");
        }


        //[Authorize(Roles = "Bill_Write")]
        //public async Task<IActionResult> PrintBill(Guid id)
        //{


        //    var l = await _billService.GetBillWithIdAsync(id);
        //    await _billService.WriteBillAsync(l);

        //    return RedirectToAction("Index", "Bill");

        //}


        [Authorize(Roles = "Bill_AddPayment")]
        public async Task<IActionResult> AddPayment(Guid id)
        {

            var list = await _paymentService.GetAllPaymentWaysAsync();

            List<SelectListItem> pw = new List<SelectListItem>();

            foreach (var item in list)
            {
                pw.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Payments = pw;

            PaymentDto paymentDto = new PaymentDto();
            paymentDto.CustomerId = id;

            var customer = await _customerService.GetCustomerWithIdAsync(id);
            paymentDto.CustomerDto = customer;
            return View(paymentDto);

        }


        [Authorize(Roles = "Bill_AddPayment")]
        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentDto paymentDto)
        {

            await _paymentService.AddPaymentAsync(paymentDto);

            return RedirectToAction("Detail", "Customer", new { id = paymentDto.CustomerId });

        }

        [Authorize(Roles = "Bill_AddDebt")]
        public async Task<IActionResult> AddDebt(Guid id)
        {
            var list = await _paymentService.GetAllPaymentWaysAsync();

            List<SelectListItem> pw = new List<SelectListItem>();

            foreach (var item in list)
            {
                pw.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Payments = pw;
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.CustomerId = id;
            var customer = await _customerService.GetCustomerWithIdAsync(id);
            paymentDto.CustomerDto = customer;

            return View(paymentDto);

        }


        [Authorize(Roles = "Bill_AddDebt")]
        [HttpPost]
        public async Task<IActionResult> AddDebt(PaymentDto paymentDto)
        {

            await _paymentService.AddDebtAsync(paymentDto);

            return RedirectToAction("Detail", "Customer", new { id = paymentDto.CustomerId });

        }

        //[Authorize(Roles = "EBill_Send")]

        //public async Task<IActionResult> SendEBill(Guid id)
        //{

        //    var c = await _billService.GetAllCompanysAsync();
        //    ViewBag.Company = c.FirstOrDefault();
        //    var b = await _billService.GetBillWithIdAsync(id);

        //    return View(b);

        //}

        //[Authorize(Roles = "EBill_Send")]
        //[HttpPost]
        //public async Task<IActionResult> SendEBill(BillDto billDto)
        //{


        //    var b = await _billService.SendEBillingAsync(billDto.Id);
        //    if (!String.IsNullOrEmpty(b)) { return RedirectToAction("Detail", "Bill", new { id = billDto.Id }); }
        //    return View();


        //}



        public async Task<IActionResult> EdmTest()
        {

            await _billService.SendInvoiceAsync(Guid.Parse("42fc5092-d9c1-4516-ba35-1d23662255f6"));
            return View();
        }

    }
}
