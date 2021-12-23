using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        public String Category_Name { get; set; }

        public String url { get; set; }

        public String parentId { get; set; }

        public int isHidden { get; set; }

        public String Picture1 { get; set; }
    }
}
