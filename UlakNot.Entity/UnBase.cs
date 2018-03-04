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
    public class UnBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Güncelleyen"), ScaffoldColumn(false), StringLength(25)]
        public string UpdatedUserName { get; set; }

        [DisplayName("Eklenme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Güncellenme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime UpdatedDate { get; set; }
    }
}