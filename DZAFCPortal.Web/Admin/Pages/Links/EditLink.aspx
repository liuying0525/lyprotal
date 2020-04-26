<%@ Page Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EditLink.aspx.cs" Inherits="NYPortal.Web.SMGPages.Links.EditLink" %>

<%@ Register Src="../../Controls/OrgAndUserChoose.ascx" TagName="OrgAndUserChoose" TagPrefix="uc3" %>


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
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <script src="/Scripts/Admin/js/common.js"></script>
    <!-- 用户自定义的样式和脚本引用 End-->
    <script type="text/javascript">
        //验证提交表单的数据
        //输入验证
        $().ready(function () {
            $("form").validate({
                rules: {
                    "<%=txtName.UniqueID %>": {
                        required: true,
                        maxlength: 50
                    },
                  <%--  "<%=txtShortName.UniqueID %>": {
                        required: true
                    },--%>
                    "<%=txtOrderNum.UniqueID %>": {
                        required: true,
                        digits: true
                    },
                    "<%=txtCode.UniqueID %>": {
                        required: true
                    }
                },
                messages: {

                }
            });
        });

        function imgVal() {
            var result = true;
            var src = $("#<%=imgUpload.ClientID%>").attr("src");
            if (src == undefined) {
                $('#imgSpan').html('必填');
                result = false;
            }
            return result;
        }

        function validateForm() {
            if ($("form").valid() && imgVal()) {
                return true;
            }
            else return false;
        }
        function indexImgChange() {
            $("#<%=btnUpload.ClientID%>").click();
        }
        function fileChange() {
            $("#btnUploadIcon").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg">
        <div class=" BEA_table_title">
        </div>
        <table class="table table-bordered">
            <tr>
                <td>名称：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" class="form-control txt_width_lg"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr style="display:none">
                <td>简称：
                </td>
                <td>
                    <asp:TextBox ID="txtShortName" runat="server" class="form-control txt_width_lg"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>首页图标：</td>
                <td>
                    <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                    <asp:Image ID="imgUpload" runat="server" Visible="false" Width="100px" Height="100px" />
                    <asp:HiddenField ID="hidUpload" runat="server" />
                    <div style="color: red">建议使用388*184像素，大小小于100K</div>
                    <span style="color: red" id="imgSpan">*</span>
                </td>
            </tr>
            <tr>
                <td>URL：
                </td>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" class="form-control txt_width_lg"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" class="form-control txt_width_sm">1</asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr id="Code" runat="server" visible="false">
                <td>单点登入：
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" class="form-control txt_width_sm"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>启用状态：
                </td>
                <td>
                    <asp:DropDownList ID="dropIsEnable" runat="server" CssClass="form-control txt_width_sm">
                        <asp:ListItem Text="启用" Value="true" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="false"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn-primary btn" OnClientClick="return validateForm();">
                                    <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                           <span class="btn btn-default" onclick=" closeWin();">
                               <span class="glyphicon glyphicon-remove">关闭</span>
                           </span>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

