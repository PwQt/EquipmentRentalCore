using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.GroupModels
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            UsersList = new List<UsersInRoleViewModel>();
        }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<UsersInRoleViewModel> UsersList { get; set; }
    }
}
