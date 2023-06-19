using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBHHuynhHiepThien.Models;
namespace QLBHHuynhHiepThien.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        NorthwindDataContext dbContext = new NorthwindDataContext();
        HomeModel objModel = new HomeModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProducts()
        {
            
            List<Product> products = dbContext.Products.Select(s => s).ToList();
            return View(products);
        }
        public ActionResult Details(int id)
        {
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        public ActionResult Create()
        {
            ViewData["NCC"] = new SelectList(dbContext.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(dbContext.Categories, "CategoryID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p, FormCollection collection)
        {
            try
            {
                p.CategoryID = int.Parse(collection["LSP"]);
                p.SupplierID = int.Parse(collection["NCC"]);
                dbContext.Products.InsertOnSubmit(p);
                dbContext.SubmitChanges();
                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            ViewData["NCC"] = new SelectList(dbContext.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(dbContext.Categories, "CategoryID", "CategoryName");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                var p = dbContext.Products.Where(s => s.ProductID == id).FirstOrDefault();

                p.ProductName = form["ProductName"];
                p.SupplierID = int.Parse(form["NCC"]);
                p.CategoryID = int.Parse(form["LSP"]);
                p.QuantityPerUnit = form["QuantityPerUnit"];
                p.UnitPrice = Decimal.Parse(form["UnitPrice"]);
                p.UnitsInStock = short.Parse(form["UnitsInStock"]);
                p.UnitsOnOrder = short.Parse(form["UnitsOnOrder"]);
                p.ReorderLevel = short.Parse(form["ReorderLevel"]);
                p.Discontinued = bool.Parse(form["Discontinued"]);

                UpdateModel(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListProducts");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
        public ActionResult Delete(int id)
        {
            Product p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var p = dbContext.Products.FirstOrDefault(s => s.ProductID == id);
                dbContext.Products.DeleteOnSubmit(p);
                dbContext.SubmitChanges();

                return RedirectToAction("ListProducts");

            }
            catch
            {
                return View();
            }
        }
    }
}