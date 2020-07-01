using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Access.Library.Model
{
    public class Article
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(200), Required]
        [Column(TypeName = "varchar(200)")]
        public string Titre { get; set; }

        [Column(TypeName = "varchar(Max)"), Required]
        public string Content { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        [Required]
        public string AppUserId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string ImageUrl { get; set; }

        public DateTime PublishedAt { get; set; }

        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public virtual IEnumerable<Rating> Ratings { get; set; }

        public int NombreVisites { get; set; }
    }
}
