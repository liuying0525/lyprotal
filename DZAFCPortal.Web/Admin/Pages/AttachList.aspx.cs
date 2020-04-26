using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages
{
    public partial class AttachList : System.Web.UI.Page
    {

        public Guid id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = new Guid(Request["id"]);
            if(!IsPostBack)
            {
                UploadAttach3.GetAttachAndBind();
            }
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    attachSave();
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>GetAttachList();</script>");
            attachSave();
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>GetAttachList();</script>");
            Fxm.Utility.Page.JsHelper.CloseWindow(false, "", "GetAttachList();");
        }

        private void attachSave()
        {
            try
            {
                UploadAttach3.SaveAttach();
                //Fxm.Utility.Page.MessageBox.Show("数据保存成功!");
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>Refresh();</script>");
                //Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!" + ex.Message);
            }
        }
    }
}