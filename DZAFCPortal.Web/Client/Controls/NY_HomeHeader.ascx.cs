
using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.Web;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class NY_HomeHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNavigator();
                Info();
                photoUser();
            }
        }
        NewsCategoryService categoryService = new NewsCategoryService();

        //NY_IndexNavigatorService navService = new NY_IndexNavigatorService();

        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();

        private NavigateService navService = new NavigateService();

        protected void rptNavigator_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //查找内嵌的Repeater
                var rptCategory = e.Item.FindControl("rptChildNav") as Repeater;

                var nav = e.Item.DataItem as Navigator;

                Expression<Func<Navigator, bool>> predicate = n => n.Enabled && n.ParentID == nav.ID;
                Expression<Func<Navigator, int>> orderCondition = n => n.OrderNum;

                var categories = NavigatorGeneration.GenerateNavSource(predicate, orderCondition);

                rptCategory.DataSource = categories;
                rptCategory.DataBind();
            }
        }

        /// <summary>
        /// 绑定导航栏
        /// </summary>
        void BindNavigator()
        {
            Expression<Func<Navigator, bool>> predicate = n => n.Enabled && n.ParentID == ConstValue.EMPTY_GUID_STR;
            Expression<Func<Navigator, int>> orderCondition = n => n.OrderNum;

            var source = NavigatorGeneration.GenerateNavSource(predicate, orderCondition);
            if (source != null && source.Count > 0)
            {
                rptNavigator.DataSource = source;
                rptNavigator.DataBind();
            }
        }


        /// <summary>
        /// 获取导航跳转至二级分类页面的URL
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="navigatorId"></param>
        /// <returns></returns>
        public string GetSecondaryUrl(string Id, string navigatorId, string Url)
        {
            //var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + "/Pages/";

            ////var nav = navService.GenericService.GetModel(new Guid(categoryId));


            //baseUrl += "SecondaryList.aspx";

            //baseUrl += "?NavigateId=" + navigatorId + "&ParentId=" + ParentId;

            //return baseUrl;

            var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + Url;
            if (Url.Contains("CategoryCode"))
            {
                baseUrl += "&TopNavId=" + navigatorId + "&CurNavId=" + Id;
            }
            else
            {
                baseUrl += "?TopNavId=" + navigatorId + "&CurNavId=" + Id;
            }
            return baseUrl;
        }
        public User CurrentUser
        {
            get
            {
                var user = new UserAuthorizationBLL().GetUserByAccount(UserInfo.Account);

                if (user == null)
                {
                    Response.Write("当前登录用户未同步到服务器中，请联系管理员!");
                    Response.End();
                }

                return user;
            }
        }



        private void photoUser()
        {
            string account = UserInfo.Account;
            var PhotoUrl = userService.GenericService.GetAll(p => p.Account == account).First().PhotoUrl;
            imgUser.ImageUrl = Convertphoto(PhotoUrl);
        }

        /// <summary>
        /// 转换头像数据
        /// </summary>
        /// <param name="photoBytes"></param>
        /// <returns></returns>
        private string Convertphoto(byte[] photoBytes)
        {
            string img = "";
            if (photoBytes != null)
            {
                img = "data:image/gif;base64," + Convert.ToBase64String(photoBytes);

            }
            else
            {
                img = "/Scripts/Client/images/ny_header_person_pic.png";
            }
            return img;
        }
        private void Info()
        {
            try
            {
                if (new DZAFCPortal.Authorization.DAL.RoleUserService().GenericService.GetAll(p => p.UserID == CurrentUser.ID).Count() > 0)
                {

                    AdminAccess.Visible = true;
                }
                else
                {

                    AdminAccess.Visible = false;
                }
            }
            catch
            {
                AdminAccess.Visible = false;
            }
        }
    }
}