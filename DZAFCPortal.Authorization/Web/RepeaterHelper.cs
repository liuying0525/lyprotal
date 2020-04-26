
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Authorization.Web
{
    public class RepeaterHelper
    {
        public static void RepeaterAuthorization(Repeater rpt)
        {
            if (rpt == null)
            {
                return;
            }

            foreach (RepeaterItem item in rpt.Items)
            {
                new ControlHelper(UserInfo.Account).ControlAuthorization(item.Controls);
            }
        }
    }
}
