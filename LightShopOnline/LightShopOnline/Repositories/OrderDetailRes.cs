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
    public class OrderDetailRes
    {
        public void Insert(OrderDetail orderDetail)
        {
            string _query = "INSERT INTO [OrderDetail] (Order_Id,Product_Id,Quantity) " +
                "values (@Order_Id,@Product_Id,@Quantity)";
            using (SqlConnection conn = new SqlConnection(ConstValue.RemoteConnection))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@Order_Id", orderDetail.Order_Id);
                    comm.Parameters.AddWithValue("@Product_Id", orderDetail.Product_Id);
                    comm.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
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

        public List<OrderDetail> getOrderbyOrderId(String URL)
        {
            string _query = "SELECT * FROM [OrderDetail] WHERE Order_Id = @Order_Id";
            List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
            using (SqlConnection conn = new SqlConnection(ConstValue.RemoteConnection))
            {
                SqlCommand comm = new SqlCommand(_query, conn);
                comm.Parameters.AddWithValue("@Order_Id", URL);
                conn.Open();
                using (SqlDataReader oReader = comm.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.Order_Id = oReader["Order_Id"].ToString();
                        orderDetail.Product_Id = string.IsNullOrEmpty(oReader["Product_Id"].ToString()) ? 0 : int.Parse(oReader["Product_Id"].ToString());
                        orderDetail.Quantity = string.IsNullOrEmpty(oReader["Quantity"].ToString()) ? 0 : int.Parse(oReader["Quantity"].ToString());
                        lstOrderDetail.Add(orderDetail);
                    }
                }
                conn.Close();
            }
            return lstOrderDetail;
        }

    }
}
