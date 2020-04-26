using DZAFCPortal.Entity;
using DZAFCPortal.ViewModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class ActivitiesService : BizGenericService<Activities>
    {
        public List<StaffHome_VM> GetHomeEvent()
        {
            List<StaffHome_VM> listHn = new List<StaffHome_VM>();
            var ee = GenericService.GetAll().OrderByDescending(p => p.CreateTime).Take(2).ToList();
            foreach (Activities na in ee)
            {
                StaffHome_VM hn = new StaffHome_VM();
                hn.Id = na.ID;
                hn.Name = na.Name;
                hn.Type = "NY_Home_event.aspx";
                hn.CreateTime = na.CreateTime;
                listHn.Add(hn);
            }
            return listHn;
        }

        /// <summary>
        /// 判断是否已经超过或未到报名时间
        /// </summary>
        /// <param name="guid">活动id</param>
        /// <returns></returns>
        public bool isBookTime(string guid)
        {
            bool result = false;
            DateTime Bookbegintime;
            DateTime Bookendtime;
            var booktimeRegister = GenericService.GetModel(guid);
            Bookbegintime = booktimeRegister.BookBeginTime;
            Bookendtime = booktimeRegister.BookEndTime;
            if (DateTime.Now > Bookendtime || DateTime.Now < Bookbegintime)
            {
                result = true;
            }
            return result;
        }
    }
}
