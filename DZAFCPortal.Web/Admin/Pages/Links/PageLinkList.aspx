<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="PageLinkList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.Links.PageLinkList" %>

<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>滚动图片</title>

    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var roleWidth = 800, roleHeight = 550;
        var type = getQueryString("type");
        function openAddRole() {
            var url = "AddLink.aspx?type=" + type;

            openWin(url, "新增滚动图片", roleWidth, roleHeight);

            return false;
        }

        function openEditeRole(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EditLink.aspx?type=" + type + "&ID=" + id;

            openWin(url, "编辑滚动图片", roleWidth, roleHeight);

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
              <%--  <a href='javascript:void(0);' onclick='openAddRole();' class="btn-primary btn">
                    <span class="glyphicon glyphicon-plus-sign">新增</span>
                </a>--%>
                 <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAddRole();" CssClass="btn-primary btn" Tag="Slider_Add_01" />
            </div>
        </div>
        <div class="row">
            <table class="table table-striped table-bordered table-hover">
                <asp:Repeater ID="rptEFamil" runat="server" OnItemCommand="rptEFamil_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th>名称</th>
                            <th>图片</th>
                            <th>URL</th>
                            <th>排序号</th>
                            <th>启用状态</th>
                            <th>操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <img src='<%#Eval("ImageUrl") %>' <%#ImgSize()%> alt="" />
                            </td>
                            <td>
                                <%# Eval("LinkUlr") %>
                            </td>
                            <td>
                                <%# Eval("OrderNum") %>
                            </td>
                            <td>
                                <%# ((bool)Eval("EnableState"))?"是":"否" %>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEditeRole(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Slider_Edit_01">
                                        <span class="glyphicon glyphicon-pencil"> 编辑</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDel" runat="server" Text="删除" CssClass="btn-danger btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Slider_Del_01">
                                        <span class="glyphicon glyphicon-trash"> 删除</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <span class="none-data" runat="server" visible="false" id="Prompt">没有数据！
            </span>
            <%--   <div class="pagenavi productnav">
                <webdiyer:AspNetPager ID="AspNetPager1" PageSize="2" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
            </div>--%>
        </div>
    </div>
</asp:Content>
