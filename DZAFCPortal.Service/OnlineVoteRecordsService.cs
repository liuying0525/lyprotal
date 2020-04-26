
using DZAFCPortal.Entity;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class OnlineVoteRecordsService : BizGenericService<OnlineVoteRecords>
    {
        /// <summary>
        /// 判断是否已投票
        /// </summary>
        /// <param name="guid">评选ID</param>
        /// <returns></returns>
        public bool IsOnline(string guid)
        {
            bool result = false;
            string userName = UserInfo.Account;
            var od = GenericService.GetAll().Where(p => p.OnlineVoteID == guid && p.UserName == userName).ToList();
            if (od.Count > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
