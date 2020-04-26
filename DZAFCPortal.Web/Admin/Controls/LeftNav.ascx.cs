using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class LeftNav : System.Web.UI.UserControl
    {
        public string CurrentUser { get { return DZAFCPortal.Utility.UserInfo.Account; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            userModules = authorizationBLL.GetUserModules(CurrentUser, "NyAdmin");

            BindData();
        }

        List<Module> userModules;


        UserAuthorizationBLL authorizationBLL = new UserAuthorizationBLL();
        private void BindData()
        {
            ModuleGroupService service = new ModuleGroupService();
            //绑定一级分类
            service.GenericService.Include(p => p.Modules);

            var moduleGrops = service.GenericService.GetAll().OrderBy(p => p.OrderNum).ToList();
            for (int i = moduleGrops.Count - 1; i >= 0; i--)
            {
                var group = moduleGrops[i];

                //表示当前级别下没有可操作的项
                //去掉父级菜单
                if (userModules.Count(p => p.ModuleGroup_ID == group.ID) <= 0)
                {
                    moduleGrops.Remove(group);
                }
            }

            rpt_LeftNav.DataSource = moduleGrops;
            rpt_LeftNav.DataBind();
        }

        protected void rpt_LeftNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rpt_Children") as Repeater;//找到里层的repeater对象

                ModuleGroup moduleGroup = (ModuleGroup)e.Item.DataItem;//找到分类Repeater关联的数据项 

                rep.DataSource = userModules.FindAll(p => p.ModuleGroup_ID == moduleGroup.ID).OrderBy(p => p.OrderNum).ToList();
                rep.DataBind();
            }
        }

        protected string FormatUrl(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (Request.Url.AbsolutePath.ToLower().Contains("/_layouts/"))
            {
                return  DZAFCPortal.Config.Base.AdminBasePath + obj.ToString();
            }
            else
            {
                return obj.ToString();
            }
        }

        public string GetIconUrl(string icon)
        {
            if (String.IsNullOrEmpty(icon))
            {
                return @"\Scripts\images\navIcon\home_ico.png";
            }
            else return icon;
            // return @"\Scripts\images\navIcon\home_ico.png";
        }
    }
}