using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    //: AuthGenericService<RoleOperation>
    public class RoleOperationService : AuthGenericService<RoleOperation>
    {
        public void Add(RoleOperation model)
        {
            using (var context = new DZAFCPortal.Authorization.Repository.AuthorizationContext())
            {
                context.RoleOperation.Add(model);
                context.SaveChanges();
            }
        }
    }
}
