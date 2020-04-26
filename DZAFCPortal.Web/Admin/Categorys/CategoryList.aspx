<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Category.CategoryList" %>

<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Admin/ThirdLibs/TableSort.js"></script>
    <title>类别管理</title>

    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }

        $(function () {
            $("#tb_category").sorttable({
                ascImgUrl: "/Scripts/Admin/images/bullet_arrow_up.png",
                descImgUrl: "/Scripts/Admin/images/bullet_arrow_down.png",
                ascImgSize: "8px",
                descImgSize: "8px",
                onSorted: function (cell) {
                    //alert(cell.index() + " -- " + cell.text());
                }
            });
        });
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var categoryWidth = 550, categoryHeight = 600;
        function openAddCategory() {
            var url = "EditCategory.aspx";

            openWin(url, "新增分类", categoryWidth, categoryHeight);
            return false;
        }

        function openEditeCategory(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EditCategory.aspx?ID=" + id;

            openWin(url, "编辑分类", categoryWidth, categoryHeight);

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
                            <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAddCategory();" CssClass="btn-primary btn" Tag="Category_Add_01" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="row">

            <table class="table table-bordered table-hover" id="tb_category">
                <asp:Repeater ID="rptCategory" runat="server" OnItemCommand="rptCategory_ItemCommand">
                    <HeaderTemplate>
                        <tr role="head">
                            <th sort="true">名称</th>
                            <th sort="true">默认图片</th>
                            <th sort="true">排序号</th>
                            <th sort="true">编码</th>
                            <th sort="true">是否在公司动态显示</th>
                            <%--    <th>关联导航</th>
                                <th>显示模式</th>
                                <th>是否在导航显示</th>
                                <th>类别内容类型</th>--%>
                            <th style="width: 150px;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <img src='<%#Eval("IndexImgUrl") %>' width="180" height="100" alt="" />
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <td>
                                <%#Eval("Code") %>
                            </td>
                            <td>

                                <%#((bool)Eval("IsShowIndexArea")) %>
                            </td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEditeCategory(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Category_Edit_01" />
                                <asp:Button ID="btnDel" runat="server" Text="删除" class="btn-primary btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Category_Del_01" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
        <div class="apsnetPaper">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页" PageSize="10"></webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>
