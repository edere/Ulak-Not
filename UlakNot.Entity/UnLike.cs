using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Likes")]
    public class UnLike
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual UnUsers LikedUser { get; set; }
        public virtual UnNotes Note { get; set; }
    }
}
