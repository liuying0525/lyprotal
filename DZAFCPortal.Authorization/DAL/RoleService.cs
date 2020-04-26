using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class RoleService : AuthGenericService<Role>
    {
        public bool IsRoleExsist(string name, string id, bool isEdit)
        {
            System.Linq.Expressions.Expression<Func<Role, bool>> predicate = p => p.Name == name;
            if (isEdit)
                predicate = p => p.Name == name && p.ID != id;

            var items =GenericService. GetAll(predicate);

            return items != null && items.Count() > 0;
        }
    }
}
