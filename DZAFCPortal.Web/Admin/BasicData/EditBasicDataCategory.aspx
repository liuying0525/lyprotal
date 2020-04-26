<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EditBasicDataCategory.aspx.cs" Inherits="DZAFCPortal.Web.Admin.BasicData.EditBasicDataCategory" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="margin-top: 10px;" class="table table-bordered">
            <tr>
                <td>名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>

            <tr>
                <td>编码：
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>关联角色：
                </td>
                <td>
                    <asp:CheckBoxList runat="server" ID="cblRoles" RepeatDirection="Horizontal" RepeatColumns="1" RepeatLayout="Flow" CssClass="checkboxList">
                    </asp:CheckBoxList>

                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <div style="padding-left: 100px;">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn-primary btn" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="关闭" CssClass="btn-warning btn" OnClick="btnBack_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(function () {
            $(function () {
                $("#<%=cblRoles.ClientID%> td").css("border-top", "0");
            });

            $("form").validate({
                rules: {
                    '<%=txtName.UniqueID%>': {
                        required: true,
                    },
                    '<%=txtCode.UniqueID%>': {
                        required: true,
                    }
                },
                messages: {
                    '<%=txtName.UniqueID%>': {
                        required: "必填",
                    },
                    '<%=txtCode.UniqueID%>': {
                        required: "必填"
                    }
                }
            });
        })
    </script>
</asp:Content>
