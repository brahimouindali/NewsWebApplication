using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Library.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(30), MinLength(4), Required(ErrorMessage = "name must be less than 30 letters")]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [MaxLength(30), MinLength(4), Required(ErrorMessage = "email required")]
        [Column(TypeName = "varchar(30)")]
        [EmailAddress]
        public string email { get; set; }

        public DateTime CommentAt { get; set; }

        [MaxLength(100), MinLength(4), Required(ErrorMessage = "title required")]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [MaxLength(1000), MinLength(4), Required(ErrorMessage = "comment must be less than 1000 letters")]
        [Column(TypeName = "varchar(1000)"), Display(Name = "Commentaire")]
        public string CommentsDetail { get; set; }

        [Required]
        public string ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }


        public int Like { get; set; }

        public int Deslike { get; set; }

    }
}
