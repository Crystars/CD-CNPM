using CAIT.SQLHelper;
using LightShopOnline.Const;
using LightShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Repositories
{
    public class CategoryProductRes
    {
        public static List<Product> GetProductByPage(string categoryUrl, int begin, int numOfProduct)
        {
            object[] value = { categoryUrl, begin, numOfProduct };
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("GetProductInCategoryByCategoryURLAndPage", value);
            List<Product> lstResult = new List<Product>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    Product product = new Product();
                    product.Product_Name = dr["Product_Name"].ToString();
                    product.url = dr["url"].ToString();
                    product.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? -1 : int.Parse(dr["Price"].ToString());
                    product.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? -1 : int.Parse(dr["Discount"].ToString());
                    product.Picture1 = dr["Picture1"].ToString(); ;
                       
                    lstResult.Add(product);
                }
            }

            return lstResult;
        }

        public static int CountSumProduct(string categoryUrl)
        {
            
            object[] value = { categoryUrl};
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("CountProductInCategory", value);
            int sumProducts = 0;
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    sumProducts = string.IsNullOrEmpty(dr["SumProduct"].ToString()) ? -1 : int.Parse(dr["SumProduct"].ToString());
                    return sumProducts;
                }
            }

            return sumProducts;
        }
    }
}
