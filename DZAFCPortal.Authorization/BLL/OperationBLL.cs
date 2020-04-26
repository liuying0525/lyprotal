using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class OperationBLL
    {
        OperationService service = new OperationService();
        public List<Operation> GetRoleOperation(string roleID)
        {
            List<RoleOperation> lstRoleOperation = new RoleOperationBLL().GetRoleOperationByRoleID(roleID);
            if (lstRoleOperation == null)
            {
                return null;
            }

            List<string> lstOperationID = lstRoleOperation.Select(p => p.OperationID).ToList();
            return service.GenericService.GetAll(p => lstOperationID.Contains(p.ID)).ToList();
        }

        public List<Operation> GetModuleOperation(string moduleID)
        {

            return service.GenericService.GetAll(p => p.ModuleID == moduleID && p.IsDelete == false && p.IsEnable == true).OrderBy(p => p.OrderNum).ToList();
        }

        public List<Operation> GetAllOperation()
        {
            return service.GenericService.GetAll(p => p.IsDelete == false && p.IsEnable == true).OrderBy(p => p.OrderNum).ToList();
        }

        /// <summary>
        /// 根据predicate 查找operations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Operation> GetAllOperation(System.Linq.Expressions.Expression<Func<Operation, bool>> predicate)
        {
            var source = service.GenericService.GetAll(predicate);
            if (source == null || source.Count() == 0)
                return new List<Operation>();

            return source.OrderBy(o => o.OrderNum).ToList();

        }


        //Create By 唐万祯 at 2014/08/04
        /// <summary>
        /// 获取某个用户所有的操作
        /// </summary>
        /// <param name="accout">用户名</param>
        /// <returns></returns>
        public List<Operation> GetUserOperation(string accout)
        {
            var operations = new List<Operation>();

            var user = new DZAFCPortal.Authorization.DAL.UserService().GenericService.FirstOrDefault(p => p.Account == accout);
            if (user == null) return null;

            var roleUsers = new DZAFCPortal.Authorization.DAL.RoleUserService().GenericService.GetAll(p => p.UserID == user.ID);
            foreach (var roleUser in roleUsers)
            {
                var temp = GetRoleOperation(roleUser.RoleID);
                if (temp != null && temp.Count > 0)
                    operations.AddRange(temp);
            }

            //去除重复项目
            var tempOp = new List<Operation>();
            foreach (var item in operations)
            {
                if (tempOp.Find(p => p.ID == item.ID) == null)
                    tempOp.Add(item);
            }

            return tempOp;
        }


        /****************zhanxl added at 2015年1月15日14:39:47*******************/

        /// <summary>
        /// 根据ModuleID获取 operations added by zhanxl
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public List<Operation> GetOperationByModuleID(string moduleID)
        {
            var ops = service.GenericService.GetAll(o => o.ModuleID == moduleID && o.IsEnable);

            if (ops == null || ops.Count() == 0)
                return new List<Operation>();

            return ops.OrderBy(o => o.OrderNum).ToList();
        }




        public void Update(Operation model)
        {
            service.GenericService.Update(model);
            service.GenericService.Save();
        }

        public void Add(Operation model)
        {
            service.GenericService.Add(model);
            service.GenericService.Save();
        }

        public Operation GetByOpeationID(string id)
        {

            return service.GenericService.GetModel(id);
        }

        public void RemoveAll(string moduleID)
        {
            GetAllOperation(o => o.ModuleID == moduleID).ForEach(o =>
            {
                service.GenericService.Delete(o);
                service.GenericService.Save();
            });
        }

        public void Remove(Operation model)
        {
            service.GenericService.Delete(model);
            service.GenericService.Save();
        }

        public void Remove(string id)
        {
            service.GenericService.Delete(id);
            service.GenericService.Save();
        }

    }
}
