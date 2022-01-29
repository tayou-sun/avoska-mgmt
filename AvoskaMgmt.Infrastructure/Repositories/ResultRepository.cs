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

    public void Create(ResultSaveDto res)
    {
       
        var result = new Result(){
           
    OrderId = res.OrderId,
      Payment = res.Payment,

      RealPrice = res.RealPrice,
      Date =  DateTime.Parse(res.Date),
      Comment = res.Comment
       
        };

        appDbContext.Results.Add( result  );
        appDbContext.SaveChanges();


    }

    public void CreateFromCheck(ResultCheckDto res)
    {
      //  res.Data = "t=20211225T2232&s=2337.59&fn=9960440301200340&i=9794&fp=2189871824&n=1";
        
        int pFrom = res.Data.IndexOf("s=") + "s=".Length;
        int pTo = res.Data.LastIndexOf("&fn=");
         
        var sum = res.Data.Substring(pFrom, pTo - pFrom);

        pFrom = res.Data.IndexOf("t=") + "t=".Length;
        pTo = res.Data.LastIndexOf("&s");
        var date = res.Data.Substring(pFrom, pTo - pFrom);

var year = int.Parse(date.Substring(0,4));
var month = int.Parse(date.Substring(4,2));
var day = int.Parse(date.Substring(6,2));

        var s = new DateTime(year, month, day);
        var payment = Decimal.ToInt32(decimal.Parse(sum));

       var resultObj = new Result(){
           
    OrderId = res.OrderId,
     

      RealPrice =payment,
      Date =  s

       
        };

        appDbContext.Results.Add( resultObj  );
        appDbContext.SaveChanges();
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
            Date = x.Date,
            Comment = x.Comment 
            
        });

        var s = dto.ToList();
        return dto.ToList();
    }
}