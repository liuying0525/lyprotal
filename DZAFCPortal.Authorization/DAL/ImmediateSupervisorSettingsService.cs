
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.DAL
{
    public class ImmediateSupervisorSettingsService : AuthGenericService<ImmediateSupervisorSettings>
    {
        public void SetImmediateSupervisor(string account, string userID)
        {
            if (String.IsNullOrEmpty(userID))
                throw new ArgumentNullException("userID不能为空");

            var supervisor = GetUserImmediateSupervisor(account);

            if (supervisor == null || supervisor.ID == string.Empty.ToString())
            {
                supervisor = new ImmediateSupervisorSettings();

                supervisor.ID = Guid.NewGuid().ToString();
                supervisor.Account = account;
                supervisor.UserID = userID;

                GenericService.Add(supervisor);
                GenericService.Save();
            }
            else
            {
                supervisor.UserID = userID;

                GenericService.Update(supervisor);
                GenericService.Save();
            }
        }

        public ImmediateSupervisorSettings GetUserImmediateSupervisor(string account)
        {
            return GenericService.GetAll(p => p.Account == account).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户上级领导(账号)
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public User GetSupervisorUser(string account)
        {
            var immediateSupervisorSettings = GetUserImmediateSupervisor(account);
            if (immediateSupervisorSettings != null)
                return new UserService().GenericService.GetModel(immediateSupervisorSettings.UserID);
            else return null;
        }
    }
}
