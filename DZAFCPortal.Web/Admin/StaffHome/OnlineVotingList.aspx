<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="OnlineVotingList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.StaffHome.OnlineVotingList" %>

<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>在线评选</title>

    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var roleWidth = 1000, roleHeight = 550;
        function openAddRole() {
            var url = "OnlineVotingAdd.aspx";

            openWin(url, "新增活动", roleWidth, roleHeight);

            return false;
        }

        function openEditeRole(obj) {
            var id = $(obj).attr("ArgVal");

            var url = "OnlineVotingAdd.aspx?ID=" + id;

            openWin(url, "编辑活动", roleWidth, roleHeight);

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
                <%--  <a href='javascript:void(0);' onclick='openAddRole();' class="btn-primary btn">
                    <span class="glyphicon glyphicon-plus-sign">新增</span>
                </a>--%>
                <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAddRole();" CssClass="btn-primary btn" Tag="Online_Add_01" />
            </div>
        </div>
        <div class="row">
            <table class="table table-striped table-bordered table-hover">
                <asp:Repeater ID="rptPrefer" runat="server" OnItemCommand="rptEFamil_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th style="width: 100px">标题</th>
                            <th style="width: 200px">标题图片</th>
                            <th style="width: 100px">发布部门</th>
                            <th style="width: 200px">投票时间</th>
                            <th style="width: 400px">简介</th>
                            <th>操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Title") %>
                            </td>
                            <td>
                                <img src='<%#Eval("ImageUrl") %>' width="150" height="100" alt="" />
                            </td>
                            <td>
                                <%# Eval("PublishDept") %>
                            </td>
                            <td>
                                <%# Fxm.Utility.StringHelper.DateToString(Eval("BeginTime"),"yyyy-MM-dd HH:mm") %> 
                                  -
                                <br />
                                <%# Fxm.Utility.StringHelper.DateToString(Eval("EndTime"),"yyyy-MM-dd HH:mm") %> 
                            </td>
                            <td>
                                <%# Eval("Summary") %>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEditeRole(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Online_Edit_01">
                                        <span class="glyphicon glyphicon-pencil"> 编辑</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDel" runat="server" Text="删除" CssClass="btn-danger btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Online_Del_01">
                                        <span class="glyphicon glyphicon-trash"> 删除</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <span class="none-data" runat="server" visible="false" id="Prompt">没有数据！
            </span>
            <div class="pagenavi productnav">
                <%--<a href="#" class="page">首页</a><span class="current">1</span><a href="#" class="page">2</a><a href="#" class="page">3</a><a href="#" class="nextpostslink">尾页</a>--%>
                <webdiyer:AspNetPager ID="AspNetPager1" PageSize="10" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
            </div>
        </div>
    </div>
</asp:Content>
