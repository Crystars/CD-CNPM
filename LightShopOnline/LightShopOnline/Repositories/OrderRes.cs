using LightShopOnline.Areas.admin.Models;
using LightShopOnline.Const;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Repositories
{
    public class OrderRes
    {
        public void Insert(Order order)
        {
            string _query = "INSERT INTO [Order] (Order_Id,paymentMethod,Price,dateCreate,Guest_Name,Guest_Phone,Address) " +
                "values (@Order_Id,@paymentMethod,@Price,@dateCreate,@Guest_Name,@Guest_Phone,@Address)";
            using (SqlConnection conn = new SqlConnection(ConstValue.RemoteConnection))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@Order_Id", order.Order_Id);
                    comm.Parameters.AddWithValue("@paymentMethod", order.paymentMethod);
                    comm.Parameters.AddWithValue("@Price", order.Price);
                    comm.Parameters.AddWithValue("@dateCreate", order.dateCreate);
                    comm.Parameters.AddWithValue("@Guest_Name", order.Guest_Name);
                    comm.Parameters.AddWithValue("@Guest_Phone", order.Guest_Phone);
                    comm.Parameters.AddWithValue("@Address", order.Address);
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Loi");
                    }
                }
            }
        }

        public Order getOrderbyOrderId(String URL)
        {
            string _query = "SELECT * FROM [Order] WHERE Order_Id = @Order_Id";
            Order order = new Order();
            using (SqlConnection conn = new SqlConnection(ConstValue.RemoteConnection))
            {
                SqlCommand comm = new SqlCommand(_query, conn);
                comm.Parameters.AddWithValue("@Order_Id", URL);
                conn.Open();
                using (SqlDataReader oReader = comm.ExecuteReader())
                {
                    while (oReader.Read())
                    {

                        order.Order_Id = oReader["Order_Id"].ToString();
                        order.Guest_Name = oReader["Guest_Name"].ToString();
                        order.Guest_Phone = oReader["Guest_Phone"].ToString();
                        order.Address = oReader["Address"].ToString();
                        order.Price = string.IsNullOrEmpty(oReader["Price"].ToString()) ? 0 : int.Parse(oReader["Price"].ToString());
                    }
                }
                conn.Close();
            }
            return order;

        }
    }
}
