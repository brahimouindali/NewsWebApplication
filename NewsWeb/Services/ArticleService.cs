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

        public ArticleService(IUnitOfWork<Article> article, IUnitOfWork<Category> category)
        {
            _article = article;
            _category = category;
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
    }
}
