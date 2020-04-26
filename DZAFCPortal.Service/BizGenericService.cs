using DZAFCPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class BizGenericService<T> where T : NySoftland.Core.BaseEntity
    {
        public NySoftland.Core.GenericService<T> GenericService;

        public BizGenericService()
        {
            var repository = DZPortalRepository<T>.Instance();

            GenericService = new NySoftland.Core.GenericService<T>(repository);
        }
    }
}
