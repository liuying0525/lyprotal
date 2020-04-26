using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Authorization.DAL;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class CorporateAssetsInfoList :BasePage
    {
        CorporateAssetsInfoService assetsInfoService = new CorporateAssetsInfoService();
        BnDictService dicService = new BnDictService();
        BnDictTypeService typeService = new BnDictTypeService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropDownBind("BD_WPCommpany", ddlCompany, true, "-全部-");
                BindData();
            }
        }

        private void BindData()
        {
            int count = 0;
            Expression<Func<CorporateAssetsInfo, bool>> pre = p => true;
            if (!string.IsNullOrEmpty(ddlCompany.SelectedValue))
            {
                pre = pre.And(p => p.Company ==ddlCompany.SelectedValue);
            }
            var source = assetsInfoService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<CorporateAssetsInfo, DateTime>(p => p.ModifyTime)).ToList();
            rptCorporateAssetsInfo.DataSource = source;
            rptCorporateAssetsInfo.DataBind();

        }

        public string GetCommpany(string code)
        {
            try
            {
                var type = typeService.GenericService.FirstOrDefault(p => p.Code == "BD_WPCommpany");
                var dic = dicService.GenericService.FirstOrDefault(p => p.BnDictTypeID == type.ID && p.Code == code);
                return dic.DisplayName;
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        public string GetType(string code)
        {
            try
            {
                var type = typeService.GenericService.FirstOrDefault(p => p.Code == "BD_WPType");
                var dic = dicService.GenericService.FirstOrDefault(p => p.BnDictTypeID == type.ID && p.Code == code);
                return dic.DisplayName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        protected void rptCorporateAssetsInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        private void Delete(string id)
        {
            assetsInfoService.GenericService.Delete(id);
            assetsInfoService.GenericService.Save();

            //删除后重新绑定数据
            BindData();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}