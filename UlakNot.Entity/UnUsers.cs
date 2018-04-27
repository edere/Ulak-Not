using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace UlakNot.Entity
{
    [Table("Users")]
    public class UnUsers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(25, ErrorMessage = "{0} alanıen fazla {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [StringLength(30), ScaffoldColumn(false)]
        public string ImageName { get; set; }

        [DisplayName("E-Posta"),
         Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(75, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
         Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "{0} alanı en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DisplayName("Cinsiyet")]
        public bool? Gender { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Üniversite"),
        StringLength(150)]
        public string University { get; set; }

        [DisplayName("Bölüm"),
        StringLength(150)]
        public string Department { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid GuidControl { get; set; }

        [DisplayName("Durum")]
        public bool ActiveStatus { get; set; }

        [DisplayName("Admin Yetkisi")]
        public bool AdminAuthority { get; set; }

        [DisplayName("Kayıt Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Güncellenme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime UpdatedDate { get; set; }

        public virtual List<UnFriend> Friends { get; set; }
        public virtual List<UnNotes> Notes { get; set; }
        public virtual List<UnHashtags> Hashtags { get; set; }
        public virtual List<UnLike> Likes { get; set; }
        public virtual List<UnBag> Bags { get; set; }
        public virtual List<UnComments> Comments { get; set; }
        public virtual List<UnMessages> Messages { get; set; }

        public UnUsers()
        {
            Friends = new List<UnFriend>();
        }
    }
}