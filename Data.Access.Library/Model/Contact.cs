using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Library.Model
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(200), Required(ErrorMessage ="*")]
        [Column(TypeName = "varchar(200)")]
        public string Nom { get; set; }

        [MaxLength(200), Required(ErrorMessage = "*")]
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }

        public DateTime ContactAt { get; set; }

        [MaxLength(200), Required(ErrorMessage = "*")]
        [Column(TypeName = "varchar(200)")]
        public string Object { get; set; }

        [MaxLength(2000), Required(ErrorMessage = "*")]
        [Column(TypeName = "varchar(200)")]
        public string Message { get; set; }
    }
}
