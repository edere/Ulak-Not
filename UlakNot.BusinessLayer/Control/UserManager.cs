using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.BusinessLayer.Results;
using UlakNot.Common.Helpers;
using UlakNot.DataLayer.EntityFramework;
using UlakNot.Entity;
using UlakNot.Entity.UserObjects;

namespace UlakNot.BusinessLayer.Control
{
    public class UserManager
    {
        private Repository<UnUsers> repo_user = new Repository<UnUsers>();

        public ErrorResult<UnUsers> RegisterUser(RegisterModel data)
        {
            ErrorResult<UnUsers> error_res = new ErrorResult<UnUsers>();
            UnUsers user = repo_user.Find(x => x.Username == data.Username || x.Email == data.EMail);

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    error_res.Error.Add("Bu kullanıcı adı kullanılıyor!");
                }

                if (user.Email == data.EMail)
                {
                    error_res.Error.Add("Bu e-posta adresi kullanılıyor!");
                }
            }
            else
            {
                int SaveResult = repo_user.Insert(new UnUsers()
                {
                    Username = data.Username,
                    Email = data.EMail,
                    Name = data.Name,
                    Surname = data.Surname,
                    Password = data.Password,
                    GuidControl = Guid.NewGuid(),
                    ActiveStatus = false,
                    AdminAuthority = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });

                if (SaveResult > 0)
                {
                    error_res.Result = repo_user.Find(x => x.Username == data.Username && x.Email == data.EMail);
                    // TODO: aktivasyon maili gönder

                    string siteUrl = ConfigReadHelper.Get<string>("SiteRootUri");
                    string activeUrl = $"{siteUrl}/Home/UserActivate/{error_res.Result.GuidControl}";
                    string body =
                        $"Merhaba {error_res.Result.Name} {error_res.Result.Surname} ({error_res.Result.Username}),</br> </br>Hesabınızı etkinleştirmek için <a href='{activeUrl}' target='_blank'>tıklayınız";

                    MailHelper.SendMail(body, error_res.Result.Email, "UlakNot Hesabınızı Aktifleştirin");
                }
            }
            return error_res;
        }

        public ErrorResult<UnUsers> GetUserById(int id)
        {
            ErrorResult<UnUsers> res = new ErrorResult<UnUsers>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.Error.Add("Kullanıcı Bulunamadı");
            }

            return res;
        }

        public ErrorResult<UnUsers> ActivateUser(Guid activateId)
        {
            ErrorResult<UnUsers> res = new ErrorResult<UnUsers>();
            res.Result = repo_user.Find(x => x.GuidControl == activateId);

            if (res.Result != null)
            {
                if (res.Result.ActiveStatus)
                {
                    res.Error.Add("Kullanıcı adı daha önce aktif edilmiştir.");
                    return res;
                }

                res.Result.ActiveStatus = true;
                repo_user.Update(res.Result);
            }
            else
            {
                res.Error.Add("Geçersiz kullanıcı doğrulaması!");
            }

            return res;
        }

        public ErrorResult<UnUsers> LoginUser(LoginModel data)
        {
            ErrorResult<UnUsers> errorRes = new ErrorResult<UnUsers>();
            errorRes.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (errorRes.Result != null)
            {
                if (!errorRes.Result.ActiveStatus)
                {
                    errorRes.Error.Add("Kullanıcı aktif değildir, lütfen e-posta adresinize gönderilen aktifleşme linkine tıklayınız!");
                }
            }
            else
            {
                errorRes.Error.Add("Kullanıcı adı veya şifre yanlış girildi!");
            }

            return errorRes;
        }
    }
}