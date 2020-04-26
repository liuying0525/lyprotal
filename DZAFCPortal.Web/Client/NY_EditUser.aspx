<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NY_EditUser.aspx.cs" Inherits="DZAFCPortal.Web.Client.NY_EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息修改</title>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/base.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/layouts.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/flickerplate.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/pagenavi.css" rel="stylesheet" />

    <script src="/Scripts/jquery/jquery-1.11.1.js"></script>
    <script src="/Scripts/jquery/jquery-migrate-1.2.1.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Scripts/Client/js/flickerplate.min.js"></script>
    <script src="/Scripts/Client/js/clientCommon.js"></script>
    <script type="text/javascript">
        function closeWin() {
            var index = parent.layer.getFrameIndex(window.name); //获取当前窗体索引
            parent.layer.close(index); //执行关闭
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: #f3f3f3;height: 315px;padding-top:30px;">
            <table class="editdiv_table">
                <tbody>
                    <tr>
                        <td class="first">账号：</td>
                        <td>
                            <asp:Label ID="labAccount" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="first">姓名：</td>
                        <td>
                            <asp:Label ID="labName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="first">手机号：</td>
                        <td>
                            <asp:TextBox ID="txtTelPhone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="first">座机：</td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
            <div style="margin: 15px 182px; width: 200px">
                <div style="float: left">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSave_Click" />
                </div>
                <div style="margin-left: 27px; float: left">
                    <input type="button" class="btn btn-default" value="取消" onclick="closeWin()" />
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </form>
</body>
</html>
