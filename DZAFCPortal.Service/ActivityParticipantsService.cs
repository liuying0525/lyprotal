
using DZAFCPortal.Entity;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class ActivityParticipantsService : BizGenericService<ActivityParticipants>
    {
        /// <summary>
        /// 判断活动名额是否已满
        /// </summary>
        /// <param name="guid">活动ID</param>
        /// <returns></returns>
        public bool isBookMax(string guid)
        {
            ActivitiesService eventSerivce = new ActivitiesService();
            bool result = false;
            var booktimeRegister = eventSerivce.GenericService.GetModel(guid);
            var teamRegister = GenericService.GetAll(p => p.NY_EventSolicitationID == guid).ToList();
            int MaxCount = booktimeRegister.MaxPersonCount;
            int UsedMaxCOunt = teamRegister.Count;
            if (teamRegister.Count >= MaxCount)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 判断当前账号是否已报名
        /// </summary>
        /// <param name="guid">活动ID</param>
        /// <param name="EventAccount">账号名字</param>
        /// <returns></returns>
        public bool isRegist(string guid, string EventAccount)
        {
            bool result = false;
            var solicitationRegister = GenericService.FirstOrDefault(p => p.NY_EventSolicitationID == guid && p.UserAccount == EventAccount);
            if (solicitationRegister != null)
            {
                if (solicitationRegister.UserAccount == UserInfo.Account
                    || solicitationRegister.EventAccount == EventAccount)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
