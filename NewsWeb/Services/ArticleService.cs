using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class ArticleService
    {
        private readonly IUnitOfWork<Article> _article;
        private readonly IUnitOfWork<Category> _category;
        private readonly IUnitOfWork<Rating> _rating;

        public ArticleService(IUnitOfWork<Article> article,
            IUnitOfWork<Category> category,
            IUnitOfWork<Rating> rating)
        {
            _article = article;
            _category = category;
            _rating = rating;
        }

        public IEnumerable<Article> GetArticles()
        {
            return _article.Entity.GetAll();
        }

        public void delete(string id)
        {
            _article.Entity.Delete(id);
        }

        public Article GetArticle(string id)
        {
            return _article.Entity.GetById(id);
        }

        public IEnumerable<Article> ArticlesOfCategory(string categoryName)
        {
            var categoryInDb = _category.Entity.GetAll();
            var category = categoryInDb.Where(c => c.Name == categoryName).FirstOrDefault();
            var articleInDb = _article.Entity.GetAll();
            return articleInDb.Where(a => a.CategoryId == category.Id);
        }

        public IEnumerable<Article> ArticlesPlusLus() =>
         GetArticles().OrderByDescending(a => a.NombreVisites).Take(5);

        public IEnumerable<Article> ArticlesPlusNote()
        {
            var ratings = _rating.Entity.GetAll();
            var artIds = ratings.OrderByDescending(r => r.Ratings).Select(r => r.ArticleId).Take(5);

            return GetArticles().Where(a => artIds.Contains(a.Id)).Take(5).ToList();
        }
    }
}
