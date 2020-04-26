using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using DZAFCPortal.Authorization.DAL;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.InfoAccess
{
    public partial class InfoAccessList : System.Web.UI.Page
    {
        InforAccessService inforAccessService = new InforAccessService();
        NewsCategoryService categoryService = new NewsCategoryService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                GetDataAndBind();
            }
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            var pre = GeneratePredicate();
            int count = 0;
            Prompt.Visible = false;
            AspNetPager1.Visible = true;
            var positive = inforAccessService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<InforAccess, DateTime>(c => c.CreateTime, true)).ToList();
            if (count == 0)
            {
                Prompt.Visible = true;
                AspNetPager1.Visible = false;
            }
            AspNetPager1.RecordCount = count;
            rptAccess.DataSource = positive;
            rptAccess.DataBind();

        }

        private Expression<Func<InforAccess, bool>> GeneratePredicate()
        {

            Expression<Func<InforAccess, bool>> predicate = p => true;
            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()) && !string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                DateTime StartTime = DateTime.Parse(txtStartTime.Text.Trim() + " 00:00");
                DateTime EndTime = DateTime.Parse(txtEndTime.Text.Trim() + " 23:59");

                predicate = predicate.And(p => p.ModifyTime >= StartTime && p.ModifyTime < EndTime);
            }
            //名字
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                predicate = predicate.And(p => p.AccessName.Contains(txtName.Text.Trim()));
            }
            //部门
            if (!string.IsNullOrEmpty(txtDeptName.Text.Trim()))
            {
                predicate = predicate.And(p => p.AccessDepartment.Contains(txtDeptName.Text.Trim()));
            }
            //标题
            if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                predicate = predicate.And(p => p.Title.Contains(txtTitle.Text.Trim()));
            }
            return predicate;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (rptAccess.Items.Count == 0)
                Response.Write("<script>alert('请先查询，再导出！')</script>");
            else
            {
                var predicate = GeneratePredicate();
                var source = inforAccessService.GenericService.GetAll(predicate).OrderByDescending(p => p.CreateTime).ToList();
                //var source = (List<InternalAccountingRecord>)(rpt_content.DataSource);

                var expSoucre =
                    from s in source
                    select new
                    {
                        Title = s.Title,
                        AccessName = s.AccessName,
                        AccessDepartment = s.AccessDepartment,
                        CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                        Type = s.Type ==1 ?"正文":"附件",
                        AttachName = s.AttachName,
                    };

                List<BoundField> headerTexts = new List<BoundField>
                {
                    new BoundField{ HeaderText = "标题", DataField = "Title" },
                    new BoundField{ HeaderText = "访问者", DataField = "AccessName" },
                    new BoundField{ HeaderText = "部门", DataField = "AccessDepartment" },
                    new BoundField{ HeaderText = "访问时间", DataField = "CreateTime" },
                    new BoundField{ HeaderText = "正文/附件", DataField = "Type" },
                    new BoundField{ HeaderText = "附件名称", DataField = "AttachName" },
                };

                ExcelHelper.ExportExcel(expSoucre, "信息发布访问数据", headerTexts);


            }
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBind();
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            GetDataAndBind();
        }

        UserService userService = new UserService();
        public string GetDisplayNameByAccount(string account)
        {
            return userService.getUserName(account);
        }

        public string Type(string id)
        {
            var navs = categoryService.GenericService.GetAll(n => n.ID == id).First();
            return navs.Name;
        }
    }
}