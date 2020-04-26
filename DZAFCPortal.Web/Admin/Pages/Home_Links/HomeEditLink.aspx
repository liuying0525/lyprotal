<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="HomeEditLink.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.Home_Links.HomeAddLink" %>

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
                        maxlength: 12
                    },
                    "<%=txtUrl.UniqueID %>": {
                        required: true
                    },
                    "<%=txtOrderNum.UniqueID %>": {
                        required: true,
                        digits: true
                    }
                    // fileIcon: "imgIcon"  //自定义验证图片
                },
                messages: {

                }
            });
        });

        function imgVal() {
            var result = true;
            var src = $("#<%=imgIcon.ClientID%>").attr("src");
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


        function middleIncoChange() {
            $("#btnSaveMiddleIcon").click();
        }

        //设置应用人群状态的显示
        function setApplayRangeDisplay() {
            //var type = $("#dropApplayType").val();
            var type = $("#<%=dropApplayType.ClientID%>").val();
            $("#traApplayUsers").css("display", "none");
            $("#trApplayRoles").css("display", "none");

            if (type == "1") {
                $("#traApplayUsers").css("display", "table-row");
            }
            else if (type == "2") $("#trApplayRoles").css("display", "table-row");
        }

        //设置推荐人群状态的显示
        function setRecommendRangeDisplay() {
            //var type = $("#dropRecommendType").val();
            var type = $("#<%=dropRecommendType.ClientID%>").val();
            $("#trRecommendUsers").css("display", "none");
            $("#trRecommendRoles").css("display", "none");
           
            if (type == "1") {
                $("#trRecommendUsers").css("display", "table-row");
            }
            
            else if (type == "2") $("#trRecommendRoles").css("display", "table-row");
        }
        function RefreshParent(id) {
            window.opener.location.href = "HomeLinkList.aspx?type=" + id;
            window.close();
        }
        $(function () {
            setApplayRangeDisplay();

            setRecommendRangeDisplay();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="heigth_20">
        </div>
        <table class="table table-bordered">
            <tr>
                <td>名称
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
          <%--  <tr>
                <td>类别</td>
                <td>
                    <asp:DropDownList runat="server" ID="dropLinkType" CssClass="form-control" Style="width: 40%;" AutoPostBack="True" OnSelectedIndexChanged="dropLinkType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>显示图标</td>
                <td>
                    <table>
                        <tr>
                            <td style="background-color: #EEE9BF;">
                                <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                                <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                                <asp:Image ID="imgIcon" runat="server" Visible="false" Width="100px" Height="100px" />
                                <asp:HiddenField ID="hidUpload" runat="server" />
                                <span class="info">
                                    <asp:Literal ID="ltrImageIconInfo" runat="server"></asp:Literal>
                                </span>
                                <span style="color: red" id="imgSpan">*</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>电脑端链接
                </td>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" class="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>平板端链接
                </td>
                <td>
                    <asp:TextBox ID="txtPadUrl" runat="server" class="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>手机端链接
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneUrl" runat="server" class="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">(注：对于以上的链接，若为空，则在对应设备使用时，链接不显示。
                        )
                </td>
            </tr>
            <tr>
                <td>排序号
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" class="form-control txt_width_sm">1</asp:TextBox><span style="color:red">注：排序号按照升序排序</span>
                </td>
            </tr>
            <tr>
                <td>描述
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>

            <tr id="tr_IsShowCommon" runat="server">
                <td>默认为常用链接
                </td>
                <td>
                    <asp:DropDownList ID="dropIsShowCommon" runat="server" CssClass="form-control txt_width_sm">
                        <asp:ListItem Text="是" Value="true" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="false"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>应用范围
                </td>
                <td>
                    <asp:DropDownList ID="dropApplayType" runat="server" CssClass=" form-control txt_width_md" onchange="setApplayRangeDisplay();">
                        <asp:ListItem Text="所有人" Value="0" Selected="True"></asp:ListItem>
                      <%--  <asp:ListItem Text="特定人员" Value="1"></asp:ListItem>--%>
                        <asp:ListItem Text="特定角色" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trApplayRoles">
                <td>应用角色</td>
                <td>
                    <asp:CheckBoxList ID="cblApplayRoles" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"  RepeatColumns="1" CssClass="checkboxList"></asp:CheckBoxList>
                </td>
            </tr>
            <tr style="display:none">
                <td>推荐范围
                </td>
                <td>
                    <asp:DropDownList ID="dropRecommendType" runat="server" CssClass=" form-control txt_width_md" onchange="setRecommendRangeDisplay();">
                        <asp:ListItem Text="不推荐" Value="0" Selected="True"></asp:ListItem>
                      <%--  <asp:ListItem Text="特定人员" Value="1"></asp:ListItem>--%>
                        <asp:ListItem Text="特定角色" Value="2"></asp:ListItem>
                        <asp:ListItem Text="所有人" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trRecommendRoles">
                <td>应用角色</td>
                <td>
                    <asp:CheckBoxList ID="cblRecommendRoles" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"></asp:CheckBoxList>
                </td>
            </tr>

            <tr>
                <td>启用状态
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
