using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangHuynhHiepThien.BLL;
using QuanLyBanHangHuynhHiepThien.Common.Request;
using QuanLyBanHangHuynhHiepThien.Common.Response;

namespace QLBHHuynhHiepThien.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductSvc productSvc;
        public ProductController()
        {
            productSvc = new ProductSvc();
        }
        [HttpPost("get-by-name")]
        public IActionResult GetProductByName([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = productSvc.Read(simpleReq.Keyword);
            return Ok(res);
        }
        [HttpPost("get-all")]
        public IActionResult getAllProducts()
        {
            var res = new SingleRsp();
            res.Data = productSvc.All;
            return Ok(res);
        }
        //[HttpPost("search-product")]
        //public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        //{
        //    var res = new SingleRsp();
        //    var products = productSvc.SearchProduct(searchProductReq);
        //    res.Data = products;
        //    return Ok(res);
        //}

        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] ProductReq reqProduct)
        {
            var res = productSvc.CreateProduct(reqProduct);
            return Ok(res);
        }

        [HttpPost("update-product")]
        public IActionResult UpdateProduct([FromBody] ProductReq reqProduct)
        {
            var res = productSvc.UpdateProduct(reqProduct);
            return Ok(res);
        }

    }
}
