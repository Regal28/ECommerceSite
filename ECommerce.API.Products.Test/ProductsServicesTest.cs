using System;
using System.Linq;
using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Profiles;
using ECommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerce.API.Products.Test
{
    public class ProductsServicesTest
    {
        [Fact]
        public async void GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(config);
            var productsProvider = new ProductsProviders(dbContext,null,mapper);
            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.products.Any());
            Assert.Null(product.ErrorMessage);
        }
        [Fact]
        public async void GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(config);
            var productsProvider = new ProductsProviders(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(10);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.Null(product.ErrorMessage);
        }
        [Fact]
        public async void GetProductReturnsProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(config);
            var productsProvider = new ProductsProviders(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }
        private void CreateProducts(ProductsDbContext productsDbContext)
        {
            for(int i = 10; i <= 20; i++)
            {
                productsDbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
                productsDbContext.SaveChanges();
            }
        }
    }
}
