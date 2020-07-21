using BLL;
using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsWeb.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IUnitOfWork<Article> _article;
        private readonly IUnitOfWork<Publisher> _publisher;
        private readonly IUnitOfWork<Comment> _comment;
        private readonly IUnitOfWork<Category> _category;
        private readonly IUnitOfWork<AppUser> _user;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<IdentityUser> _userManager;

        public ArticlesController(IUnitOfWork<Article> article,
                IUnitOfWork<Publisher> publisher,
                IUnitOfWork<Comment> comment,
                IUnitOfWork<Category> category,
                IUnitOfWork<AppUser> user,
                IHostingEnvironment hosting,
                UserManager<IdentityUser> UserManager)
        {
            _article = article;
            _publisher = publisher;
            _comment = comment;
            _category = category;
            _user = user;
            _hosting = hosting;
            _userManager = UserManager;
        }

        [Authorize(Roles = RoleName.WritersRole)]
        public async Task<IActionResult> Create()
        {
            var categories = _category.Entity.GetAll();

            //var usersInWriterRole = _userManager.GetUsersInRoleAsync(RoleName.WritersRole).Result;
            //var users = new List<AppUser>();
            //foreach (var user in usersInWriterRole)
            //{
            //    users.Add(user as AppUser);
            //}
            //var publishers = users;

            ArticleViewModel model = new ArticleViewModel
            {
                //Publishers = publishers,
                Categories = categories
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    var directory = _hosting.WebRootPath + "/img";
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    string fullPath = Path.Combine(directory, $"{model.File.FileName}");

                    if (!Directory.Exists(fullPath))
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                string myStr = model.Titre;
                string titre = Regex.Replace(myStr, "[^a-zA-Z0-9_]+", "-");
                string id = Regex.Replace(titre, " ", "-");


                string userId = CurrentUser();

                Article article = new Article
                {
                    Id = id,
                    Titre = model.Titre,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    //AppUserId = model.PublisherId,
                    AppUserId = userId,
                    ImageUrl = model.File.FileName,
                    PublishedAt = DateTime.Now,
                    IsVisible = false
                };

                _article.Entity.Insert(article);
                _article.Save();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }

        private string CurrentUser()
        {
            //Retreving userId
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim.Value;
        }

        public IActionResult Details(string Id)
        {
            try
            {
                var article = _article.Entity.GetById(Id);
                if (article != null)
                {
                    var categories = _category.Entity.GetAll();
                    var category = categories.Where(c => c.Id == article.CategoryId).FirstOrDefault();
                    var publishers = _user.Entity.GetAll();
                    var publisher = publishers.Where(p => p.Id == article.AppUserId).FirstOrDefault();


                    var articleViewModel = new ArticleCommentsViewModel
                    {
                        Publisher = publisher,
                        Article = article,
                        Category = category
                        
                    };

                    article.NombreVisites++;
                    _article.Entity.Update(article);
                    _article.Save();
                    return View(articleViewModel);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = RoleName.WritersRole)]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = _article.Entity.GetById(id);            
            var categories = _category.Entity.GetAll();

            //var usersInWriterRole = _userManager.GetUsersInRoleAsync(RoleName.WritersRole).Result;
            //var users = new List<AppUser>();
            //foreach (var user in usersInWriterRole)
            //{
            //    users.Add(user as AppUser);
            //}
            //var publishers = users;
            var userId = CurrentUser();
            if (article == null)
            {
                return NotFound();
            }
            ArticleViewModel model = new ArticleViewModel
            {
                Id = article.Id,
                Titre = article.Titre,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                //Publishers = publishers,
                Categories = categories,
                PublishedAt = article.PublishedAt,
                CategoryId = article.CategoryId,
                PublisherId = userId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ArticleViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string upload = Path.Combine(_hosting.WebRootPath, @$"img\{model.File.FileName}");

                        if (!Directory.Exists(upload))
                            model.File.CopyTo(new FileStream(upload, FileMode.Create));
                    }

                    string myStr = model.Titre;
                    string titre = Regex.Replace(myStr, "[^a-zA-Z0-9_]+", "-");
                    string idReplaced = Regex.Replace(titre, " ", "-");

                    Article article = new Article
                    {
                        Id = idReplaced,
                        Titre = model.Titre,
                        Content = model.Content,
                        AppUserId = model.PublisherId,
                        ImageUrl = model.File != null ? model.File.FileName : model.ImageUrl,
                        CategoryId = model.CategoryId,
                        PublishedAt = model.PublishedAt
                    };

                    _article.Entity.Update(article);
                    _article.Save();

                    return RedirectToAction(nameof(Index), "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    var isExist = ArticleExists(model.Id);
                    if (!isExist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            var article = _article.Entity.GetById(id);

            var categoryInDb = _category.Entity.GetAll();
            var category = categoryInDb.Where(c => c.Id == article.CategoryId).FirstOrDefault();

            var publisherInDb = _user.Entity.GetAll();
            var publisher = publisherInDb.Where(p => p.Id == article.AppUserId).FirstOrDefault();

            var articleViewModel = new ArticleCommentsViewModel
            {
                Article = article,
                Category = category,
                Publisher = publisher
            };

            return View(articleViewModel);
        }

        [Authorize(Roles = RoleName.EditorRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _article.Entity.Delete(id);
            _article.Save();
            return RedirectToAction("index", "home");
        }

        private bool ArticleExists(string id)
        {
            var articles = _article.Entity.GetAll();
            return articles.Any(a => a.Id == id);
        }
    }
}