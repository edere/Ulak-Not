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
    [Table("Hashtags")]
    public class UnHashtags
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Hashtag"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Code { get; set; }

        [DisplayName("Açıklama"),
         StringLength(100, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Description { get; set; }

        public virtual UnUsers HashtagUser { get; set; }
        public virtual List<UnNotes> Notes { get; set; }
        public virtual UnCategories Categories { get; set; }
    }
}