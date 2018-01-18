using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.RoomViewModels
{
    public class ListRoomsModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Room name or number")]
        public string RoomName { get; set; }
        [Display(Name = "Equipments who are inside of this room")]
        public List<Equipment> EquipmentAttachedList { get; set; }
    }
}
