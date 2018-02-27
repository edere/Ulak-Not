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
    public class UnHashtags
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(30)]
        public string Code { get; set; }

        public virtual UnUsers HashtagUser { get; set; }
        public virtual UnNotes HashtagNote { get; set; }

    }
}
