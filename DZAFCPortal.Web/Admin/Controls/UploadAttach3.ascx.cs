using DZAFCPortal.Config.Enums;
using DZAFCPortal.Entity;
using DZAFCPortal.Service.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.DAL;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class UploadAttach3 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BindDropDownList();
                GetAttachAndBind();
                //AttachId = (this.Parent.FindControl("hidAttach") as HtmlInputHidden).Value;

            }
        }

        AttachService attachService = new AttachService();
        BnDictTypeService dicTypeService = new BnDictTypeService();
        BnDictService bnDicService = new BnDictService();

        public string ContractId
        {
            get
            {
                var id = Request["id"];
                if (id == null)
                {
                    Fxm.Utility.Page.MessageBox.Show("参数错误，请检查是否当前页面url或联系管理员");
                    return "";
                }
                else
                    return id;
            }
        }

        #region-----------对于附件的操作-----------
        //附件类型
        public AttachType AttachType { get; set; }
        public string AttachId { get; set; }
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="content_ID">外键ID</param>
        /// <returns></returns>
        private List<Attach> CreateAttachs(string content_ID)
        {
            var attachs = new List<Attach>();

            string[] attachIds = Request.Form.GetValues("hid_attach_id");
            string[] attachFileNames = Request.Form.GetValues("hid_attach_fileName");
            string[] attachFileShowNames = Request.Form.GetValues("hid_attach_fileShowName");
            string[] attachFileUrls = Request.Form.GetValues("hid_attach_fileUrl");
            string[] attachFileSizes = Request.Form.GetValues("hid_attach_fileSize");
            string[] attachFileExtension = Request.Form.GetValues("hid_attach_fileExtension");

            if (attachIds != null && attachFileNames != null && attachFileUrls != null
                && attachFileSizes != null && attachFileExtension != null
                && attachIds.Length > 0 && attachFileNames.Length > 0 && attachFileUrls.Length > 0
                && attachFileSizes.Length > 0 && attachFileExtension.Length > 0)
            {
                for (int i = 0; i < attachIds.Length; i++)
                {
                    var model = new Attach();

                    if (String.IsNullOrEmpty(attachIds[i]))
                    {

                    }
                    else
                    {
                        model.ID = attachIds[i];
                    }

                    model.ContentID = content_ID;
                    model.FileName = attachFileNames[i];
                    model.Url = attachFileUrls[i];
                    model.Size = Convert.ToInt64(attachFileSizes[i]);
                    model.FileShowName = attachFileShowNames[i];
                    model.Extension = attachFileExtension[i];

                    //model.Type = (int)AttachType;
                    //int type = int.Parse(dropType.SelectedValue);
                    model.Type = dropType.SelectedValue;
                    model.Creator = Utils.CurrentUser.DisplayName;
                    attachs.Add(model);
                }
            }

            return attachs;
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="content_ID"></param>
        public void SaveAttach()
        {
            //if (AttachType <= 0) throw new Exception("请设置AttachType属性");
            var attachs = CreateAttachs(ContractId);
            var attachsInDb = attachService.GenericService.GetAll(p => p.ContentID == ContractId && p.Type == dropType.SelectedValue).ToList();

            foreach (var item in attachsInDb)
            {
                if (attachs.FindIndex(p => p.ID == item.ID) < 0)
                {
                    attachService.GenericService.Delete(item);
                }
            }

            foreach (var item in attachs)
            {
                if (item.ID == Guid.Empty.ToString())
                {
                    item.ID = Guid.NewGuid().ToString();
                    attachService.GenericService.Add(item);
                }
            }

            attachService.GenericService.Save();
            GetAttachAndBind();
        }

        /// <summary>
        /// 在编辑页获取附件信息并绑定
        /// </summary>
        public void GetAttachAndBind()
        {
           // if (ContractId == null) { Fxm.Utility.Page.MessageBox.Show("参数错误，请检查当前页面的url或联系管理员。"); return; }

            var content_ID = ContractId;
            var type = dropType.SelectedValue;
            var attachs = attachService.GenericService.GetAll(p => p.ContentID == content_ID && p.Type == type).ToList();

            rptAttachList.DataSource = attachs;
            rptAttachList.DataBind();
        }

        protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAttachAndBind();
        }


        private void BindDropDownList()
        {
            var type = Request["type"];
            string code = "BD_SCAttachType";
            if(type=="pur")
            {
                code = "BD_PCAttachType";
            }
            var saleAttachType = dicTypeService.GenericService.GetAll(p => p.Code == code).FirstOrDefault();
            var dics = bnDicService.GenericService.GetAll(p => p.BnDictTypeID == saleAttachType.ID && p.IsVisible).OrderBy(p => p.OrderNum).ToList();

            var source = new Dictionary<string, string>();

            dics.ForEach(d =>
            {
                source.Add(d.Code, d.DisplayName);
            });

            dropType.DataSource = source;
            dropType.DataValueField = "key";
            dropType.DataTextField = "value";
            dropType.DataBind();



        }

        #endregion
    }
}