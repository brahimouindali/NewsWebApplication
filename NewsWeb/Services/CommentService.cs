using Data.Access.Library.Interfaces;
using Data.Access.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Comment getComment(Guid id) =>
            _comment.Entity.GetById(id);

        public IEnumerable<Comment> getAll() =>
             _comment.Entity.GetAll();

        public IEnumerable<Comment> getVisibleComments() =>
             _comment.Entity.GetAll().Where(c => c.IsVisible);

        public IEnumerable<Comment> getNewComments() =>
            getAll().OrderByDescending(c => c.CommentAt);

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
