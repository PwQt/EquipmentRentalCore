using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.RentViewModels
{
    public class RentListModel
    {
        public int RentID { get; set; }
        public int EquipmentID { get; set; }
        [Display(Name = "Name of rented equipment")]
        public string EquipmentName { get; set; }
        [Display(Name = "User who rented equipment")]
        public string RentedByUser { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Rental Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime RentStartDate { get; set; }
        [Display(Name = "Rental End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime RentEndDate { get; set; }
    }
}
