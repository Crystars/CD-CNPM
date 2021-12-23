using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Models
{
    [Table("UserTable")]
    public class UserTable
    {
        [Key]
        public int User_Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(20)]
        public string ggAuthId { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        [StringLength(50)]
        public string Gmail { get; set; }
    }
}
