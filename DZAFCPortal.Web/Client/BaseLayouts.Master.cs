using DZAFCPortal.Service;
using DZAFCPortal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Client
{
    public partial class BaseLayouts : System.Web.UI.MasterPage
    {
        NySoftland.Core.GenericService<Navigator> nav_srv = new NavigateService().GenericService;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLefNavs();

                var parent_id = Request["TopId"];
                if (!string.IsNullOrEmpty(parent_id))
                {
                    BindTopNavs(parent_id);
                }
            }
        }

        void BindLefNavs()
        {
            var top_five_navs = nav_srv.GetAll(n => n.ParentID == ConstValue.EMPTY_GUID_STR)
                                       .OrderBy(n => n.OrderNum)
                                       .Take(5)
                                       .ToList();

            rptLeftNav.DataSource = top_five_navs;
            rptLeftNav.DataBind();
        }

        void BindTopNavs(string parent_id)
        {

            var navs = nav_srv.GetAll(n => n.ParentID == parent_id)
                              .OrderBy(n => n.OrderNum)
                              .ToList(); ;

            rptTopNav.DataSource = navs;
            rptTopNav.DataBind();
        }

        protected void rptLeftNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //var current_nav = e.Item.DataItem as Navigator;

                //var navs = nav_srv.GetAll(n => n.ParentID == current_nav.ID)
                //                  .OrderBy(n => n.OrderNum)
                //                  .ToList(); ;

                //rptTopNav.DataSource = navs;
                //rptTopNav.DataBind();
            }
        }
    }
}