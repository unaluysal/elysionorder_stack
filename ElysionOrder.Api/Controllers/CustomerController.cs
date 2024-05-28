using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ElysionOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }


        [HttpGet("GetAllCustomerTypes")]
        public async Task<ActionResult> GetAllCustomerTypes()
        {


            var list = await _customerService.GetAllCustomerTypesAsync();


            return Ok(list);

        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {


            var list = await _customerService.GetAllCustomersAsync();


            return Ok(list);

        }


        [HttpPost("GetCustomerTypeById")]
        public async Task<ActionResult> GetCustomerTypeById(Guid guid)
        {


            var u = await _customerService.GetCustomerTypeWithIdAsync(guid);


            return Ok(u);

        }

        [HttpPost("GetCustomerById")]
        public async Task<ActionResult> GetCustomerById(Guid guid)
        {


            var u = await _customerService.GetCustomerWithIdAsync(guid);


            return Ok(u);

        }


        [HttpPost("AddCustomerType")]
        public async Task<ActionResult> AddCustomerType(CustomerTypeDto CustomerTypeDto)
        {


            await _customerService.AddCustomerTypeAsync(CustomerTypeDto);


            return Ok();

        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDto CustomerDto)
        {


            await _customerService.AddCustomerAsync(CustomerDto);


            return Ok();

        }

        [HttpPost("UpdateCustomerType")]
        public async Task<ActionResult> UpdateCustomerType(CustomerTypeDto CustomerTypeDto)
        {


            await _customerService.UpdateCustomerTypeAsync(CustomerTypeDto);


            return Ok();

        }

        [HttpPost("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer(CustomerDto CustomerDto)
        {


            await _customerService.UpdateCustomerAsync(CustomerDto);


            return Ok();

        }


        [HttpPost("DeleteCustomerType")]
        public async Task<ActionResult> DeleteCustomerType(Guid guid)
        {


            await _customerService.DeleteCustomerTypeAsync(guid);


            return Ok();

        }

        [HttpPost("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomer(Guid guid)
        {


            await _customerService.DeleteCustomerAsync(guid);


            return Ok();

        }

    }
}
