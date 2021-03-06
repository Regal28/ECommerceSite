using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.DB
{
    public class OrdersDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
