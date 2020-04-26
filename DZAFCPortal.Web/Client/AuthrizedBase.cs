using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client
{
    public class AuthrizedBase : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var navId = Request["CurNavId"];
            if (navId == null)
            {
                //return;
                Response.Write("缺少CurNavId参数，或参数不为指定的值");
                Response.End();
            }

            try
            {
                var nav = new DZAFCPortal.Service.NavigateService().GenericService.GetModel(navId);
                if (nav == null)
                    throw new Exception(string.Format("系统中无法检索[ID]为{0}的记录。", navId));

                var curUserId = Utils.CurrentUser.ID;
                var roleIds = new DZAFCPortal.Authorization.DAL.RoleUserService().GenericService.GetAll(r => r.UserID == curUserId).Select(r => r.RoleID).ToArray();

                if (
                    !DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(Utils.CurrentUser.Account)
                    &&
                    !string.IsNullOrEmpty(nav.ApplyRoles)
                    &&
                    !NavigatorGeneration.IsNavVisible(nav.ApplyRoles.Split(','), roleIds)
                    )
                    throw new Exception(string.Format("您没有权限访问该页面。", navId));

            }
            catch (Exception ex)
            {
                Response.Write("<h2>" + ex.Message + "</h2>");
                Response.End();
            }

        }


        /// <summary>
        /// 顶部ID
        /// </summary>
        protected string TopNavId
        {
            get
            {
                var topNavId = Request["TopNavId"];
                if (string.IsNullOrEmpty(topNavId))
                {
                    //return string.Empty;
                    Response.Write("缺少TopNavId参数，或参数不为指定的值");
                    Response.End();
                }
                return topNavId;
            }
        }

        /// <summary>
        /// 左侧ID
        /// </summary>
        protected string CurNavId
        {
            get
            {

                var curNavId = Request["CurNavId"];
                if (string.IsNullOrEmpty(curNavId))
                {
                    //return string.Empty;
                    Response.Write("缺少CurNavId参数，或参数不为指定的值");
                    Response.End();
                }
                return curNavId;
            }
        }

        BnDictTypeService typeService = new BnDictTypeService();
        BnDictService bnDictService = new BnDictService();
        public void dropDownBind(string Code, DropDownList droplist)
        {
            var BnType = typeService.GenericService.GetAll(p => p.Code == Code).First();
            var BnDict = bnDictService.GenericService.GetAll(p => p.BnDictTypeID == BnType.ID && p.IsVisible).OrderBy(p => p.OrderNum).ToList();

            var source = new Dictionary<string, string>();

            BnDict.ForEach(n =>
            {
                source.Add(n.Code, n.DisplayName);
            });

            droplist.DataSource = source;
            droplist.DataTextField = "value";
            droplist.DataValueField = "key";

            droplist.DataBind();
        }
    }
}