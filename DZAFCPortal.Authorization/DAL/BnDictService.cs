using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class BnDictService : AuthGenericService<BnDict>
    {
        public string GetDicNameByID(string id)
        {
            return GenericService.GetModel(id).DisplayName;
        }
    }
}
