using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Result {
    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
    public string Check {get;set;}
    
    public string Comment {get;set;}
    
        public int OrderId {get;set;}
    public int Payment {get;set;}

    public decimal RealPrice {get;set;}
    public decimal StorePrice {get;set;}

public DateTime Date {get;set;}

}