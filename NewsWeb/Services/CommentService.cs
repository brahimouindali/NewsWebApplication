using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsWeb.Services
{
    public class CommentService
    {
        private readonly IUnitOfWork<Comment> _comment;

        public CommentService(IUnitOfWork<Comment> comment)
        {
            _comment = comment;
        }
        public void Insert(Comment comment)
        {
            _comment.Entity.Insert(comment);
            _comment.Save();
        }

        public IEnumerable<Comment> getAll()
        {
            return _comment.Entity.GetAll();
        }

        public void Update(Comment comment)
        {
            _comment.Entity.Update(comment);
            _comment.Save();
        }
        public int Like(Guid commentId)
        {
            var comment = _comment.Entity.GetById(commentId);
            int c = comment.Like;
            //  string count = _comment.Entity.GetAll().Select(c => c.Deslike).ToString();
            return c;
        }

        public int DesLike(Guid commentId)
        {
            var comment = _comment.Entity.GetById(commentId);
            int c = comment.Deslike;
            return c;
        }
    }
}
