<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EditCategory.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Categorys.EditCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 框架必须的样式引用 Start-->
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!--框架必须的样式引用 End-->
    <!-- 框架必须的脚本引用 Start-->
    <script src="/Scripts/jquery/jquery-1.11.1.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <!-- 框架必须的脚本引用 End-->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="http://cdn.bootcss.com/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="/Scripts/bootstrap/js/respond.min.js"></script>
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
            $("#<%=btnUpload.ClientID%>").click();
        }
        function fileChange() {
            $("#btnUploadIcon").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="margin-top: 10px;" class="table table-bordered">
            <tr>
                <td>名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><span style="color: red">* <span id="spanName"></span></span>
                </td>
            </tr>
            <%--      <tr>
                <td>英文名称：
                </td>
                <td>
                    <asp:TextBox ID="txtEnglishName" runat="server"></asp:TextBox><span style="color: red">* <span id="spanEName"></span></span>
                </td>
            </tr>--%>
            <tr>
                <td>默认图片：</td>
                <td>
                    <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                    <asp:Image ID="imgUpload" runat="server" Visible="false" Width="180px" Height="100px" />
                    <asp:HiddenField ID="hidUpload" runat="server" />
                    <div style="color: red">建议使用360x200像素,大小小于100K</div>
                </td>
            </tr>
            <tr>
                <td>排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" TextMode="Number">1</asp:TextBox><span style="color: red">* <span id="spanOrderNum"></span></span>
                </td>
            </tr>
            <tr>
                <td>Code：
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox><span style="color: red">* <span id="spanCode"></span></span>
                </td>
            </tr>
            <%--  <tr>
                <td>关联导航：
                </td>
                <td>
                    <asp:DropDownList ID="ddlRelatedNav" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>是否在导航显示:
                </td>
                <td>
                    <asp:DropDownList ID="ddlIsShowNav" runat="server">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>

            <tr>
                <td>是否在公司动态显示:
                </td>
                <td>
                    <asp:DropDownList ID="ddlIsShowIndex" runat="server">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
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
