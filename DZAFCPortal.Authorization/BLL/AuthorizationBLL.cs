/******************************************/
//  文件名： AuthorizationBLL.cs
//  说明： 用户权限类，主要包括菜单、页面访问权限、页面元素访问权限的控制的数据的返回
//         该类不对页面UI做操作，仅通过权限返回数据
//  
/******************************************/

using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    /// <summary>
    /// 用户权限
    /// 主要包括菜单、页面访问权限、页面元素访问权限的控制
    /// </summary>
    public class Authorization
    {


        ModuleGroupService groupService = new ModuleGroupService();
        ModuleService moduleService = new ModuleService();

        /// <summary>
        /// 获取当前登录用户可访问的 GroupMoudel 列表，页面中一般显示在第一级菜单
        /// 注： 若用户权限配置有 ModuleGroup 但是该Group没有任何模块(Module)
        /// 则该 ModuleGroup 会排除出返回列表
        /// </summary>
        /// <returns>当前用户可访问的 ModuleGroup 列表</returns>
        //public List<ModuleGroup> GetUserGroup()
        //{
        //    // var moduleGroups=service.GetAll(p=>p.)
        //}

        ///// <summary>
        ///// 获取当前登录用户可访问的 Module(模块) 列表，页面中一般显示在为二级菜单（或以下）
        ///// 注： 若用户权限配置有 该模块权限，但是该模块下没有操作(Operate)
        ///// 则 该 Module 会排除出返回列表
        ///// </summary>
        ///// <returns></returns>
        //public List<Module> GetUserModuls()
        //{
        //     var models=
        //}

        ///// <summary>
        ///// 获取当前用户的操作
        ///// </summary>
        ///// <returns></returns>
        //public List<Module_Operation_RL> GetUserOperations()
        //{ 
        //       var model
        //}
    }
}
