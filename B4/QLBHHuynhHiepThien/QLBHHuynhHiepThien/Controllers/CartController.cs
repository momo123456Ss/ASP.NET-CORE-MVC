using QLBHHuynhHiepThien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace QLBHHuynhHiepThien.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private NorthwindDataContext dbContext = new NorthwindDataContext();
        HomeModel objModel = new HomeModel();



        public List<Cart> GetListCarts()
        {

            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }
        private int Count()
        {
            int n = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                n = carts.Sum(s => s.Quanity);

            }
            return n;
        }
        private decimal Total()
        {
            decimal? total = 0;
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts != null)
            {
                total = carts.Sum(s => s.Total);

            }
            return (decimal)total;

        }
        public ActionResult AddCart(int id)
        {
            
            List<Cart> carts = GetListCarts();
            Cart c = carts.Find(s => s.ProductID == id);

            if (c == null)
            {
                c = new Cart(id);
                carts.Add(c);
            }
            else
            {
                c.Quanity++;
            }
            ViewBag.CountProduct = carts.Sum(s => s.Quanity);
            ViewBag.Total = carts.Sum(s => s.Total);
            return RedirectToAction("ListCarts");
        }
        public ActionResult ListCarts(FormCollection f)
        {
            List<Cart> carts = GetListCarts();
            ViewBag.CountProduct = carts.Sum(s => s.Quanity);
            ViewBag.Total = carts.Sum(s => s.Total);
            return View(carts);
        }

        public ActionResult Delete(int id)
        {
            List<Cart> carts = GetListCarts();
            Cart c = carts.Find(s => s.ProductID == id);
            carts.Remove(c);
            return RedirectToAction("ListCarts");

        }
        public ActionResult Edit(int id)
        {
            List<Cart> carts = GetListCarts();
            Cart c = carts.Find(s => s.ProductID == id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                List<Cart> carts = GetListCarts();
                var c = carts.Where(s => s.ProductID == id).FirstOrDefault();
                UpdateModel(c);


                return RedirectToAction("ListCarts");

            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }
       
        public ActionResult OrderProduct(FormCollection f)
        {
            string body = "";
            string subject = "";
            double money = 0;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    List<Cart> carts;
                    //1.Tao moi don hang
                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    dbContext.Orders.InsertOnSubmit(order);
                    dbContext.SubmitChanges();
                    body = "Đang vận chuyển đơn hàng của bạn gồm :";
                    subject = "Đơn hàng của bạn " + order.OrderID;
                    //int maDH = int.Parse(dbContext.MaxOrde().ToString());
                    //2.Them cac sp chi tiet
                    //2.1 lay gio hang
                    carts = GetListCarts();
                    //2.2 Duyet tung sp trong GH
                    foreach (Cart item in carts)
                    {
                        //Tao 1 orderDetails
                        Order_Detail d = new Order_Detail();
                        d.OrderID = order.OrderID;
                        d.ProductID = item.ProductID;
                        //d.ProductID = maDH;
                        d.UnitPrice = (decimal)item.UnitPrice;
                        d.Quantity = (short)item.Quanity;
                        d.Discount = 0;
                        //Them SP vao OrderDetail
                        dbContext.Order_Details.InsertOnSubmit(d);
                        money += item.Quanity * (double)item.UnitPrice;
                        body += "<br>+ " +item.ProductName + " ,Số lượng: " + item.Quanity;
                    }
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));
                    content = content.Replace("{{CustomerEmail}}", "nino07052002@gmail.com");
                    content = content.Replace("{{Body}}", body);
                    content = content.Replace("{{Total}}", money.ToString("N3"));
                    Gmail gmail = new Gmail();
                    gmail.To = "nino07052002@gmail.com";
                    gmail.Subject = subject;
                    gmail.Body = content;
                    gmail.SendMail();
                    dbContext.SubmitChanges();
                    //lam rong session              
                    tran.Complete();
                    Session["Cart"] = null;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    RedirectToAction("ListCarts");
                }
            }
            
            return Content(body + subject);
        }

        //View thong ke
        //Ngay MaDH SoLuongSP
        


    }
}
