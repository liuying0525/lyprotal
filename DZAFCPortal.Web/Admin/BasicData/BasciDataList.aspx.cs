using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using NySoftland.Core.Repositorys;
//using NySoftland.Core.Repositorys;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class BasciDataList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropdownlist();
                GetDataAndBind();
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptCategory);
            }
        }

        #region Private Fields

        BnDictTypeService typeService = new BnDictTypeService();

        BnDictService dicService = new BnDictService();

        #endregion

        #region PageEvent Handle

        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "AddChildren": ShowAddChildrenPage(e.CommandArgument.ToString()); break;
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }

        /// <summary>
        /// 执行分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBind();
        }

        protected void ddlSearchDicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedDicType"] = ddlSearchDicType.SelectedValue;

            GetDataAndBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataAndBind();
        }

        #endregion

        #region ResourceGeneration & Binding


        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            int count = 0;
            var pre = GeneratePredicate();
            var source = dicService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<BnDict, string>(p => p.BnDictTypeID), new OrderByMultiCondition<BnDict, int>(p => p.OrderNum)).ToList();
            //.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.OrderNum, false).ToList();

            rptCategory.DataSource = source;
            rptCategory.DataBind();
        }

        #endregion

        #region Extension Method

        /// <summary>
        /// 删除字典类别的同时删除该类别下的字典数据
        /// </summary>
        /// <param name="id"></param>
        private void Delete(string id)
        {
            dicService.GenericService.Delete(id);
            dicService.GenericService.Save();

            //删除后重新绑定数据
            GetDataAndBind();
        }

        public string GetTypeNameByID(object typeId)
        {
            return GetTypeModelByID(typeId).Name;
        }

        public string GetTypeCodeByID(object typeId)
        {
            return GetTypeModelByID(typeId).Code;
        }

        public BnDictType GetTypeModelByID(object typeId)
        {
            var id = typeId.ToString();
            return typeService.GenericService.FirstOrDefault(t => t.ID == id);
        }

        void BindDropdownlist()
        {
            var curUserAccount = DZAFCPortal.Utility.UserInfo.Account;

            var curUser = new UserService().GenericService.FirstOrDefault(u => u.Account == curUserAccount);

            var roleIds = new RoleUserService().GenericService.GetAll(r => r.UserID == curUser.ID).Select(r => r.RoleID).ToList();

            var superAdminId = new RoleService().GenericService.FirstOrDefault(r => r.Code == "cjgly").ID;
            var sysAdminId = new RoleService().GenericService.FirstOrDefault(r => r.Code == "sysadmin").ID;

            var types = typeService.GenericService.GetAll().OrderBy(t => t.Name).ToList();
            if (!roleIds.Contains(superAdminId) && !roleIds.Contains(sysAdminId))
            {
                for (int i = types.Count - 1; i >= 0; i--)
                {
                    var item = types[i];
                    var rIds = item.AssociatedRoles.Split(';').ToList();
                    if (rIds.Intersect(roleIds).Count() == 0)
                        types.Remove(item);

                }
            }


            Dictionary<string, string> d = new Dictionary<string, string>();

            if (types.Count == 0)
            {
                Response.Write("当前账户无权限访问任何字典类别，请联系管理员处理。");
                return;
            }

            types.ForEach(t =>
            {
                d.Add(t.ID, t.Name);
            });

            ddlSearchDicType.DataValueField = "key";
            ddlSearchDicType.DataTextField = "value";
            ddlSearchDicType.DataSource = d;
            ddlSearchDicType.DataBind();
        }

        Expression<Func<BnDict, bool>> GeneratePredicate()
        {
            var selectedTypeId =
                ViewState["SelectedDicType"] == null ?
                ddlSearchDicType.SelectedValue : ViewState["SelectedDicType"].ToString();

            return p => p.BnDictTypeID == selectedTypeId;
        }
        #endregion

      


    }
}