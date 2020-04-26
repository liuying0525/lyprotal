using DZAFCPortal.Config.Enums;
using DZAFCPortal.Entity;
using DZAFCPortal.Service.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class UploadAttach2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        AttachService attachService = new AttachService();

        #region-----------对于附件的操作-----------
        //附件类型
        public AttachType AttachType { get; set; }

        public string AttachDiv { get; set; }

        private string containerID = "container_" + Guid.NewGuid().ToString().Replace('-', '_');

        /// <summary>
        /// 容器ID
        /// </summary>
        public string ContainerID
        {
            get
            {
                return containerID + "_" + AttachDiv;
            }
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="content_ID">外键ID</param>
        /// <returns></returns>
        private List<Attach> CreateAttachs(string content_ID, int Type)
        {
            var attachs = new List<Attach>();

            string[] attachIds = Request.Form.GetValues("hid_attach_id");
            string[] attachFileNames = Request.Form.GetValues("hid_attach_fileName");
            string[] attachFileShowNames = Request.Form.GetValues("hid_attach_fileShowName");
            string[] attachFileUrls = Request.Form.GetValues("hid_attach_fileUrl");
            string[] attachFileSizes = Request.Form.GetValues("hid_attach_fileSize");
            string[] attachFileExtension = Request.Form.GetValues("hid_attach_fileExtension");
            string[] attachType = Request.Form.GetValues("hid_attach_type");

            if (attachIds != null && attachFileNames != null && attachFileUrls != null
                && attachFileSizes != null && attachFileExtension != null
                && attachIds.Length > 0 && attachFileNames.Length > 0 && attachFileUrls.Length > 0
                && attachFileSizes.Length > 0 && attachFileExtension.Length > 0)
            {
                for (int i = 0; i < attachIds.Length; i++)
                {
                    if (attachType[i] == Type.ToString())
                    {
                        var model = new Attach();

                        if (String.IsNullOrEmpty(attachIds[i]))
                        {
                        }
                        else
                        {
                            model.ID =attachIds[i];
                        }

                        model.ContentID = content_ID;
                        model.FileName = attachFileNames[i];
                        model.Url = attachFileUrls[i];
                        model.Size = Convert.ToInt64(attachFileSizes[i]);
                        model.FileShowName = attachFileShowNames[i];
                        model.Extension = attachFileExtension[i];

                        model.Type = ((int)AttachType).ToString();

                        attachs.Add(model);
                    }

                }
            }

            return attachs;
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="content_ID"></param>
        public void SaveAttach(string content_ID)
        {
            if (AttachType <= 0) throw new Exception("请设置AttachType属性");

            var attachs = CreateAttachs(content_ID, (int)AttachType);
            var attachsInDb = attachService.GenericService.GetAll(p => p.ContentID == content_ID && p.Type == ((int)AttachType).ToString()).ToList();

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
        }

        /// <summary>
        /// 在编辑页获取附件信息并绑定
        /// </summary>
        public void GetAttachAndBind(string content_ID)
        {
            if (AttachType <= 0) throw new Exception("请设置AttachType属性");

            var attachs = attachService.GenericService.GetAll(p => p.ContentID == content_ID && p.Type == ((int)AttachType).ToString()).ToList();
            rptAttachList.DataSource = attachs;
            rptAttachList.DataBind();
        }

        public int GetAttchCount(string content_ID)
        {
            if (AttachType <= 0) throw new Exception("请设置AttachType属性");

            var attachs = attachService.GenericService.GetAll(p => p.ContentID == content_ID && p.Type == ((int)AttachType).ToString()).ToList();
            return attachs.Count();
        }
        #endregion
    }
}