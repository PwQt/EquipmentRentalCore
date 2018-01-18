using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquipmentRentalCore.Models.RoomViewModels;

namespace EquipmentRentalCore.Controllers
{
    public class RoomController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EquipmentRentalContext _context;

        public RoomController(UserManager<User> userManager, EquipmentRentalContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var roomList = await _context.Rooms
                    .Include(x => x.Equipments)
                    .ToListAsync();

            var elements = new List<ListRoomsModel>();
            foreach (var item in roomList)
                elements.Add(new ListRoomsModel
                {
                    Id = item.Id,
                    RoomName = item.Name,
                    EquipmentAttachedList = item.Equipments.ToList()
                });

            return View(elements);
        }
    }
}