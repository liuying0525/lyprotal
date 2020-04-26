
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Authorization
{
    public partial class Authorize : System.Web.UI.Page
    {
        DZAFCPortal.Authorization.BLL.OperationBLL operationService = new DZAFCPortal.Authorization.BLL.OperationBLL();
        DZAFCPortal.Authorization.BLL.RoleOperationBLL roleOperationService = new DZAFCPortal.Authorization.BLL.RoleOperationBLL();
        DZAFCPortal.Authorization.DAL.ModuleGroupService moduleGroupService = new DZAFCPortal.Authorization.DAL.ModuleGroupService();

        public List<Operation> lstAllOperation;
        /// <summary>
        /// 用户所能操作的模块
        /// </summary>
        public List<Operation> lstRoleOperation;
        private string roleID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["roleID"];
            roleID = id;


            lstAllOperation = operationService.GetAllOperation().Distinct(new OperationCodeComparer()).ToList();
            lstRoleOperation = operationService.GetRoleOperation(roleID);

            var groups = from r in lstAllOperation
                         group r by r.ModuleID into g
                         select g.Key;

            List<ModuleGroup> moduleGroupLST = new List<ModuleGroup>();


            foreach (var moduleID in groups)
            {
                var module = new DZAFCPortal.Authorization.DAL.ModuleService().GenericService.GetModel(moduleID);
                if (module == null) continue;
                var mg = moduleGroupService.GenericService.FirstOrDefault(m => m.ID == module.ModuleGroup_ID);
                if (mg == null) continue;
                if (moduleGroupLST.Find(m => m.ID == mg.ID) == null)
                    moduleGroupLST.Add(mg);
            }

            if (!Page.IsPostBack)
            {
                //表示默认角色
                if (DZAFCPortal.Config.AppSettings.DefaultRoles.Contains(new DZAFCPortal.Authorization.DAL.RoleService().GenericService.GetModel(roleID).Name))
                {
                    panel_1.Enabled = false;
                }

                panel_2.Visible = false;
                rptModuleGroup.DataSource = moduleGroupLST.OrderBy(p=>p.OrderNum).ToList();
                rptModuleGroup.DataBind();
            }

        }

        protected void rptModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Module module = (Module)e.Item.DataItem;
                    CheckBoxList chkList_Item = (CheckBoxList)e.Item.FindControl("chkList_Item");

                    if (chkList_Item != null && lstAllOperation != null)
                    {
                        chkList_Item.DataSource = lstAllOperation.Where(p => p.ModuleID == module.ID).OrderBy(p => p.OrderNum).ToList();

                        chkList_Item.DataTextField = "Name";
                        chkList_Item.DataValueField = "ID";
                        chkList_Item.DataBind();
                        SetCheckListBoxSelected(chkList_Item);
                    }
                }
            }
            catch
            {
                throw new Exception("绑定module出错");
            }
        }

        protected void rptModuleGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ModuleGroup mg = e.Item.DataItem as ModuleGroup;
                    Repeater rp = (Repeater)e.Item.FindControl("rptModule");
                    var modules = new List<Module>();
                    if (new DZAFCPortal.Authorization.DAL.RoleService().GenericService.GetModel(roleID).Name != "超级管理员")
                        modules = new DZAFCPortal.Authorization.DAL.ModuleService().GenericService.GetAll(m => m.ModuleGroup_ID == mg.ID && m.Code != "mokuaiguanli" && m.Code != "caozuoguanli").OrderBy(m => m.OrderNum).ToList();
                    else
                        modules = new DZAFCPortal.Authorization.DAL.ModuleService().GenericService.GetAll(m => m.ModuleGroup_ID == mg.ID).OrderBy(m => m.OrderNum).ToList();

                    rp.DataSource = modules;
                    rp.DataBind();
                }
            }
            catch
            {
                throw new Exception("绑定moduleGroup出错");
            }
        }

        private void SetCheckListBoxSelected(CheckBoxList chkList)
        {
            try
            {
                if (chkList != null)
                {
                    foreach (ListItem item in chkList.Items)
                    {
                        string opID = item.Value;
                        if (lstRoleOperation.Select(p => p.ID).Contains(opID))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("设置cbl选中项出错;");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            List<string> lstSelectedID = new List<string>();

            foreach (RepeaterItem groupItem in rptModuleGroup.Items)
            {
                Repeater rptModule = groupItem.FindControl("rptModule") as Repeater;
                foreach (RepeaterItem rptItem in rptModule.Items)
                {
                    CheckBoxList chkList = (CheckBoxList)rptItem.FindControl("chkList_Item");

                    foreach (ListItem item in chkList.Items)
                    {
                        if (item.Selected)
                        {
                            lstSelectedID.Add(item.Value);
                        }
                    }
                }
            }
            panel_1.Visible = false;
            panel_2.Visible = true;
            try
            {
                //这里需要注意，修改用户
                string res = roleOperationService.SetRoleOperation(roleID, lstSelectedID, "admin");
                if (res == "true")
                {
                    lbl_Result.Text = "授权成功！";
                }
                else
                {
                    lbl_Result.Text = "Error:设置失败";
                }
            }
            catch (Exception ex)
            {
                lbl_Result.Text = "Error:" + ex.Message;
            }
        }
        protected void btn_View_Click(object sender, EventArgs e)
        {
            // Server.Transfer((UrlHelper.FormatUrl(Request.Url.AbsoluteUri) + "/Authorization/AuthorizeSuccess.aspx?roleID=" + roleID));
        }


    }

    public class OperationCodeComparer : IEqualityComparer<Operation>
    {
        public bool Equals(Operation x, Operation y)
        {
            if (x == null)
                return y == null;
            return x.ControlID == y.ControlID && x.ModuleID == y.ModuleID;
        }

        public int GetHashCode(Operation obj)
        {
            if (obj == null)
                return 0;
            return obj.ControlID.GetHashCode();
        }
    }

    //public class ModuleGroupCodeComparer : IEqualityComparer<ModuleGroup>
    //{
    //    public bool Equals(ModuleGroup x, ModuleGroup y)
    //    {
    //        if (x == null)
    //            return y == null;
    //        return x.ID == y.ID;
    //    }

    //    public int GetHashCode(ModuleGroup obj)
    //    {
    //        if (obj == null)
    //            return 0;
    //        return obj.ID.GetHashCode();
    //    }
    //}
}