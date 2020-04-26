using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.NavMgt
{
    public partial class NavigatorMgt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
        }
        public string SYSTEM_STARUP_TIME = DateTime.Now.ToString();
        public string GetRoles()
        {
            var roles = (from r in new DZAFCPortal.Authorization.DAL.RoleService().GenericService.GetAll(p => p.Code != "cjgly").OrderBy(p => p.CreateTime)
                         select new { r.ID, r.Name }).ToList();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(roles);

            return json;
        }
    }
}