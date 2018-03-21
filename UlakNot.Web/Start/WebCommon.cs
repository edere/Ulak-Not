using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UlakNot.Common;
using UlakNot.Entity;

namespace UlakNot.Web.Start
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                UnUsers user = HttpContext.Current.Session["login"] as UnUsers;
                return user.Username;
            }

            return "ulaknot";
        }
    }
}