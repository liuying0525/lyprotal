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

namespace DZAFCPortal.Web.Admin.Pages.EmployeeInfor
{
    public partial class EmployeeInforList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                dropDownBind("BD_PoliticalStatus", dropZZMM); //政治面貌
                dropZZMM.Items.Insert(0, new ListItem("全部", "All"));
                dropZZMM.SelectedValue = "All";
                GetDataAndBind();
            }
        }

        EmployeeInforService employeeInforService = new EmployeeInforService();
        UserService userService = new UserService();
        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rpt_content_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        private void Delete(string id)
        {
            try
            {
                var employee = employeeInforService.GenericService.GetModel(id);
                employeeInforService.GenericService.Delete(employee);
                employeeInforService.GenericService.Save();

                //删除后重新绑定数据
                GetDataAndBind();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("删除过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("删除活动风采[NY_ActivityList.aspx]", ex);
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
            var employee = employeeInforService.GenericService.GetListByPage(
                AspNetPager1.PageSize,
                AspNetPager1.CurrentPageIndex,
                out count,
                pre,
                new OrderByMultiCondition<EmployeeInfo, String>(c => c.NameAccount, false))
                //new OrderByMultiCondition<EmployeeInfo, Int64>(p => p.Int_ID, false))
                .ToList();
            if (count == 0)
            {
                Prompt.Visible = true;
                AspNetPager1.Visible = false;
            }
            AspNetPager1.RecordCount = count;
            rpt_content.DataSource = employee;
            rpt_content.DataBind();
            SingleOrgChooseControl.ChosenDeptName = hidDepartment.Value;
            SingleOrgChooseControl.chosenDeptname = hidDepartment.Value;
        }

        private Expression<Func<EmployeeInfo, bool>> GeneratePredicate()
        {

            Expression<Func<EmployeeInfo, bool>> predicate = p => true;
            //部门
            if (!string.IsNullOrEmpty(SingleOrgChooseControl.chosenDeptname))
            {
                predicate = predicate.And(p => p.DeptName.Contains(SingleOrgChooseControl.chosenDeptname));
                hidDepartment.Value = SingleOrgChooseControl.chosenDeptname;
            }
            //名字        
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                predicate = predicate.And(p => p.Name.Contains(txtName.Text.Trim()));
            }
            //职位btnExport_Click
            if (!string.IsNullOrEmpty(txtPostion.Text.Trim()))
            {
                predicate = predicate.And(p => p.Position.Contains(txtPostion.Text.Trim()));
            }
            //当前状态

            predicate = predicate.And(p => p.Enable.ToString() == ddlEmpStatus.SelectedValue);


            if (dropZZMM.SelectedValue != "All")
                predicate = predicate.And(p => p.PoliticalStatus == dropZZMM.SelectedValue);

            return predicate;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataAndBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (rpt_content.Items.Count == 0)
                Response.Write("<script>alert('请先查询，再导出！')</script>");
            else
            {
                var predicate = GeneratePredicate();
                var source = employeeInforService.GenericService.GetAll(predicate).OrderBy(p => p.OrderNum).ToList();
                //var source = (List<InternalAccountingRecord>)(rpt_content.DataSource);

                var expSoucre =
                    from s in source
                    select new
                    {
                        Name = GetUserName(s.NameAccount),
                        Sex = s.Sex.ToString() == "0" ? "男" : "女",
                        BirthDate = s.BirthDate != null ? DateTime.Parse(s.BirthDate.ToString()).ToString("yyyy-MM-dd") : "",
                        DeptName = GetDepName(s.NameAccount),
                        //LastDeptName = GetLDepName(s.NameAccount),
                        Position = s.Position,
                        RecruitNature = GetBasicDateName(s.RecruitNature),
                        Education = GetBasicDateName(s.Education),
                        EducationalLevel = s.EducationalLevel,
                        GraduatedSchool = s.GraduatedSchool,
                        Profession = s.Profession,
                        TrialStartTime = s.TrialStartTime != null ? DateTime.Parse(s.TrialStartTime.ToString()).ToString("yyyy-MM-dd") : "",
                        TrialEndTime = s.TrialEndTime != null ? DateTime.Parse(s.TrialEndTime.ToString()).ToString("yyyy-MM-dd") : "",
                        Enable = GetEnables(s.Enable.ToString()),
                        Creator = GetDisplayNameByAccount(s.Creator),
                        CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                        OutdutyDate = s.OutdutyDate.HasValue ? s.OutdutyDate.Value.ToString("yyyy-MM-dd") : ""
                    };

                List<BoundField> headerTexts = new List<BoundField>
                {
                    new BoundField{ HeaderText = "姓名", DataField = "Name" },
                    new BoundField{ HeaderText = "性别", DataField = "Sex" },
                    new BoundField{ HeaderText = "出生日期", DataField = "BirthDate" },
                    new BoundField{ HeaderText = "部门", DataField = "DeptName" },
                    new BoundField{ HeaderText = "原部门", DataField = "LastDeptName" },
                    new BoundField{ HeaderText = "职位", DataField = "Position" },
                    new BoundField{ HeaderText = "招聘性质", DataField = "RecruitNature" },
                    new BoundField{ HeaderText = "学历", DataField = "Education" },
                    new BoundField{ HeaderText = "学位", DataField = "EducationalLevel" },
                    new BoundField{ HeaderText = "毕业院校", DataField = "GraduatedSchool" },
                    new BoundField{ HeaderText = "专业", DataField = "Profession" },
                    new BoundField{ HeaderText = "试用开始日期", DataField = "TrialStartTime" },
                    new BoundField{ HeaderText = "试用结束日期", DataField = "TrialEndTime" },
                    new BoundField{ HeaderText = "当前状态", DataField = "Enable" },
                    new BoundField{ HeaderText = "离职日期", DataField = "OutdutyDate" }
                };

                ExcelHelper.ExportExcel(expSoucre, "员工信息数据", headerTexts);


            }
        }

        public string GetEnables(string Enable)
        {
            switch (Enable)
            {
                case "0":
                    return "离职";
                case "1":
                    return "在职";
                case "2":
                    return "其它";
            }
            return "";
        }

        /// <summary>
        /// 获取用户名字
        /// </summary>
        /// <param name="acount"></param>
        /// <returns></returns>
        public string GetUserName(string acount)
        {
            return userService.getUserName(acount);
        }

        /// <summary>
        /// 获取用户部门名字
        /// </summary>
        /// <param name="acount"></param>
        /// <returns></returns>
        public string GetDepName(string acount)
        {
            return userService.getUserDepName(acount);
        }

    

    }
}