using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.GroupModels
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            UsersList = new List<UsersInRoleViewModel>();
            ChooseAdditionalUserList = new List<SelectListItem>();
        }
        public int RoleID { get; set; }
        [Display(Name = "Group name")]
        public string RoleName { get; set; }
        [Display(Name = "List of users in group")]
        public List<UsersInRoleViewModel> UsersList { get; set; }
        public int? ChosenListID { get; set; }
        [Display(Name = "Choose user to be added")]
        public List<SelectListItem> ChooseAdditionalUserList { get; set; }
    }
}
