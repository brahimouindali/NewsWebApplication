using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.AspNetCore.Mvc;

namespace NewsWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork<Category> _category;

        public CategoryController(IUnitOfWork<Category> category)
        {
            _category = category;
        }
        public IActionResult Create(string name)
        {
            var category = new Category() { Name = name };
            _category.Entity.Insert(category);
            _category.Save();
            return RedirectToAction("index", "home");
        }
    }
}
