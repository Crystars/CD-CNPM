using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string Order_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_Id { get; set; }

        public int Quantity { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Cart_Id { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
