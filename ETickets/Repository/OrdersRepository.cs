/*using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETickets.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext context;

        public OrdersRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Order> GetOrdersByUserIdAndRole(string userId, string userRole)
        {
            var orders = context.Orders
                .Include(n => n.OrderItems)
                .ThenInclude(n => n.Movie)
                .Include(n => n.User)
                .ToList();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            context.Orders.Add(order);
            context.SaveChanges();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Amount,
                    MovieId = item.Movie.Id,
                    Id = order.Id,
                    Price = item.Movie.Price
                };
                context.OrderItems.Add(orderItem);
            }
            context.SaveChanges();
        }
    }
}

    

*/