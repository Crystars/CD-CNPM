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
    public class ProductRes
    {
        public static List<Product> GetAll()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("Product_GetAll", value);
            List<Product> lstResult = new List<Product>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    Product sp = new Product();
                    sp.Product_Id = string.IsNullOrEmpty(dr["Product_Id"].ToString()) ? 0 : int.Parse(dr["Product_Id"].ToString());
                    sp.Product_Name = dr["Product_Name"].ToString();
                    sp.url = dr["url"].ToString();
                    sp.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    sp.Warrant = dr["Warrant"].ToString();
                    sp.Size = dr["Size"].ToString();
                    sp.Color = dr["Color"].ToString();
                    sp.Description = dr["Description"].ToString();
                    sp.Brand = dr["Brand"].ToString();
                    sp.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : float.Parse(dr["Discount"].ToString());
                    sp.isHidden = string.IsNullOrEmpty(dr["isHidden"].ToString()) ? 0 : int.Parse(dr["isHidden"].ToString());
                    sp.Picture1 = dr["Picture1"].ToString();

                    lstResult.Add(sp);
                }
            }

            return lstResult;
        }

        public static Product GetDetailByURL(String URL)
        {
            object[] value = { URL };
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("Product_GetDetailByURL", value);
            Product sp = new Product();
            try
            {
                if (connection.errorCode == 0 && result.Rows.Count > 0 && result.Rows.Count < 2)
                {
                    foreach (DataRow dr in result.Rows)
                    {
                        sp.Product_Id = string.IsNullOrEmpty(dr["Product_Id"].ToString()) ? 0 : int.Parse(dr["Product_Id"].ToString());
                        sp.Product_Name = dr["Product_Name"].ToString();
                        sp.url = dr["url"].ToString();
                        sp.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                        sp.Warrant = dr["Warrant"].ToString();
                        sp.Size = dr["Size"].ToString();
                        sp.Color = dr["Color"].ToString();
                        sp.Description = dr["Description"].ToString();
                        sp.Brand = dr["Brand"].ToString();
                        sp.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : float.Parse(dr["Discount"].ToString());
                        sp.isHidden = string.IsNullOrEmpty(dr["isHidden"].ToString()) ? 0 : int.Parse(dr["isHidden"].ToString());
                        sp.Picture1 = dr["Picture1"].ToString();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return sp;
        }

        public static List<Product> GetCart()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("Product_GetAll", value);
            List<Product> lstResult = new List<Product>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    Product sp = new Product();
                    sp.Product_Id = string.IsNullOrEmpty(dr["Product_Id"].ToString()) ? 0 : int.Parse(dr["Product_Id"].ToString());
                    sp.Product_Name = dr["Product_Name"].ToString();
                    sp.url = dr["url"].ToString();
                    sp.Price = string.IsNullOrEmpty(dr["Price"].ToString()) ? 0 : int.Parse(dr["Price"].ToString());
                    sp.Warrant = dr["Warrant"].ToString();
                    sp.Size = dr["Size"].ToString();
                    sp.Color = dr["Color"].ToString();
                    sp.Description = dr["Description"].ToString();
                    sp.Brand = dr["Brand"].ToString();
                    sp.Discount = string.IsNullOrEmpty(dr["Discount"].ToString()) ? 0 : float.Parse(dr["Discount"].ToString());
                    sp.isHidden = string.IsNullOrEmpty(dr["isHidden"].ToString()) ? 0 : int.Parse(dr["isHidden"].ToString());
                    sp.Picture1 = dr["Picture1"].ToString();

                    lstResult.Add(sp);
                }
            }

            return lstResult;
        }


    }
}
