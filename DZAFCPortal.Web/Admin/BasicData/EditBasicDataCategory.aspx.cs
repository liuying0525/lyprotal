using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class EditBasicDataCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoles();
                if (!IsOperationCreate)
                {

                    txtCode.Enabled = false;
                    LoadItem();
                }
            }
        }

        #region Private Fields & Public Properties

        RoleService roleService = new RoleService();

        BnDictTypeService typeService = new BnDictTypeService();

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

        public string RecordID
        {
            get
            {
                return Request["ID"];
            }
        }

        public string AdminIds
        {
            get
            {
                var superAdminId = roleService.GenericService.FirstOrDefault(r => r.Code == "cjgly").ID;
                var sysAdminId = roleService.GenericService.FirstOrDefault(r => r.Code == "sysadmin").ID;

                return superAdminId + ";" + sysAdminId + ";";
            }
        }

        #endregion

        #region PageEvent Handle

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsOperationCreate)
            {
                var isDumplicatedCode = typeService.GenericService.FirstOrDefault(r => r.Code == txtCode.Text.Trim()) != null;
                if (isDumplicatedCode)
                {
                    Fxm.Utility.Page.MessageBox.Show("编码已存在，请重新输入。");
                    return;
                }
            }

            Save();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(true);
        }

        #endregion

        #region Extension Method

        /// <summary>
        /// 更新类别实体数据
        /// </summary>
        private BnDictType CreateEditType()
        {

            var item = typeService.GenericService.GetModel(RecordID);

            item.Name = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            item.ModifyTime = DateTime.Now;

            string ids = string.Empty;
            ids += AdminIds;
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                if (cblRoles.Items[i].Selected)
                {
                    ids += cblRoles.Items[i].Value + ";";
                }
            }
            item.AssociatedRoles = ids;

            return item;
        }

        /// <summary>
        /// 新增类别实体数据
        /// </summary>
        private BnDictType CreateNewType()
        {
            var item = new BnDictType();
            item.ID = Guid.NewGuid().ToString();

            item.Name = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.IsEdit = true;
            item.Creator = DZAFCPortal.Utility.UserInfo.Account;
            item.CreateTime = DateTime.Now;
            item.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            item.ModifyTime = DateTime.Now;

            string ids = string.Empty;
            ids += AdminIds;
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                if (cblRoles.Items[i].Selected)
                {
                    ids += cblRoles.Items[i].Value + ";";
                }
            }
            item.AssociatedRoles = ids;
            return item;
        }


        /// <summary>
        /// 页面载入时加载数据
        /// </summary>
        private void LoadItem()
        {
            var item = typeService.GenericService.GetModel(Request["ID"]);


            if (item != null)
            {
                txtName.Text = item.Name;
                txtCode.Text = item.Code.ToString();

                List<string> roleIds = item.AssociatedRoles.Split(';').ToList();
                if (roleIds.Count > 0)
                {
                    foreach (ListItem i in cblRoles.Items)
                    {
                        if (roleIds.Contains(i.Value))
                        {
                            i.Selected = true;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }

        private BnDictType GetDicType()
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
                var type = GetDicType();

                if (IsOperationCreate)
                {
                    typeService.GenericService.Add(type);
                }
                else typeService.GenericService.Update(type);

                typeService.GenericService.Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("字典类别保存出错！", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！", "refresh");
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        private void BindRoles()
        {
            cblRoles.DataSource = roleService.GenericService.GetAll(p => p.Code != "cjgly").OrderBy(r => r.OrderNum).ToList();
            cblRoles.DataTextField = "Name";
            cblRoles.DataValueField = "ID";
            cblRoles.DataBind();
        }
        #endregion
    }
}