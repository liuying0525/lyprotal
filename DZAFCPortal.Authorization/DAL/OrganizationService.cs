using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class OrganizationService : AuthGenericService<Organization>
    {
        public Organization GetOrganizationByCode(string code)
        {
            using (AuthorizationContext context = new AuthorizationContext())
            {
                return context.Organization.FirstOrDefault(p => p.IsDelete == false && p.Code == code);
            }

        }

        public Organization GetOrganizationByCode(string id, string code)
        {
            using (AuthorizationContext context = new AuthorizationContext())
            {
                return context.Organization.FirstOrDefault(p => p.IsDelete == false && p.ID != id && p.Code == code);
            }
        }

    }
}
