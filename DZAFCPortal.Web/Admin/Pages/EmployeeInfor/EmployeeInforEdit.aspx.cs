using DZAFCPortal.Entity;
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
    public partial class EmployeeInforEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //指定附件类型
            UploadAttach.AttachType = DZAFCPortal.Config.Enums.AttachType.员工信息管理;
            if (!IsPostBack)
            {
                DataBindUsers();
                dropDownBind("BD_ZZRecruitNature", dropRecruitNature); //招聘性质
                dropDownBind("BD_ZPEducation", dropEducation); //学历
                dropDownBind("BD_PoliticalStatus", dropZZMM); //政治面貌

                dropEducation.Items.RemoveAt(0);
                dropEducation.SelectedValue = "BD_ZPEducation_04";
                if (!string.IsNullOrEmpty(ItemID))
                {
                    LoadItem();
                    selectApplyMan.Visible = false;
                    txtName.Visible = true;
                }

            }
        }

        EmployeeInforService employeeInforService = new EmployeeInforService();
        UserService userService = new UserService();

        #region---------控件与实体之间转换---------
        private EmployeeInfo GetNewItem()
        {

            var employee = new EmployeeInfo();
            if (!string.IsNullOrEmpty(ItemID))
            {
                employee = employeeInforService.GenericService.GetModel(ItemID);
            }

            employee.Name = userService.getUserName(selectApplyMan.Value);
            employee.NameAccount = selectApplyMan.Value;
            employee.Sex = int.Parse(dropSex.SelectedValue);
            if (!string.IsNullOrEmpty(txtBirthDate.Text.Trim()))
            {
                employee.BirthDate = DateTime.Parse(txtBirthDate.Text.Trim());
            }
            employee.DeptName = txtDeptName1.Value.Trim();
            //employee.DeptNameID = txtPosition.Text.Trim();
            employee.Position = txtPosition.Text.Trim();
            employee.RecruitNature = dropRecruitNature.SelectedValue;
            employee.Education = dropEducation.SelectedValue;
            employee.PoliticalStatus = dropZZMM.SelectedValue;

            employee.EducationalLevel = txtEducationalLevel.Text.Trim();
            employee.GraduatedSchool = txtGraduatedSchool.Text.Trim();
            employee.Profession = txtProfession.Text.Trim();
            if (!string.IsNullOrEmpty(txtGraduationTime.Text.Trim()))
            {
                employee.GraduationTime = DateTime.Parse(txtGraduationTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtGraduatesCheckTime.Text.Trim()))
            {
                employee.GraduatesCheckTime = DateTime.Parse(txtGraduatesCheckTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtInternshipStartTime.Text.Trim()))
            {
                employee.InternshipStartTime = DateTime.Parse(txtInternshipStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtInternshipEndTime.Text.Trim()))
            {
                employee.InternshipEndTime = DateTime.Parse(txtInternshipEndTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTrialStartTime.Text.Trim()))
            {
                employee.TrialStartTime = DateTime.Parse(txtTrialStartTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTrialEndTime.Text.Trim()))
            {
                employee.TrialEndTime = DateTime.Parse(txtTrialEndTime.Text.Trim());
            }
            employee.Creator = DZAFCPortal.Utility.UserInfo.Account;
            employee.Modifier = DZAFCPortal.Utility.UserInfo.Account;
            employee.Enable = int.Parse(dropEnable.SelectedValue);
            employee.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            if (!string.IsNullOrEmpty(txtOutdutyDate.Text))
                employee.OutdutyDate = DateTime.Parse(txtOutdutyDate.Text);
            else
                employee.OutdutyDate = null;
            return employee;

        }

        private void LoadItem()
        {
            var id = ItemID;
            var employee = employeeInforService.GenericService.GetModel(id);
            var ds = userService.GenericService.GetAll().Distinct()
            .Select(c => new { Account = c.Account, DisplayName = c.Account + "_" + c.DisplayName }).OrderBy(c => c.Account).ToList();
            var appindex = ds.FindIndex(c => c.Account == employee.NameAccount);
            if (employee == null)
            {
                Response.Write("未能获取数据，该数据可能已被删除！");
                Response.End();
            }
            selectApplyMan.SelectedIndex = appindex;
            txtName.Text = userService.getUserName(employee.NameAccount);
            dropSex.SelectedValue = employee.Sex.ToString();
            if (employee.BirthDate != null)
            {
                txtBirthDate.Text = DateTime.Parse(employee.BirthDate.ToString()).ToString("yyyy-MM-dd");
            }
            txtDeptName1.Value = userService.getUserDepName(employee.NameAccount);
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
            UploadAttach.GetAttachAndBind(id);
        }
        #endregion

        /// <summary>
        /// 初始化绑定用户
        /// </summary>
        public void DataBindUsers()
        {
            var ds = userService.GenericService.GetAll().Distinct()
                       .Select(c => new { Account = c.Account, DisplayName = c.Account + "_" + c.DisplayName }).OrderBy(c => c.Account).ToList();

            //var index = ds.FindIndex(c => c.Account == DZAFCPortal.Utility.UserInfo.Account);
            selectApplyMan.DataSource = ds;
            selectApplyMan.DataValueField = "Account";
            selectApplyMan.DataTextField = "DisplayName";
            selectApplyMan.DataBind();
            //selectApplyMan.Value = DZAFCPortal.Utility.UserInfo.Account;
            //selectApplyMan.SelectedIndex = index;
            //txtDeptName.Text = Utils.CurrentUser.Department;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ItemID))
                {
                    int count = employeeInforService.GenericService.GetAll(p => p.NameAccount == selectApplyMan.Value).Count();
                    if (count > 0)
                    {
                        Fxm.Utility.Page.MessageBox.Show("用户已存在!");
                        return;
                    }
                }
                var employee = GetNewItem();
                if (!string.IsNullOrEmpty(ItemID))
                {
                    employeeInforService.GenericService.Update(employee);
                }
                else
                {
                    employeeInforService.GenericService.Add(employee);
                }
                //保存附件
                UploadAttach.SaveAttach(employee.ID);
                employeeInforService.GenericService.Save();
                //Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh()");
                Fxm.Utility.Page.MessageBox.Show("保存成功!");
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("转正申请添加异常[EmployeeInforEdit.aspx]", ex);
            }
        }
    }
}