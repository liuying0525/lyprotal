<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="HighlightAdd.aspx.cs" Inherits="DZAFCPortal.Web.Admin.StaffHome.HighlightAdd" %>

<%@ Register Src="../Controls/UploadAttach.ascx" TagPrefix="uc1" TagName="UploadAttach" %>
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
            var contentId = "<%=txtItemSummary.ClientID%>";
            UE.getEditor(contentId);
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
                    "<%=beginTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=endTime.UniqueID %>": {
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
                maxDate: '#F{$dp.$D(\'<%=endTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
                }
            });
        }

        function dpBookEndTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                minDate: '#F{$dp.$D(\'<%=beginTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
                }
            });
        }

        function indexImgChange() {
            $("#<%=btnUpload.ClientID%>").click();
        }

        function validateForm() {
            if ($("form").valid() && imgVal()) {
                $(<%=btnSave.ClientID%>).hide();
                return true;
            }
            else return false;
        }

        function RefreshParent() {
            //window.opener.location.href = window.opener.location.href;
            //window.close();
            window.opener = null;
            window.open('', '_self'); 
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg">
        <div class=" BEA_table_title">
        </div>
        <table class="table table-bordered BEA_table" id="tableOnline" runat="server">
            <tr>
                <td style="width: 100px;">活动标题：
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
                <td>发布部门：</td>
                <td>
                    <asp:TextBox ID="txtPublishDept" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>活动时间：</td>
                <td>
                    <input id="beginTime" type="text" runat="server" onclick="dpStartTime()" />-
                    <input id="endTime" type="text" runat="server" onclick="dpEndTime()" />
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>简介：</td>
                <td>
                    <asp:TextBox ID="txtSummary" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>活动描述：</td>
                <td>
                    <textarea id="txtItemSummary" style="width: 100%; min-height: 200px;" runat="server"></textarea><span style="color: red">*</span>
                </td>
            </tr>
        </table>
        <div class="BEA_table_title">
            <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btn-primary btn cancel" OnClientClick="return validateForm()">
                                <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
            </asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;
                        <span class="btn btn-default" onclick=" RefreshParent();">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </span>
        </div>
    </div>
</asp:Content>
