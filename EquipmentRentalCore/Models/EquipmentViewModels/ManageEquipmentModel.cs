using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.EquipmentViewModels
{
    public class ManageEquipmentModel
    {
        public ManageEquipmentModel()
        {

        }

        public ManageEquipmentModel(List<EquipmentType> typesList, List<Room> roomsList)
        {
            EquipmentTypeList = new List<SelectListItem>();
            foreach (var item in typesList)
                EquipmentTypeList.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.TypeID.ToString()
                });

            RoomList = new List<SelectListItem>();
            foreach (var item in roomsList)
                RoomList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
        }
        public ManageEquipmentModel(List<EquipmentType> typesList, List<Room> roomsList, int typeID, int roomID)
        {
            EquipmentTypeId = typeID;
            RoomID = roomID;
            EquipmentTypeList = new List<SelectListItem>();
            foreach (var item in typesList)
                EquipmentTypeList.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.TypeID.ToString(),
                    Selected = (item.TypeID == typeID) ? true : false
                });

            RoomList = new List<SelectListItem>();
            foreach (var item in roomsList)
                RoomList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = (item.Id == roomID) ? true : false
                });
        }
        [HiddenInput]
        public int? EquipmentId { get; set; }
        [Required(ErrorMessage = "You need to provide your equipment name", AllowEmptyStrings = false)]
        [Display(Name = "Equipment name")]
        [StringLength(30, ErrorMessage = "Name is too long")]
        public string EquipmentName { get; set; }
        [Required(ErrorMessage = "Type of equipment needs to be chosen")]
        public int EquipmentTypeId { get; set; }
        [Display(Name = "Equipment type")]
        public List<SelectListItem> EquipmentTypeList { get; set; }
        [Required(ErrorMessage = "Room needs to be chosen")]
        public int RoomID { get; set; }
        [Display(Name = "Add equipment to room")]
        public List<SelectListItem> RoomList { get; set; }
    }
}
