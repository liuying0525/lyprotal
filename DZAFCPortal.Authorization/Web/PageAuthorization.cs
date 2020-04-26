
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Web
{
    public class PageAuthorization
    {
        //private static Authorize.AuthorizeServiceSoapClient client = new Authorize.AuthorizeServiceSoapClient();

        public static string ProcessAuthorization(string url)
        {
            //return "true";
            OperationUrlSerivce opUlrService = new OperationUrlSerivce();
            List<Operation> operations = UserOperation.GetUserOperation(UserInfo.Account);

            if (operations == null || operations.Count == 0)
            {
                return "<h2 style='text-align:center'>抱歉您没权限访问该页面！</h2>";
            }
            foreach (var op in operations)
            {
                var urls = opUlrService.GenericService.GetAll(p => p.OperationID == op.ID);

                foreach (var u in urls)
                {
                    if (url.ToLower().Contains(u.URL.ToLower()))
                    {
                        return "true";
                    }
                }
            }
            return "<h2 style='text-align:center'>抱歉您没权限访问该页面！</h2>";
        }

    }
}
