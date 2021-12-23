﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Models
{
    [Table("Category")]
    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Category_Product = new HashSet<Category_Product>();
        }

        [Key]
        [Display(Name = "Category ID")]
        public int Category_Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Category Name")]
        public string Category_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Category Url")]

        public string url { get; set; }

        [StringLength(20)]
        [Display(Name = "Parent Cat ID")]
        public string parentId { get; set; }

        public int isHidden { get; set; }

        [StringLength(255)]
        public string Picture1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category_Product> Category_Product { get; set; }
    }
}
