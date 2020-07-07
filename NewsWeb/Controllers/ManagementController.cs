using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsWeb.Controllers
{
    public class ManagementController : Controller
    {
        [Route("/users")]
        [Authorize(Roles = RoleName.AdminsRole)]
        public IActionResult Users() => View();

        [Route("/articles")]
        [Authorize(Roles = RoleName.EditorRole)]
        public IActionResult Articles() => View();

        [Route("/comments")]
        [Authorize(Roles = RoleName.EditorRole)]
        public IActionResult Comments() => View();
    }
}
