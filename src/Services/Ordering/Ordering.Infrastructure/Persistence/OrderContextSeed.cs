using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }

        public static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "gabi", FirstName = "Gabi", LastName = "Lapadat", EmailAddress = "lapadatrobert123@gmail.com", AddressLine = "Craiova", Country = "Romania", TotalPrice = 350 }
            };
        }
    }
}
