<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EditCorporateAssetsInfo.aspx.cs" Inherits="DZAFCPortal.Web.Admin.BasicData.EditCorporateAssetsInfo" %>
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
    <script type="text/vbscript"></script>
    <!-- 输入验证脚本 End -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="margin-top: 10px;" class="table table-bordered">
              <tr>
                <td>物品编码：
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>物品名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>所属公司</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCompany">
                    </asp:DropDownList><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>物品类型</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlType">
                    </asp:DropDownList><span style="color: red">*</span>
                </td>
            </tr>
            
            <tr>
                <td>保管人：
                </td>
                <td>
                    <asp:TextBox ID="txtPreserver" runat="server"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
             <tr>
                <td>委托代管人：
                </td>
                <td>
                    <asp:TextBox ID="txtEntrusted" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>是否启用：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlIsEnable">
                        <asp:ListItem Selected="True" Value="True" Text="是"></asp:ListItem>
                        <asp:ListItem Value="False" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>是否外借：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlIsBorrow">
                        <asp:ListItem Selected="True" Value="True" Text="是"></asp:ListItem>
                        <asp:ListItem Value="False" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    备注：
                </td>
                <td>
                    <textarea runat="server" id="txtRemark" rows="3" style="width:100%;"></textarea>
                  
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
            $("form").validate({
                rules: {
                    '<%=txtName.UniqueID%>': {
                        required: true,
                    },
                    '<%=txtCode.UniqueID%>': {
                        required: true,
                    },
                    '<%=ddlCompany.UniqueID%>': {
                        ddl: true,
                       
                    },
                    '<%=ddlType.UniqueID%>': {
                        ddl: true,

                    },
                    '<%=txtPreserver.UniqueID%>': {
                        required: true,

                    },

                },
                messages: {
                   
                }
            });
        })

        $.validator.addMethod("ddl", function (value, element) {
         
            return value != '';

        }, "必选"); 
        
    </script>
</asp:Content>
