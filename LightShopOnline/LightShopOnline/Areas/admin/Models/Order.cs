using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Models
{
    [Table("Order")]
    public class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [StringLength(20)]
        public string Order_Id { get; set; }

        [StringLength(255)]
        public string Guest_Name { get; set; }

        [StringLength(255)]
        public string Guest_Phone { get; set; }

        public DateTime? dateCreate { get; set; }

        [StringLength(510)]
        public string Address { get; set; }

        public long? Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
