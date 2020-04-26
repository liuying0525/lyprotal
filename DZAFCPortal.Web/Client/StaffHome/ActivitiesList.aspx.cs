using DZAFCPortal.Entity;
using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.StaffHome
{
    public partial class ActivitiesList : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataAndBind();
                GetDataAndBindall();
            }
        }

        #region--------Service 私有变量---------
        ActivitiesService eeSerivce = new ActivitiesService();
        ActivityParticipantsService esSerivce = new ActivityParticipantsService();
        ActivityTeamsService teamService = new ActivityTeamsService();
        StatusFacade nf = new StatusFacade();
        #endregion


        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            var Ny_Events = eeSerivce.GenericService.GetAll().OrderByDescending(p => p.BookBeginTime).Take(10).ToList();
            rptEvents.DataSource = Ny_Events.ToList();
            rptEvents.DataBind();
        }


        private void GetDataAndBindall()
        {
            System.Linq.Expressions.Expression<Func<Activities, bool>> pre = p => true;
            int count = 0;
            var Ny_Events = eeSerivce.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<Activities, DateTime>(c => c.BookBeginTime, true));
            AspNetPager1.RecordCount = count;
            repPreferent.DataSource = Ny_Events.ToList();
            repPreferent.DataBind();
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>active();</script>"); 
        }

        public string DateTimeConver(string datetime)
        {
            return nf.DateTimeConver(datetime);
        }
        public string Status(string BookBeginTime, string BookEndTime, string ActBeginTime, string ActEndTime)
        {
            return nf.Status(BookBeginTime, BookEndTime, ActBeginTime, ActEndTime);
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBindall();
        }

        public string GenerateContentUrl(string id)
        {
            return "ActivityDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id;
        }

        public string HaveSignedCount(string id, string type)
        {

            string count = "";
            if (type == "1")
            {
                var solicitationRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == id).ToList();
                count = solicitationRegister.Count.ToString();
            }
            else
            {
                var teamRegister = teamService.GenericService.GetAll(p => p.NY_EventSolicitationID == id).ToList();
                count = teamRegister.Count.ToString();
            }
            return count;
        }
    }
}