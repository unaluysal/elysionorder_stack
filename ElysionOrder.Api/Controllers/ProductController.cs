using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Application.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace ElysionOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {


            var list = await _productService.GetAllProductsAsync();


            return Ok(list);

        }

        [HttpGet("GetAllProductTypes")]
        public async Task<ActionResult> GetAllProductTypes()
        {


            var list = await _productService.GetAllProductTypesAsync();


            return Ok(list);

        }

        [HttpGet("GetAllSubProductTypes")]
        public async Task<ActionResult> GetAllSubProductTypes()
        {


            var list = await _productService.GetAllSubProductTypesAsync();


            return Ok(list);

        }

        [HttpPost("GetProductById")]
        public async Task<ActionResult> GetProductById(Guid guid)
        {


            var u = await _productService.GetProductWithIdAsync(guid);


            return Ok(u);

        }

        [HttpPost("GetProductTypeById")]
        public async Task<ActionResult> GetProductTypeById(Guid guid)
        {


            var u = await _productService.GetProductTypeWithIdAsync(guid);


            return Ok(u);

        }


        [HttpPost("GetProductTypeByTypeId")]
        public async Task<ActionResult> GetProductTypeByTypeId(Guid guid)
        {


            var u = await _productService.GetProductWithTypeIdAsync(guid);


            return Ok(u);

        }


        [HttpPost("GetSubProductTypeById")]
        public async Task<ActionResult> GetSubProductTypeById(Guid guid)
        {


            var u = await _productService.GetSubProductTypeWithIdAsync(guid);


            return Ok(u);

        }

        [HttpPost("GetSubProductTypeByTypeId")]
        public async Task<ActionResult> GetSubProductTypeByTypeId(Guid guid)
        {


            var u = await _productService.GetSubProductTypeWithTypeIdAsync(guid);


            return Ok(u);

        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct(ProductDto productDto)
        {


           await _productService.AddProductAsync(productDto);


            return Ok();

        }

        [HttpPost("AddProductType")]
        public async Task<ActionResult> AddProductType(ProductTypeDto productTypeDto)
        {


            await _productService.AddProductTypeAsync(productTypeDto);


            return Ok();

        }

        [HttpPost("AddSubProductType")]
        public async Task<ActionResult> AddSubProductType(SubProductTypeDto productTypeDto)
        {


            await _productService.AddSubProductTypeAsync(productTypeDto);


            return Ok();

        }

    }
}
