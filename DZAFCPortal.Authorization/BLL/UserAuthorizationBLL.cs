using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class UserAuthorizationBLL
    {
        ApplicationBLL appBLL = new ApplicationBLL();
        UserService userSetvice = new UserService();
        RoleUserService roleUserService = new RoleUserService();
        RoleService roleService = new RoleService();
        OperationBLL opBLL = new OperationBLL();
        ModuleService moduleService = new ModuleService();
        DZAFCPortal.Authorization.DAL.OrganizationService orgService = new DZAFCPortal.Authorization.DAL.OrganizationService();

        public User GetUserByAccount(string account)
        {
            return userSetvice.GenericService.FirstOrDefault(p => p.Account == account);
        }

        //Create By 唐万祯 at 2014/08/06
        /// <summary>
        /// 获取用户所拥有的模块
        /// </summary>
        /// <param name="account">用户名称</param>
        /// <param name="appCode">应用程序的Code，如果取所有 该值为空</param>
        /// <returns></returns>
        public List<Module> GetUserModules(string account, string appCode)
        {
            Applications app = null;

            if (!String.IsNullOrEmpty(appCode))
            {
                app = appBLL.GetApplicationsByCode(appCode);
                if (app == null) throw new Exception(String.Format("数据库中不存在[Code]={0}的Application，请查看数据库中(SYS_Application表)是否有该记录", appCode));
            }

            var modules = new List<Module>();

            var ops = opBLL.GetUserOperation(account);
            if (ops != null)
            {
                var moduleIds = (from op in ops
                                 select op.ModuleID).Distinct().ToList();

                if (app == null)
                    modules = moduleService.GenericService.GetAll(p => moduleIds.Contains(p.ID)).ToList();
                else modules = moduleService.GenericService.GetAll(p => moduleIds.Contains(p.ID) && p.ApplicationID == app.ID).ToList();
            }

            return modules;
        }


       
        /// <summary>
        /// 通过操作，获取拥有这些操作的用户列表
        /// </summary>
        /// <param name="ops">操作集合</param>
        /// <returns></returns>
        public List<User> GetUsersByOpration(IEnumerable<string> ops)
        {
            if (ops != null)
            {
                //通过操作获取角色
                var roleIds = new DZAFCPortal.Authorization.DAL.RoleOperationService().GenericService.GetAll(p => ops.Contains(p.OperationID)).Select(p => p.RoleID);

                if (roleIds != null)
                {
                    //通过角色获取用户
                    var userIds = new DZAFCPortal.Authorization.DAL.RoleUserService().GenericService.GetAll(p => roleIds.Contains(p.RoleID)).Select(p => p.UserID);
                    //获取用户实体

                    if (userIds != null)
                    {
                        var users = new DZAFCPortal.Authorization.DAL.UserService().GenericService.GetAll(p => userIds.Contains(p.ID)).ToList();
                        if (users != null)
                        {
                            return users;
                        }
                    }
                }
            }

            return new List<User>();
        }

        public List<User> GetOrganizationUser(string orgID, int pageSize, int PageCurrent, out int PageCount)
        {
            List<string> lstUserID = new RoleUserService().GenericService.GetAll(p => p.OrganizationID == orgID).Select(p => p.UserID).ToList();
            var res = userSetvice.GenericService.GetAll(p => lstUserID.Contains(p.ID)).OrderBy(p => p.Account);
            PageCount = res.Count();
            return res.Skip((PageCurrent - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<User> GetDefaultOrganizationUser(string orgID, int pageSize, int PageCurrent, out int PageCount)
        {
            List<string> lstUserID = new RoleUserService().GenericService.GetAll(p => p.OrganizationID == orgID && p.IsDefault).Select(p => p.UserID).ToList();
            var res = userSetvice.GenericService.GetAll(p => lstUserID.Contains(p.ID)).OrderBy(p => p.Account);
            PageCount = res.Count();
            return res.Skip((PageCurrent - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<User> GetOrganizationUser(string orgID)
        {
            List<string> lstUserID = new RoleUserService().GenericService.GetAll(p => p.OrganizationID == orgID).Select(p => p.UserID).ToList();
            var res = userSetvice.GenericService.GetAll(p => lstUserID.Contains(p.ID)).OrderBy(p => p.Account);
            return res.ToList();
        }

        /// <summary>
        /// 根据角色获取用户
        /// </summary>
        /// <remarks>Created by zhanxl 2014年11月4日10:38:43</remarks>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<User> GetUserByRole(Role role)
        {
            List<User> lstUsers = new List<User>();
            var roleUser = roleUserService.GenericService.GetAll(r => r.RoleID == role.ID);
            if (roleUser == null || roleUser.Count() == 0)
                return null;

            roleUser.ToList().ForEach(r =>
            {
                lstUsers.Add(userSetvice.GenericService.GetModel(r.UserID));
            });

            return lstUsers;
        }

        /// <summary>
        /// 通过账号，获取该用户所属组织机构名称
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GetOrganizationName(string account)
        {
            string orgName = String.Empty;

            var user = GetUserByAccount(account);
            if (user != null)
            {
                if (user.OrganizationID != null)
                {
                    var org = orgService.GenericService.GetModel(user.OrganizationID);
                    if (org != null) orgName = org.Name;
                }
            }

            return orgName;
        }
    }
}
