using CAIT.SQLHelper;
using LightShopOnline.Const;
using LightShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Repositories
{
    public class AccountRes
    {
        public static int LoginCheck(UserTable account)
        {
            if(account.Username == null || account.Password == null)
            {
                return 0;
            }
            SqlConnection conn = new SqlConnection(ConstValue.ConnectionString);
            SqlCommand com = new SqlCommand("Login_Check", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Username", account.Username);
            com.Parameters.AddWithValue("@Password", account.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);

            conn.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);
            conn.Close();

            return res;
        }
    }
}
