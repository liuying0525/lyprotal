<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="IndexNavigatorList.aspx.cs" Inherits="NYPortal.Web.Admin.IndexNavigator.IndexNavigatorList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title>首页导航管理</title>
    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var width = 700, height = 600;
        function openAdd() {
            var url = "EditIndexNavigator.aspx";

            openWin(url, "新增导航项", width, height);

            return false;
        }

        function openEdit(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EditIndexNavigator.aspx?ID=" + id;

            openWin(url, "编辑导航项", width, height);

            return false;
        }
    </script>
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
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAdd();" CssClass="btn-primary btn" Tag="Nav_Add_01" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="row">
            <table class="table table-bordered table-hover">
                <asp:Repeater ID="rptSlider" runat="server" OnItemCommand="rptSlider_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th>标题</th>
                            <th>排序号</th>
                            <th>Code</th>
                            <th style="width: 150px;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Title") %>
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <%#Eval("Code") %>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEdit(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Nav_Edit_01" />
                                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn-primary btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Nav_Del_01" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
