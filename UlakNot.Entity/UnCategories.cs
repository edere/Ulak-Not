using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Categories")]
    public class UnCategories:UnBase
    {
        [Required, StringLength(75)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public virtual List<UnNotes> Notes { get; set; }

    }
}
