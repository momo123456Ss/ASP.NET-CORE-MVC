using QuanLyBanHangHuynhHiepThien.Common.BLL;
using QuanLyBanHangHuynhHiepThien.Common.Response;
using QuanLyBanHangHuynhHiepThien.DAL;
using QuanLyBanHangHuynhHiepThien.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangHuynhHiepThien.BLL
{
    public class CategorySvc: GenericSvc<CategoryRep,Category>
    {
        private CategoryRep categoryRep;
        public CategorySvc()
        {
            categoryRep = new CategoryRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = categoryRep.Read(id);
            return res;
        }
    }
}
