using System.Collections.Generic;

public class OrderProductTagDto{
public string Tag {get;set;}
public List<OrderProductDto> Products{get;set;}
}

public class OrderProductDto {
    public int ProductId {get;set;}
    public int Count {get;set;}

    public string Name {get;set;}

     public decimal Price {get;set;}
     public decimal? NewPrice {get;set;}
       public string ImageUrl {get;set;}
       public string TagName {get;set;}
}