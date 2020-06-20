using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Library.Model
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }
            
        [Required]
        public string ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        public int Ratings { get; set; }
    }
}
