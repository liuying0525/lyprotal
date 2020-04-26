<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EidtUser.aspx.cs" MasterPageFile="../../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Users.EidtUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        //验证提交表单的数据
        <%--    function validateForm() {

            if ($.trim($("#<%=txtName.ClientID %>").val()) == "") {
                $("#errorDisplay").html("* 显示名称不能为空!");
                return false;
            }
            return true;
        }--%>
        function RefreshParent() {
            //window.opener.location.href = window.opener.location.href+"&search=<%=search%>";
            window.opener.location.href = "UserList.aspx?search=<%=search%>";
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="heigth_20">
        </div>
        <asp:Panel ID="panel" runat="server">
            <table style="width: 100%;" class="table table-bordered">
                <tr>
                    <td>账号</td>
                    <td>
                        <asp:Label ID="labAccount" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>姓名</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtName" CssClass="form-control txt_width_md"></asp:TextBox>--%>
                        <asp:Label ID="labName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>组织机构</td>
                    <td>
                        <asp:Literal runat="server" ID="ltrUp"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>邮箱</td>
                    <td>
                        <%--  <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>--%>
                        <asp:Label ID="labEmail" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>地址</td>
                    <td>
                        <%-- <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>--%>
                        <asp:Label ID="labAddress" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>移动电话</td>

                    <td>
                        <%-- <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control txt_width_md"></asp:TextBox>--%>
                        <asp:Label ID="labMobile" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>直线号码</td>
                    <td>
                        <asp:TextBox ID="txtDirectPhone" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>移动短号</td>
                    <td>
                        <asp:TextBox ID="txtShortMobilePhone" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>应急电话</td>
                    <td>
                        <asp:TextBox ID="txtEmergencyPhone" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>A3业务员名称</td>
                    <td>
                        <asp:TextBox ID="txtA3Name" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>排序号</td>

                    <td>
                        <asp:TextBox ID="txtOrderNum" runat="server" CssClass="form-control txt_width_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>显示状态</td>

                    <td>
                        <asp:DropDownList ID="dropIsEnable" runat="server" CssClass="form-control txt_width_sm">
                            <asp:ListItem Text="显示" Value="true" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不显示" Value="false"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" Text="保存" OnClientClick="return validateForm();" OnClick="btnSave_Click">
                                <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
                        </asp:LinkButton>
                        <span class="btn btn-default" onclick=" javascript: window.close()" id="btnCancel">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </span>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
