using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class OrganizationBLL
    {
        UserAuthorizationBLL userBll = new UserAuthorizationBLL();
        DZAFCPortal.Authorization.DAL.OrganizationService orgService = new DAL.OrganizationService();

        public Organization GetOranazationByAccount(string account)
        {
            var user = userBll.GetUserByAccount(account);
            if (user != null && user.OrganizationID != null)
            {
                return orgService.GenericService.GetModel(user.OrganizationID);
            }
            else return null;
        }

        public string GetOranazationNameByAccount(string account)
        {
            var rs = String.Empty;

            var org = GetOranazationByAccount(account);
            if (org == null) rs = String.Empty;
            else rs = org.Name;

            return rs;
        }
    }
}
