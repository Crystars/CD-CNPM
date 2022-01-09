using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Const
{
    public class ConstValue
    {
        // Chứa dường dẫn để connect tới CSDL
        // , sửa lại cho phù hợp vơi máy của bạn
        

        public static string LocalConnection = "Data Source=DESKTOP-FLTCGG8\\SQLEXPRESS01;Initial catalog=LightShopOnline;User ID=sa;Password=123456";
        public static string RemoteConnection = "workstation id=51800791-51800929.mssql.somee.com;packet size=4096;user id=cystalic_SQLLogin_1;pwd=1knhzfx1ee;data source=51800791-51800929.mssql.somee.com;persist security info=False;initial catalog=51800791-51800929";
        public static string ConnectionString = RemoteConnection;
    }
}
