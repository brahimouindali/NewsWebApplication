using BLL.Repository;
using Data.Access.Library;
using Data.Access.Library.Interfaces;

namespace BLL.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<T> _entity;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity
        {
            get
            { return _entity ?? (_entity = new GenericRepository<T>(_context)); }
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
