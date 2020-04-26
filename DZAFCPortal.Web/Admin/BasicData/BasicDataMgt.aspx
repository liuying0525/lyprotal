<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicDataMgt.aspx.cs" Inherits="DZAFCPortal.Web.Admin.BasicData.BasicDataMgt" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
    <!-- Ztree 脚本和样式 -->
    <link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
    <script src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
    <script src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.exedit-3.5.min.js"></script>
    <!-- Ztree End -->

    <!-- 用户自定义的样式 Start-->
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <!-- 用户自定义的样式和脚本引用 End-->

    <script src="/Scripts/jquery/jquery-form.js"></script>
    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
    <script src="NavManager.js?<%=SYSTEM_STARUP_TIME %>"></script>

    <script type="text/javascript">

        var navJs = new NavManager();
        $(function () {
            navJs.LoadTree("treeNavigator");
        });

        function newGuid() {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        }
    </script>
    <style>
        .bread_nav {
            padding-bottom: 0px;
            margin: 0px 0 20px 0;
            font-size: 14px;
            height: 40px;
            line-height: 40px;
            padding-left: 20px;
            font-weight: bold;
            border-bottom: 1px solid #808080;
            -moz-box-shadow: 0 0 20px #808080;
            -webkit-box-shadow: 0 0 20px #808080;
            box-shadow: 0 0 20px #808080;
            color: #ffffff;
            background: #075579;
            background-image: -webkit-gradient(linear, left bottom, left top, color-stop(0.32, #007A9C), color-stop(0.66, #075579));
            background-image: -webkit-linear-gradient(#007A9C, #075579);
            background-image: -moz-linear-gradient(top,#007A9C, #075579);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#007A9C', endColorstr='#075579');
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr='#007A9C', endColorstr='#075579')";
            background-image: -ms-linear-gradient(#007A9C, #075579);
            background-image: -o-linear-gradient(#007A9C, #075579);
            background-image: linear-gradient(#007A9C, #075579);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container container_Left" style="max-width: 98%">
            <div class="row">
                <div class="bread_nav">
                    <asp:Literal runat="server" ID="LiteralSiteMap">
                    </asp:Literal>
                </div>
            </div>

            <div class="center_left fl">
                <a class="btn-primary btn" data-bind=" click: navJs.currentModel.Clear">+ 新增</a>

                <ul id="treeNavigator" class="ztree smg_cms_tree" style="margin-top: 10px !important;"></ul>
            </div>
            <div class="center_right fr" data-bind="visible: navJs.currentModel.isOnclick">
                <span class="center_right_title">填写信息</span>
                <table class="table table-bordered BEA_table" style="background-color: #f3f3f3; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px; overflow: hidden;">

                    <tr>
                        <td>名称：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navItem.Name" />
                        </td>
                    </tr>
                    <tr>
                        <td>英文名称：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navItem.EnglishName" />
                        </td>
                    </tr>
                    <tr>
                        <td>上级导航名称：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navJs.currentModel.ParentNavName" disabled="disabled" />
                        </td>
                    </tr>
                    <tr>
                        <td>排序号：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navItem.OrderNum" />
                        </td>
                    </tr>
                    <tr>
                        <td>Url：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navItem.Url" />

                        </td>
                    </tr>
                    <tr>
                        <td>描述：</td>
                        <td>
                            <input type="text" class="form-control" data-bind=" value: navItem.Description" />
                        </td>
                    </tr>
                    <tr>
                        <td>图标：</td>
                        <td>
                            <input runat="server" type="file" id="uploadFile" name="uploadFile" accept="image/*" class="file" onchange="UploadImg()" />
                            <input type="submit" id="btnSubmitFile" value=" 提交" onclick="SubmitImg();" style="display: none" />
                            <img class="thumb" data-bind="attr: { src: navItem.IconUrl }" />
                            <%--<input type="text" class="form-control" data-bind=" value: navItem.Description" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>是否显示：
                        </td>
                        <td>
                            <select data-bind=" value: navItem.IsShow" class=" form-control">
                                <option value="1">显示</option>
                                <option value="0">隐藏</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>跳转类型：
                        </td>
                        <td>
                            <select data-bind=" value: navItem.IsOpenedNewTab" class=" form-control">
                                <option value="0">直接跳转</option>
                                <option value="1">打开新标签页</option>
                            </select>
                        </td>
                    </tr>
                    <!-- 当应用范围选中为 特定人员时，角色选择可见 -->
                    <tr>
                        <td>可见范围</td>
                        <td>
                            <select data-bind=" value: navJs.currentModel.visibleScope" class=" form-control">
                                <option value="1">全部</option>
                                <option value="0">指定角色</option>
                            </select>
                            <ul data-bind=" foreach: navJs.currentModel.Roles, visible: navJs.currentModel.visibleScope() == '0'" class=" list-unstyled">
                                <li>
                                    <input type="checkbox" data-bind=" value: ID, checked: navJs.currentModel.navItem.ApplyRoles" />
                                    <label data-bind=" text: Name"></label>
                                </li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <a class="btn btn-default" data-bind="click: navJs.currentModel.Save" style="width: 120px; text-align: center; margin: 0 auto">保存</a>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>

