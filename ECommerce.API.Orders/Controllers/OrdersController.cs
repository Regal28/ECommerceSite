using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider CustomersProvider;

        public OrdersController(IOrdersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersAsync(int id)
        {
            var result = await CustomersProvider.GetOrdersAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
