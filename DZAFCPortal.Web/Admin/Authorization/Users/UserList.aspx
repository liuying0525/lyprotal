<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" MasterPageFile="../../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Users.UserList" %>

<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var categoryWidth = 550;
        var categoryHeight = 600;
     
        function openEditUserInfo(trigger) {
            var search = $('#<%= txtSearch.ClientID%>').val();
            var id = $(trigger).attr("ArgVal");
            var url = "EidtUser.aspx?ID=" + id + "&search=" + search;
            openWin(url, "编辑员工信息", categoryWidth, categoryHeight);
            return false;
        }

        function openGrantAuthorize(trigger) {
            var search = $('#<%= txtSearch.ClientID%>').val();
            var id = $(trigger).attr("ArgVal");
            var url = "SetRole.aspx?ID=" + id + "&search=" + search;;
            openWin(url, "角色分派", categoryWidth, categoryHeight);
            return false;
        }

        function OpenAddUser() {
            var url = "AddUser.aspx";
            openWin(url, "新增员工账号", categoryWidth, 500);
            return false;
        }

        function refresh() {
            this.location = this.location;
        }

    </script>

    <style type="text/css">
        .redFont {
            color: red;
        }

        .roleInfo {
            background: url("role.png") no-repeat;
            left: 0;
            padding: 10px 25px;
        }

            .roleInfo span {
                padding-right: 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container container_Left" style="max-width: 98%">
        <div class="row">
            <div class="bread_nav">
                <asp:Literal runat="server" ID="LiteralSiteMap">
                </asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="toolBox well">
                <asp:LinkButton runat="server" ID="btnAdd" Text="新增" ArgVal='<%#Eval("ID") %>' CssClass="btn btn-primary" Tag="User_Add_01" OnClientClick='return OpenAddUser();' Visible="false"><span class="glyphicon glyphicon-plus-sign"> 新增</span></asp:LinkButton>
                组织机构/账号/姓名：&nbsp;<asp:TextBox ID="txtSearch" runat="server" OnTextChanged="Button1_Click"></asp:TextBox>

                <asp:LinkButton runat="server" ID="Button1" Tag="User_View_01" Text="查询" CssClass="btn btn-primary" OnClick="Button1_Click"><span class="glyphicon glyphicon-search"> 查询</span></asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <table class="table table-bordered ">
                <asp:Repeater ID="rptUser" runat="server" OnItemCommand="rptUser_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th style="width: 10%">组织机构</th>
                            <th style="width: 10%">账号</th>
                            <th style="width: 10%">姓名</th>
                            <th style="width: 10%">用户状态</th>
                            <th style="width: 10%">显示状态</th>
                            <th style="width: 5%">排序号</th>
                            <th style="width: 25%">用户角色</th>
                            <th style="width: 20%">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Department") %>
                            </td>
                            <td>
                                <%#Eval("Account") %>
                            </td>
                            <td>
                                <%#Eval("DisplayName") %>
                            </td>
                            <td>
                                <%#Eval("Status").ToString() == "1"?"启用":"禁用" %>
                            </td>
                            <td>
                                <%#Eval("IsShow").ToString()== "True" ? "显示": "不显示" %> 
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <td>
                                <%# GetRoleByUser(Eval("ID")) %>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" Tag="User_Edit_01" ID="User_Edit_01" CssClass="btn btn-primary" Text="编辑" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' ArgVal='<%#Eval("ID") %>' OnClientClick='return openEditUserInfo(this);'> <span class="glyphicon glyphicon-pencil"> 编辑</span></asp:LinkButton>
                                <%--<asp:LinkButton runat="server" Tag="User_Del_01" ID="User_Del_01" CssClass="btn btn-danger" Text="删除" ArgVal='<%#Eval("ID") %>' OnClientClick="return confirm('您确定删除吗？该操作造成的后果不可逆转')" CommandName="Delete" CommandArgument='<%#Eval("ID") %>'> <span class="glyphicon glyphicon-trash"> 删除</span></asp:LinkButton>--%>
                                <asp:LinkButton runat="server" Tag="User_Role_01" ID="User_Role_01" CssClass="btn btn-primary" Text="分派角色" CommandName="SetRole" CommandArgument='<%#Eval("ID") %>' ArgVal='<%#Eval("ID") %>' OnClientClick='return openGrantAuthorize(this);'><span class="  glyphicon glyphicon-flag"> 分派角色</span></asp:LinkButton>

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="pagenavi productnav">
            <webdiyer:AspNetPager ID="AspNetPager1" PageSize="10" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>
