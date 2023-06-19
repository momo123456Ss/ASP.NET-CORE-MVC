using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace QLBHHuynhHiepThien.Models
{
    public class HomeModel
    {
        public static NorthwindDataContext dbContext = new NorthwindDataContext();

        public static List<Category> ListCategory = dbContext.Categories.ToList();
        public static List<Product> ListProduct = dbContext.Products.ToList();

    }
}