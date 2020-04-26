using DZAFCPortal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class Kn_IssueService : BizGenericService<Kn_Issue>
    {
        public void Delete(string id)
        {
            var fixService = new Kn_FixService();

            //删除前先把该问题的答复权限删除
            fixService.GenericService.Delete(p => p.IssueID == id);
            fixService.GenericService.Save();

            //通过ID删除问题本身
            GenericService.Delete(id);
            GenericService.Save();
        }
    }
}
