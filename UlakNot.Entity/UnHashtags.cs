using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Hashtags")]
    public class UnHashtags:UnBase
    {
        [Required,StringLength(20)]
        public string Code { get; set; }
        [Required,StringLength(40)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual UnUsers Owner { get; set; }
        public virtual List<UnNotes> Notes { get; set; }
        public virtual List<UnComments> Comments { get; set; }
        public virtual UnCategories Category { get; set; }

    }
}
