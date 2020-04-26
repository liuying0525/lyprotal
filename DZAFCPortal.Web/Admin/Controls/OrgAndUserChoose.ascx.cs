using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NYPortal.Web.Controls
{
    public partial class OrgAndUserChoose : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选中的组织机构ID，多项以 , 隔开
        /// </summary>
        public string CheckedOrgIds
        {
            get
            {
                return hfdCheckedOrgIds.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedOrgIds.Value = value;
            }
        }

        /// <summary>
        /// 选中的用户ID，多项以 , 隔开
        /// </summary>
        public string CheckedUserIds
        {
            get
            {
                return hfdCheckedUserIds.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedUserIds.Value = value;
            }
        }

        //public string CheckedOrgsJson
    }
}