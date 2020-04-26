<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Roles.RoleList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>角色管理</title>

    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var roleWidth = 800, roleHeight = 600;
        function openAddRole() {
            var url = "AddRole.aspx";

            openWin(url, "新增角色", roleWidth, roleHeight);

            return false;
        }

        function openEditeRole(obj) {
            var id = $(obj).attr("ArgVal");

            var url = "EditRole.aspx?ID=" + id;

            openWin(url, "编辑角色", roleWidth, roleHeight);

            return false;
        }

        function openAuthorize(obj) {
            var roleID = $(obj).attr("ArgVal");

            var url = "../Authorize.aspx?roleID=" + roleID;

            openWin(url, "角色授权", 800, 600);

            return false;
        }
    </script>


    <style type="text/css">
        .userInfo {
            background: url("user.png") no-repeat;
            left: 0;
            padding-top: 2px;
            padding-left: 20px;
        }

            .userInfo span {
                padding-right: 5px;
            }

        table td {
            word-break: break-all;
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
                <a href='javascript:void(0);' onclick='openAddRole();' class="btn-primary btn">
                    <span class="glyphicon glyphicon-plus-sign" style="top: 2px; margin-right: 8px;"></span>新增
                </a>
                <asp:DropDownList ID="dropRoleTypes" runat="server" Width="105px" OnSelectedIndexChanged="dropRoleTypes_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                    <asp:ListItem Text="全部" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="前台权限" Value="0"></asp:ListItem>
                    <asp:ListItem Text="后台权限" Value="1"></asp:ListItem>
                    <asp:ListItem Text="流程审批" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <table class="table table-striped table-bordered table-hover">
                <asp:Repeater ID="rptRole" runat="server" OnItemCommand="rptRole_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th style="width: 10%;">角色编号</th>
                            <th style="width: 12%;">角色名称</th>
                            <th style="width: 5%;">排序号</th>
                            <th style="width: 8%;">角色类型</th>
                            <th style="width: 40%;">用户</th>
                            <th style="width: 25%;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Code") %>
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <td>
                                <%#Eval("RoleTypes").ToString()=="0"?"前台权限":Eval("RoleTypes").ToString()=="1"?"后台权限":"流程审批" %>
                            </td>
                            <td>
                                <%# GetUsersByRole(Eval("ID")) %>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEditeRole(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Role_Edit_01">
                                        <span class="glyphicon glyphicon-pencil"> 编辑</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnAuth" runat="server" Text="授权" OnClientClick="return openAuthorize(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Role_Auth_01">
                                         <span class="glyphicon glyphicon-lock"> 授权</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDel" runat="server" Text="删除" CssClass="btn-danger btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Role_Del_01">
                                        <span class="glyphicon glyphicon-trash"> 删除</span>
                                </asp:LinkButton>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
