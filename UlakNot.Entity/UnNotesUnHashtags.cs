using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("HastingControl")]
    public class UnNotesUnHashtags
    {

        public virtual UnNotes Notes { get; set; }
        public virtual UnHashtags Hashtags { get; set; }
    }
}
