using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Cart
    {
        [Key]
        public string Cart_Id { get; set; }

        public int User_Id { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
