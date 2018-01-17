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
                new Models.Equipment(){ EquipmentName = "Sprzęcior", EquipmentTypeID = 1, RoomID = 3 }
            };

            foreach (var item in equipments)
                context.Equipments.Add(item);
            
            context.SaveChanges();

            var rooms = new Models.Room[]
            {
                new Models.Room() {Name = "Pokój 303"}, new Models.Room() {Name = "Schowek"}, new Models.Room() { Name = "Garaż"}
            };

            foreach (var item in rooms)
                context.Rooms.Add(item);

            context.SaveChanges();
        }
    }
}
