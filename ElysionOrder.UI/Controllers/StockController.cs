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
    public class StockController : Controller
    {

        readonly IStoreService _storeService;
        readonly IStockService _stockService;
        readonly IProductService _productService;
        readonly ICustomerService _customerService;
        readonly IPaymentService _paymentService;
        readonly IPriceService _priceService;

        public StockController(IStoreService storeService, IStockService stockService, IProductService productService,
            ICustomerService customerService, IPaymentService paymentService, IPriceService priceService)
        {
            _storeService = storeService;
            _stockService = stockService;   
            _productService = productService;
            _customerService= customerService;
            _paymentService = paymentService;
            _priceService = priceService;
        }

        #region Stock

        [Authorize(Roles ="Stock_View")]
        public async Task<IActionResult> Index()
        {
            var l =await _stockService.GetAllStocksAsync();
            return View(l);
        }
        [Authorize(Roles = "Stock_Add")]
        public async Task<ActionResult> AddStock()
        {
            var plist = await _productService.GetAllProductsAsync();

            List<SelectListItem> pls = new List<SelectListItem>();

            foreach (var item in plist)
            {
                pls.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
           
            ViewBag.Products= pls;


            var slist = await _storeService.GetAllStoresAsync();

            List<SelectListItem> sls = new List<SelectListItem>();

            foreach (var item in slist)
            {
                sls.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.Stores = sls;

            return View();
        } 
        
        [Authorize(Roles = "Stock_Add")]
        public async Task<ActionResult> BillDone(Guid id)
        {
           var bi = await  _stockService.GetStokBillWithIdAsync(id);

            return View(bi);
        }
        [Authorize(Roles = "Stock_Add")]
        [HttpPost]
        public async Task<ActionResult> BillDone(BillDto billDto)
        {
            var bt = await _stockService.GetStokBillWithIdAsync(billDto.Id);

            await _stockService.BillToStockAsync(billDto.Id);
           await _paymentService.AddCustomerDebtAsync(new SalesDto
            {

                CustomerId = bt.InvoicerId,
                OrderTotalPrice= bt.Total

           });
            return RedirectToAction("Index", "Stock");
        }

        [Authorize(Roles = "Stock_BillAdd")]
        public async Task<ActionResult> BillAdd()
        {
            var plist = await _customerService.GetAllCustomersAsync();

            List<SelectListItem> pls = new List<SelectListItem>();

            foreach (var item in plist)
            {
                pls.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.Customers = pls;


           

            return View();
        }

        
        [Authorize(Roles = "Stock_BillAdd")]
        [HttpPost]
        public async Task<ActionResult> BillAdd(BillDto billDto)
        {
          billDto.Id= Guid.NewGuid();
          await  _stockService.AddStokBillAsync(billDto);

            return RedirectToAction("BillItems", "Stock", new { id = billDto.Id });

        }

        [Authorize(Roles = "Stock_BillAdd")]
        public async Task<ActionResult> BillItems(Guid id)
        {
           
            var bt =await _stockService.GetStokBillWithIdAsync(id);
            var pricelist = await _productService.GetAllProductsAsync();
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in pricelist)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.ProductList = list;


            var taxlist = await _priceService.GetAllTaxesAsync();
            List<SelectListItem> taxs = new List<SelectListItem>();

            foreach (var item in taxlist)
            {
                taxs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.TaxList = taxs;

            var stlist = await _storeService.GetAllStoresAsync();
            List<SelectListItem> stl = new List<SelectListItem>();

            foreach (var item in stlist)
            {
                stl.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.StoreList = stl;

            return View(bt);
        }

        [Authorize(Roles = "Stock_BillAdd")]
        [HttpPost]
        public async Task<ActionResult> AddBillItem(BillItemDto billItemDto)
        {
          

            await _stockService.AddStokBillItemAsync(billItemDto);
            return RedirectToAction("BillItems", "Stock", new { id = billItemDto.BillId });

        }


        [Authorize(Roles = "Stock_BillDelete")]
        public async Task<ActionResult> DeleteBillItem(Guid id)
        {

           var bi =await _stockService.GetStokBillItemAsync(id);


            return View(bi);
        }
        [Authorize(Roles = "Stock_BillDelete")]
        [HttpPost]
        public async Task<ActionResult> DeleteBillItem(BillItemDto billItemDto)
        {

            await _stockService.DeleteStokBillItemAsync(billItemDto.Id);


            return RedirectToAction("BillItems", "Stock", new { id = billItemDto.BillId });

        }

        [Authorize(Roles = "Stock_BillDelete")]
        public async Task<ActionResult> DeleteBill(Guid id)
        {

            var bi = await _stockService.GetStokBillWithIdAsync(id);


            return View(bi);
        }
        [Authorize(Roles = "Stock_BillDelete")]
        [HttpPost]
        public async Task<ActionResult> DeleteBill(BillDto billDto)
        {

            await _stockService.DeleteStokBillAsync(billDto.Id);


            return RedirectToAction("Index", "Stock");

        }



        [Authorize(Roles = "Stock_Add")]
        [HttpPost]
        public async Task<ActionResult> AddStock(StockDto stockDto)
        {
            await _stockService.AddStockAsync(stockDto);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Stock_Edit")]
        public async Task<ActionResult> EditStock(Guid id)
        {
            var s = await _stockService.GetStockWithIdAsync(id);

            var plist = await _productService.GetAllProductsAsync();

            List<SelectListItem> pls = new List<SelectListItem>();

            foreach (var item in plist)
            {
                pls.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.Products = pls;


            var slist = await _storeService.GetAllStoresAsync();

            List<SelectListItem> sls = new List<SelectListItem>();

            foreach (var item in slist)
            {
                sls.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.Stores = sls;


            return View(s);
        }
        [Authorize(Roles = "Stock_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditStock(StockDto stockDto)
        {
            await _stockService.UpdateStockAsync(stockDto);


            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Stock_Delete")]
        public async Task<ActionResult> DeleteStock(Guid id)
        {
            var type = await _stockService.GetStockWithIdAsync(id);


            return View(type);
        }
        [Authorize(Roles = "Stock_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteStock(StockDto stockDto)
        {
            await _stockService.DeleteStockAsync(stockDto.Id);

            return RedirectToAction("Index");
        }




        #endregion


        #region Store

        [Authorize(Roles ="StoreType_View")]
        public async Task<IActionResult> StoreTypes()
        {
            var list = await _storeService.GetAllStoreTypesAsync();

            return View(list);
        }
        [Authorize(Roles = "StoreType_Add")]
        public ActionResult AddStoreType()
        {


            return View();
        }
        [Authorize(Roles = "StoreType_Add")]
        [HttpPost]
        public async Task<ActionResult> AddStoreType(StoreTypeDto storeTypeDto)
        {
            await _storeService.AddStoreTypeAsync(storeTypeDto);
            return RedirectToAction("StoreTypes");
        }
        [Authorize(Roles = "StoreType_Edit")]
        public async Task<ActionResult> EditStoreType(Guid id)
        {
            var s = await _storeService.GetStoreTypeWithIdAsync(id);



            return View(s);
        }
        [Authorize(Roles = "StoreType_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditStoreType(StoreTypeDto storeTypeDto)
        {
            await _storeService.UpdateStoreTypeAsync(storeTypeDto);


            return RedirectToAction("StoreTypes");
        }
        [Authorize(Roles = "StoreType_Delete")]
        public async Task<ActionResult> DeleteStoreType(Guid id)
        {
            var type = await _storeService.GetStoreTypeWithIdAsync(id);


            return View(type);
        }
        [Authorize(Roles = "StoreType_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteStoreType(StoreTypeDto storeTypeDto)
        {
            await _storeService.DeleteStoreTypeAsync(storeTypeDto.Id);

            return RedirectToAction("StoreTypes");
        }




        [Authorize(Roles ="Store_View")]
        public async Task<IActionResult> Stores()
        {
            var list = await _storeService.GetAllStoresAsync();

            return View(list);
        }

        [Authorize(Roles = "Store_Add")]
        public async Task<ActionResult> AddStore()
        {
            var l = await _storeService.GetAllStoreTypesAsync();


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in l)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.StoreTypes = list;

            return View();
        }
        [Authorize(Roles = "Store_Add")]
        [HttpPost]
        public async Task<ActionResult> AddStore(StoreDto storeDto)
        {
            await _storeService.AddStoreAsync(storeDto);
            return RedirectToAction("Stores");
        }
        [Authorize(Roles = "Store_Edit")]
        public async Task<ActionResult> EditStore(Guid id)
        {
            var s = await _storeService.GetStoreWithIdAsync(id);
            var l = await _storeService.GetAllStoreTypesAsync();


            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in l)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.StoreTypes = list;


            return View(s);
        }

        [Authorize(Roles = "Store_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditStore(StoreDto storeDto)
        {
            await _storeService.UpdateStoreAsync(storeDto);


            return RedirectToAction("Stores");
        }
        [Authorize(Roles = "Store_Delete")]
        public async Task<ActionResult> DeleteStore(Guid id)
        {
            var type = await _storeService.GetStoreWithIdAsync(id);


            return View(type);
        }
        [Authorize(Roles = "Store_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteStore(StoreDto storeDto)
        {
            await _storeService.DeleteStoreAsync(storeDto.Id);

            return RedirectToAction("Stores");
        }

        #endregion
    }
}
