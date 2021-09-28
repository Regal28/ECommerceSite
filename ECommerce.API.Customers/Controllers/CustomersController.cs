using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider CustomersProvider;

        public CustomersController(ICustomersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await CustomersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.customer);
            }
            return NotFound();
        }
    }
}
