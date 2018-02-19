using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Comments")]
    public class UnComments : UnBase
    {
        [Required] public string Text { get; set; }
        public virtual UnUsers Owner { get; set; }
        public virtual UnNotes Note { get; set; }
    }
}
