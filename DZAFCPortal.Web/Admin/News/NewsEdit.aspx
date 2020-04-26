<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="DZAFCPortal.Web.Admin.News.NewsEdit" %>

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
            var contentId = "<%=editorContent.ClientID%>";
            UE.getEditor(contentId);
           
        });

        //验证提交表单的数据
        //输入验证
        $().ready(function () {
            $("form").validate({
                rules: {
                    "<%=txtTitle.UniqueID %>": {
                        required: true,
                        maxlength: 80
                    },
                    "<%=txtPulishTime.UniqueID %>": {
                        required: true
                    }
                },
                messages: {

                }
            });
        });
        function check() {
            var Id = getQueryString("ID");
            var ids = $('input[type="checkbox"]');
            var flag = false;
            if (Id != null) {
                flag = true;
                return flag;
            }
            for (var i = 0; i < ids.length; i++) {
                if (ids[i].checked) {
                    flag = true;
                    break;
                }
            }
        
            if (!flag) {
                alert("请最少选择一项类别！");
                flag = false;
            }
            return flag;
        }
        function validateForm() {
            if ($("form").valid() && imgVal()) {
                $(<%=btnSave.ClientID%>).hide();
                return true;
            }
            else {
                return false
            };
        }
        function imgVal() {
            var result = true;
            var src = $("#<%=imgUpload.ClientID%>").attr("src");
            if (src == undefined) {
                $('#imgSpan').html('*必填');
                result = false;
            }
          <%--  var picName = $("#<%=hidUpload.ClientID %>").val();
            if (VerSpeChar(picName))
            {
                $('#imgSpan').html('*图片名字含有特殊字符');
                result = false;
            }--%>
            return result;
        }


        function indexImgChange() {
            $("#<%=btnUpload.ClientID%>").click();
        }

        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return decodeURI(r[2]); return null;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="BEA_div_bg">
        <table class="table table-bordered">
            <tr>
                <td style="width: 100px;">标题：
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
             <tr>
                <td>标题图片：</td>
                <td>
                    <asp:FileUpload ID="fileIndexImgUrl" runat="server" onchange="indexImgChange()" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" Style="display: none" CssClass="cancel" />
                    <asp:Image ID="imgUpload" runat="server" Visible="false" Width="180px" Height="100px" />
                    <asp:HiddenField ID="hidUpload" runat="server" />
                    <div style="color: red">建议使用360x200像素,大小小于100K</div>
                    <span style="color: red" id="imgSpan">*</span>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">栏目：
                </td>
                <td>
                  <%--  <asp:CheckBoxList ID="checkboxType" runat="server"></asp:CheckBoxList>--%>
                    <asp:DropDownList ID="dropType" runat="server" Visible="false" Enabled="false"></asp:DropDownList>
                    <asp:Label ID="labType" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr id="trType" runat="server">
                <td style="width:100px" id="tdType" runat="server">其他栏目：
                </td>
                <td>
                     <asp:CheckBoxList ID="checkboxType" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>发布人：</td>
                <td>
                    <%--<asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control"></asp:TextBox>--%>
                    <asp:Label ID="labPublisher" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>发布时间：</td>
                <td>
                    <asp:TextBox ID="txtPulishTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="form-control"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
            </tr>
           <%-- <tr>
                <td>简介：</td>
                <td>
                    <asp:TextBox ID="txtSummary" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td>排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNum" runat="server" TextMode="Number">1</asp:TextBox><span style="color: red">* <span id="spanOrderNum">信息将按排序号、发布日期降序排序</span></span>
                </td>
            </tr>
            <tr>
                <td class="td_label">详细描述：
                </td>
                <td>
                    <textarea id="editorContent" style="width: 100%; min-height: 200px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="td_label">附件列表：
                </td>
                <td>
                    <uc1:UploadAttach runat="server" ID="UploadAttach" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">

                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn-primary btn" OnClientClick="return validateForm()">
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
