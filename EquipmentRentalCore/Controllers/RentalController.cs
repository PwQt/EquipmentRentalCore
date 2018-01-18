using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRentalCore.Controllers
{
    public class RentalController : Controller
    {
        private readonly EquipmentRentalContext _context;

        public RentalController(EquipmentRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.RentalUser)
                .Include(e => e.RentalEquipment)
                .AsNoTracking()
                .ToListAsync();
            return View(rentals);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.RentalUser)
                .Include(e => e.RentalEquipment)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RentalID == id);

            if (rental == null)
                return NotFound();

            return View(rental);
        }
    }
}