<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Roles.EditRole" MasterPageFile="../../BaseLayout.Master" %>

<%@ Register Src="../../Controls/MutiUserChooseControl.ascx" TagPrefix="uc1" TagName="MutiUserChooseControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //验证提交表单的数据
        function validateForm() {
            var rs = true;

            if ($("#<%=txtName.ClientID%>").val().trim() == "") {
                $("#spanName").html("名称不能为空!");
                rs = false;
            }

            if ($("#<%=txtCode.ClientID%>").val().trim() == "") {
                $("#spanCode").html("编号不能为空!");
                rs = false;
            }

            if ($("#<%=txtSortNum.ClientID%>").val().trim() == "") {
                $("#spanCode").html("排序号不能为空!");
                rs = false;
            }
            return rs;
        }


    </script>
    <style>
        .specialPerList ul li {
            background: #f3f3f3;
        }

        body {
            background: #f3f3f3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panel" runat="server">
        <div class="container">
            <div class="heigth_20">
            </div>
            <table class="table table-bordered">
                <tr>
                    <td>角色名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control txt_width_md"></asp:TextBox><span style="color: red">* <span id="spanName"></span></span>
                    </td>
                </tr>
                <tr>
                    <td>角色编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control txt_width_md" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>角色类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="dropRoleTypes" runat="server" Width="105px">
                            <asp:ListItem Text="前台权限" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="后台权限" Value="1"></asp:ListItem>
                            <asp:ListItem Text="流程审批" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>排序号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSortNum" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>用户选择：
                    </td>
                    <td>
                        <uc1:MutiUserChooseControl runat="server" ID="MutiUserChooseControl" />
                    </td>
                </tr>
                <tr>
                    <td>备注：</td>
                    <td>
                        <asp:TextBox ID="txtRemark" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control txt_width_md"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="text-center">

                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn-primary btn" OnClientClick="return validateForm();"> <span class="glyphicon glyphicon-floppy-disk"> 保存</span></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <span class="btn btn-default" onclick=" RefreshParent();">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </span>

                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
