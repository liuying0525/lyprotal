using DZAFCPortal.Service;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.EmployeeInfor
{
    public partial class EmployeeInforDisplay : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //指定附件类型
            UploadAttach.AttachType = DZAFCPortal.Config.Enums.AttachType.员工信息管理;
            if (!IsPostBack)
            {
                dropDownBind("BD_ZZRecruitNature", dropRecruitNature); //招聘性质
                dropDownBind("BD_ZPEducation", dropEducation); //学历
                dropDownBind("BD_PoliticalStatus", dropZZMM); //政治面貌

                dropEducation.Items.RemoveAt(0);
                dropEducation.SelectedValue = "BD_ZPEducation_04";
                if (!string.IsNullOrEmpty(ItemID))
                {
                    LoadItem();
                }

            }
        }
        EmployeeInforService employeeInforService = new EmployeeInforService();
        UserService userService = new UserService();
        OrganizationService orgService = new OrganizationService();
        private void LoadItem()
        {
          
            var employee = employeeInforService.GenericService.GetModel(ItemID);
            if (employee == null)
            {
                Response.Write("未能获取数据，该数据可能已被删除！");
                Response.End();
            }
            txtName.Text = userService.getUserName(employee.NameAccount);
            dropSex.SelectedValue = employee.Sex.ToString();
            if (employee.BirthDate != null)
            {
                txtBirthDate.Text = DateTime.Parse(employee.BirthDate.ToString()).ToString("yyyy-MM-dd");
            }
            txtDeptName.Text = userService.getUserDepName(employee.NameAccount);
            txtPosition.Text = employee.Position;
            dropRecruitNature.SelectedValue = employee.RecruitNature;
            dropEducation.SelectedValue = employee.Education;
            dropZZMM.SelectedValue = employee.PoliticalStatus;

            txtEducationalLevel.Text = employee.EducationalLevel;
            txtGraduatedSchool.Text = employee.GraduatedSchool;
            txtProfession.Text = employee.Profession;
            if (employee.GraduationTime != null)
            {
                txtGraduationTime.Text = DateTime.Parse(employee.GraduationTime.ToString()).ToString("yyyy-MM-dd");
            }
            if (employee.GraduatesCheckTime != null)
            {
                txtGraduatesCheckTime.Text = DateTime.Parse(employee.GraduatesCheckTime.ToString()).ToString("yyyy-MM-dd");
            }
            if (employee.InternshipStartTime != null)
            {
                txtInternshipStartTime.Text = DateTime.Parse(employee.InternshipStartTime.ToString()).ToString("yyyy-MM-dd");
            }
            if (employee.InternshipEndTime != null)
            {
                txtInternshipEndTime.Text = DateTime.Parse(employee.InternshipEndTime.ToString()).ToString("yyyy-MM-dd");
            }
            if (employee.TrialStartTime != null)
            {
                txtTrialStartTime.Text = DateTime.Parse(employee.TrialStartTime.ToString()).ToString("yyyy-MM-dd");
            }
            if (employee.TrialEndTime != null)
            {
                txtTrialEndTime.Text = DateTime.Parse(employee.TrialEndTime.ToString()).ToString("yyyy-MM-dd");
            }
            dropEnable.SelectedValue = employee.Enable.ToString();
            if (employee.OutdutyDate.HasValue)
                txtOutdutyDate.Text = employee.OutdutyDate.Value.ToString("yyyy-MM-dd");
            txtOrderNum.Text = employee.OrderNum.ToString();
            //加载附件
            UploadAttach.GetAttachAndBind(ItemID);
        }
    }
}