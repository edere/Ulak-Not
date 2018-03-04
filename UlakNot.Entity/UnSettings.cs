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
    [Table("Settings")]
    public class UnSettings
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Site Adı"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(125)]
        public string Title { get; set; }

        [DisplayName("Site Açıklaması"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(250)]
        public string Description { get; set; }

        [DisplayName("Anahtar Kelimeler"), StringLength(250)]
        public string Keywords { get; set; }

        [DisplayName("Durum")]
        public bool Status { get; set; }
    }
}