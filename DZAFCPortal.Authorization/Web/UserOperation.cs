using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Web
{
    public class UserOperation
    {
        public static List<Operation> GetUserOperation(string accout)
        {
            return new DZAFCPortal.Authorization.BLL.OperationBLL().GetUserOperation(accout);
        }

    }
}
