<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationTree.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.OrganizationTree" %>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="CascadeTree">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TreeView ID="tv" runat="server" ShowLines="True" ShowCheckBoxes="All" OnSelectedNodeChanged="tv_SelectedNodeChanged" ExpandDepth="0">
            </asp:TreeView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
