using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity
{
    [Table("Users")]
    public class UnUsers:UnBase
    {
        [Required,StringLength(30)]
        public string Name { get; set; }
        [Required,StringLength(30)]
        public string Surname { get; set; }
        [Required,StringLength(20)]
        public string Username { get; set; }
        [Required,StringLength(30)]
        public string Password { get; set; }
        [Required,StringLength(75)]
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(150)]
        public string University { get; set; }
        [StringLength(150)]
        public string Department { get; set; }
        public Guid GuidControl { get; set; }
        public bool ActiveStatus { get; set; }
        public bool AdminAuthority { get; set; }
        public virtual List<UnNotes> Notes { get; set; }
        public virtual List<UnHashtags> Hashtags { get; set; }
        public virtual List<UnLike> Likes { get; set; }
        public virtual List<UnComments> Comments { get; set; }

    }
}
