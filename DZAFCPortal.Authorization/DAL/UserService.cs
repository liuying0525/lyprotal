using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class UserService : AuthGenericService<User>
    {
        /// <summary>
        /// 判断用户是否存在，如果是新增，将id 设置为 string.Empry
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="id">账号的ID，默认值为 string.Empty 表示洗澡能</param>
        /// <returns></returns>
        public bool IsUserExsist(string account, string id)
        {
            var items = GenericService.GetAll(p => p.Account == account && p.ID == id);

            return items != null && items.Count() > 0;
        }

        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string getUserName(string account)
        {
            string name = "";
            var items = GenericService.GetAll(p => p.Account == account).ToList();
            if (items != null&&items.Count>0)
            {
               return name = items.FirstOrDefault().DisplayName;
            }
            return name;
        }

        /// <summary>
        /// 获取部门名字
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string getUserDepName(string account)
        {
            string depName = "";
            var items = GenericService.GetAll(p => p.Account == account).ToList();
            if (items != null && items.Count > 0)
            {
                return depName = items.FirstOrDefault().OrganizationName;
            }
            return depName;
        }


    }
}
