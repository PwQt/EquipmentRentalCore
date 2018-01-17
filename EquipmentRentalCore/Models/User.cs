using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models
{
    public class User : IdentityUser<int>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
