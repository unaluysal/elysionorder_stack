using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElysionOrder.UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        readonly IProductService _productService;
        readonly IPriceService _priceService;
        readonly IStoreService _storeService;

       public ProductController(IProductService productService, IPriceService priceService, IStoreService storeService)
        {

            _productService = productService;
            _priceService = priceService;
            _storeService = storeService;
        }

        #region Product

        [Authorize(Roles = "Product_View")]
        public async Task<IActionResult> Index()
        {
            var list = await _productService.GetAllProductsAsync();

            return View(list);
        }

        [Authorize(Roles = "Product_Add")]
        public async Task<ActionResult> Add()
        {
            var list = await _productService.GetAllBrandsAsync();
            List<SelectListItem> brands = new List<SelectListItem>();

            foreach (var item in list)
            {
                brands.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Brands = brands;


            var blist = await _productService.GetAllSubProductTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in blist)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;


            var clist = await _priceService.GetAllCurrenciesAsync();
            List<SelectListItem> clists = new List<SelectListItem>();

            foreach (var item in clist)
            {
                clists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Currencys = clists;


            var tax = await _priceService.GetAllTaxesAsync();
            List<SelectListItem> taxs = new List<SelectListItem>();

            foreach (var item in tax)
            {
                taxs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Taxs = taxs;

            return View();
        }

        [Authorize(Roles = "Product_Add")]
        [HttpPost]
        public async Task<ActionResult> Add(ProductDto productDto)
        {
            await _productService.AddProductAsync(productDto);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Product_Edit")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var list = await _productService.GetAllBrandsAsync();
            List<SelectListItem> brands = new List<SelectListItem>();

            foreach (var item in list)
            {
                brands.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Brands = brands;


            var blist = await _productService.GetAllSubProductTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in blist)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;



            var clist = await _priceService.GetAllCurrenciesAsync();
            List<SelectListItem> clists = new List<SelectListItem>();

            foreach (var item in clist)
            {
                clists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Currencys = clists;


            var tax = await _priceService.GetAllTaxesAsync();
            List<SelectListItem> taxs = new List<SelectListItem>();

            foreach (var item in tax)
            {
                taxs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Taxs = taxs;

            var t = await _productService.GetProductWithIdAsync(id);

            return View(t);
        }

        [Authorize(Roles = "Product_Edit")]
        [HttpPost]
        public async Task<ActionResult> Edit(ProductDto productDto)
        {
            await _productService.UpdateProductAsync(productDto);


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Product_Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var t = await _productService.GetProductWithIdAsync(id);


            return View(t);
        }

        [Authorize(Roles = "Product_Delete")]
        [HttpPost]
        public async Task<ActionResult> Delete(ProductDto productDto)
        {
            await _productService.DeleteProductAsync(productDto.Id);

           

            return RedirectToAction("Index");
        }



        #endregion


        #region Type

        [Authorize(Roles = "ProductType_Add")]
        public ActionResult AddType()
        {


            return View();
        }

        [Authorize(Roles = "ProductType_Add")]
        [HttpPost]
        public async Task<ActionResult> AddType(ProductTypeDto productTypeDto)
        {
            await _productService.AddProductTypeAsync(productTypeDto);

            return RedirectToAction("ProductType");
        }

        [Authorize(Roles = "ProductType_Edit")]
        public async Task<ActionResult> EditType(Guid id)
        {
            var t =await _productService.GetProductTypeWithIdAsync(id);

            return View(t);
        }

        [Authorize(Roles = "ProductType_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditType(ProductTypeDto productTypeDto)
        {
            await _productService.UpdateProductTypeAsync(productTypeDto);


            return RedirectToAction("ProductType");
        }
        [Authorize(Roles = "ProductType_Delete")]
        public async Task<ActionResult> DeleteType(Guid id)
        {
            var type = await _productService.GetProductTypeWithIdAsync(id);


            return View(type);
        }

        [Authorize(Roles = "ProductType_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteType(ProductTypeDto productTypeDto)
        {
            await _productService.DeleteProductTypeAsync(productTypeDto.Id);


            return RedirectToAction("ProductType");
        }

        [Authorize(Roles = "ProductType_View")]
        public async Task<IActionResult> ProductType()
        {
            var list = await _productService.GetAllProductTypesAsync();
            return View(list);
        }


        #endregion


        #region SubType

        [Authorize(Roles = "ProductSubType_View")]
        public async Task<IActionResult> SubProductType()
        {
            var list = await _productService.GetAllSubProductTypesAsync();
            return View(list);
        }

        [Authorize(Roles = "ProductSubType_Add")]
        public async Task<ActionResult> AddSubType()
        {
            var list =await _productService.GetAllProductTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;
            return View();
        }
        [Authorize(Roles = "ProductSubType_Add")]
        [HttpPost]
        public async Task<ActionResult> AddSubType(SubProductTypeDto subproductTypeDto)
        {
            await _productService.AddSubProductTypeAsync(subproductTypeDto);

            return RedirectToAction("SubProductType");
        }

        [Authorize(Roles = "ProductSubType_Edit")]
        public async Task<ActionResult> EditSubType(Guid id)
        {
            var list = await _productService.GetAllProductTypesAsync();
            List<SelectListItem> types = new List<SelectListItem>();

            foreach (var item in list)
            {
                types.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Types = types;
            var t = await _productService.GetSubProductTypeWithIdAsync(id);

            return View(t);
        }

        [Authorize(Roles = "ProductSubType_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditSubType(SubProductTypeDto subproductTypeDto)
        {
            await _productService.UpdateSubProductTypeAsync(subproductTypeDto);


            return RedirectToAction("SubProductType");
        }

        [Authorize(Roles = "ProductSubType_Delete")]
        public async Task<ActionResult> DeleteSubType(Guid id)
        {
            var type = await _productService.GetSubProductTypeWithIdAsync(id);


            return View(type);
        }

        [Authorize(Roles = "ProductSubType_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteSubType(SubProductTypeDto subproductTypeDto)
        {
            await _productService.DeleteSubProductTypeAsync(subproductTypeDto.Id);


            return RedirectToAction("SubProductType");
        }

        #endregion


       

        #region Currency

        [Authorize(Roles = "Currency_Add")]
        public ActionResult AddCurrency()
        {


            return View();
        }
        [Authorize(Roles = "Currency_Add")]
        [HttpPost]
        public async Task<ActionResult> AddCurrency(CurrencyDto currencyDto)
        {
            await _priceService.AddCurrencyAsync(currencyDto);

            return RedirectToAction("Currency");
        }

        [Authorize(Roles = "Currency_Edit")]
        public async Task<ActionResult> EditCurrency(Guid id)
        {
            var t = await _priceService.GetCurrencyWithIdAsync(id);

            return View(t);
        }

        [Authorize(Roles = "Currency_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditCurrency(CurrencyDto currencyDto)
        {
            await _priceService.UpdateCurrencyAsync(currencyDto);


            return RedirectToAction("Currency");
        }

        [Authorize(Roles = "Currency_Delete")]
        public async Task<ActionResult> DeleteCurrency(Guid id)
        {
            var c = await _priceService.GetCurrencyWithIdAsync(id);


            return View(c);
        }
        [Authorize(Roles = "Currency_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteCurrency(ProductTypeDto productTypeDto)
        {
            await _priceService.DeleteCurrencyAsync(productTypeDto.Id);


            return RedirectToAction("Currency");
        }
        [Authorize(Roles = "Currency_View")]
        public async Task<IActionResult> Currency()
        {
            var list = await _priceService.GetAllCurrenciesAsync();
            return View(list);
        }


        #endregion

        #region Tax


        [Authorize(Roles = "Tax_Add")]
        public ActionResult AddTax()
        {


            return View();
        }
        [Authorize(Roles = "Tax_Add")]
        [HttpPost]
        public async Task<ActionResult> AddTax(TaxDto taxDto)
        {
            await _priceService.AddTaxAsync(taxDto);

            return RedirectToAction("Tax");
        }
        [Authorize(Roles = "Tax_Edit")]
        public async Task<ActionResult> EditTax(Guid id)
        {
            var t = await _priceService.GetTaxWithIdAsync(id);

            return View(t);
        }
        [Authorize(Roles = "Tax_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditTax(TaxDto taxDto)
        {
            await _priceService.UpdateTaxAsync(taxDto);


            return RedirectToAction("Tax");
        }

        [Authorize(Roles = "Tax_Delete")]
        public async Task<ActionResult> DeleteTax(Guid id)
        {
            var t = await _priceService.GetTaxWithIdAsync(id);


            return View(t);
        }
        [Authorize(Roles = "Tax_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteTax(TaxDto taxDto)
        {
            await _priceService.DeleteTaxAsync(taxDto.Id);


            return RedirectToAction("Tax");
        }

        [Authorize(Roles = "Tax_View")]
        public async Task<IActionResult> Tax()
        {
            var list = await _priceService.GetAllTaxesAsync();
            return View(list);
        }

        #endregion


        #region Brand
        [Authorize(Roles = "Brand_View")]
        public async Task<IActionResult> Brand()
        {
            var list = await _productService.GetAllBrandsAsync();

            return View(list);
        }

        [Authorize(Roles = "Brand_Add")]
        public  ActionResult AddBrand()
        {
            
            return View();
        }
        [Authorize(Roles = "Brand_Add")]
        [HttpPost]
        public async Task<ActionResult> AddBrand(BrandDto brandDto)
        {
            await _productService.AddBrandAsync(brandDto);

            return RedirectToAction("Brand");
        }

        [Authorize(Roles = "Brand_Edit")]
        public async Task<ActionResult> EditBrand(Guid id)
        {
            var t = await _productService.GetBrandWithIdAsync(id);

            return View(t);
        }
        [Authorize(Roles = "Brand_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditBrand(BrandDto brandDto)
        {
            await _productService.UpdateBrandAsync(brandDto);


            return RedirectToAction("Brand");
        }
        [Authorize(Roles = "Brand_Delete")]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            var t = await _productService.GetBrandWithIdAsync(id);


            return View(t);
        }
        [Authorize(Roles = "Brand_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteBrand(BrandDto brandDto)
        {
            await _productService.DeleteBrandAsync(brandDto.Id);


            return RedirectToAction("Brand");
        }

        #endregion


        #region Iban


        [Authorize(Roles = "Iban_View")]
        public async Task<IActionResult> Iban()
        {
            var list =await _priceService.GetAllIbansAsync();

            return View(list);
        }

        [Authorize(Roles = "Iban_Add")]
        public async Task<ActionResult> AddIban()
        {
            var clist = await _priceService.GetAllCurrenciesAsync();

            List<SelectListItem> curs = new List<SelectListItem>();

            foreach (var item in clist)
            {
                curs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Currencies = curs;

            return View();
        }
        [Authorize(Roles = "Iban_Add")]
        [HttpPost]
        public async Task<ActionResult> AddIban(IbanDto ibanDto)
        {
            await _priceService.AddIbanAsync(ibanDto);
            return RedirectToAction("Iban");
        }


        [Authorize(Roles = "Iban_Edit")]
        public async Task<ActionResult> EditIban(Guid id)
        {
            var clist = await _priceService.GetAllCurrenciesAsync();

            List<SelectListItem> curs = new List<SelectListItem>();

            foreach (var item in clist)
            {
                curs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Currencies = curs;
            var t = await _priceService.GetIbanWithIdAsync(id);

            return View(t);
        }
        [Authorize(Roles = "Iban_Edit")]
        [HttpPost]
        public async Task<ActionResult> EditIban(IbanDto ibanDto)
        {
            await _priceService.UpdateIbanAsync(ibanDto);


            return RedirectToAction("Iban");
        }
        [Authorize(Roles = "Iban_Delete")]
        public async Task<ActionResult> DeleteIban(Guid id)
        {
            var type = await _priceService.GetIbanWithIdAsync(id);


            return View(type);
        }
        [Authorize(Roles = "Iban_Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteIban(IbanDto ibanDto)
        {
            await _priceService.DeleteIbanAsync(ibanDto.Id);

            return RedirectToAction("Iban");
        }
        #endregion


      
    }
}
