<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EmployeeInforDisplay.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.EmployeeInfor.EmployeeInforDisplay" %>

<%@ Register Src="../../Controls/UploadAttach.ascx" TagPrefix="uc1" TagName="UploadAttach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg" style="padding-bottom: 20px;">
        <input type="hidden" id="hidAttach" runat="server" />
        <input type="hidden" id="hidDelAttachID" runat="server" />
        <table class="table table-bordered" id="formSales">
            <tr>
                <td style="text-align: right;">姓名：
                </td>
                <td style="width: 201px">
                    <asp:TextBox ID="txtName" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">性别：
                </td>
                <td>
                    <asp:DropDownList ID="dropSex" runat="server" Enabled="false">
                        <asp:ListItem Value="0">男</asp:ListItem>
                        <asp:ListItem Value="1">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">出生日期：
                </td>
                <td>
                    <asp:TextBox ID="txtBirthDate" runat="server" onclick="dpBirthDate()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">部门：
                </td>
                <td>
                    <asp:TextBox ID="txtDeptName" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">职位：
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">招聘性质：
                </td>
                <td>
                    <asp:DropDownList ID="dropRecruitNature" runat="server" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">学历：</td>
                <td>
                    <asp:DropDownList ID="dropEducation" runat="server" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">学位：</td>
                <td>
                    <asp:TextBox ID="txtEducationalLevel" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">毕业学校：</td>
                <td>
                    <asp:TextBox ID="txtGraduatedSchool" runat="server" Style="width: 100%;" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">专业：</td>
                <td>
                    <asp:TextBox ID="txtProfession" runat="server" Style="width: 100%;" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">毕业时间：
                </td>
                <td>
                    <asp:TextBox ID="txtGraduationTime" runat="server" Style="width: 100%;" onclick="dpGraduationTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">应届生报到日期：
                </td>
                <td>
                    <asp:TextBox ID="txtGraduatesCheckTime" runat="server" Style="width: 100%;" onclick="dpGraduatesCheckTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td style="text-align: right;">实习开始日期：
                </td>
                <td>
                    <asp:TextBox ID="txtInternshipStartTime" runat="server" Style="width: 100%;" onclick="dpInternshipStartTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">实习结束日期：
                </td>
                <td>
                    <asp:TextBox ID="txtInternshipEndTime" runat="server" Style="width: 100%;" onclick="dpInternshipEndTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">试用开始日期：
                </td>
                <td>
                    <asp:TextBox ID="txtTrialStartTime" runat="server" Style="width: 100%;" onclick="dpTrialStartTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="text-align: right;">试用结束日期：
                </td>
                <td>
                    <asp:TextBox ID="txtTrialEndTime" runat="server" Style="width: 100%;" onclick="dpTrialEndTime()" Enabled="false" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">当前状态：
                </td>
                <td>
                    <asp:DropDownList ID="dropEnable" runat="server" Enabled="false">
                        <asp:ListItem Value="1">在职</asp:ListItem>
                        <asp:ListItem Value="0">离职</asp:ListItem>
                        <asp:ListItem Value="2">其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">离职日期：
                </td>
                <td>
                    <asp:TextBox ID="txtOutdutyDate" runat="server" Enabled="false" Style="width: 100%;"></asp:TextBox>
                </td>
                <td style="text-align: right; display: none">排序号：
                </td>
                <td style="display: none">
                    <asp:TextBox ID="txtOrderNum" runat="server" Style="width: 100%;" Enabled="false" ReadOnly="true">1</asp:TextBox>
                </td>
                <td style="text-align: right;">政治面貌：
                </td>
                <td colspan="3" style="width: 120px">
                    <asp:DropDownList ID="dropZZMM" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">附件列表：
                </td>
                <td colspan="3">
                    <uc1:UploadAttach runat="server" ID="UploadAttach" />
                </td>
            </tr>
            <tr>
                <td colspan="8" class="text-center">
                    <span class="btn btn-default" onclick=" closeWin();">
                        <span class="glyphicon glyphicon-remove">关闭</span>
                    </span>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
