﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        
        public int? RentID { get; set; }
        public int EquipmentTypeID { get; set; }
        public int RoomID { get; set; }

        public EquipmentType EquipmentType { get; set; }
        public Rental Rental { get; set; }
        public Room Room { get; set; }
    }
}
