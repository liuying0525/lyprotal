<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="AttachList.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.AttachList" %>

<%@ Register Src="../Controls/UploadAttach3.ascx" TagPrefix="uc1" TagName="UploadAttach3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    <%--    function GetAttachList() {
            $("#<%=btnSave.ClientID%>").click();
        }--%>
    </script>
    <style>
        .modal-dialog {
            padding-top: 10px !important;
        }

        .uploadify {
            height: 25px !important;
            width: 80px;
            margin: 0 !important;
        }

        #fileQueue {
            height: auto !important;
            min-height: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UploadAttach3 runat="server" ID="UploadAttach3" />
  <%--  <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" Style="line-height: 32px; width: 80px; background: #428bca; border: 1px solid #ccc; text-align: center; border-radius: 5px;" />--%>
   <%-- <asp:Button ID="btnCancel" runat="server"  Text="保存并返回" OnClick="btnCancel_Click" Style="line-height: 32px; width: 80px; background: #fff; border: 1px solid #ccc; text-align: center; border-radius: 5px;" />--%>
      <asp:LinkButton ID="btnSave" runat="server" OnClick="btnCancel_Click" CssClass="btn-primary btn" >
            <span class="glyphicon glyphicon-floppy-disk"> 保存并返回</span>
      </asp:LinkButton>
</asp:Content>
