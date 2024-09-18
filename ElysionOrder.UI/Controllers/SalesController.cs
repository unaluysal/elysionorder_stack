using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.ProductServices;
using ElysionOrder.Application.Services.SalesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        readonly ISalesService _salesService;
        readonly ICustomerService _customerService;
        readonly IOrderService _orderService;
        readonly IPriceService _priceService;
        readonly IStockService _stockService;
        readonly IProductService _productService;
        readonly IBillService _billService;
        public SalesController(ISalesService salesService, ICustomerService customerService, IOrderService orderService,
            IPriceService priceService, IStockService stockService, IProductService productService, IBillService billService)
        {

            _salesService = salesService;
            _customerService = customerService;
            _orderService = orderService;
            _priceService = priceService;
            _stockService = stockService;
            _productService = productService;
            _billService = billService;
        }
        #region Sales

        [Authorize(Roles = "Sales_View")]
        public async Task<IActionResult> Index()
        {
            var list = await _salesService.GetAllSalesesAsync();
            return View(list);
        }

        [Authorize(Roles = "Sales_View")]
        public async Task<IActionResult> Active()
        {
            var list = await _salesService.GetAllActiveSalesesAsync();
            return View(list);
        }

        [Authorize(Roles = "Sales_View")]
        public async Task<IActionResult> Passive()
        {
            var list = await _salesService.GetAllPassiveSalesesAsync();
            return View(list);
        }


        [Authorize(Roles = "Sales_Add")]
        public async Task<IActionResult> Add()
        {
            var customer = await _customerService.GetAllCustomersAsync();
            List<SelectListItem> cust = new List<SelectListItem>();

            foreach (var item in customer)
            {
                cust.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Customers = cust;



            return View();
        }

        [Authorize(Roles = "Sales_Add")]
        public async Task<IActionResult> BasicAdd()
        {
            var customer = await _customerService.GetAllCustomersAsync();
            List<SelectListItem> cust = new List<SelectListItem>();

            foreach (var item in customer)
            {
                cust.Add(new SelectListItem { Text = item.Name + " " + item.CustomerNumber, Value = item.Id.ToString() });
            }
            ViewBag.Customers = cust;



            var products = await _productService.GetAllProductsIfHaveStockAsync();


            ViewBag.Products = products;

            return View();
        }

        [Authorize(Roles = "Sales_Add")]
        [HttpPost]
        public async Task<IActionResult> BasicAdd(BasicOrderDto basicOrderDto)
        {
            var customer = await _customerService.GetAllCustomersAsync();
            List<SelectListItem> cust = new List<SelectListItem>();

            foreach (var item in customer)
            {
                cust.Add(new SelectListItem { Text = item.Name + " " + item.CustomerNumber, Value = item.Id.ToString() });
            }
            ViewBag.Customers = cust;



            var products = await _productService.GetAllProductsIfHaveStockAsync();


            ViewBag.Products = products;

            var resp = await _salesService.CreateBasicSalesAsync(basicOrderDto);


            return RedirectToAction("Detail", new { id = resp.Id });


        }

        [Authorize(Roles = "Sales_Add")]
        [HttpPost]
        public async Task<IActionResult> Add(SalesDto salesDto)
        {

            var cr = await _salesService.CreateSalesAsync(salesDto);

            return RedirectToAction("SalesNext", "Sales", new { id = cr.Id });
        }


        [Authorize(Roles = "Sales_Action")]
        public async Task<IActionResult> Action(Guid id)
        {
            var sales = await _salesService.GetSalesByIdWithStepsAsync(id);




            return View(sales);
        }






        [Authorize(Roles = "Sales_Action")]
        [HttpPost]
        public async Task<IActionResult> NextStepSales(SalesDto salesDto)
        {

            var res = await _salesService.SetSalesNextStepAsync(salesDto);
            if (res == -1)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (res == 5)
            {
               await _billService.SendInvoiceAsync(salesDto.Id);
                return RedirectToAction("Index", "Sales");
            }

            return RedirectToAction("Action", "Sales", new { id = salesDto.Id });
        }





        [Authorize(Roles = "Sales_Action")]
        [HttpPost]
        public async Task<IActionResult> BackStepSales(SalesDto salesDto)
        {

            var res = await _salesService.SetSalesBackStepAsync(salesDto);
            if (res == -1)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (res == 0)
            {
                return RedirectToAction("Index", "Sales");
            }

            return RedirectToAction("Action", "Sales", new { id = salesDto.Id });
        }

        [Authorize(Roles = "Sales_Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sales = await _salesService.GetSalesWithIdAsync(id);



            return View(sales);
        }

        [Authorize(Roles = "Sales_Edit")]

        public async Task<IActionResult> Edit(Guid id)
        {


            var sales = await _salesService.GetSalesWithIdAsync(id);


            return View(sales);
        }


        [Authorize(Roles = "Sales_Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(SalesDto salesDto)
        {

            await _salesService.DeleteSalesAsync(salesDto.Id);

            return RedirectToAction("Index", "Sales");
        }
        [Authorize(Roles = "Sales_Next")]
        public async Task<IActionResult> SalesNext(Guid id, string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Stok = message;
            }
            var prds = await _productService.GetAllProductsAsync();
            List<SelectListItem> pr = new List<SelectListItem>();

            foreach (var item in prds)
            {
                pr.Add(new SelectListItem { Text = item.Name + " " + item.Price + " " + item.CurrencyDto.Name, Value = item.Id.ToString() });
            }

            ViewBag.Products = pr;

            var sales = await _salesService.GetSalesWithIdAsync(id);

            return View(sales);
        }

        #endregion

        #region Order

        [Authorize(Roles = "Order_Add")]
        [HttpPost]
        public async Task<ActionResult> AddOrder(OrderDto orderDto)
        {


            var res = await _stockService.CanBeOrderAsync(orderDto.ProductId, orderDto.Piece);
            if (res == false)
            {



                return RedirectToAction("SalesNext", "Sales", new { id = orderDto.SalesId, message = "Girdiğiniz Üründen Yeterli Stok Yok" });


            }

            await _orderService.AddOrderAsync(orderDto);
            return RedirectToAction("SalesNext", "Sales", new { id = orderDto.SalesId });

        }

        [Authorize(Roles = "Order_Delete")]

        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var ao = await _orderService.GetOrderWithIdAsync(id);
            return View(ao);

        }

        [Authorize(Roles = "Order_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteOrder(OrderDto orderDto)
        {
            await _orderService.DeleteOrderAsync(orderDto.Id);
            return RedirectToAction("SalesNext", "Sales", new { id = orderDto.SalesId });

        }
        #endregion

        #region SalesStatus

        [Authorize(Roles = "SalesStatus_Add")]
        public IActionResult AddSalesStatus()
        {

            return View();
        }
        [Authorize(Roles = "SalesStatus_Add")]
        [HttpPost]
        public async Task<IActionResult> AddSalesStatus(SalesStatusDto salesStatusDto)
        {
            await _salesService.AddSalesStatusAsync(salesStatusDto);

            return RedirectToAction("SalesStatus");
        }

        [Authorize(Roles = "SalesStatus_Delete")]
        public async Task<IActionResult> DeleteSalesStatus(Guid id)
        {
            var status = await _salesService.GetSalesStatusWithIdAsync(id);
            return View(status);
        }
        [Authorize(Roles = "SalesStatus_Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteSalesStatus(SalesStatusDto salesStatusDto)
        {
            await _salesService.DeleteSalesStatusAsync(salesStatusDto.Id);

            return RedirectToAction("SalesStatus");
        }
        [Authorize(Roles = "SalesStatus_View")]
        public async Task<IActionResult> SalesStatus()
        {

            var list = await _salesService.GetAllSalesStatusesAsync();

            return View(list);
        }
        [Authorize(Roles = "SalesStatus_View")]
        public async Task<IActionResult> Detail(Guid id)
        {

            var list = await _salesService.GetSalesWithIdAsync(id);

            return View(list);
        }




        #endregion

    }
}
