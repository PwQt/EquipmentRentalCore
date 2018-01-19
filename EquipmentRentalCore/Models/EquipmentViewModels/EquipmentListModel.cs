using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.EquipmentViewModels
{
    public class EquipmentListModel
    {
        public int Id { get; set; }
        [Display(Name = "Name of equipment")]
        public string EquipmentName { get; set; }
        public int EquipmentTypeID { get; set; }
        [Display(Name = "Equipment type")]
        public string EquipmentTypeText { get; set; }
        public int? RoomID { get; set; }
        [Display(Name = "Room name")]
        public string RoomName { get; set; }
        public int? RentID { get; set; }
        [Display(Name = "Is the equipment rented?")]
        public bool IsRented { get; set; }
    }
}
