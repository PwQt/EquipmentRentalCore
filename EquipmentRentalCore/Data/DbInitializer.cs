using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EquipmentRentalContext context)
        {
            context.Database.EnsureCreated();

            if (context.Equipments.Any())
                return;

            var users = new Models.User[]
            {
                new Models.User(){Login="Admin", Password="Admin",Name="Paweł", Surname="Kucia"}
            };

            foreach (var item in users)
                context.Users.Add(item);

            context.SaveChanges();


            var equipmentTypes = new Models.EquipmentType[]
            {
                new Models.EquipmentType(){TypeName="Telefon"},
                new Models.EquipmentType(){TypeName="Książka"}
            };

            foreach (var item in equipmentTypes)
                context.EquipmentTypes.Add(item);

            context.SaveChanges();


            var equipments = new Models.Equipment[]
            {
                new Models.Equipment(){ EquipmentName = "Sprzęcior", EquipmentTypeID = 1 }
            };

            foreach (var item in equipments)
                context.Equipments.Add(item);
            
            context.SaveChanges();


            var rentals = new Models.Rental[]
            {
                new Models.Rental(){RentalStart=new DateTime(2017,08,08), RentalEnd=new DateTime(2017,09,08), RentalEquipmentID=1, RentalUserID=1}
            };

            foreach (var item in rentals)
                context.Rentals.Add(item);

            context.SaveChanges();
        }
    }
}
