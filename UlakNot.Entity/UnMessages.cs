using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Messages")]
    public class UnMessages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        [Required, StringLength(50)]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public virtual UnUsers From { get; set; }

    }
}
