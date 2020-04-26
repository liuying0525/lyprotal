<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EditIndexNavigator.aspx.cs" Inherits="NYPortal.Web.Admin.IndexNavigator.EditIndexNavigator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 框架必须的样式引用 Start-->
    <link href="/Scripts/Admin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!--框架必须的样式引用 End-->
    <!-- 框架必须的脚本引用 Start-->
    <script src="/Scripts/Admin/jquery/jquery-1.11.1.js"></script>
    <script src="/Scripts/Admin/bootstrap/js/bootstrap.min.js"></script>
    <!-- 框架必须的脚本引用 End-->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="/Scripts/Admin/bootstrap/js/respond.min.js"></script>
    <![endif]-->

    <!--  输入验证脚本 Start -->
    <script src="/Scripts/Admin/js/jquery.validate.min.js"></script>
    <script src="/Scripts/Admin/js/messages_zh.min.js"></script>
    <!-- 输入验证脚本 End -->

    <!-- 用户自定义的样式和脚本引用 Start-->
    <script src="/Scripts/Admin/js/common.js"></script>
    <!-- 用户自定义的样式和脚本引用 End-->
    <script type="text/javascript">
        function indexImgChange() {
           <%-- $("#<%=btnUpload.ClientID%>").click();--%>
     }
     function fileChange() {
         $("#btnUploadIcon").click();
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="table table-bordered">
            <tr>
                <td>标题：
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox>
                    <span style="color: red">*<span id="spanTitle"></span></span>
                </td>
            </tr>

            <tr>
                <td>排序号：</td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
              <tr>
              <td>Code：</td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"  Text="1"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
          <%--  <tr>
                <td>页面背景图：</td>
                <td>
                    <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                    <asp:Image ID="imgUpload" runat="server" Visible="false" Width="100px" Height="100px" />
                    <asp:HiddenField ID="hidUpload" runat="server" />
                    <div style="color: red">建议使用430x150像素</div>
                </td>
            </tr>
            <tr>
                <td>跳转模式：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlURL" OnSelectedIndexChanged="ddlURL_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1">内部跳转</asp:ListItem>
                        <asp:ListItem Value="0">外部跳转</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr runat="server" id="trURL">
                <td>跳转URL：</td>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" Width="300"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>显示位置：</td>
                <td>
                    <asp:DropDownList ID="ddlDisplayPosition" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>启用状态：</td>
                <td>
                    <asp:DropDownList ID="dropEnableState" runat="server">
                        <asp:ListItem Text="启用" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <div style="padding-left: 100px;">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn-primary btn" OnClientClick="return validateForm();" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="关闭" CssClass="btn-warning btn" OnClick="btnBack_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
