using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class AuthGenericService<T> where T : NySoftland.Core.BaseEntity
    {
        public NySoftland.Core.GenericService<T> GenericService;

        public AuthGenericService()
        {
            var repository = AuthRepository<T>.Instance();

            GenericService = new NySoftland.Core.GenericService<T>(repository);
        }
    }
}
