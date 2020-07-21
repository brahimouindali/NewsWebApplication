using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NewsWeb.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IUnitOfWork<AppUser> _user;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(IUnitOfWork<AppUser> user,
            UserManager<IdentityUser> UserManager)
        {
            _user = user;
            _userManager = UserManager;
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        IEnumerable<AppUser> UsersByRole(string roleName)
        {
            var usersInWriterRole = _userManager.GetUsersInRoleAsync(roleName).Result;
            var users = new List<AppUser>();
            foreach (var user in usersInWriterRole)
            {
                users.Add(user as AppUser);
            }
            return users;
        }

        [Route("Team")]
        public IActionResult Team()
        {
            ViewBag.admins = UsersByRole(RoleName.AdminsRole);
            ViewBag.editeurs = UsersByRole(RoleName.EditorRole);
            ViewBag.journalistes = UsersByRole(RoleName.WritersRole);
            return View();
        }
    }
}
