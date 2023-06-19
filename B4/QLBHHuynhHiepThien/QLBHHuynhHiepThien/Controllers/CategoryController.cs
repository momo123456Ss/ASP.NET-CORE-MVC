using QLBHHuynhHiepThien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHHuynhHiepThien.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            Product p = HomeModel.ListProduct.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        public ActionResult ProductCategory(int Id)
        {
            ViewBag.ListProductCategory_SortByCategoryID = Id;
            return View();
        }
    }
}