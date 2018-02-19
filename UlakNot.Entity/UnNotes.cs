using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Notes")]
    public class UnNotes:UnBase
    {
        [Required,StringLength(60)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Draft { get; set; }
        public int Like { get; set; }
        public int HashtagsId { get; set; }
        public virtual UnUsers Owner { get; set; }
        public virtual List<UnHashtags> Hashtags { get; set; }
        public virtual List<UnLike> Likes { get; set; }
        public virtual List<UnComments> Comments { get; set; }

    }
}
