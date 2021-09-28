using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProviders : IProductsProvider
    {
        private readonly ProductsDbContext context;
        private readonly ILogger<ProductsProviders> logger;
        private readonly IMapper mapper;

        public ProductsProviders(ProductsDbContext context,ILogger<ProductsProviders> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!context.Products.Any())
            {
                context.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                context.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 10, Inventory = 100 });
                context.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 200, Inventory = 100 });
                context.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 250, Inventory = 100 });
                context.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await context.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result=mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x=>x.Id==id);
                if (product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
