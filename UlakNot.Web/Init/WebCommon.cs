using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UlakNot.Common;
using UlakNot.Entity;
using UlakNot.Web.Models;

namespace UlakNot.Web.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            UnUsers user = SessionManager.User;
            if (user != null)
            {
                return user.Username;
            }
            else
                return "ulaknot";
        }
    }
}