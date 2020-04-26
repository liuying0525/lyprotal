<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetRole.aspx.cs" MasterPageFile="../../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Users.SetRole" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#<%=cblRoles.ClientID%> td").css("border-top", "0");
        });
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
        <div id="baseInfo">
            <table class="table table-bordered">
                <tr>
                    <td>账号：<asp:Label runat="server" ID="lblUserAccount"></asp:Label></td>
                </tr>
                <tr>
                    <td>姓名：<asp:Label runat="server" ID="lblUserName"></asp:Label></td>
                </tr>
            </table>
            <div>
            </div>
        </div>
        <div>
            <asp:Panel ID="panel" runat="server">
                <table class="table table-bordered">
                    <tr>
                        <td>角色选择：</td>
                        <td>
                            <asp:CheckBoxList runat="server" ID="cblRoles" RepeatDirection="Horizontal" RepeatColumns="1" RepeatLayout="Flow" CssClass="checkboxList">
                            </asp:CheckBoxList>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="padding-left: 100px">
                                <asp:LinkButton runat="server" CssClass="btn btn-primary" ID="btnSave" Text="分派" OnClick="btnSave_Click" OnClientClick="return confirm('您确定分派吗？')"> <span class="glyphicon glyphicon-floppy-disk"> 保存</span></asp:LinkButton>

                                <span id="cancel" class="btn btn-default" onclick=" window.close();return false;">
                                    <span class="glyphicon glyphicon-remove">关闭</span>
                                </span>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
