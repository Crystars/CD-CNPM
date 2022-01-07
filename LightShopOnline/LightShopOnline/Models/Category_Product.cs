using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Category_Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Key]
        public int Category_Id { get; set; }

        [Key]
        public int Position { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
