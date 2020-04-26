<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="InfoAccessList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.InfoAccess.InfoAccessList" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="/Scripts/Admin/ThirdLibs/My97DatePicker/WdatePicker.js"></script>
      <script type="text/javascript">
          function dpStartTime() {
              WdatePicker({
                  dateFmt: 'yyyy-MM-dd',
                  maxDate: '#F{$dp.$D(\'<%=txtEndTime.ClientID%>\')}',
                  onpicked: function () {
                      // onDateChange(true);
                  }
              });
          }

          function dpEndTime() {
              WdatePicker({
                  dateFmt: 'yyyy-MM-dd',
                  minDate: '#F{$dp.$D(\'<%=txtStartTime.ClientID%>\')}',
                  onpicked: function () {
                      //onDateChange(true);
                  }
              });
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
                <table style="width: 70%;">
                    <tr>
                        <td style="width: 10%; text-align: right;">部门：</td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtDeptName" runat="server" Style="width: 100%;"></asp:TextBox>
                        </td>
                        <td style="width: 10%; text-align: right;">姓名：</td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="txtName" runat="server" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">查看信息日期：</td>
                        <td style="width: 21%;">
                             <asp:TextBox ID="txtStartTime" CssClass="form-control" runat="server" onclick="dpStartTime()" Style="width: 115px;"></asp:TextBox>~
                            <asp:TextBox ID="txtEndTime" CssClass="form-control" runat="server" onclick="dpEndTime()" Style="width: 115px;"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">标题：  </td>
                        <td style="width: 20%;">
                             <asp:TextBox ID="txtTitle" runat="server" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Button ID="btnSerach" runat="server" Text="查询" CssClass="btn-primary btn" OnClick="btnSerach_Click" OnClientClick="SetVal()" />
                            <asp:Button ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" CssClass="btn btn-warning" />
                        </td>
                    </tr>
                </table>

            </div>

        </div>
        <div class="row" style="overflow: auto;">
            <div style="width: 1050px;">
                <table class="table table-bordered table-hover">
                    <asp:Repeater ID="rptAccess" runat="server" >
                        <HeaderTemplate>
                            <tr>
                                <th style="width: 150px">标题</th>
                                <th style="width: 100px">栏目</th>
                                <th style="width: 100px">部门</th>
                                <th style="width: 80px;">访问者</th>
                                <th style="width: 100px;">访问时间</th>
                                <th style="width: 80px;">正文/附件</th>
                                <th style="width: 50px">附件名字</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("Title") %>
                                </td>
                                <td>
                                    <%# Type(Eval("CategoryID").ToString()) %>
                                </td>
                                 <td>
                                    <%#Eval("AccessDepartment") %>
                                </td>
                                <td>
                                    <%#Eval("AccessName") %>
                                </td>
                                <td>
                                    <%#DateTime.Parse(Eval("CreateTime").ToString()).ToString("yyyy-MM-dd HH:mm") %>
                                </td>
                                <td>
                                    <%#Eval("Type").ToString() == "1"?"正文":"附件" %>
                                </td>
                                <td>
                                    <%#Eval("AttachName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <span class="none-data" runat="server" visible="false" id="Prompt">没有数据！
                </span>

            </div>

        </div>
        <div class="pagenavi productnav">
            <%--<a href="#" class="page">首页</a><span class="current">1</span><a href="#" class="page">2</a><a href="#" class="page">3</a><a href="#" class="nextpostslink">尾页</a>--%>
            <webdiyer:AspNetPager ID="AspNetPager1" PageSize="10" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>
