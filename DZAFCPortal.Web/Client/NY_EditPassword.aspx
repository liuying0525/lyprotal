<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NY_EditPassword.aspx.cs" Inherits="DZAFCPortal.Web.Client.NY_EditPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户密码修改</title>
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

        function Verification() {
            var result = true;
            
            var newPassword = $("#<%=txtNewPassword.ClientID %>").val();
            var againPassword = $("#<%=txtAgainPassword.ClientID %>").val();
            if (newPassword == '') {
                $("#spanMessage").html('新密码为空！');
                result = false;
                return result;
            }
           
            if (againPassword == '') {
                $("#spanMessage").html('确认密码为空！');
                result = false;
                return result;
            }
            if (newPassword != againPassword) {
                $("#spanMessage").html('两次密码不一致，请重新输入！');
                result = false;
                return result;
            }
            return result;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width: 500px; height: 350px;">
        <div style="width: 500px; height: 280px; padding: 20px; background-color: #f3f3f3">
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
                        <td class="first">设置新密码：</td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="first">确认新密码：</td>
                        <td>
                            <asp:TextBox ID="txtAgainPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
            <div style="margin: 15px 150px; width: 200px">
                <div style="float: left">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return Verification();"/>
                </div>
                <div style="margin-left: 50px; float: left">
                    <input type="button" class="btn btn-default" value="取消" onclick="closeWin()" />
                </div>
            </div>
            <span id="spanMessage" style="color:red"></span>
        </div>
    </form>
</body>
</html>
