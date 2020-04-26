using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;

namespace DZAFCPortal.Web.Client
{
    /// <summary>
    /// 导航数据源集成权限获取辅助类
    /// </summary>
    public class NavigatorGeneration
    {
        /// <summary>
        /// 获取导航数据源
        /// </summary>
        /// <returns></returns>
        public static List<Navigator> GenerateNavSource<S>(Expression<Func<Navigator, bool>> predicate,
                                                           Expression<Func<Navigator, S>> orderCondition,
                                                           bool isDesc = false)
        {
            NavigateService navService = new NavigateService();

            var curUserId = Utils.CurrentUser.ID;
            string[] roleIds = new RoleUserService().GenericService.GetAll(r => r.UserID == curUserId).Select(r => r.RoleID).ToArray();

            var navs = isDesc ?
                navService.GenericService.GetAll(predicate).OrderByDescending(orderCondition).ToList()
                :
                navService.GenericService.GetAll(predicate).OrderBy(orderCondition).ToList();

            //非超级管理员做筛选
            if (!DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(Utils.CurrentUser.Account))
            {
                for (int i = navs.Count - 1; i >= 0; i--)
                {
                    var nav = navs[i];
                    if (!string.IsNullOrEmpty(nav.ApplyRoles))
                    {

                        if (!IsNavVisible(nav.ApplyRoles.Split(','), roleIds))
                        {
                            navs.Remove(nav);
                            continue;
                        }
                    }
                }
            }

            return navs;
        }

        /// <summary>
        /// 判断导航是否有访问权限
        /// </summary>
        /// <param name="tempRoleId">导航本身的角色关联</param>
        /// <param name="actuallyRoleId">实际用户的角色关联</param>
        /// <returns></returns>
        public static bool IsNavVisible(string[] tempRoleId, string[] actuallyRoleId)
        {
            var count = (
                from id in actuallyRoleId
                where tempRoleId.Contains(id)
                select id
                ).Count();

            return count > 0;

        }

    }
}