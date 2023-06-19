using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHangHuynhHiepThien.BLL;
using QuanLyBanHangHuynhHiepThien.Common.Request;
using QuanLyBanHangHuynhHiepThien.Common.Response;

namespace QLBHHuynhHiepThien.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategorySvc categorySvc;
        public CategoryController()
        {
            categorySvc = new CategorySvc();
        }
        [HttpPost("get-by-id")]
        public IActionResult GetCategoryByID([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = categorySvc.Read(simpleReq.Id);
            return Ok(res);
        }
        [HttpPost("get-all")]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = categorySvc.All;
            return Ok(res);
        }
    }
}
