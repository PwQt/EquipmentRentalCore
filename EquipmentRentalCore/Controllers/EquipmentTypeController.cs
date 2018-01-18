using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Data;
using EquipmentRentalCore.Models;
using EquipmentRentalCore.Models.EquipmentTypeViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRentalCore.Controllers
{
    public class EquipmentTypeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EquipmentRentalContext _context;

        public EquipmentTypeController(UserManager<User> userManager, EquipmentRentalContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var list = await _context.EquipmentTypes
                    .Include(e => e.Equipments)
                    .AsNoTracking()
                    .ToListAsync();

            var elements = new List<ListEquipmentTypeViewModel>();
            foreach (var item in list)
                elements.Add(new ListEquipmentTypeViewModel(item.TypeID, item.TypeName, item.Equipments.ToList()));

            return View(elements);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var element = await _context.EquipmentTypes.FirstOrDefaultAsync(x => x.TypeID.Equals(id));

            var item = new ManageEquipmentTypeModel
            {
                Id = element.TypeID,
                Name = element.TypeName
            };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManageEquipmentTypeModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var elementToModify = await _context.EquipmentTypes.FirstOrDefaultAsync(x => x.TypeID.Equals(model.Id));
                elementToModify.TypeName = model.Name;
                _context.Entry(elementToModify).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}