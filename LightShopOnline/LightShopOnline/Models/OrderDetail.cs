using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class OrderDetail
    {
        [Key]
        public string Order_Id { get; set; }

        [Key]
        public int Product_Id { get; set; }

        public int Quantity { get; set; }

        [Key]
        [StringLength(20)]
        public string Cart_Id { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
