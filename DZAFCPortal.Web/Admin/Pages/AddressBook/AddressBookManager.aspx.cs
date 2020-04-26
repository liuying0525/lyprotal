using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.AddressBook
{
    public partial class AddressBookManager : System.Web.UI.Page
    {
        public string DeputyManager_containerID { get { return SingeUserChooseControl1.ContainerID; } }

        public string BusinessManager_containerID { get { return SingeUserChooseControl2.ContainerID; } }

        public string deptManager_containerID { get { return SingeUserChooseControl3.ContainerID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}