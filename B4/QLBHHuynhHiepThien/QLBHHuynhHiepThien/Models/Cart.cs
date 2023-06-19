using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBHHuynhHiepThien.Models
{
    public class Cart
    {
        NorthwindDataContext dbContext = new NorthwindDataContext();
        public int ProductID { get; set; }
        public string ProductName { get; set;}
        public decimal? UnitPrice { get; set;}
        private int quanity;
        public decimal? Total { get { return UnitPrice*Quanity; } }

        public int Quanity { get => quanity; set => quanity = value; }

        public Cart(int productID)
        {
            this.ProductID = productID;
            Product p = dbContext.Products.Single(n => n.ProductID == productID);
            ProductName = p.ProductName;
            UnitPrice = p.UnitPrice;
            Quanity = 1;
        }
        
    }
}