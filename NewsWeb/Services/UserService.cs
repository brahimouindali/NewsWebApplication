using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class UserService
    {
        private readonly IUnitOfWork<AppUser> _user;

        public UserService(IUnitOfWork<AppUser> user)
        {
            _user = user;
        }

        public IEnumerable<AppUser> GetUsers() =>
            _user.Entity.GetAll();

        public AppUser GetUser(string id) =>
            _user.Entity.GetById(id);

        public void UpdateUser(AppUser user)
        {
            _user.Entity.Update(user);
            _user.Save();
        }

        public void LockUnlockUser(string id)
        {
            var user = GetUser(id);
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            user.LockoutEnabled = !user.LockoutEnabled;
            _user.Entity.Update(user);
            _user.Save();
        }

        public void DeleteUser(string id)
        {
            _user.Entity.Delete(id);
            _user.Save();
        }
    }
}
