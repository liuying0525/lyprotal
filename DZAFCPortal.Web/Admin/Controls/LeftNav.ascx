<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNav.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.LeftNav" %>

<ul class="nav_list">
    <asp:Repeater ID="rpt_LeftNav" runat="server" OnItemDataBound="rpt_LeftNav_ItemDataBound">
        <ItemTemplate>
            <li>
                <a href="javascript:void(0);">
                    <%--<img src='<%# GetIconUrl(Eval("Icon").ToString()) %>' style="width: 24px; height: 24px; margin-right: 5px;" />--%>
                    <span class="menu-text"><%#Eval("Name") %></span>
                    <b class="arrow icon-angle-down"></b>
                </a>
                <ul class="submenu" style="display: none;">
                    <asp:Repeater ID="rpt_Children" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="<%#FormatUrl(Eval("Url")) %>" target="iframe_default">
                                    <i class="icon-double-angle-right"></i>
                                    <%#Eval("Name") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
