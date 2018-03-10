using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity.UserObjects
{
    public class RegisterModel
    {
        [DisplayName("İsim"),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Name { get; set; }

        [DisplayName("Soyisim "),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı adı"),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Username { get; set; }

        [DisplayName("E-posta"),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         StringLength(75, ErrorMessage = "{0} max. {1} karakter olmalı."),
         EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string EMail { get; set; }

        [DisplayName("Şifre"),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         DataType(DataType.Password),
         StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "{0} alanı en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
         Required(ErrorMessage = "{0} alanı boş geçilemez."),
         DataType(DataType.Password),
         StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "{0} alanı en az 6 karakter olmalıdır."),
         Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}