<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadAttach.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.UploadAttach" %>


<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/jquery-ui-1.11.0.custom/jquery-ui.js"></script>
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/uploadify/jquery.uploadify.min.js"></script>
<link href="/Scripts/Admin/ThirdLibs/jquery-ui-1.11.0.custom/jquery-ui.css" rel="stylesheet" />

<!--第三方插件uploadify用到的样式和脚本-->
<link href="/Scripts/Admin/ThirdLibs/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

<!-- uploadify 样式END -->

<!--第三方插件uploadify操作附件上传-->
<script type="text/javascript">
    $(document).ready(function () {
        $("#uploadify").uploadify({
            //是否开启调试  
            'debug': false,
            //是否自动上传  
            'auto': false,
            'buttonText': '请选择文件',
            //显示的flash效果  
            'swf': "/Scripts/Admin/ThirdLibs/uploadify/uploadify.swf",
            //文件选择后的容器ID  
            'queueID': 'fileQueue',
            //响应上传事件
            'uploader': '<%= DZAFCPortal.Config.Base.AdminBasePath  %>/AjaxPage/uploadifyHandler.ashx',
            'width': '80',
            'height': '25',
            //是否允许多文件上传
            'multi': true,
            'fileTypeDesc': '支持的格式：',
            //允许上传的文件格式  *.格式
            // 'fileTypeExts': '*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.doc;*.docx;*.pdf;*.zip;*.rar;*.xls;*.xlsx;*.txt;*.ppt;*.pptx',
            'fileTypeExts': '*.*',
            'removeTimeout': 1,
            'queueSizeLimit': 50,
            'onUploadSuccess': function (file, data, response) {
                $(data).appendTo($("#showAttachList ul"));
            },
            //处理上传失败事件
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                if (errorString == "Cancelled") {

                }
                else {
                    alert(file.name + "文件上传过程发生错误，请重试");
                }
            }
        });
    });

    //开始上传
    function upload() {
        //清空提示消息
        $('#uploadify').uploadify('upload', '*')
    }
    //取消所有上传
    function cancelUpload() {
        $('#uploadify').uploadify("cancel");
    }

    //删除附件节点
    function delAttachNode(obj, hdName) {

        $(obj).parent().remove();
    }
</script>
<!--uploadify 附件上传 END -->

<style type="text/css">
    /*附件列表样式 Start */
    .attach-list {
        margin: 0;
        padding: 10px 0 0 0;
    }

        .attach-list ul {
            margin: 0;
            list-style: none;
            *display: inline-block;
        }

            .attach-list ul:after {
                content: ".";
                display: block;
                height: 0;
                clear: both;
                visibility: hidden;
            }

            .attach-list ul li {
                position: relative;
                float: left;
                margin: 0 15px 15px 0;
                padding: 5px 18px 8px 18px;
                border: 1px solid #e1e1e1;
                box-shadow: 0 0 3px 0 rgba(0, 0, 0, 0.2);
                width: 245px;
            }

                .attach-list ul li .icon {
                    position: absolute;
                    display: block;
                    top: 8px;
                    left: 2px;
                    width: 14px;
                    height: 14px;
                    text-indent: -99em;
                    background: url(/Scripts/Admin/images/del.png) no-repeat;
                    overflow: hidden;
                }

                .attach-list ul li .del {
                    position: absolute;
                    display: block;
                    top: 10px;
                    right: 0;
                    width: 20px;
                    height: 20px;
                    text-indent: -99em;
                    background: url(/Scripts/Admin/images/del.png) no-repeat;
                    cursor: pointer;
                    overflow: hidden;
                }

                .attach-list ul li .title {
                    display: block;
                    margin-bottom: 3px;
                    border-bottom: 1px solid #ccc;
                    line-height: 24px;
                    height: 26px;
                    font-weight: bold;
                    white-space: nowrap;
                    word-break: break-all;
                    overflow: hidden;
                }

                .attach-list ul li .info {
                    display: block;
                    line-height: 24px;
                }
    /* 附件样式表 END*/
</style>
<div>

    <div>

        <button type="button" class="btn btn-primary smg-client-upload-button" data-toggle="modal" data-target="#myModal">
            <span class="glyphicon glyphicon-open">上传附件</span>
        </button>

        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="padding-top: 84px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="file" name="uploadify" id="uploadify" title="请选择 图片，Word文档，PDF文档，文件压缩包，Excel文档等数据格式的文件" />
                    </div>
                    <div class="modal-body">
                        <div id="fileQueue" style="height: 300px;"></div>
                    </div>
                    <div class="modal-footer">
                        <button onclick="upload();return false;" class="btn btn-primary">
                            <span class="glyphicon glyphicon-open">开始上传</span>
                        </button>
                        <button onclick="cancelUpload();return false;" class="btn btn-danger">
                            <span class="glyphicon glyphicon-share-alt">取消上传</span>
                        </button>
                        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">
                            <span class="glyphicon glyphicon-remove">关闭</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="showAttachList" class="attach-list">
        <ul>
            <asp:Repeater ID="rptAttachList" runat="server">
                <ItemTemplate>
                    <li>
                        <input name="hid_attach_id" type="hidden" value="<%#Eval("ID")%>" />
                        <input name="hid_attach_fileName" type="hidden" value="<%#Eval("FileName")%>" />
                        <input name="hid_attach_fileShowName" type="hidden" value="<%#Eval("FileShowName")%>" />
                        <input name='hid_attach_fileExtension' type='hidden' value="<%#Eval("Extension")%>" />
                        <input name="hid_attach_fileUrl" type="hidden" value="<%#Eval("Url")%>" />
                        <input name="hid_attach_fileSize" type="hidden" value="<%#Eval("Size")%>" />

                        <a href="javascript:;" onclick="delAttachNode(this,'hid_attach_fileUrl');" title="删除附件" class="del">删除</a>
                        <div class="title">
                            <a href='<%#Eval("Url") %>' target="_blank">
                                <span class="glyphicon glyphicon-paperclip" title='<%#Eval("FileShowName")%>'><%#Eval("FileShowName")%> </span></a>
                        </div>
                        <div class="info">类型：<span class="ext"><%#Eval("Extension")%></span> 大小：<span class="size"><%#Fxm.Utility.FileHelper.FileSizeToString((long)Eval("Size")) %></span></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>


