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

namespace DZAFCPortal.Web.Admin.Category
{
    public partial class CategoryList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataAndBind();
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptCategory);
            }
        }

        #region--------Service 私有变量---------
        NewsCategoryService categorySerivce = new NewsCategoryService();

        #endregion

        #region----------页面私有变量----------
        protected String BackUrl
        {
            get
            {
                return @"/Category/CategoryList.aspx";
            }
        }

        #endregion

        #region--------页面控件的响应事件-----------
        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //   // Response.Redirect("AddCategory.aspx");
        //    AegonCMS.Utility.JsHelper.OepnWindow(this, "AddCategory.aspx");
        //}

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

        #endregion

        #region------------私有方法----------

        private void Delete(string id)
        {
            //if (HasChildren(id))
            //{
            //    AegonCMS.Utility.JsHelper.Alert(this, "该类别包含子项数据，无法删除。请先将子项数据删除");
            //    return;
            //}
            //else
            //{
            //    categorySerivce.Delete(new Guid(id));
            //    categorySerivce.Save();

            //    //删除后重新绑定数据
            //    GetDataAndBind();
            //}

            //if (HasContents(id))
            //{
            //    Fxm.Utility.Page.MessageBox.Show("该类别包含文章，无法删除。请先将该类别下文章删除");
            //    return;
            //}


            categorySerivce.GenericService.Delete(id);
            categorySerivce.GenericService.Save();

            ////对应删除模型数据
            //moduleService.Delete(tempID);
            //moduleService.Save();

            ////对应删除操作以及 移除角色和操作之间的关联
            //var ops = opService.GetAll(p => p.ModuleID == tempID).ToList();
            //if (ops != null && ops.Count > 0)
            //{
            //    foreach (var op in ops)
            //    {
            //        opService.Delete(op);

            //        //删除操作与角色的关联
            //        var roleOps = roleOpService.GetAll(p => p.OperationID == op.ID).ToList();
            //        if (roleOps != null && roleOps.Count > 0)
            //        {
            //            roleOps.ForEach(p => roleOpService.Delete(p));

            //            roleOpService.Save();
            //        }
            //    }

            //    opService.Save();
            //}

            //删除后重新绑定数据
            GetDataAndBind();
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            //设置默认的查询条件
            Expression<Func<NewsCategory, bool>> predicate = p => p.ID != ConstValue.EMPTY_GUID_STR;
            int count = 0;
            var categories = categorySerivce.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.OrderNum, false).ToList();
            AspNetPager1.RecordCount = count;

            rptCategory.DataSource = categories;
            rptCategory.DataBind();
        }

        /// <summary>
        /// 判断该类别是否包含子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HasChildren(string id)
        {
            var item = categorySerivce.GenericService.GetModel(id);
            return item != null && item.ID != Guid.Empty.ToString();
        }

        /// <summary>
        /// 判断当前类别下是否有新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public bool HasContents(string id)
        //{
        //    var item = new AegonCMS.BLL.ContentService().GetAll(p => p.CategoryId == new Guid(id)).FirstOrDefault();

        //    if (item == null) return false;
        //    else return true;
        //}

        /// <summary>
        /// 获取category关联的导航
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNavByID(string navId)
        {
            return new NavigateService().GenericService.GetModel(navId).Title;
        }

        public string GetDisplayMode(bool isMixDisplay)
        {
            return isMixDisplay ? "混合显示" : "单一显示";
        }
        #endregion
    }
}