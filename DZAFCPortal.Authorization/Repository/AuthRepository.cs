using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Repository
{
    public sealed class AuthRepository<T> where T : NySoftland.Core.BaseEntity
    {
        public static NySoftland.Core.Repositorys.BaseRepository<T> Instance()
        {
            System.Data.Entity.DbContext context = new AuthorizationContext();

            return new NySoftland.Core.Repositorys.BaseRepository<T>(context);
        }
    }
}
