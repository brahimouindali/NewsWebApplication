using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class ContactService
    {
        private readonly IUnitOfWork<Contact> _contact;

        public ContactService(IUnitOfWork<Contact> contact)
        {
            _contact = contact;
        }

        public void Send(Contact contact)
        {
            _contact.Entity.Insert(contact);
            _contact.Save();
        }
    }
}
