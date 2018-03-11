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
    [Table("Categories")]
    public class UnCategories
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Bölüm"), Required(ErrorMessage = "{0} adı gereklidir."),
         StringLength(75, ErrorMessage = "{0} adı en fazla {1} karakter içermeli.")]
        public string Name { get; set; }

        [DisplayName("Açıklama"),
         StringLength(200, ErrorMessage = "{0} alanı en fazla {1} karakter içermeli.")]
        public string Description { get; set; }

        [DisplayName("Eklenme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Güncellenme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime UpdatedDate { get; set; }

        public virtual List<UnHashtags> Hashtags { get; set; }
    }
}