using Data.Access.Library.Model;
using System.Collections.Generic;

namespace NewsWebsite.ViewModels
{
    public class ArticleCommentsViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public AppUser Publisher { get; set; }
        public Category Category { get; set; }
    }
}
