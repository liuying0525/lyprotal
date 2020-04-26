<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EmployeeInforList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.EmployeeInfor.EmployeeInforList" %>

<%@ Register Src="../../Controls/SingleOrgChooseControl.ascx" TagPrefix="uc1" TagName="SingleOrgChooseControl" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .specialPerList {
            margin: 0px;
            padding: 5px;
        }

        .toolBox > table > tbody > tr > td {
            vertical-align: middle;
            padding: 5px;
            line-height: 1.42857143;
        }
    </style>
    <script src="/Scripts/Admin/ThirdLibs/TableSort.js"></script>
    <script src="/Scripts/Admin/ThirdLibs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
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
        var width = 1150, height = 450;
        function openAddRecord() {
            var url = "EmployeeInforEdit.aspx?Op=Add";

            openWin(url, "新增员工信息", width, height);
            return false;
        }

        function openEditRecord(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EmployeeInforEdit.aspx?Op=Edit&ID=" + id;

            openWin(url, "编辑员工信息", width, height);

            return false;
        }
        function openShowRecord(obj) {
            var id = $(obj).attr("ArgVal");
            var url = "EmployeeInforDisplay.aspx?ID=" + id;

            openWin(url, "查看员工信息", width, height);

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

                        <td style="text-align: right;">姓名:</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">职位名称:</td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtPostion" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">部门:</td>
                        <td style="width: 280px">
                            <uc1:SingleOrgChooseControl runat="server" ID="SingleOrgChooseControl" />
                            <asp:HiddenField runat="server" ID="hidDepartment" />
                        </td>
                        <td style="text-align: right;">当前状态:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlEmpStatus">
                                <asp:ListItem Selected="True" Value="1">在职</asp:ListItem>
                                <asp:ListItem Value="0">离职</asp:ListItem>
                                <asp:ListItem Value="2">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">政治面貌:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="dropZZMM">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="btnSearch_Click" CssClass="btn-primary btn" />
                            <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClientClick="return openAddRecord();" CssClass="btn-primary btn" Tag="Emplpyee_Add_01" />
                            <asp:Button ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" CssClass="btn btn-warning" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="row" style="overflow: auto;">
            <div style="width: 1600px;">
                <table class="table table-bordered table-hover" id="tb_category" style="width: 100%;">
                    <asp:Repeater ID="rpt_content" runat="server" OnItemCommand="rpt_content_ItemCommand">
                        <HeaderTemplate>
                            <tr role="head">
                                <th sort="true" style="width: 70px">姓名</th>
                                <th sort="true" style="width: 60px">性别</th>
                                <th sort="true" style="width: 100px">出生日期</th>
                                <th sort="true" style="width: 150px">部门</th>
                                <th sort="true" style="width: 150px">原部门</th>
                                <th sort="true" style="width: 120px">职位</th>
                                <th sort="true" style="width: 95px">招聘性质</th>
                                <th sort="true" style="width: 100px">学历</th>
                                <th sort="true" style="width: 70px">学位</th>
                                <th sort="true" style="width: 120px">毕业院校</th>
                                <th sort="true" style="width: 120px">专业</th>
                                <th sort="true" style="width: 130px">试用开始日期</th>
                                <th sort="true" style="width: 130px">试用结束日期</th>
                                <th sort="true" style="width: 95px">政治面貌</th>
                                <th sort="true" style="width: 95px">当前状态</th>
                                <th sort="true" style="width: 95px">离职日期</th>
                                <th style="width: 150px;">操作</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><a href="javascript:void(0)" onclick='openShowRecord(this)' argval='<%#Eval("ID") %>'><%# GetUserName(Eval("NameAccount").ToString())%></a></td>
                                <td><%#Eval("Sex").ToString() == "0"?"男":"女"%></td>
                                <td><%#Eval("BirthDate") != null? DateTime.Parse(Eval("BirthDate").ToString()).ToString("yyyy-MM-dd"):""%></td>
                                <td><%# GetDepName(Eval("NameAccount").ToString()) %></td>
                                <td><%# GetLDepName(Eval("NameAccount").ToString()) %></td>
                                <td><%#Eval("Position") %></td>
                                <td><%#GetBasicDateName(Eval("RecruitNature").ToString()) %></td>
                                <td><%# GetBasicDateName(Eval("Education").ToString()) %></td>
                                <td><%#Eval("EducationalLevel") %></td>
                                <td><%#Eval("GraduatedSchool") %></td>
                                <td><%#Eval("Profession") %></td>
                                <%--  <td><%#DateTime.Parse(Eval("TrialStartTime").ToString()).ToString("yyyy-MM-dd")%></td>--%>
                                <td><%#Eval("TrialStartTime") != null?DateTime.Parse(Eval("TrialStartTime").ToString()).ToString("yyyy-MM-dd"):""%></td>
                                <td><%#Eval("TrialEndTime") != null?DateTime.Parse(Eval("TrialEndTime").ToString()).ToString("yyyy-MM-dd"):""%></td>
                                <%-- <td><%#DateTime.Parse(Eval("TrialEndTime").ToString()).ToString("yyyy-MM-dd")%></td>--%>
                                <td><%# GetBasicDateName(Eval("PoliticalStatus")==null?"":Eval("PoliticalStatus").ToString()) %></td>
                                <td><%# GetEnables(Eval("Enable").ToString()) %></td>
                                <td><%#Eval("OutdutyDate") != null?DateTime.Parse(Eval("OutdutyDate").ToString()).ToString("yyyy-MM-dd"):""%></td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="编辑" OnClientClick="return openEditRecord(this);" ArgVal='<%#Eval("ID") %>' CssClass="btn-primary btn" Tag="Emplpyee_Edit_01" />
                                    <asp:Button ID="btnDel" runat="server" Text="删除" class="btn-primary btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Emplpyee_Del_01" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </div>
        </div>
        <span class="none-data" runat="server" visible="false" id="Prompt">没有数据！
        </span>
        <div class="pagenavi productnav">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页" PageSize="10"></webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>
