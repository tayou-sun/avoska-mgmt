using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class OrderRepository : IOrderRepository
{
    private AppDbContext appDbContext;
    public OrderRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public void Create(OrderDto order)
    {
/* 
        var t = new List<OrderProduct>();

        foreach (var product in order.Products)
        {
            var a1 = new OrderProduct()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count
            };

            t.Add(a1);
            appDbContext.OrderProducts.Add(a1);

        }
        appDbContext.SaveChanges();

        var orderToSave = new Order()
        {
            // public List<OrderProduct> Products{get;set;}
            Products = t,
            Address = order.Address,
            Name = order.Name,
            Phone = order.Phone,

            Comment = order.Comment,

            CreateDate = order.CreateDate
        };


        var a = appDbContext.Orders.ToList();
        appDbContext.Orders.Add(orderToSave);
        appDbContext.SaveChanges(); */
    }

    public  IEnumerable<OrderProductDto> Get(int id)
    {

        var prod = appDbContext.Orders.OrderByDescending(x=>x.Id).First();
        id = prod.Id;
        var productOrders = appDbContext.Orders.Include(x=>x.OrderProducts).FirstOrDefault(x=>x.Id == id);
        var images = appDbContext.Products.Where(x=>productOrders.OrderProducts.Select(y=>y.Name).Contains(x.Name)).ToList();

//var a = productOrders.OrderProducts.Select(x=>new OrderProductDto(){Name = x.Name, ImageUrl = images.FirstOrDefault(y=>y.Name == x.Name).ImageUrl, Price = x.Price}).ToList();
        return productOrders.OrderProducts.Select(x=>new OrderProductDto(){Name = x.Name, ImageUrl = images.FirstOrDefault(y=>y.Name == x.Name).ImageUrl, Price = x.Price, Count = x.Count});
    }
}