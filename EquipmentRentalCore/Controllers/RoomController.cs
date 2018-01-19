using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquipmentRentalCore.Models.RoomViewModels;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var element = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (element != null)
            {
                var item = new ManageRoomsModel
                {
                    Id = element.Id,
                    Name = element.Name
                };
                return View(item);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageRoomsModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var elementToModify = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
                elementToModify.Name = model.Name;
                _context.Entry(elementToModify).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Errors during saving of elements - please check!");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var elementToDelete = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (elementToDelete != null)
            {
                _context.Rooms.Remove(elementToDelete);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new ManageRoomsModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManageRoomsModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var elementToAdd = new Room
                {
                    Name = model.Name
                };
                await _context.Rooms.AddAsync(elementToAdd);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string returnUrl = null)
        {
            var element = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (element != null)
            {
                var item = new ListRoomsModel
                {
                    Id = element.Id,
                    RoomName = element.Name,
                    EquipmentAttachedList = element.Equipments != null ? element.Equipments.ToList() : new List<Equipment>()
                };
                return View(item);
            }
            else
            {
                return NotFound();
            }
        }

    }
}