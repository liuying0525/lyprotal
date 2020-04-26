<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="HomeLinkList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.Home_Links.HomeLinkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 框架必须的样式引用 Start-->
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!--框架必须的样式引用 End-->
    <!-- 框架必须的脚本引用 Start-->
    <script src="/Scripts/jquery/jquery-1.11.1.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <!-- 框架必须的脚本引用 End-->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="/Scripts/bootstrap/js/respond.min.js"></script>
    <![endif]-->

    <!--  输入验证脚本 Start -->
    <script src="/Scripts/Admin/js/jquery.validate.min.js"></script>
    <script src="/Scripts/Admin/js/messages_zh.min.js"></script>
    <!-- 输入验证脚本 End -->

    <!-- 用户自定义的样式和脚本引用 Start-->
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <script src="/Scripts/Admin/js/common.js"></script>
    <!-- 用户自定义的样式和脚本引用 End-->
    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">

        var linkWidth = 1024;
        var linkHeight = 800;
        function openAddLink() {
            var url = "HomeEditLink.aspx?Op=<%=DZAFCPortal.Config.Enums.OperateEnum.Add %>";

            openWin(url, "新增", linkWidth, linkHeight);

            return false;
        }

        function openEditeLink(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "HomeEditLink.aspx?Op=<%=DZAFCPortal.Config.Enums.OperateEnum.Edit %>" + "&ID=" + id;

            openWin(url, "编辑", linkWidth, linkHeight);

            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class=" container container_Left" style="max-width: 98%">
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
                            <asp:LinkButton ID="btnAdd" runat="server" OnClientClick="return openAddLink();" CssClass="btn-primary btn" Tag="Link_Add_01">
                                     <span class="glyphicon glyphicon-plus-sign">&nbsp;新增</span>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <table class="table table-bordered table-hover">
                <asp:Repeater ID="rptLink" runat="server" OnItemCommand="rptLink_ItemCommand" OnItemDataBound="rptLink_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <th style="width: 5%">序号</th>
                            <th style="width: 15%">名称</th>
                            <th style="width: 10%">显示图标</th>
                            <th style="width: 5%">排序号</th>
                            <th style="width: 10%">默认常用链接</th>
                            <th style="width: 10%">应用范围</th>
                          <%--  <th style="width: 10%">推荐范围</th>--%>
                            <th style="width: 5%">状态</th>
                            <th style="width: 15%">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <asp:Literal ID="ltrIcon" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <td>
                                <%# ((bool)Eval("IsCommonLink"))?"是":"否" %>
                            </td>
                            <td>
                                <%# (Eval("AppalyType").ToString()=="0")?"所有人":(Eval("AppalyType").ToString()=="1")?"特定人员":"特定角色" %>
                            </td>
                          <%--  <td>
                                <%# (Eval("RecommendType").ToString()=="0")?"不推荐":(Eval("RecommendType").ToString()=="1")?"特定人员":(Eval("RecommendType").ToString()=="2")?"特定角色":"所有人" %>
                            </td>--%>
                            <td>
                                <%# ((bool)Eval("IsEnable"))?"启用":"禁用" %>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="return openEditeLink(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Link_Edit_01">
                                           <span class="glyphicon glyphicon-pencil"> 编辑</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDel" runat="server" Text="删除" CssClass="btn-danger btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Link_Del_01">
                                         <span class="glyphicon glyphicon-trash"> 删除</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <%-- <div class="apsnetPaper">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页" PageSize="5"></webdiyer:AspNetPager>
            </div>--%>
    </div>

</asp:Content>
