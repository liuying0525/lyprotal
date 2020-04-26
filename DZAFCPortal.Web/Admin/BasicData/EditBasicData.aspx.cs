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
    public partial class EditBasicData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdownlist();

                if (!IsOperationCreate)
                {
                    txtCode.Enabled = false;
                    LoadItem();
                }
                else
                {
                    var specifiedTypeId = Request["DicTypeId"];
                    ddlDicType.SelectedValue = specifiedTypeId;
                }
            }
        }

        #region Private Fields & Public Properties

        BnDictTypeService typeService = new BnDictTypeService();

        BnDictService dicService = new BnDictService();

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

        #endregion

        #region PageEvent Handle

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsOperationCreate)
            {
                var isDumplicatedCode = dicService.GenericService.FirstOrDefault(r => r.Code == txtCode.Text.Trim()) != null;
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
        private BnDict CreateEditType()
        {

            var item = dicService.GenericService.GetModel(RecordID);

            item.DisplayName = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.BnDictTypeID = ddlDicType.SelectedValue;
            item.OrderNum = int.Parse(txtSortNum.Text);
            item.IsVisible = ddlIsVisible.SelectedValue == "True";

            item.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            item.ModifyTime = DateTime.Now;
            return item;
        }

        /// <summary>
        /// 新增类别实体数据
        /// </summary>
        private BnDict CreateNewType()
        {
            var item = new BnDict();
            item.ID = Guid.NewGuid().ToString();

            item.ParentID = Guid.Empty.ToString();
            item.DisplayName = txtName.Text.Trim();
            item.Code = txtCode.Text.Trim();
            item.BnDictTypeID = ddlDicType.SelectedValue;
            item.OrderNum = int.Parse(txtSortNum.Text);
            item.IsVisible = ddlIsVisible.SelectedValue == "True";

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
            var item = dicService.GenericService.GetModel(Request["ID"].Trim());


            if (item != null)
            {
                txtName.Text = item.DisplayName;
                txtCode.Text = item.Code.ToString();
                txtSortNum.Text = item.OrderNum.ToString();
                ddlDicType.SelectedValue = item.BnDictTypeID;
                ddlIsVisible.SelectedValue = item.IsVisible ? "True" : "False";
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }

        private BnDict GetDic()
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
                var type = GetDic();

                if (IsOperationCreate)
                {
                    dicService.GenericService.Add(type);
                }
                else dicService.GenericService.Update(type);

                dicService.GenericService.Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("字典类别保存出错！", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh()");
        }


        private void BindDropdownlist()
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
            if (types.Count == 0)
            {
                Response.Write("当前账户无权限访问任何字典类别，请联系管理员处理。");
                return;
            }

            Dictionary<string, string> d = new Dictionary<string, string>();

            types.ForEach(s =>
            {
                d.Add(s.ID, s.Name);
            });

            ddlDicType.DataSource = d;

            ddlDicType.DataTextField = "value";
            ddlDicType.DataValueField = "key";
            ddlDicType.DataBind();
        }

        #endregion
    }
}