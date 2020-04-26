<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" MasterPageFile="../../BaseLayout.Master"  Inherits="DZAFCPortal.Web.Admin.Authorization.Users.AddUser" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">

    <script type="text/javascript">
        //验证提交表单的数据
        function validateForm() {

            if ($.trim($("#<%=txtAccount.ClientID%>").val()) == "") {
                $("#errorAccount").html("* 账号不能为空!");
                return false;
            }

            if ($.trim($("#<%=txtDisplayName.ClientID%>").val()) == "") {
                $("#errorDisplay").html("* 显示名称不能为空!");
                return false;
            }
            return true;
        }

    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
        <div class="container">
            <div class="heigth_20">
            </div>
            <table style="width: 100%;" class="table table-bordered">
                <tr>

                    <td>账号</td>
                    <td>
                        <ul class="ulLeft">
                            <li>
                                <asp:TextBox runat="server" ID="txtAccount" class="form-control txt_width_md"></asp:TextBox></li>
                            <li><span id="errorAccount" style="color: red;">*</span></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>显示名称</td>

                    <td>
                        <ul class="ulLeft">
                            <li>
                                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></li>
                            <li><span id="errorDisplay" style="color: red;">*</span></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>邮箱</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>地址</td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>移动电话</td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClick="btnSave_Click" OnClientClick="return validateForm();">
                              <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
                        </asp:LinkButton>
                        <span class="btn btn-default" onclick=" javascript: window.close()" id="btnCancel">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </span>
                    </td>

                </tr>
            </table>

        </div>
</asp:content>
