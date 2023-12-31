﻿using QuanLyBanHangHuynhHiepThien.Common.DAL;
using QuanLyBanHangHuynhHiepThien.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangHuynhHiepThien.DAL
{
    public class CategoryRep: GenericRep<NorthwindContext,Category>
    {
        public CategoryRep()
        {

        }
        public override Category Read(int id)
        {
            var res = All.FirstOrDefault(c => c.CategoryId == id);
            return res;
        }
        public override Category Read(string code)
        {
            return base.Read(code);
        }
    }
}
