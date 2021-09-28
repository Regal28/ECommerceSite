using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customers, string ErrorMessage)> GetCustomersAsync(int id);
    }
}
