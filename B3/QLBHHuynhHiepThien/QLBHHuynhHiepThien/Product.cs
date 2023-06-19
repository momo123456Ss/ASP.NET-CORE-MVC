using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHHuynhHiepThien
{
    public class Product
    {
        //string tenSP, double donGia, int maNCC, int maLSP
        int productID;
        string productName;
        double unitPrice;
        string quantityPerUnit;
        int categoryID;
        int supplierID;

        public int ProductID { get => productID; set => productID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
      
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public int SupplierID { get => supplierID; set => supplierID = value; }
        public string QuantityPerUnit { get => quantityPerUnit; set => quantityPerUnit = value; }
    }
}
