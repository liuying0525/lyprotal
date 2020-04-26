using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Repository
{
    public class InitData
    {
        public static void InitDbData(AuthorizationContext context)
        {
            #region-----添加Application------
            //后台
            var application_background = new Applications();
            application_background.ID = Guid.NewGuid().ToString();
            application_background.IsEnable = true;
            application_background.Code = "NyAdmin";
            application_background.Name = "后台管理";

            context.Application.Add(application_background);
            //前台
            var application_front = new Applications();
            application_front.ID = Guid.NewGuid().ToString();
            application_front.IsEnable = true;
            application_front.Code = "NyClient";
            application_front.Name = "前台管理";

            context.Application.Add(application_front);
            #endregion

            #region-----添加角色------
            var role = new Role();
            role.ID = Guid.NewGuid().ToString();
            role.ApplicationID = application_background.ID;
            role.Name = "超级管理员";
            role.Code = "cjgly";
            role.IsDelete = false;
            role.IsEnable = true;

            context.Role.Add(role);

            //create zhanxl
            //编排人员
            var role1 = new Role();
            role1.ID = Guid.NewGuid().ToString();
            role1.ApplicationID = application_background.ID;
            role1.Name = "编排人员";
            role1.Code = "GZBPRY";
            role1.IsDelete = false;
            role1.IsEnable = true;

            context.Role.Add(role1);

            //反馈人员
            var role2 = new Role();
            role2.ID = Guid.NewGuid().ToString();
            role2.ApplicationID = application_background.ID;
            role2.Name = "反馈人员";
            role2.Code = "FKRY";
            role2.IsDelete = false;
            role2.IsEnable = true;

            context.Role.Add(role2);

            //领导
            var role3 = new Role();
            role3.ID = Guid.NewGuid().ToString();
            role3.ApplicationID = application_background.ID;
            role3.Name = "领导";
            role3.Code = "LD";
            role3.IsDelete = false;
            role3.IsEnable = true;

            context.Role.Add(role3);

            var roleBgs = new Role();

            roleBgs.ID = Guid.NewGuid().ToString();
            roleBgs.ApplicationID = application_background.ID;
            roleBgs.Name = "会务管理办公室";
            roleBgs.Code = "hwglbgs";
            roleBgs.IsDelete = false;
            roleBgs.IsEnable = true;

            context.Role.Add(roleBgs);
            #endregion

            #region------添加用户-------
            User user = new User
            {
                Account = "spadmin",
                NamePY = "spadmin",
                ID = Guid.NewGuid().ToString(),
                FirstName = "admin",
                LastName = "sp",
                DisplayName = "管理员",
             
                Address = "",
                Email = "",
                MobilePhone = "",
                Gender = 1,
         
                Status = 1,
                
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                
                OrganizationID = Guid.NewGuid().ToString(),
            };

            context.User.Add(user);

            #endregion

            #region-------添加用户和角色的关系---------
            var roleUser = new RoleUser();
            roleUser.ID = Guid.NewGuid().ToString();
            roleUser.RoleID = role.ID;
            roleUser.UserID = user.ID;

            context.RoleUser.Add(roleUser);
            #endregion

            #region-------添加ModuleGroup------
            var modulGroup = new ModuleGroup()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "权限管理",
                Summary = "权限管理",
                Icon = "ico",
                OrderNum = 99,
            };

            context.ModuleGroup.Add(modulGroup);

            #endregion

            #region--------添加Module 以及其关联-------
            var moduleManage = new Module()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "模块管理",
                Code = "mokuaiguanli",
                Url = "/Authorization/Modules/ModuleManagement.aspx",
                IsEnable = true,
                IsDelete = false,
                OrderNum = 1,
                ModuleGroup_ID = modulGroup.ID,
                ApplicationID = application_background.ID
            };
            context.Module.Add(moduleManage);

            var OperationManage = new Module()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "操作管理",
                Code = "caozuoguanli",
                Url = "/Authorization/Operations/OperationManagement.aspx",
                IsEnable = true,
                IsDelete = false,
                OrderNum = 2,
                ModuleGroup_ID = modulGroup.ID,
                ApplicationID = application_background.ID
            };
            context.Module.Add(OperationManage);

            #endregion

            #region----添加Operation及与Role关联------

            /*********添加Operation***********/
            //模块管理 --> '管理'操作
            var Op_ModuleManage = new Operation
            {
                ID = Guid.NewGuid().ToString(),
                Name = "管理",
                Code = "ModuleManage_Manage_01",
                OrderNum = 1,
                ControlID = "ModuleManage_Manage_01",
                IsEnable = true,
                IsDelete = false,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                ModuleID = moduleManage.ID
            };

            context.Operation.Add(Op_ModuleManage);


            //操作管理 --> '管理'操作
            var Op_OperationManage = new Operation
            {
                ID = Guid.NewGuid().ToString(),
                Name = "管理",
                Code = "OperationManage_Manage_01",
                OrderNum = 1,
                ControlID = "OperationManage_Manage_01",
                IsEnable = true,
                IsDelete = false,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                ModuleID = OperationManage.ID
            };

            context.Operation.Add(Op_OperationManage);

            /*********添加Operation与Role的关联***********/
            var oP_modulemanage_Role_RL = new RoleOperation
            {
                ID = Guid.NewGuid().ToString(),
                OperationID = Op_ModuleManage.ID,
                RoleID = role.ID,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            };

            context.RoleOperation.Add(oP_modulemanage_Role_RL);


            var oP_operationmanage_Role_RL = new RoleOperation
            {
                ID = Guid.NewGuid().ToString(),
                OperationID = Op_OperationManage.ID,
                RoleID = role.ID,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            };

            context.RoleOperation.Add(oP_operationmanage_Role_RL);

            #endregion

            context.SaveChanges();
        }
    }
}
