using Data.Access.Library.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.ViewModels
{
    public class ArticleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please type title of the article")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Please type content of the article")]
        public string Content { get; set; }

        [Display(Name = "Publisher name")]
        public string PublisherId { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        
        public IFormFile File { get; set; }

        public DateTime PublishedAt { get; set; }

        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        public IEnumerable<AppUser> Publishers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}