using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class ChooseUserAccout : System.Web.UI.UserControl
    {
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindUsers();
            }
        }

        /// <summary>
        /// 初始化绑定用户
        /// </summary>
        public void DataBindUsers()
        {
            var allUsers = userService.GenericService.GetAll(p => p.Status == 1).Distinct()
                       .Select(c => new { Account = c.Account, DisplayName = c.Account + "_" + c.DisplayName }).OrderBy(c => c.Account).ToList();
            selectManagerName.DataSource = allUsers;
            selectManagerName.DataValueField = "Account";
            selectManagerName.DataTextField = "DisplayName";
            selectManagerName.DataBind();
        }
    }
}