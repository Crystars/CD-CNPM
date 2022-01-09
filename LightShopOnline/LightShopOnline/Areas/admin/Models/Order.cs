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
        [Display(Name = "Tên khách hàng")]
        public string Guest_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Số điện thoại")]
        public string Guest_Phone { get; set; }

        public DateTime? dateCreate { get; set; }

        [StringLength(510)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        public long? Price { get; set; }

        [StringLength(20)]
        public string Coupon_Id { get; set; }

        [StringLength(50)]
        public string paymentMethod { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int User_Id { get; set; }

        public virtual Coupon Coupon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
