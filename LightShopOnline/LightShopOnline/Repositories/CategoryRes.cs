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
    public class CategoryRes
    {
        public static List<Category> GetAll()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConstValue.ConnectionString);
            DataTable result = connection.Select("Category_GetAll", value);
            List<Category> lstResult = new List<Category>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    Category cat = new Category();
                    cat.Category_Id = string.IsNullOrEmpty(dr["Category_Id"].ToString()) ? 0 : int.Parse(dr["Category_Id"].ToString());
                    cat.Category_Name = dr["Category_Name"].ToString();
                    cat.url = dr["url"].ToString();
                    cat.parentId = dr["parentId"].ToString();
                    cat.isHidden = string.IsNullOrEmpty(dr["isHidden"].ToString()) ? 0 : int.Parse(dr["isHidden"].ToString());
                    cat.Picture1 = dr["Picture1"].ToString();

                    lstResult.Add(cat);
                }
            }

            return lstResult;
        }
    }
}
