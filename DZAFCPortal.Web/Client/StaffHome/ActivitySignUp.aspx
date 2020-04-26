<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySignUp.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.ActivitySignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>个人活动报名</title>
    <script type="text/javascript">
        function Verification() {
            var result = true;
            var txtName = $("#txtName").val();
            if (txtName == '') {
                $("#spanName").text('报名名字不能为空！');
                result = false;
            }
            else {
                $("#spanName").text('');
            }

            var txtPhoneNumber = $("#txtPhoneNumber").val();
            if (txtPhoneNumber == '') {
                $("#spanphoneNumber").text('电话号码不能为空！');
                result = false;
            }
            else {
                $("#spanphoneNumber").text('');
            }

            var patrn=/^[0-9]{11}$/;
            if (!patrn.exec(trim(txtPhoneNumber))){
                  $("#spanphoneNumber").text('请输入正确手机号码！');
                  return false
            }
            else{
                 $("#spanphoneNumber").text('');
            }


            var txtEventAccount = $("#txtEventAccount").val();
            if (trim(txtEventAccount) == '') {
                $("#spanEventAccount").text('8位员工号不能为空！');
                result = false;
            }
            else {
                $("#spanEventAccount").text('');
            }

            if (trim(txtEventAccount).length != 8) {
                $("#spanEventAccount").text('请输入8位员工号！');
                result = false;
            }
            else {
                $("#spanEventAccount").text('');
            }

            var txtEmail = $("#txtEmail").val();
            if (txtEmail == '') {
                $("#spanEmail").text('邮箱不能为空！');
                result = false;
            }
            else {
                $("#spanEmail").text('');
            }
            return result;
        }
        function ltrim(s) {
            return s.replace(/^\s*/, "");
        }
        //去右空格;
        function rtrim(s) {
            return s.replace(/\s*$/, "");
        }
        function trim(s) {
            return rtrim(ltrim(s));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 600px;padding:20px 0px">
            <div class="BEA_Event_addper_div">
                <span class="BEA_Event_addper_title fl">报名名字：</span><asp:TextBox ID="txtName" runat="server"></asp:TextBox><span style="color: red" id="spanName">*</span></div>
            <div class="BEA_Event_addper_div">
                <span class="BEA_Event_addper_title fl">性别：</span>
                <asp:DropDownList ID="dropSex" runat="server">
                    <asp:ListItem Text="男" Value="0"></asp:ListItem>
                    <asp:ListItem Text="女" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="BEA_Event_addper_div">
                <span class="BEA_Event_addper_title fl">电话：</span>
                <asp:TextBox ID="txtPhoneNumber" runat="server" ></asp:TextBox><span style="color: red" id="spanphoneNumber">*</span></div>
            <div class="BEA_Event_addper_div">
                <span class="BEA_Event_addper_title fl">8位员工号：</span>
                <asp:TextBox ID="txtEventAccount" runat="server"></asp:TextBox><span style="color: red" id="spanEventAccount">*</span></div>
            <div  class="BEA_Event_addper_div">
                <span class="BEA_Event_addper_title fl">邮箱：</span>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>@hkbea.com<span style="color: red" id="spanEmail">*</span></div>
            <asp:Button ID="btnRegistration" runat="server" Text="保存" CssClass="BEA_Detail_submit_btn fl" OnClick="btnRegistration_Click" OnClientClick="return Verification()" />
        </div>
    </form>
</body>
</html>
