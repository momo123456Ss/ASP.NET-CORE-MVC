using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHHuynhHiepThien
{
    public class DALSanPham
    {
        Product p;
        public DALSanPham()
        {
            //d = new QLBHHuynhHiepThien.DTO();
            KetNoi();
        }
        SqlConnection conn;
        public void KetNoi()
        {
            string chuoiKN = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            conn = new SqlConnection(chuoiKN);
        }

        public DataTable LayDSSP()
        {
            //return daSP.LayDSSP();
            DataTable dt = new DataTable();
            string query = "select ProductID, ProductName, QuantityPerUnit, UnitPrice, CategoryID, SupplierID from Products";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        public DataTable LayDSNhaCC()
        {
            DataTable dt = new DataTable();
            string query = "Select SupplierID,CompanyName From Suppliers";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }
        public DataTable LayLoaiSP()
        {
            DataTable dt = new DataTable();
            string query = "Select CategoryID,CategoryName From Categories";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        public bool ThemSP(Product p)
        {
            string query = String.Format("Insert into Products(ProductName,UnitPrice,SupplierID,CategoryID) values(N'{0}',{1},{2},{3})", p.ProductName, p.UnitPrice, p.SupplierID, p.CategoryID);

            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    return false;

            }
            catch (SqlException)
            {
                return false;

            }
            finally
            {
                conn.Close();
            }
            return true;
        }
        public bool IsExistProduct(int proID)
        {
            string query = String.Format("SELECT * FROM Products WHERE ProductID = {0}", proID);
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(query, conn).Fill(dataTable);
            return dataTable.Rows.Count > 0;

        }

        public bool SuaSP(Product p)
        {
            
            string query = String.Format("Update Products Set UnitPrice = {0},SupplierID = {1},CategoryID = {2},ProductName = '{3}', QuantityPerUnit = '{4}' WHERE ProductID = {5}", p.UnitPrice,p.SupplierID,p.CategoryID,p.ProductName,p.QuantityPerUnit,p.ProductID);
            

            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    return false;

            }
            catch (SqlException)
            {
                return false;

            }
            finally
            {
                conn.Close();
            }
            return true;
        }
    }
}
