<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="CorporateAssetsInfoList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.BasicData.CorporateAssetsInfoList" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Admin/ThirdLibs/TableSort.js"></script>
    <title>物品信息管理</title>

    <script type="text/javascript">
        function refresh() {
          
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
        var width = 500, height = 400;
        function openAddRecord() {
            var url = "EditCorporateAssetsInfo.aspx?Op=Add";

            openWin(url, "新增物品信息", width, height);
            return false;
        }

        function openEditeRecord(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EditCorporateAssetsInfo.aspx?Op=Edit&ID=" + id;

            openWin(url, "编辑物品信息", width, height);

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
                       <td>所属公司：</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAddRecord();" CssClass="btn-primary btn" Tag="CorporateAssetsInfo_Add_01" />
                           
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="row">

            <table class="table table-bordered table-hover" id="tb_category">
                <asp:Repeater ID="rptCorporateAssetsInfo" runat="server" OnItemCommand="rptCorporateAssetsInfo_ItemCommand">
                    <HeaderTemplate>
                        <tr role="head">
                            <th sort="true">物品名称</th>
                            <th sort="true">物品编码</th>
                            <th sort="true">所属公司</th>
                            <th sort="true">物品类型</th>
                            <th sort="true">保管人</th>
                            <th sort="true">委托代管人</th>
                            <th sort="true">是否启用</th>
                            <th sort="true">是否外借</th>
                            <th style="width: 150px;">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <%#Eval("Code") %>
                            </td>
                            <td>
                                <%#GetCommpany(Eval("Company").ToString()) %>
                            </td>
                            <td>
                                <%#GetType(Eval("Type").ToString())%>
                            </td>
                            <td>
                                <%#Eval("Preserver") %>
                            </td>
                            <td>
                                <%#Eval("Entrusted") %>
                            </td>
                             <td>
                                <%#bool.Parse(Eval("IsEnabled").ToString())?"是":"否"%>
                            </td>
                             <td>
                                <%#bool.Parse(Eval("IsBorrow").ToString())?"是":"否"%>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="btnEdit" OnClientClick="return openEditeRecord(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="CorporateAssetsInfo_Edit_01">编辑</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnDel" OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="CorporateAssetsInfo_Del_01">删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
        <div class="apsnetPaper">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页" PageSize="50"></webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>
