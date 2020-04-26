<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.News.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function refresh() {
            this.location = this.location;
        }
    </script>

    <!--处理打开各个新窗口的页面脚本 -->
    <script type="text/javascript">
        var width = 700, height = 600;
        var type = getQueryString("type");
        function openAdd() {
            var url = "NewsEdit.aspx?Op=" + type + "&CategoryCode=<%=codeId%>";
            openWin(url, "新增导航项", width, height);

            return false;
        }

        function openEdit(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "NewsEdit.aspx?Op=" + type + "&ID=" + id;

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
                <%--系统管理 &gt;信息发布--%>
            </div>
        </div>

        <div class="row">
            <div class="toolBox well">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAdd();" CssClass="btn-primary btn" Tag="Content_Add_01" />
                        </td>
                        <td style="text-align: right; float: right; width: 400px">
                            <%--  <asp:DropDownList ID="dropType" runat="server"></asp:DropDownList>--%>
                            标题：
                            <asp:TextBox ID="txtName" runat="server" Style="width: 50%;"></asp:TextBox>
                            <asp:Button ID="btnSerach" runat="server" Text="查询" CssClass="btn-primary btn" OnClick="btnSerach_Click" />
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
                            <th style="width:400px">标题</th>
                            <th>标题图片</th>
                            <th>发布人</th>
                            <th>类型</th>
                            <th>排序号</th>
                            <th>发布日期</th>
                            <th style="width: 150px;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href="<%#GenerateContentUrl(Eval("ID").ToString()) %>" target="_blank"><%#Eval("Title") %></a>
                            </td>
                            <td>
                                <img src='<%#Eval("IndexImgUrl") %>' width="180" height="100" alt="" />
                            </td>
                            <td>
                                <%#Eval("Publisher") %>
                            </td>
                            <td>
                                <%#Type(Eval("CategoryID").ToString()) %>
                            </td>
                            <td>
                                <%#Eval("OrderNum") %>
                            </td>
                            <td>
                                <%#Eval("CreateTime") %>
                            </td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEdit(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Content_Edit_01" />
                                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="btn-primary btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Content_Del_01" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
