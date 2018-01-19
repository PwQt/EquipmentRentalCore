using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.RentViewModels
{
    public class ProlongationRentalModel
    {
        public int RentID { get; set; }
        [Display(Name = "How much do you want to prolong this rental?")]
        public int MonthProlongation { get; set; }
    }
}
