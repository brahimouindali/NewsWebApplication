using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL;
using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsWeb.Models;

namespace NewsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork<Article> _article;
        private readonly IUnitOfWork<Category> _category;
        private readonly IUnitOfWork<AppUser> _user;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork<Article> article, IUnitOfWork<Category> category, IUnitOfWork<AppUser> user)
        {
            _logger = logger;
            _article = article;
            _category = category;
            _user = user;
        }

        private IEnumerable<Article> ArticlesVisible() =>
            _article.Entity.GetAll().Where(a => a.IsVisible);

        public IActionResult Index()
        {
            var articles = ArticlesVisible().OrderByDescending(d => d.PublishedAt).Take(9);
            return View(articles);
        }

        public IActionResult Search(string query)
        {
            if (!String.IsNullOrEmpty(query) || !String.IsNullOrWhiteSpace(query))
            {
                ViewBag.query = query;
                query = query.ToLower();

                var articles = ArticlesVisible()
                    .Where(a =>  a.Titre.ToLower().Contains(query));

                if (articles.Count() == 0)
                {
                    ViewBag.isFound = false;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.isFound = true;
                    return View("Index", articles);
                }
            }
            else
            {
                return View("index");
            }
        }

        [Route(SD.Politique)]
        public IActionResult Politique()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Politique).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Economie)]
        public IActionResult Economie()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Economie).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Monde)]
        public IActionResult Monde()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Monde).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Societe)]
        public IActionResult Societe()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Societe).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Sport)]
        public IActionResult Sport()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Sport).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Culture)]
        public IActionResult Culture()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Culture).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        [Route(SD.Tech)]
        public IActionResult Tech()
        {
            var category = _category.Entity.GetAll().Where(c => c.Name == SD.Tech).FirstOrDefault();
            return View("Index", ArticlesVisible().Where(a => a.CategoryId == category.Id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
