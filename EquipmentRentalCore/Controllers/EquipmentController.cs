using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using EquipmentRentalCore.Models.EquipmentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRentalCore.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EquipmentRentalContext _context;

        public EquipmentController(UserManager<User> userManager, EquipmentRentalContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var equipmentList = await _context
                    .Equipments
                    .Include(t => t.EquipmentType)
                    .Include(r => r.Room)
                    .Include(ren => ren.Rental)
                    .AsNoTracking()
                    .ToListAsync();

            var equipment = new List<EquipmentListModel>();
            foreach (var item in equipmentList)
                equipment.Add(new EquipmentListModel
                {
                    Id = item.EquipmentID,
                    EquipmentName = item.EquipmentName,
                    EquipmentTypeID = item.EquipmentType.TypeID,
                    EquipmentTypeText = item.EquipmentType.TypeName,
                    RoomID = item.Room.Id,
                    RoomName = item.Room.Name,
                    RentID = item.Rental != null ? item.Rental.RentalID : default(int?),
                    IsRented = item.Rental != null ? true : false
                });
            return View(equipment);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var typesList = await _context.EquipmentTypes.ToListAsync();
            var roomsList = await _context.Rooms.ToListAsync();
            return View(new ManageEquipmentModel(typesList, roomsList));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManageEquipmentModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var item = new Equipment
                {
                    EquipmentName = model.EquipmentName,
                    EquipmentTypeID = model.EquipmentTypeId,
                    RoomID = model.RoomID,
                };
                await _context.Equipments.AddAsync(item);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Could not add element!");
            }
            var typesList = await _context.EquipmentTypes.ToListAsync();
            var roomsList = await _context.Rooms.ToListAsync();
            model.EquipmentTypeList = new List<SelectListItem>();
            foreach (var item in typesList)
                model.EquipmentTypeList.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.TypeID.ToString()
                });

            model.RoomList = new List<SelectListItem>();
            foreach (var item in roomsList)
                model.RoomList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var element = await _context.Equipments.FirstOrDefaultAsync(x => x.EquipmentID == id);
            if (element != null)
            {
                var typesList = await _context.EquipmentTypes.ToListAsync();
                var roomsList = await _context.Rooms.ToListAsync();
                var item = new ManageEquipmentModel(typesList, roomsList, element.EquipmentTypeID, element.RoomID)
                {
                    EquipmentName = element.EquipmentName,
                    EquipmentId = element.EquipmentID
                };
                return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageEquipmentModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var element = await _context.Equipments.FirstOrDefaultAsync(x => x.EquipmentID == model.EquipmentId);
                element.EquipmentName = model.EquipmentName;
                element.EquipmentTypeID = model.EquipmentTypeId;
                element.RoomID = model.RoomID;
                _context.Entry(element).State = EntityState.Modified;
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Could not modify element!");
            }
            var typesList = await _context.EquipmentTypes.ToListAsync();
            var roomsList = await _context.Rooms.ToListAsync();

            model.EquipmentTypeList = new List<SelectListItem>();
            foreach (var item in typesList)
                model.EquipmentTypeList.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.TypeID.ToString(),
                    Selected = (item.TypeID == model.EquipmentTypeId) ? true : false
                });

            model.RoomList = new List<SelectListItem>();
            foreach (var item in roomsList)
                model.RoomList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = item.Id == model.RoomID ? true : false
                });
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var element = await _context.Equipments.FirstOrDefaultAsync(x => x.EquipmentID == id);
            if (element != null)
            {
                _context.Equipments.Remove(element);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}