using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string url { get; set; }

        public int Price { get; set; }

        public string Warrant { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public float Discount { get; set; }

        public int isHidden { get; set; }

        public string Picture1 { get; set; }

        public string Picture2 { get; set; }

        public string Picture3 { get; set; }

        public string Picture4 { get; set; }
    }
}
