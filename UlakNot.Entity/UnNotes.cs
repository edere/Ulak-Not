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
    [Table("Notes")]
    public class UnNotes : UnBase
    {
        [DisplayName("Not Başlığı"), Required(ErrorMessage = "{0} gereklidir."),
         StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Not İçeriği"), Required(ErrorMessage = "{0} gereklidir.")]
        public string Text { get; set; }

        [DisplayName("Taslak")]
        public bool Draft { get; set; }

        [DisplayName("Beğenilme")]
        public int LikeTotal { get; set; }

        [DisplayName("Hashtag")]
        public int HashtagsId { get; set; }

        [DisplayName("Çantada")]
        public int BagTotal { get; set; }

        public virtual List<UnBag> Bags { get; set; }
        public virtual UnHashtags Hashtags { get; set; }
        public virtual List<UnLike> Likes { get; set; }
        public virtual List<UnComments> Comments { get; set; }
        public virtual UnUsers Owner { get; set; }

        public UnNotes()
        {
            Comments = new List<UnComments>();
            Likes = new List<UnLike>();
            Bags = new List<UnBag>();
        }
    }
}