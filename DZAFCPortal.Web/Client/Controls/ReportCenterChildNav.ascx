<%--报表中心子级导航--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportCenterChildNav.ascx.cs" Inherits="DZAFCPortal.Web.Client.Controls.ReportCenterChildNav" %>
<ul class="ny_business_ul">
    <asp:Repeater runat="server" ID="rptContent">
        <ItemTemplate>
            <li>
                <a href='<%#FormatReportCenterUrl(Eval("Url"),Eval("ID"),Eval("ParentId")) %>' target="_blank">
                    <img src="<%#Eval("IconUrl") %>" alt="" />
                    <span><%#Eval("Title") %></span>
                </a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
