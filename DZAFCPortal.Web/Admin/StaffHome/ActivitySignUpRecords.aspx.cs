using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.StaffHome
{
    public partial class ActivitySignUpRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request["id"];
                GetRegAndBind(id);
            }
        }
        ActivityParticipantsService esSerivce = new ActivityParticipantsService();

        protected void rptNYFamil_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
                //case "Out": Out(e.CommandArgument.ToString()); break;
            }
        }

        private void Delete(string id)
        {
            try
            {
                var ee = esSerivce.GenericService.GetModel(id);
                esSerivce.GenericService.Delete(ee);
                esSerivce.GenericService.Save();
                string Id = Request["id"];
                //删除后重新绑定数据
                GetRegAndBind(Id);
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("删除过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("删除优惠活动[PreferentActionList.aspx]", ex);
            }
        }


        private void GetRegAndBind(string id)
        {
            var solicitationRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == id).OrderBy(p => p.RegistTime).ToList();
            rptRegister.DataSource = solicitationRegister;
            rptRegister.DataBind();

        }
    }
}