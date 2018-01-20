using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.GroupModels
{
    public class GroupListModel
    {
        public GroupListModel()
        {
            RoleNameList = new List<RolesViewModel>();
        }
        public List<RolesViewModel> RoleNameList { get; set; }
    }
}
