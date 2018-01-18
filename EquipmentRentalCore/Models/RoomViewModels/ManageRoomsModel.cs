using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.RoomViewModels
{
    public class ManageRoomsModel
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Display(Name = "Room name")]
        [StringLength(30, ErrorMessage = "Name is too long")]
        [Required(ErrorMessage = "Name of room is required", AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
