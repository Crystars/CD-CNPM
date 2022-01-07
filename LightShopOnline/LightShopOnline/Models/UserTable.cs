using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Models
{
    public class UserTable
    {
        [Key]
        public int User_Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ggAuthId { get; set; }

        public string Role { get; set; }

        public string Gmail { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
