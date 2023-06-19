using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBHHuynhHiepThien
{
    public class BusSanPham
    {
        DALSanPham daSP = new DALSanPham();
        public BusSanPham()
        {
            daSP = new QLBHHuynhHiepThien.DALSanPham();
        }
        public DataTable LayDSSP()
        {
            return daSP.LayDSSP();
            //DataTable dt = new DataTable();
            //string query = "select ProductID, ProductName, QuantityPerUnit, UnitPrice, CategoryID, SupplierID from Products";
            //SqlDataAdapter da = new SqlDataAdapter(query, conn);
            //da.Fill(dt);
            //return dt;
        }
        public void LayLoaiSP(ComboBox cb)
        {
            cb.DataSource =daSP.LayLoaiSP();
            cb.DisplayMember = "CategoryName";
            cb.ValueMember = "CategoryID";
            //return daSP.LayDSSP();
        }
        public void LayDSNhaCC(ComboBox cb)
        {
            cb.DataSource = daSP.LayDSNhaCC();
            cb.DisplayMember = "CompanyName";
            cb.ValueMember = "SupplierID";
            //return daSP.LayDSSP();
        }

        public bool ThemSP(Product p)
        {
            //string query = String.Format("Insert into Products(ProductName,UnitPrice,SupplierID,CategoryID) values(N'{0}',{1},{2},{3})", p.ProductName, p.UnitPrice, p.SupplierID, p.CategoryID);

            //SqlCommand cmd = new SqlCommand(query, conn);


            //try
            //{
            //    //conn.Open();
            //    //if (cmd.ExecuteNonQuery() == 0)
            //    //    return false;

            //}
            //catch (SqlException)
            //{
            //    return false;

            //}
            //finally
            //{
            //    //conn.Close();
            //}
            //return true;

            return daSP.ThemSP(p);
        }

        public bool SuaSP(Product p)
        {
            return daSP.SuaSP(p);
        }

        private bool IsExistProduct(int proID)
        {
            return daSP.IsExistProduct(proID);
        }

        public bool UpdateProduct(Product product)
        {
            if (IsExistProduct(product.ProductID))
                return daSP.SuaSP(product);
            return false;
        }
    }
}
