using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Order
    {
        [Key]
        public string Order_Id { get; set; }

        public string Guest_Name { get; set; }

        public string Guest_Phone { get; set; }

        public DateTime dateCreate { get; set; }

        public string Address { get; set; }

        public int Price { get; set; }
    }
}
