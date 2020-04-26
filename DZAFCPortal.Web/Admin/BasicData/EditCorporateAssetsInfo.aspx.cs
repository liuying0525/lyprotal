using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class EditCorporateAssetsInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropDownBind("BD_WPCommpany",ddlCompany,true,"-请选择-");
                dropDownBind("BD_WPType", ddlType, true, "-请选择-");
                if (!IsOperationCreate)
                {
                    txtCode.Enabled = false;
                    LoadItem();
                }
                
            }
        }
        #region Private Fields & Public Properties

        CorporateAssetsInfoService assetsInfoService = new CorporateAssetsInfoService();


        public bool IsOperationCreate
        {
            get
            {
                var para = Request["Op"];
                if (para == null)
                    throw new Exception("缺少Op参数，请联系管理员。");

                if (para == "Add") return true;
                else if (para == "Edit") return false;
                else throw new Exception("Op参数值不在范围内，请确认链接是否正确。");
            }
        }

        public string ID
        {
            get
            {
                return Request["ID"];
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsOperationCreate)
            {
                var isExitCode = assetsInfoService.GenericService.FirstOrDefault(r => r.Code == txtCode.Text.Trim()) != null;
                if (isExitCode)
                {
                    Fxm.Utility.Page.MessageBox.Show("物品编码已存在，请重新输入。");
                    return;
                }
            }

            Save();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(true);
        }

        #region Extension Method

        /// <summary>
        /// 更新实体数据
        /// </summary>
        private CorporateAssetsInfo CreateEditType()
        {

            var item = assetsInfoService.GenericService.GetModel(ID);
            item.Name = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.Company =ddlCompany.SelectedValue;
            item.Type = ddlType.SelectedValue;
            item.Preserver = txtPreserver.Text.Trim();
            item.Entrusted = txtEntrusted.Text.Trim();
            item.IsEnabled = ddlIsEnable.SelectedValue == "True";
            item.IsBorrow = ddlIsBorrow.SelectedValue == "True";
            item.Remark = txtRemark.Value.Trim();
            item.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            item.ModifyTime = DateTime.Now;
            return item;
        }

        /// <summary>
        /// 新增类别实体数据
        /// </summary>
        private CorporateAssetsInfo CreateNewType()
        {
            var item = new CorporateAssetsInfo();
            item.ID = Guid.NewGuid().ToString();
            item.Name = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.Company = ddlCompany.SelectedValue;
            item.Type = ddlType.SelectedValue;
            item.Preserver = txtPreserver.Text.Trim();
            item.Entrusted = txtEntrusted.Text.Trim();
            item.IsEnabled = ddlIsEnable.SelectedValue == "True";
            item.IsBorrow = ddlIsBorrow.SelectedValue == "True";
            item.Remark = txtRemark.Value.Trim();
            item.Creator = DZAFCPortal.Utility.UserInfo.Account;
            item.CreateTime = DateTime.Now;
            item.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            item.ModifyTime = DateTime.Now;

            return item;
        }


        /// <summary>
        /// 页面载入时加载数据
        /// </summary>
        private void LoadItem()
        {
            var item = assetsInfoService.GenericService.GetModel(Request["ID"].Trim());


            if (item != null)
            {
                txtName.Text = item.Name;
                txtCode.Text = item.Code.ToString();
                txtPreserver.Text = item.Preserver;
                txtEntrusted.Text = item.Entrusted;
                txtRemark.Value = item.Remark;
                ddlCompany.SelectedValue = item.Company;
                ddlType.SelectedValue = item.Type;
                ddlIsEnable.SelectedValue = item.IsEnabled ? "True" : "False";
                ddlIsBorrow.SelectedValue = item.IsBorrow ? "True" : "False";
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }

        private CorporateAssetsInfo GetModel()
        {

            if (IsOperationCreate)
                return CreateNewType();
            else
                return CreateEditType();
        }

        private void Save()
        {
            try
            {
                var model = GetModel();

                if (IsOperationCreate)
                {
                    assetsInfoService.GenericService.Add(model);
                }
                else assetsInfoService.GenericService.Update(model);

                assetsInfoService.GenericService.Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                
            }

            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
        }

        #endregion
    }
}