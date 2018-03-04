using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Contact")]
    public class UnContact
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("İsim"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Name { get; set; }

        [DisplayName("Soysim"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Surname { get; set; }

        [DisplayName("E-posta"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(75, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Mail { get; set; }

        [DisplayName("Konu"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Subject { get; set; }

        [DisplayName("Mesaj"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(750, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Message { get; set; }

        [StringLength(25)]
        public string CreateUserName { get; set; }
    }
}