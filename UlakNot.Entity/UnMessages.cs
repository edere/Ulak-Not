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
        [Key]
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Subject { get; set; }
        [Required]
        public string MessageText { get; set; }
        public string From { get; set; }
        public DateTime SendDate { get; set; }

    }
}
