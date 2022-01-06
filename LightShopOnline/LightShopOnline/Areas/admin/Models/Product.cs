using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Models
{
    [Table("Product")]
    public class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Category_Product = new HashSet<Category_Product>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Display(Name = "Product Id")]
        public int Product_Id { get; set; }

        [StringLength(510)]
        [Display(Name = "Product Name")]
        public string Product_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Product URL")]
        public string url { get; set; }

        [Display(Name = "Price")]
        public long? Price { get; set; }

        [StringLength(50)]
        [Display(Name = "Warrant")]
        public string Warrant { get; set; }

        [StringLength(50)]
        [Display(Name = "Size")]
        public string Size { get; set; }

        [StringLength(50)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Product Description")]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Description { get; set; }

        [StringLength(50)]
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Discount")]
        public double? Discount { get; set; }

        public int isHidden { get; set; }

        [StringLength(255)]
        [Display(Name = "Image")]
        public string Picture1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category_Product> Category_Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
