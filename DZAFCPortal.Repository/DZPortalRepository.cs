using NySoftland.Core;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Repository
{
    public class DZPortalRepository<T> where T : BaseEntity
    {
        public static BaseRepository<T> Instance()
        {
            System.Data.Entity.DbContext context = new DZPortalContext();

            return new BaseRepository<T>(context);
        }
    }
}
