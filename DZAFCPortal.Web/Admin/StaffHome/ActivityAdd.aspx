<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="ActivityAdd.aspx.cs" Inherits="DZAFCPortal.Web.Admin.StaffHome.ActivityAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Admin/ThirdLibs/My97DatePicker/WdatePicker.js"></script>
    <!--  输入验证脚本 Start -->
    <script src="/Scripts/Admin/js/jquery.validate.min.js"></script>
    <script src="/Scripts/Admin/js/messages_zh.min.js"></script>
    <!-- 输入验证脚本 End -->
    <!-- Ueditor必要的脚本 -->
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.min.js"> </script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>
    <!-- Ueditor END -->
    <script type="text/javascript">
        //加载时，设置需要显示的内容类型
        $(function () {
            //实例化编辑器
            //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
            var contentId = "<%=txtActDescription.ClientID%>";
            var actWay = "<%=txtActWay.ClientID%>";
            UE.getEditor(contentId);
            UE.getEditor(actWay);
        });

        //验证提交表单的数据
        //输入验证
        $().ready(function () {
            $("form").validate({
                rules: {
                    "<%=txtName.UniqueID %>": {
                        required: true,
                        maxlength: 50
                    },
                    "<%=txtMaxPersonCount.UniqueID %>": {
                        required: true,
                        digits: true
                    },
                    "<%=beginTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=endTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=bookbeginTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=bookendTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=txtSummary.UniqueID %>": {
                        required: true,
                        maxlength: 100
                    },
                    "<%=txtPublishDept.UniqueID %>": {
                        required: true,
                    },
                },
                messages: {

                }
            });
        });

        function descriptionVal() {
            var result = true;
            var description = UE.getEditor('<%=txtActDescription.ClientID%>').hasContents();

            if (!description) {
                $('#spanDes').html('必填');
                result = false;
            }
            return result;
        }

        function imgVal() {
            var result = true;
            var src = $("#<%=imgUpload.ClientID%>").attr("src");
            if (src == undefined) {
                $('#imgSpan').html('必填');
                result = false;
            }
            return result;
        }

        function dpStartTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                maxDate: '#F{$dp.$D(\'<%=endTime.ClientID%>\')}',
                onpicked: function () {
                    // onDateChange(true);
                }
            });
        }

        function dpEndTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                minDate: '#F{$dp.$D(\'<%=beginTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
                }
            });
        }

        function dpBookStartTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                maxDate: '#F{$dp.$D(\'<%=bookendTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
                }
            });
        }

        function dpBookEndTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                minDate: '#F{$dp.$D(\'<%=bookbeginTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
                }
            });
        }

        function indexImgChange() {
            $("#<%=btnUpload.ClientID%>").click();
        }

        function validateForm() {
            if ($("form").valid() && imgVal() && descriptionVal()) {
                //if ($("form").valid()) {
                $(<%=btnSave.ClientID%>).hide();
                return true;
            }
            else return false;
        }
        function RefreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg">
        <div class=" BEA_table_title">
        </div>
        <table class="table table-bordered BEA_table">
            <tr>
                <td style="width: 120px;">标题：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>标题图片：</td>
                <td>
                    <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                    <asp:Image ID="imgUpload" runat="server" Visible="false" Width="150px" Height="100px" />
                    <asp:HiddenField ID="hidUpload" runat="server" />
                    <div style="color: red">建议使用280x200像素,大小小于100K</div>
                    <span style="color: red" id="imgSpan">*</span>
                </td>
            </tr>
            <tr>
                <td>活动名额：</td>
                <td>
                    <asp:TextBox ID="txtMaxPersonCount" runat="server" CssClass="form-control"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr runat="server" visible="false">
                <td>报名类型：
                </td>
                <td>
                    <asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="个人活动"></asp:ListItem>
                        <asp:ListItem Value="2" Text="团队活动"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trTeamMaxPerson" visible="false">
                <td>团队人数：</td>
                <td>
                    <asp:TextBox ID="txtTeamMaxPerson" runat="server" CssClass="form-control"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>活动时间：</td>
                <td>
                    <input id="beginTime" type="text" runat="server" onclick="dpStartTime()" />&nbsp;-
                    <input id="endTime" type="text" runat="server" onclick="dpEndTime()" />
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>报名时间：</td>
                <td>
                    <input id="bookbeginTime" type="text" runat="server" onclick="dpBookStartTime();" />&nbsp;-
                    <input id="bookendTime" type="text" runat="server" onclick="dpBookEndTime();" />
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>发布部门：</td>
                <td>
                    <asp:TextBox ID="txtPublishDept" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>简介：</td>
                <td>
                    <asp:TextBox ID="txtSummary" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>活动描述：</td>
                <td>
                    <textarea id="txtActDescription" name="ActDescription" style="width: 100%; min-height: 200px;" runat="server"></textarea>
                    <span style="color: red" id="spanDes">*</span>
                    <%--<asp:TextBox ID="txtActDescription" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                <td class="td_label">报名方式：</td>
                <td>
                    <%-- <asp:TextBox ID="txtActWay" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>--%>
                    <textarea id="txtActWay" style="width: 100%; min-height: 200px;" runat="server"></textarea>
                </td>
            </tr>

            <tr>
                <td colspan="2" class="text-center">

                    <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn-primary btn cancel" OnClientClick="return validateForm()"> <span class="glyphicon glyphicon-floppy-disk">保存</span> </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        <span class="btn btn-default" onclick=" closeWin();">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </span>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
