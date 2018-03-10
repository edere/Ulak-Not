using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlakNot.Entity.UserObjects
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} en fazla {1} uzunluğunda olmalı.")]
        public string Username { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "{0} alanı en az 6 karakter olmalıdır.")]
        public string Password { get; set; }
    }
}