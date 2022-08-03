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


    public IEnumerable<OrderProductTagDto> Get()
    {

        // var prod = appDbContext.Orders.OrderByDescending(x=>x.Id).First();
        var prod = appDbContext.Orders.OrderByDescending(x => x.Id).First();
        var id = prod.Id;
        var productOrders = appDbContext.Orders.Include(x => x.OrderProducts).FirstOrDefault(x => x.Id == id);
        var images = appDbContext.Products.Include(x => x.Tags).Where(x => productOrders.OrderProducts.Select(y => y.Name.ToLower()).Contains(x.Name.ToLower())).ToList();
        var bb = new List<OrderProductDto>();
        productOrders.OrderProducts.ForEach(x =>
        {
            var o = new OrderProductDto();
            o.Name = x.Name;
            o.ImageUrl = images?.FirstOrDefault(y => y.Name == x.Name).ImageUrl;
            o.Price = x.Price;
            o.Count = x.Count;
            o.TagName = images?.FirstOrDefault(y => y.Name == x.Name).Tags?.First()?.Name;

            bb.Add(o);
        });

        var c = bb.GroupBy(x => x.TagName)?.Select(x => new OrderProductTagDto()
        {
            Tag = x.Key,
            Products = x.Select(y => new OrderProductDto()
            {

                Name = y.Name,
                ImageUrl = images?.FirstOrDefault(z => z.Name == y.Name)?.ImageUrl,
                Price = y.Price,
                Count = y.Count
            }).ToList()
        }).ToList();
        /*    var bb = productOrders.OrderProducts.Select(x =>
          new OrderProductDto()
          {
              Name = x.Name,
              ImageUrl = images?.FirstOrDefault(y => y.Name == x.Name)?.ImageUrl,
              Price = x.Price,
              Count = x.Count,
              TagName = images?.FirstOrDefault(y => y.Name == x.Name).Tags?.First().Name
          })
               .GroupBy(x => x.TagName)?.Select(x => new OrderProductTagDto()
               {
                   Tag = x.Key,
                   Products = x.Select(y => new OrderProductDto()
                   {

                       Name = y.Name,
                       ImageUrl = images?.FirstOrDefault(z => z.Name == y.Name)?.ImageUrl,
                       Price = y.Price,
                       Count = y.Count
                   }).ToList()
               }).ToList(); */
        //var a = productOrders.OrderProducts.Select(x=>new OrderProductDto(){Name = x.Name, ImageUrl = images.FirstOrDefault(y=>y.Name == x.Name).ImageUrl, Price = x.Price}).ToList();
        return c;
    }
    public IEnumerable<OrderProductTagDto> Get(int id)
    {

        // var prod = appDbContext.Orders.OrderByDescending(x=>x.Id).First();
        var prod = appDbContext.Orders.FirstOrDefault(x => x.Id == id);
        //var prod = appDbContext.Orders.OrderByDescending(x=>x.Id).First();

        id = prod.Id;
        var productOrders = appDbContext.Orders.Include(x => x.OrderProducts).FirstOrDefault(x => x.Id == id);
        var images = appDbContext.Products.Include(x => x.Tags).Where(x => productOrders.OrderProducts.Select(y => y.Name.ToLower()).Contains(x.Name.ToLower())).ToList();
        var bb = new List<OrderProductDto>();
        productOrders.OrderProducts.ForEach(x =>
        {
            var o = new OrderProductDto();
            o.Name = x.Name;
            o.ImageUrl = images?.FirstOrDefault(y => y.Name == x.Name)?.ImageUrl;
            o.Price = x.Price;
            o.Count = x.Count;
            o.TagName = images?.FirstOrDefault(y => y.Name == x.Name).Tags?.First()?.Name;

            bb.Add(o);
        });

        var c = bb.GroupBy(x => x.TagName)?.Select(x => new OrderProductTagDto()
        {
            Tag = x.Key,
            
            Products = x.Select(y => new OrderProductDto()
            {

                Name = y.Name,
                ImageUrl = images?.FirstOrDefault(z => z.Name == y.Name)?.ImageUrl,
                Price = y.Price,
                Count = y.Count,
            
            }).ToList()
        }).ToList();
        /*    var bb = productOrders.OrderProducts.Select(x =>
          new OrderProductDto()
          {
              Name = x.Name,
              ImageUrl = images?.FirstOrDefault(y => y.Name == x.Name)?.ImageUrl,
              Price = x.Price,
              Count = x.Count,
              TagName = images?.FirstOrDefault(y => y.Name == x.Name).Tags?.First().Name
          })
               .GroupBy(x => x.TagName)?.Select(x => new OrderProductTagDto()
               {
                   Tag = x.Key,
                   Products = x.Select(y => new OrderProductDto()
                   {

                       Name = y.Name,
                       ImageUrl = images?.FirstOrDefault(z => z.Name == y.Name)?.ImageUrl,
                       Price = y.Price,
                       Count = y.Count
                   }).ToList()
               }).ToList(); */
        //var a = productOrders.OrderProducts.Select(x=>new OrderProductDto(){Name = x.Name, ImageUrl = images.FirstOrDefault(y=>y.Name == x.Name).ImageUrl, Price = x.Price}).ToList();
        return c;
    }

    public StatusDto GetStatus(int id)
    {
       var res = appDbContext.StatusOrder
        .Include(x=>x.Status)
        .ThenInclude(x=>x.Next)
       .Include(x=>x.Status)
       .Include(x=>x.Order)
      
       .Where(x=>x.Order.Id == id)
       .OrderByDescending(x=>x.Id)
       .FirstOrDefault();
       ;


       var statusDto = new StatusDto{
           Id = res.Status.Id,
           Name = res.Status.Name,
           NextId = res.Status.Next.Id
       };

       return statusDto;
    }

    public StatusDto SetStatus(int id)
    {
          var res = appDbContext.StatusOrder
        .Include(x=>x.Status)
        .ThenInclude(x=>x.Next)
        .ThenInclude(x=>x.Next)
       .Include(x=>x.Order)
      
       .Where(x=>x.Order.Id == id)
       .OrderByDescending(x=>x.Id)
       .FirstOrDefault();
       ;

        var status = new StatusOrder(){
            Order = res.Order,
            Status = res.Status.Next,
            CreateDate = DateTime.Now
        };

        appDbContext.StatusOrder.Add(status);
        appDbContext.SaveChanges();

        return new StatusDto{
            Name = status.Status.Name,
            NextId = status.Status.Next != null ?  status.Status.Next.Id: -1,
            Id = status.Status.Id
        };
    }
}