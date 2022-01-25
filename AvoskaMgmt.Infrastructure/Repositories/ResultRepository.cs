using CStuffControl.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class ResultRepository : IResultRepository
{
    private AppDbContext appDbContext;
    public ResultRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IEnumerable<ResultDto> Get()
    {
        var res = appDbContext.Results.ToList();
         var orders = appDbContext.Orders.Include(x=>x.OrderProducts).ToList();

        var dto = res.Select(x=>new ResultDto(){
            Id = x.Id,
            Check = x.Check,
            Payment = x.Payment,
            Price = orders.FirstOrDefault(y=>y.Id == x.OrderId).OrderProducts.Sum(x=>x.Price * x.Count),
            RealPrice = x.RealPrice,
            StorePrice = x.StorePrice,
            Order = orders.FirstOrDefault(y=>y.Id == x.OrderId),
            Date = x.Date
            
        });

        var s = dto.ToList();
        return dto.ToList();
    }
}