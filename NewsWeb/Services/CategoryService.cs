using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork<Category> _category;

        public CategoryService(IUnitOfWork<Category> category)
        {
            _category = category;
        }

        public IEnumerable<Category> getCategories()
        {
            return  _category.Entity.GetAll();
        }
    }
}
