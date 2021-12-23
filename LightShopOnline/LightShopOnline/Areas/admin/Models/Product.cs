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
        public int Product_Id { get; set; }

        [StringLength(510)]
        public string Product_Name { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        public long? Price { get; set; }

        [StringLength(50)]
        public string Warrant { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        public double? Discount { get; set; }

        public int isHidden { get; set; }

        [StringLength(255)]
        public string Picture1 { get; set; }

        [StringLength(255)]
        public string Picture2 { get; set; }

        [StringLength(255)]
        public string Picture3 { get; set; }

        [StringLength(255)]
        public string Picture4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category_Product> Category_Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
