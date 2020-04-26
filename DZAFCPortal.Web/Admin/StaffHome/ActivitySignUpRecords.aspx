<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="ActivitySignUpRecords.aspx.cs" Inherits="DZAFCPortal.Web.Admin.StaffHome.ActivitySignUpRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-striped table-bordered table-hover">
        <thead>
            <tr>
                <th style="width: 10%">序号</th>
                <th style="width: 20%">部门</th>
                <th style="width: 15%">姓名</th>
                <th style="width: 30%">报名时间</th>
                <th style="width: 25%">操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptRegister" runat="server" OnItemCommand="rptNYFamil_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1%></td>
                        <td><%#Eval("EventAccount")%></td>
                        <td><%#Eval("UserDisplayName")%></td>
                        <td><%#Eval("RegistTime")%></td>
                        <td>
                            <asp:LinkButton ID="btnDel" runat="server" Text="删除" CssClass="btn-danger btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确认删除？删除后数据无法恢复。');" Tag="Events_Del_01">
                                        <span class="glyphicon glyphicon-trash"> 删除</span>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
