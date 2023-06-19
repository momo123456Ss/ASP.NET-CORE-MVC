using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace QLBHHuynhHiepThien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            busSP = new QLBHHuynhHiepThien.BusSanPham();
            InitializeComponent();
        }
        //SqlConnection conn;
        BusSanPham busSP;
        //private void KetNoi()
        //{
        //    string chuoiKN = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
        //    conn = new SqlConnection(chuoiKN);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //KetNoi();
            gvSanPham.DataSource = busSP.LayDSSP();
            busSP.LayLoaiSP(cbLoaiSP);
            busSP.LayDSNhaCC(cbNCC);
            //cbLoaiSP.DataSource = LayLoaiSP();
            //cbLoaiSP.DisplayMember = "CategoryName";
            //cbLoaiSP.ValueMember = "CategoryID";
            //cbNCC.DataSource = LayDSNhaCC();
            //cbNCC.DisplayMember = "CompanyName";
            //cbNCC.ValueMember = "SupplierID";
        }
        //private DataTable LayDSSP()
        //{
        //    DataTable dt = new DataTable();
        //    string query = "select ProductID, ProductName, QuantityPerUnit, UnitPrice, CategoryID, SupplierID from Products";
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    da.Fill(dt);
        //    return dt;
        //}
        //private DataTable LayDSNhaCC()
        //{
        //    DataTable dt = new DataTable();
        //    string query = "Select SupplierID,CompanyName From Suppliers";
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    da.Fill(dt);
        //    return dt;
        //}
        //private DataTable LayLoaiSP()
        //{
        //    DataTable dt = new DataTable();
        //    string query = "Select CategoryID,CategoryName From Categories";
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    da.Fill(dt);
        //    return dt;
        //}
        private void gvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > 0 && e.ColumnIndex < gvSanPham.Rows.Count)
            {
                txtID.Text = gvSanPham.Rows[e.RowIndex].Cells["ProductId"].Value.ToString();
                txtTenSP.Text = gvSanPham.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                txtSoLuong.Text = gvSanPham.Rows[e.RowIndex].Cells["QuantityPerUnit"].Value.ToString();
                txtDonGia.Text = gvSanPham.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();

                cbLoaiSP.SelectedValue = gvSanPham.Rows[e.RowIndex].Cells["CategoryId"].Value.ToString();
                cbNCC.SelectedValue = gvSanPham.Rows[e.RowIndex].Cells["SupplierId"].Value.ToString();
            }
        }
        //private bool ThemSP(string tenSP,double donGia, int maNCC , int maLSP)
        //{
        //    string query = String.Format("Insert into Products(ProductName,UnitPrice,SupplierID,CategoryID) values(N'{0}',{1},{2},{3})", tenSP, donGia, maNCC, maLSP);

        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    try
        //    {
        //        conn.Open();
        //        if(cmd.ExecuteNonQuery() == 0)
        //            return false;

        //    }
        //    catch (SqlException)
        //    {
        //        return false;

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return true;
        //}
        //private bool XoaSP(string tenSP)
        //{
        //    string query = String.Format("delete from Products where ProductName = N'{0}'" ,tenSP);

        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    try
        //    {
        //        conn.Open();
        //        if (cmd.ExecuteNonQuery() == 0)
        //            return false;

        //    }
        //    catch (SqlException)
        //    {
        //        return false;

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return true;
        //}
        private void btThem_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ProductName = txtTenSP.Text;
            p.UnitPrice = double.Parse(txtDonGia.Text);
            p.CategoryID = int.Parse(cbNCC.SelectedValue.ToString());
            p.SupplierID = int.Parse(cbLoaiSP.SelectedValue.ToString());
            if (busSP.ThemSP(p))
            {
                MessageBox.Show("Them thanh cong");
                gvSanPham.DataSource = busSP.LayDSSP();
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            //if (XoaSP(txtTenSP.Text))
            //{
            //    MessageBox.Show("Xoa thanh cong");
            //    gvSanPham.DataSource = LayDSSP();
            //}
        }
        //private bool SuaSP(string tenSP, double donGia, int maNCC, int maLSP)
        //{
        //    string query = String.Format("Update Products Set UnitPrice = {0},SupplierID = {1},CategoryID = {2} WHERE ProductName = '{3}'",donGia, maNCC, maLSP,tenSP);
        //    MessageBox.Show(query);
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    try
        //    {
        //        conn.Open();
        //        if (cmd.ExecuteNonQuery() == 0)
        //            return false;

        //    }
        //    catch (SqlException)
        //    {
        //        return false;

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return true;
        //}
        private void btSua_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.ProductID = int.Parse(txtID.Text);
            p.QuantityPerUnit = txtSoLuong.Text;
            p.ProductName = txtTenSP.Text;
            p.UnitPrice = double.Parse(txtDonGia.Text);
            p.CategoryID = int.Parse(cbNCC.SelectedValue.ToString());
            p.SupplierID = int.Parse(cbLoaiSP.SelectedValue.ToString());
            //MessageBox.Show(String.Format("Update Products Set UnitPrice = {0},SupplierID = {1},CategoryID = {2},ProductName = '{3}' WHERE ProductID = {4}", p.UnitPrice, p.SupplierID, p.CategoryID, p.ProductName, p.ProductID));
            if (p == null)
            {
                MessageBox.Show("San pham ko ton tai");
               
            }
            else
            {
                //if (busSP.SuaSP(p))
                //{
                //    MessageBox.Show("Sua thanh cong");
                //    gvSanPham.DataSource = busSP.LayDSSP();
                //}

                if (busSP.UpdateProduct(p))
                {
                    gvSanPham.DataSource = busSP.LayDSSP();
                    MessageBox.Show("Ok");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
