using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Utility;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class BasicDataCategoryList : BasePage
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

        #endregion

        #region ResourceGeneration & Binding


        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            //设置默认的查询条件
            Expression<Func<BnDictType, bool>> predicate = p => p.ID != ConstValue.EMPTY_GUID_STR;
            int count = 0;
            var categories = typeService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.Name, false).ToList();
            AspNetPager1.RecordCount = count;

            rptCategory.DataSource = categories;
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
            typeService.GenericService.Delete(id);

            var dics = dicService.GenericService.GetAll(p => p.BnDictTypeID == id).ToList();
            dics.ForEach(d =>
            {
                dicService.GenericService.Delete(d);
            });

            dicService.GenericService.Save();
            typeService.GenericService.Save();

            //删除后重新绑定数据
            GetDataAndBind();
        }


        #endregion
    }
}