using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Coupon
    {
        [Key]
        public string Coupon_Id { get; set; }

        public string Detail { get; set; }

        public double Calculator { get; set; }

        public int NumberForUsed { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
