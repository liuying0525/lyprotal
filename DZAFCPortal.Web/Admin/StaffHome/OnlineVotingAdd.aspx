<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="OnlineVotingAdd.aspx.cs" Inherits="DZAFCPortal.Web.Admin.StaffHome.OnlineVotingAdd" %>
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
        //验证提交表单的数据
        //输入验证
        $(function () {
            //实例化编辑器
            //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
            var contentId = "<%=txtActDescription.ClientID%>";
            UE.getEditor(contentId);
        });
        $().ready(function () {
            $("form").validate({
                rules: {
                    "<%=txtName.UniqueID %>": {
                        required: true,
                        maxlength: 50
                    },
                    "<%=txtPublishDept.UniqueID %>": {
                        required: true,
                        maxlength: 20
                    },
                    "<%=txtSummary.UniqueID %>": {
                        required: true,
                        maxlength: 100,
                    },
                    "<%=beginTime.UniqueID %>": {
                        required: true,
                    },
                    "<%=endTime.UniqueID %>": {
                        required: true,
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

        function descriptionVal() {
            var result = true;
            var description = UE.getEditor('<%=txtActDescription.ClientID%>').hasContents();

            if (!description) {
                $('#spanDes').html('必填');
                result = false;
            }
            return result;
        }


        function validateForm() {
            if ($("form").valid() && imgVal() && descriptionVal()) {
            //if ($("form").valid()) {
                var option = Option();
                if (option) {
                    $(<%=btnSave.ClientID%>).hide();
                    return true;
                }
                else {
                    return false;
                }
            }
            else return false;
        }
        function Option() {
            var sucess = true;
            var dropOnline = $("#<%=dropOnline.ClientID%>").val();
            if (dropOnline == '1') {
                alert('请选择投票选项！');
                $("#dropOnline").focus();
                sucess = false;
                return false;
            }
            var num = 0;
            $("input,textarea").each(function (i) {
                if ($(this).attr("t") == "option") {
                    if ($(this).val() == "") {
                        alert('投票选项不能为空！');
                        $(this).focus();
                        sucess = false;
                        return false;
                    }
                    num = num+1;
                }
            });

            $("input,textarea").each(function (i) {
                if ($(this).attr("t") == "option") {
                    if ($(this).val().length >50) {
                        alert('投票选项最大长度为50！');
                        $(this).focus();
                        sucess = false;
                        return false;
                    }
                }
            });
            var dropOnline = parseInt($("#<%=dropNum.ClientID%>").val());
            if (dropOnline > num) {
                alert('可选数量不能大于投票个数！');
                sucess = false;
                return false;
            }
            return sucess;
        }
        function indexImgChange() {
            $("#<%=btnUpload.ClientID%>").click();
        }

        function RefreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
        function dpStartTime() {
            WdatePicker({
                dateFmt: 'yyyy-MM-dd HH:mm',
                maxDate: '#F{$dp.$D(\'<%=endTime.ClientID%>\')}',
                onpicked: function () {
                    //onDateChange(true);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="BEA_div_bg">
        <div class=" BEA_table_title">
        </div>
        <table class="table table-bordered BEA_table" id="tableOnline" runat="server">
            <tr>
                <td>标题：
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox><span style="color: red">* <span id="spanCodeTitle"></span></span>
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
                    <asp:TextBox ID="txtPublishDept" runat="server" CssClass="form-control"></asp:TextBox> <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>投票时间：</td>
                <td>
                    <input id="beginTime" type="text" runat="server" onclick="dpStartTime()" />-
                    <input id="endTime" type="text" runat="server" onclick="dpEndTime()" />
                     <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>简介：</td>
                <td>
                    <asp:TextBox ID="txtSummary" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><span style="color: red">* <span></span></span>
                </td>
            </tr>
            <tr>
                <td>投票描述：</td>
                <td>
                    <textarea id="txtActDescription" name="ActDescription" style="width: 100%; min-height: 200px;" runat="server"></textarea>
                    <span style="color: red" id="spanDes">*</span>
                </td>
            </tr>
              <tr style="display:none">
                <td class="td_label">附件列表：
                </td>
                <td>
                    <uc1:UploadAttach runat="server" ID="UploadAttach" />
                </td>
            </tr>
            <tr>
                <td>可选数量：</td>
                <td>
                    <asp:DropDownList ID="dropNum" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>投票类型：</td>
                <td>
                    <asp:DropDownList ID="dropVoteType" runat="server">
                        <asp:ListItem Value="MaxNum" Text="最多可选"></asp:ListItem>
                        <asp:ListItem Value="MustNum" Text="必须选"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trOnline" runat="server">
                <td>投票选项：</td>
                <td>
                    <asp:DropDownList ID="dropOnline" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
