using DZAFCPortal.Entity;
using DZAFCPortal.ViewModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class OnlineVoteService : BizGenericService<OnlineVote>
    {
        public List<StaffHome_VM> GetHomeOnline()
        {
            List<StaffHome_VM> listHn = new List<StaffHome_VM>();
            var online = GenericService.GetAll().OrderByDescending(p => p.CreateTime).Take(2).ToList();
            foreach (OnlineVote na in online)
            {
                StaffHome_VM hn = new StaffHome_VM();
                hn.Id = na.ID;
                hn.Name = na.Title;
                hn.Type = "NY_Online.aspx";
                hn.CreateTime = na.CreateTime;
                listHn.Add(hn);
            }
            return listHn;
        }
    }
}
