using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class OperationService : AuthGenericService<Operation>
    {
        public OperationService()
        {
           // Include(p => p.Urls);
        }
    }
}
