using System;
using System.Collections.Generic;

public class Order{

    public Order() {
        OrderProducts = new List<OrderProduct>();
    }
    public int Id {get;set;}
    public List<OrderProduct> OrderProducts{get;set;}

    public string Address {get;set;}
    public string Name {get;set;}
    public string Phone {get;set;}

    public string Comment {get;set;}

    public DateTime CreateDate {get;set;}

    public List<StatusOrder> StatusOrders {get;set;}
}