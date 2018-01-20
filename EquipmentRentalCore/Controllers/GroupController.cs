using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using EquipmentRentalCore.Models.GroupModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EquipmentRentalCore.Controllers
{
    
    public class GroupController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<GroupController> _logger;
        private readonly EquipmentRentalContext _context;
        private readonly RoleManager<Group> _roleManager;

        public GroupController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<GroupController> logger, EquipmentRentalContext context, RoleManager<Group> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var roles = await _context.Roles.ToListAsync();
            var model = new List<RolesViewModel>();
            foreach (var item in roles)
            {
                var usersInRole = await _context.UserRoles.Where(x => x.RoleId.Equals(item.Id)).ToListAsync();
                var usersList = new List<UsersInRoleViewModel>();
                foreach (var element in usersInRole)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(element.UserId));
                    usersList.Add(new UsersInRoleViewModel
                    {
                        UserID = user.Id,
                        Username = user.Name + " " + user.Surname
                    });
                }
                model.Add(new RolesViewModel
                {
                    RoleID = item.Id,
                    RoleName = item.Name,
                    UsersList = usersList
                });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> _AddUserToRole(int id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var getUsersInRole = await _context.UserRoles.Where(x => x.RoleId.Equals(id)).ToListAsync();

            var listUsersNotInRole = new List<User>();

            foreach (var item in await _context.Users.ToListAsync())
            {
                if (!getUsersInRole.Any(x => x.UserId == item.Id))
                    listUsersNotInRole.Add(item);
            }
            return View();
        }
    }
}