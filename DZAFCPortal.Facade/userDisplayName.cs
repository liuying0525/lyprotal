using DZAFCPortal.Authorization.Web;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Facade
{
    public class userDisplayName
    {
        public static string UserDisplayName()
        {
            var acount = UserInfo.Account;
            var user = new UserService().GenericService.GetAll(p => p.Account == acount).FirstOrDefault();
            return user.DisplayName;
        }
    }
}
