using DZAFCPortal.Entity;
using System.Collections.Generic;
using System.Linq;
using DZAFCPortal.ViewModel.Client;

namespace DZAFCPortal.Service
{
    public class HighlightsService : BizGenericService<Highlights>
    {
        public List<StaffHome_VM> GetHighlights()
        {
            List<StaffHome_VM> listHn = new List<StaffHome_VM>();
            var auction = GenericService.GetAll().OrderByDescending(p => p.CreateTime).Take(2).ToList();
            foreach (Highlights na in auction)
            {
                StaffHome_VM hn = new StaffHome_VM();
                hn.Id = na.ID;
                hn.Name = na.AuctionName;
                hn.Type = "HighlightList.aspx";
                hn.CreateTime = na.CreateTime;
                listHn.Add(hn);
            }
            return listHn;
        }
    }
}
