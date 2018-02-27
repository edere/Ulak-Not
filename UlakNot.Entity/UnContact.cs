using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Contact")]
    public class UnContact
    {
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(30)]
        public string Name { get; set; }
        [Required,StringLength(30)]
        public string Surname { get; set; }
        [Required,StringLength(75)]
        public string Mail { get; set; }
        [Required,StringLength(50)]
        public string Subject { get; set; }
        [Required,StringLength(750)]
        public string Message { get; set; }
        [Required, StringLength(20)]
        public string CreateUserName { get; set; }
    }
}
