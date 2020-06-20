using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Library.Model
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ImageUrl { get; set; }
    }
}
