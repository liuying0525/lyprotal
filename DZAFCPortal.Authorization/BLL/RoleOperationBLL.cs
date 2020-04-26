using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class RoleOperationBLL
    {
        RoleOperationService service = new RoleOperationService();

        public List<RoleOperation> GetRoleOperationByRoleID(string id)
        {
            return service.GenericService.GetAll(p => p.RoleID == id).ToList();
        }


        public string SetRoleOperation(string roleID, List<string> lstOperationID, string createAccount)
        {
            //List<string> lstOpera = new OperationBLL().GetAllOperation().Select(p => p.ID).ToList();
            //todo:验证该人有没有权限
            List<RoleOperation> lstRoleOperation_ToDelete = service.GenericService.GetAll(p => p.RoleID == roleID).ToList();

            foreach (var item in lstRoleOperation_ToDelete)
            {
                service.GenericService.Delete(item);
            }

            //update by 唐万祯 at 2014/08/07
            //修改通过 复选框获取其选中的操作方法
            //修正为通过ID查找操作，再在操作中取出与该操作模块以及ControlID一致的操作
            var opService = new OperationService();
            List<string> lstOpID = new List<string>();
            foreach (var id in lstOperationID)
            {
                var op = opService.GenericService.GetModel(id);

                var tempIds = opService.GenericService.GetAll(p => p.ModuleID == op.ModuleID && p.ControlID == op.ControlID).Select(p => p.ID).ToList();
                if (tempIds != null && tempIds.Count > 0)
                    lstOpID.AddRange(tempIds);
            }
            //delete by 唐万祯 at 2014/08/08
            //new OperationBLL().GetAllOperation().Where(p =>  lstOperationID.Contains(p.ControlID)).Select(p => p.ID).ToList();
            try
            {
                foreach (var operationID in lstOpID)
                {
                    RoleOperation ro = new RoleOperation();
                    ro.ID = Guid.NewGuid().ToString();
                    ro.RoleID = roleID;
                    ro.OperationID = operationID;
                    ro.CreateTime = DateTime.Now;
                    ro.Creator = createAccount;
                    service.Add(ro);
                }

                int res = service.GenericService.Save();
                return "true";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }

        public void RemoveRelationWithOpID(string opid)
        {
            var del = service.GenericService.FirstOrDefault(r => r.OperationID == opid);
            if (del != null)
                service.GenericService.Delete(del);
            return;
        }

        public void Add(RoleOperation model)
        {
            service.GenericService.Add(model);
            service.GenericService.Save();
        }
    }
}
