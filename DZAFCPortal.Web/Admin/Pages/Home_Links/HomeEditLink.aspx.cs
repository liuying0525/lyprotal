using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.Home_Links
{
    public partial class HomeAddLink : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitLoadPage();

                InitLoadRoles(cblApplayRoles);

                InitLoadRoles(cblRecommendRoles);

                if (Op == DZAFCPortal.Config.Enums.OperateEnum.Edit)
                {
                    LoadItem();
                }
            }

            //SetBrandTypeItemDissable();
        }
        #region--------Service 私有变量---------
        CommonLinkService linkSerivce = new CommonLinkService();
        //SMGPortal.BLL.PageLinkTypeService linkTypeService = new BLL.PageLinkTypeService();
        //SMGPortal.BLL.ApplyOrgPathService applyService = new BLL.ApplyOrgPathService();
        #endregion

        #region--------页面控件响应事件--------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetLinkItem();
                if (Op == DZAFCPortal.Config.Enums.OperateEnum.Add)
                {
                    linkSerivce.GenericService.Add(item);
                }
                else linkSerivce.GenericService.Update(item);

                linkSerivce.GenericService.Save();

                //保存通知公告应用范围的组织路径
                //var applyService = new SMGPortal.BLL.ApplyOrgPathService();
                //保存应用范围的组织路径
                //applyService.SavePaths(item.AppalyOrgPaths, item.ID, 2);
                //保存推荐范围的组织路路径
                //applyService.SavePaths(item.RecommendOrgPaths, item.ID, 3);

                //Fxm.Utility.Page.JsHelper.CloseWindow(true, "保存成功！");
                Fxm.Utility.Page.MessageBox.Show("保存成功！");
                //var linkTypeID = new Guid(dropLinkType.SelectedValue.Split(',')[0]);
                //Response.Redirect("LinkList.aspx?type=" + linkTypeID);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                //记录错误日志
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(false);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string type = Request["type"];
            //byte[] uploadFileBytes =  //Convert.FromBase64String(Request[fileIndexImgUrl.PostedFile.FileName]);
            var file = fileIndexImgUrl.PostedFile;
            var datas = new byte[file.ContentLength];
            var stream = file.InputStream;
            stream.Read(datas, 0, file.ContentLength);

            string uploadFileName = Guid.NewGuid() + "_" + System.IO.Path.GetFileName(fileIndexImgUrl.PostedFile.FileName);
            var savePath = "/Uploads/Ny_Images/";
            string fileFullName = savePath + uploadFileName;
            //string[] Extensions = { ".jpg" };
            //Result rt = Fxm.Utility.FileHelper.UploadToWeb(Extensions, fileIndexImgUrl.PostedFile, savePath, uploadFileName);
            var result = FileHelper.UploadDocument(datas, fileFullName, DZAFCPortal.Config.AppSettings.SharePointSiteUrl, DZAFCPortal.Config.Base.ImageExtensions);
            if (result.IsSucess)
            {
                imgIcon.ImageUrl = fileFullName;
                imgIcon.Visible = true;
                hidUpload.Value = fileFullName;
                if (type == "200")
                {
                    imgIcon.Width = 300;
                    imgIcon.Height = 100;
                }
                Fxm.Utility.Page.MessageBox.Show(result.Message);
            }
            else
            {
                Fxm.Utility.Page.MessageBox.Show(result.Message);
            }
        }

        /// <summary>
        /// 链接类别改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var linkTypeID = new Guid(dropLinkType.SelectedValue.Split(',')[0]);
            //var linkType = new PageLinkTypeService().GetModel(linkTypeID);

            ////表示该类别可以显示在常用链接
            //if (linkType.IsShowCommon)
            //{
            //    tr_IsShowCommon.Visible = true;
            //}
            //else
            //{
            //    tr_IsShowCommon.Visible = false;
            //    dropIsShowCommon.SelectedIndex = 0;
            //}
        }
        #endregion

        #region-------------控件与实体之间赋值操作------------
        /// <summary>
        /// 加载信息
        /// </summary>
        private void LoadItem()
        {
            var item = GetItemByID();

            txtName.Text = item.Name;

            imgIcon.ImageUrl =item.Icon;
            imgIcon.Visible = true;
            dropIsShowCommon.SelectedIndex = item.IsCommonLink ? 0 : 1;

            var typeIdValue = item.Type.ToString() + ",1";
            //dropLinkType.SelectedIndex = dropLinkType.Items.IndexOf(dropLinkType.Items.FindByValue(typeIdValue));

            txtUrl.Text = item.Url;
            txtPadUrl.Text = item.PadUrl;
            txtPhoneUrl.Text = item.PhoneUrl;
            txtDescription.Text = item.Description;
            txtOrderNum.Text = item.OrderNum.ToString();
            dropIsEnable.SelectedIndex = item.IsEnable ? 0 : 1;

            #region---------设置应用人群---------
            dropApplayType.SelectedValue = item.AppalyType.ToString();

            //MutiOrgAndUserControl.CheckedOrgIds = item.AppalyOrgs;
            //MutiOrgAndUserControl.CheckedOrgPaths = item.AppalyOrgPaths;
            //MutiOrgAndUserControl.CheckedUserIds = item.AppalyUsers;

            //加载选中的角色
            LoadSelectedRoles(cblApplayRoles, item.AppalyRoles);
            #endregion

            #region---------设置推荐人群---------
            dropRecommendType.SelectedValue = item.RecommendType.ToString();

            //MutiOrgAndUserControl1.CheckedOrgIds = item.RecommendOrgs;
            //MutiOrgAndUserControl1.CheckedUserIds = item.RecommendUsers;
            //MutiOrgAndUserControl1.CheckedOrgPaths = item.RecommendOrgPaths;

            //加载选中的角色
            LoadSelectedRoles(cblRecommendRoles, item.RecommendRoles);
            #endregion
        }

        /// <summary>
        /// 通过页面控件，创建一个实体
        /// </summary>
        /// <returns></returns>
        private CommonLink CreateEditPageLink()
        {
            var item = GetItemByID();

            item.Name = txtName.Text.Trim();
            item.Icon = imgIcon.ImageUrl;
            item.IsCommonLink = bool.Parse(dropIsShowCommon.SelectedValue);
            //item.Type = new Guid(dropLinkType.SelectedValue.Split(',')[0]);

            item.Url = txtUrl.Text.Trim();
            item.PadUrl = txtPadUrl.Text.Trim();
            item.PhoneUrl = txtPhoneUrl.Text.Trim();
            item.Description = txtDescription.Text.Trim();

            #region------------获取应用人群系列数据---------------
            item.AppalyType = int.Parse(dropApplayType.SelectedValue);

            if (item.AppalyType == 1)
            {
                //item.AppalyOrgs = MutiOrgAndUserControl.CheckedOrgIds;
                //item.AppalyUsers = MutiOrgAndUserControl.CheckedUserIds;
                //item.AppalyOrgPaths = MutiOrgAndUserControl.CheckedOrgPaths;

                item.AppalyRoles = String.Empty;
            }
            else if (item.AppalyType == 2)
            {
                item.AppalyOrgs = String.Empty;
                item.AppalyUsers = String.Empty;
                item.AppalyOrgPaths = String.Empty;

                item.AppalyRoles = GetRolesByCheckBoxList(cblApplayRoles);
            }
            else
            {
                item.AppalyOrgs = String.Empty;
                item.AppalyUsers = String.Empty;
                item.AppalyOrgPaths = String.Empty;

                item.AppalyRoles = String.Empty;
            }
            #endregion

            #region------------获取推荐人群系列数据---------------
            item.RecommendType = int.Parse(dropRecommendType.SelectedValue);

            if (item.RecommendType == 1)
            {
                //item.RecommendOrgs = MutiOrgAndUserControl1.CheckedOrgIds;
                //item.RecommendOrgPaths = MutiOrgAndUserControl1.CheckedOrgPaths;
                //item.RecommendUsers = MutiOrgAndUserControl1.CheckedUserIds;

                item.RecommendRoles = String.Empty;
            }
            else if (item.RecommendType == 2)
            {
                item.RecommendOrgs = String.Empty;
                item.RecommendOrgPaths = String.Empty;
                item.RecommendUsers = String.Empty;

                item.RecommendRoles = GetRolesByCheckBoxList(cblRecommendRoles);
            }
            else
            {
                item.RecommendOrgs = String.Empty;
                item.RecommendOrgPaths = String.Empty;
                item.RecommendUsers = String.Empty;

                item.RecommendRoles = String.Empty;
            }
            #endregion


            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            item.IsEnable = bool.Parse(dropIsEnable.SelectedValue);

            return item;
        }

        /// <summary>
        /// 通过页面控件，创建一个实体
        /// </summary>
        /// <returns></returns>
        private CommonLink CreateNewPageLink()
        {
            var item = new CommonLink();

            item.ID = Guid.NewGuid().ToString();
            item.Name = txtName.Text.Trim();
            item.Icon = imgIcon.ImageUrl;
            item.IsCommonLink = bool.Parse(dropIsShowCommon.SelectedValue);
            //item.Type = new Guid(dropLinkType.SelectedValue.Split(',')[0]);

            item.Url = txtUrl.Text.Trim();
            item.PadUrl = txtPadUrl.Text.Trim();
            item.PhoneUrl = txtPhoneUrl.Text.Trim();
            item.Description = txtDescription.Text.Trim();

            item.AppalyType = int.Parse(dropApplayType.SelectedValue);

            #region------------获取应用人群系列数据---------------
            item.AppalyType = int.Parse(dropApplayType.SelectedValue);

            if (item.AppalyType == 1)
            {
                //item.AppalyOrgs = MutiOrgAndUserControl.CheckedOrgIds;
                //item.AppalyUsers = MutiOrgAndUserControl.CheckedUserIds;
                //item.AppalyOrgPaths = MutiOrgAndUserControl.CheckedOrgPaths;

                item.AppalyRoles = String.Empty;
            }
            else if (item.AppalyType == 2)
            {
                item.AppalyOrgs = String.Empty;
                item.AppalyUsers = String.Empty;
                item.AppalyOrgPaths = String.Empty;

                item.AppalyRoles = GetRolesByCheckBoxList(cblApplayRoles);
            }
            else
            {
                item.AppalyOrgs = String.Empty;
                item.AppalyUsers = String.Empty;
                item.AppalyOrgPaths = String.Empty;

                item.AppalyRoles = String.Empty;
            }

            #endregion

            #region------------获取推荐人群系列数据---------------
            item.RecommendType = int.Parse(dropRecommendType.SelectedValue);

            if (item.RecommendType == 1)
            {
                //item.RecommendOrgs = MutiOrgAndUserControl1.CheckedOrgIds;
                //item.RecommendOrgPaths = MutiOrgAndUserControl1.CheckedOrgPaths;
                //item.RecommendUsers = MutiOrgAndUserControl1.CheckedUserIds;

                item.RecommendRoles = String.Empty;
            }
            else if (item.RecommendType == 2)
            {
                item.RecommendOrgs = String.Empty;
                item.RecommendOrgPaths = String.Empty;
                item.RecommendUsers = String.Empty;

                item.RecommendRoles = GetRolesByCheckBoxList(cblRecommendRoles);
            }
            else
            {
                item.RecommendOrgs = String.Empty;
                item.RecommendOrgPaths = String.Empty;
                item.RecommendUsers = String.Empty;

                item.RecommendRoles = String.Empty;
            }
            #endregion

            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            item.IsEnable = bool.Parse(dropIsEnable.SelectedValue);
            //item.Type = int.Parse(Request["Type"]);

            return item;
        }

        private CommonLink GetLinkItem()
        {
            switch (Op)
            {
                case DZAFCPortal.Config.Enums.OperateEnum.Add: return CreateNewPageLink();
                case DZAFCPortal.Config.Enums.OperateEnum.Edit: return CreateEditPageLink();
                default: return CreateNewPageLink();
            }
        }
        #endregion

        #region-------私有方法---------
        private CommonLink GetItemByID()
        {
            var id = Request["ID"];
            var item = linkSerivce.GenericService.GetModel(id);
            if (item == null)
            {
                Response.Write("找不到该ID数据，可能已被删除！");
                Response.End();
            }

            return item;
        }

        /// <summary>
        /// 初始化页面显示的信息
        /// </summary>
        private void InitLoadPage()
        {
            BindLinkTypes();

            //设置图标的大小
            imgIcon.Width = 63;
            imgIcon.Height = 63;
            //设置提示信息
            ltrImageIconInfo.Text = String.Format("(请上传{0} * {1}大小的 png格式图片 )", imgIcon.Width, imgIcon.Height);
        }

        /// <summary>
        /// 加载所有角色
        /// </summary>
        private void InitLoadRoles(CheckBoxList cbl)
        {
            var roles = new DZAFCPortal.Authorization.DAL.RoleService().GenericService.GetAll(p => p.IsEnable).OrderBy(p=>p.OrderNum);

            cbl.DataTextField = "Name";
            cbl.DataValueField = "ID";

            cbl.DataSource = roles.ToList();
            cbl.DataBind();
        }

        private void LoadSelectedRoles(CheckBoxList cbl, string applayRoles)
        {
            string[] roleIds = String.IsNullOrEmpty(applayRoles) ? new string[0] : applayRoles.Split(',');

            foreach (ListItem item in cbl.Items)
            {
                item.Selected = roleIds.Contains(item.Value);
            }
        }
        /// <summary>
        /// 获取应用角色
        /// </summary>
        private string GetRolesByCheckBoxList(CheckBoxList cbl)
        {
            var checkedRoles = String.Empty;

            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    checkedRoles += item.Value + ",";
                }
            }

            return checkedRoles;
        }

        /// <summary>
        /// 绑定分类下拉
        /// 下拉的 Value 为 ID 和是否可选组合
        /// 项以 , 隔开
        /// 例如：guid(值),1  :其中1表示可用  0：则表示禁用
        /// </summary>
        private void BindLinkTypes()
        {
            //var types = linkTypeService.GetList();

            //dropLinkType.Items.Clear();
            //foreach (var item in types)
            //{
            //    var listItem = new ListItem();
            //    listItem.Text = Server.HtmlDecode(item.Title);
            //    listItem.Value = item.ID + "," + (HasChildren(item.ID) ? "0" : "1");

            //    dropLinkType.Items.Add(listItem);
            //}
        }

        private void SetBrandTypeItemDissable()
        {
            //foreach (ListItem item in dropLinkType.Items)
            //{
            //    var enbale = item.Value.Split(',')[1] == "1";
            //    if (!enbale) item.Attributes.Add("disabled", "disabled");
            //}
        }

        //private bool HasChildren(Guid parentID)
        //{
        //    return linkTypeService.GetAll(p => p.ParentID == parentID).Count() > 0;
        //}
        #endregion
    }
}