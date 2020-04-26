<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="EmployeeInforEdit.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.EmployeeInfor.EmployeeInforEdit" %>

<%@ Register Src="../../Controls/UploadAttach.ascx" TagPrefix="uc1" TagName="UploadAttach" %>
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
    <link href="/Scripts/Admin/css/chosen/chosen.css" rel="stylesheet" />
    <script src="/Scripts/Admin/ThirdLibs/chosen/chosen.jquery.min.js"></script>
    <!--  人员搜索及部门自动赋值 Start -->
    <script src="../../BusinessData/js/bootstrap-typeahead.js"></script>
    <script src="../../BusinessData/js/jquery.linq.min.js"></script>
    <script src="../../BusinessData/js/underscore-min.js"></script>
    <script src="../../BusinessData/js/CRMAccountInfoSelect.js"></script>
    <script src="../../BusinessData/js/searchpeople.js"></script>
    <!--  人员搜索及部门自动赋值 Start -->

    <script src="/Scripts/Admin/ThirdLibs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $("button[data-target='#myModal']").width(120);



            var result = true;
            $('.chosen-select').chosen({
                allow_single_deselect: true,
                max_selected_options: 1,
                placeholder_text_multiple: " "
            });
            $.validator.setDefaults({ ignore: ":hidden:not(select)" });
            $.ajax({
                async: false,
                type: "post",
                contentType: "application/x-www-form-urlencoded",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getUserHandler.ashx',
                data: {
                    Op: "all",
                },
                dataType: "json",
                success: function (rel) {
                    if (rel != null) {
                        var allusers = rel;

                        $('#<%=selectApplyMan.ClientID%>').on('change', function (evt, params) {
                            if (params.selected != undefined) {
                                var applicantaccount = params.selected;
                                var queryapplicant = $.Enumerable.From(allusers).Where(function (x) { return x.Account == applicantaccount }).Select().FirstOrDefault();
                                $('#<%=txtDeptName1.ClientID%>').val(queryapplicant.Department);
                                validateUser(queryapplicant.Account);

                            } else {
                                $('#<%=txtDeptName1.ClientID%>').val('');

                            }
                        })
                    }
                },
                error: function () { alert(rel.d.ErrorMessage); }
            });

            $("form").validate({
                rules: {
                    "<%=selectApplyMan.UniqueID %>": {
                        required: true,
                        maxlength: 30
                    },
                  <%--  "<%=txtBirthDate.UniqueID %>": {
                        required: true,
                        maxlength: 30
                    },
                    "<%=txtPosition.UniqueID %>": {
                        required: true,
                        maxlength: 30
                    },--%>
                    "<%=txtOrderNum.UniqueID %>": {
                        required: true,
                        digits: true
                    }
                },
                messages: {

                }
            });

            if ($("#<%=dropEnable.ClientID%>").val() == '0') {
                $("#<%=txtOutdutyDate.ClientID%>").rules("add", "required");
            }

            //当前状态为离职时，离职日期不得为空
            $("#<%=dropEnable.ClientID%>").change(function () {
                var selectedVal = $(this).val();
                if (selectedVal != '0') {
                    $("#<%=txtOutdutyDate.ClientID%>").val('');
                    $("#<%=txtOutdutyDate.ClientID%>").rules("remove", "required");
                }
                else {
                    $("#<%=txtOutdutyDate.ClientID%>").rules("add", "required");
                }
            });
        })



        function validateUser(account) {
            $.ajax({
                //async: false,
                type: "post",
                contentType: "application/x-www-form-urlencoded",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/NyPages/EmployeeInfor/AjaxPage/getEmployeeHandler.ashx',
                data: {
                    Op: "validateUser",
                    Account: account
                },
                dataType: "json",
                success: function (data) {
                    $("#userSpan").html("*");
                    result = true;
                    if (!data.IsSucess) {
                        $("#userSpan").html("*" + data.Message);
                        result = false;
                    }
                }
            });
        }


        function validateForm() {
            if ($("form").valid() && result) {
                $(<%=btnSave.ClientID%>).hide();
                return true;
            }
            else {
                return false
            };
        }
        function dpBirthDate() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }
        function dpOutdutyDate() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                onpicked: function () {
                    $("#<%=dropEnable.ClientID%>").val('0');
                }
            });
        }



        function dpGraduationTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }

        function dpGraduatesCheckTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',

                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }

        function dpInternshipStartTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                maxDate: '#F{$dp.$D(\'<%=txtInternshipEndTime.ClientID%>\')}',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }


        function dpInternshipEndTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                minDate: '#F{$dp.$D(\'<%=txtInternshipStartTime.ClientID%>\')}',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }

        function dpTrialStartTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                maxDate: '#F{$dp.$D(\'<%=txtTrialEndTime.ClientID%>\')}',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }

        function dpTrialEndTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd',
                minDate: '#F{$dp.$D(\'<%=txtTrialStartTime.ClientID%>\')}',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg" style="padding-bottom: 20px;">
        <input type="hidden" id="hidAttach" runat="server" />
        <input type="hidden" id="hidDelAttachID" runat="server" />
        <table class="table table-bordered" id="formSales">
            <tr>
                <td style="text-align: right;">姓名：
                </td>
                <td style="width: 203px">
                    <select multiple="" class="form-control chosen-select" id="selectApplyMan" name="SelectApplyMan" runat="server" data-placeholder="Choose a Account">
                        <option value=""></option>
                    </select>
                    <asp:TextBox ID="txtName" runat="server" Enabled="false" ReadOnly="true" Visible="false" BackColor="#F3F3F3"></asp:TextBox>
                    <span style="color: red" id="userSpan">*</span>
                </td>
                <td style="text-align: right;">性别：
                </td>
                <td>
                    <asp:DropDownList ID="dropSex" runat="server">
                        <asp:ListItem Value="0">男</asp:ListItem>
                        <asp:ListItem Value="1">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">出生日期：
                </td>
                <td style="width: 203px">
                    <asp:TextBox ID="txtBirthDate" runat="server" onclick="dpBirthDate()"></asp:TextBox><%--<span style="color: red">*</span>--%>
                </td>
                <td style="text-align: right;">部门：
                </td>
                <td>
                    <%-- <asp:TextBox ID="txtDeptName" runat="server" ReadOnly="true" BackColor="#F3F3F3"></asp:TextBox>--%>
                    <input size="10" type="text" id="txtDeptName1" name="DeptName" runat="server" class="form-control " readonly="readonly" style="background-color: #F3F3F3" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">职位：
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox><%--<span style="color: red">*</span>--%>
                </td>
                <td style="text-align: right;">招聘性质：
                </td>
                <td>
                    <asp:DropDownList ID="dropRecruitNature" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">学历：</td>
                <td>
                    <asp:DropDownList ID="dropEducation" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">学位：</td>
                <td>
                    <asp:TextBox ID="txtEducationalLevel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">毕业学校：</td>
                <td>
                    <asp:TextBox ID="txtGraduatedSchool" runat="server" Style="width: 100%;"></asp:TextBox>
                </td>
                <td style="text-align: right;">专业：</td>
                <td>
                    <asp:TextBox ID="txtProfession" runat="server" Style="width: 100%;"></asp:TextBox>
                </td>
                <td style="text-align: right;">毕业时间：
                </td>
                <td>
                    <asp:TextBox ID="txtGraduationTime" runat="server" Style="width: 100%;" onclick="dpGraduationTime()"></asp:TextBox>
                </td>
                <td style="text-align: right;">应届生报到日期：
                </td>
                <td>
                    <asp:TextBox ID="txtGraduatesCheckTime" runat="server" Style="width: 100%;" onclick="dpGraduatesCheckTime()"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td style="text-align: right;">实习开始日期：
                </td>
                <td>
                    <asp:TextBox ID="txtInternshipStartTime" runat="server" Style="width: 100%;" onclick="dpInternshipStartTime()"></asp:TextBox>
                </td>
                <td style="text-align: right;">实习结束日期：
                </td>
                <td>
                    <asp:TextBox ID="txtInternshipEndTime" runat="server" Style="width: 100%;" onclick="dpInternshipEndTime()"></asp:TextBox>
                </td>
                <td style="text-align: right;">试用开始日期：
                </td>
                <td>
                    <asp:TextBox ID="txtTrialStartTime" runat="server" Style="width: 100%;" onclick="dpTrialStartTime()"></asp:TextBox>
                </td>
                <td style="text-align: right;">试用结束日期：
                </td>
                <td>
                    <asp:TextBox ID="txtTrialEndTime" runat="server" Style="width: 100%;" onclick="dpTrialEndTime()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">当前状态：
                </td>
                <td>
                    <asp:DropDownList ID="dropEnable" runat="server">
                        <asp:ListItem Value="1">在职</asp:ListItem>
                        <asp:ListItem Value="0">离职</asp:ListItem>
                        <asp:ListItem Value="2">其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">离职日期：
                </td>
                <td>
                    <asp:TextBox ID="txtOutdutyDate" runat="server" Style="width: 100%;" onclick="dpOutdutyDate()"></asp:TextBox><%--<span style="color: red">*</span>--%>
                </td>
                <td style="text-align: right; display: none">排序号：
                </td>
                <td style="display: none">
                    <asp:TextBox ID="txtOrderNum" runat="server" Style="width: 100%;">1</asp:TextBox>
                </td>
                <td style="text-align: right;">政治面貌：
                </td>
                <td colspan="3" style="width: 120px">
                    <asp:DropDownList ID="dropZZMM" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">附件列表：
                </td>
                <td colspan="3" style="width: 120px">
                    <uc1:UploadAttach runat="server" ID="UploadAttach" />
                </td>
            </tr>

            <tr>
                <td colspan="8" class="text-center">

                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn-primary btn" UseSubmitBehavior="false" OnClientClick="return validateForm()">
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
