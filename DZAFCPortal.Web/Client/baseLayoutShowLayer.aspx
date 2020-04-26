<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baseLayoutShowLayer.aspx.cs" Inherits="DZAFCPortal.Web.Client.baseLayoutShowLayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设置常用链接</title>
    <meta name="renderer" content="ie-comp" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/base.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/layouts.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/flickerplate.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/pagenavi.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/jqpager.css" rel="stylesheet" />
    <script src="/Scripts/jquery/jquery-1.11.1.js"></script>
    <script src="/Scripts/jquery/jquery-migrate-1.2.1.js"></script>
    <script src="/Scripts/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Scripts/Client/js/flickerplate.min.js"></script>
    <script src="/Scripts/Client/js/clientCommon.js"></script>

    <script src="/Scripts/Client/ThirdLibs/layer/layer.min.js"></script>
    <link href="/Scripts/Client/ThirdLibs/layer/skin/layer.css" rel="stylesheet" />

    <script src="/Scripts/Client/ThirdLibs/knockout-3.2.0.js"></script>

    <script type="text/javascript" src="/Scripts/jquery/jquery-ui-1.11.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery/jquery.ui.touch-punch.min.js"></script>
    <!-- 本地自定义的脚本的合集 -->
    <style>
        html {
            min-height: 500px;
            min-width: 700px;
        }

        .xubox_layer, .xubox_main {
            -moz-border-radius: 15px;
            -webkit-border-radius: 15px;
            border-radius: 15px;
        }

        .xubox_title {
            -webkit-border-top-left-radius: 15px;
            border-top-left-radius: 15px;
            -webkit-border-top-right-radius: 15px;
            border-top-right-radius: 15px;
        }

        body {
            background: #ffffff !important;
        }

        .div_selectIcon {
            width: 385px;
            height: 425px;
            border: 3px double #AFAFAF;
            float: left;
            padding: 15px;
            -moz-border-radius: 15px;
            -webkit-border-radius: 15px;
            border-radius: 15px;
            margin: 0px 5px 0px 9px;
            background-color: #F2F2F2;
            overflow-y: scroll;
        }

            .div_selectIcon .dataitem:hover, .dataitem:focus {
                -moz-box-shadow: 0 2px 10px #999999;
                -webkit-box-shadow: 0 2px 10px #999999;
                box-shadow: 0 2px 10px #999999;
            }

        #div_unSelectedIcon .removeCurrent {
            display: none;
            height: 14px;
            width: 14px;
            position: absolute;
            background: url("/Scripts/Client/images/removeCurrent_close.png") no-repeat;
            right: 6px;
            top: 6px;
        }

        #div_SelectedIcon .removeCurrent {
            display: block;
            height: 14px;
            width: 14px;
            position: absolute;
            background: url("/Scripts/Client/images/removeCurrent_close.png") no-repeat;
            right: 8px;
            top: 8px;
        }

            #div_unSelectedIcon .removeCurrent:hover, #div_SelectedIcon .removeCurrent:hover {
                cursor: pointer;
            }

        .div_showSortIndex_span {
            display: block;
            width: 370px;
            margin: 10px 0px 0px 30px;
            font-family: 'Microsoft YaHei';
            font-weight: bold;
            font-size: 14px;
        }

        .dataitem {
            /*position: relative !important; */
            width: 83px;
            height: 83px;
            /*border: 1px solid #dddddd;*/
        }

        /* 半透明的遮罩层 */
        #overlay {
            background: #000;
            filter: alpha(opacity=0); /* IE的透明度 */
            opacity: 0; /* 透明度 */
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            z-index: 1000; /* 此处的图层要大于页面 */
        }


        .div_showSortIndexIcon_ul {
            margin-right: 18px;
        }

            .div_showSortIndexIcon_ul li {
                font-family: 'Microsoft YaHei';
                width: 70px;
                height: 25px;
                line-height: 25px;
                margin-left: 1px;
                float: left;
                background-color: #f9f9f9;
                border: 1px solid #dddddd;
                border-bottom: 0px;
                -webkit-border-top-left-radius: 10px;
                border-top-left-radius: 10px;
                -webkit-border-top-right-radius: 10px;
                border-top-right-radius: 10px;
                text-align: center;
                cursor: pointer;
                color: #999999;
            }

                .div_showSortIndexIcon_ul li:hover {
                    opacity: 0.5 !important;
                    color: #009BDF !important;
                }

                .div_showSortIndexIcon_ul li.activeli {
                    background-color: #F2F2F2;
                    border-color: #bbbbbb;
                    color: #009BDF;
                }

        
    </style>

    <!-- 首页项目选中||取消脚本 -->
    <script type="text/javascript">
        //移除选中项
        function unChecked(obj) {
            var html = $(obj).parent().parent();

            $(html).removeClass().addClass("fl dataitem draggable").addClass("ui-draggable-handle");

            SetDraggable($(html));

            $("#div_unSelectedIcon").append($(html));
        }

        //设置左侧项可以拖动到右侧
        function SetDraggable(obj) {
            $(obj).draggable({
                connectToSortable: "#div_SelectedIcon",
                revert: "invalid",
                start: function (event, ui) {
                    isStartDragge = true;
                },
                stop: function (event, ui) {
                    isStartDragge = false;
                    if ($(this).parent().attr("id") == "div_SelectedIcon") {
                        //移除拖动样式，添加排序样式
                        $(this).removeClass().addClass("fl dataitem ui-sortable-handle");
                    }
                }
            });
        }

        //保存拖动排序的数据，传入到后台处理
        function postSortHanddlerAndColoseLayer() {
            var selectedOrder = "";
            var unSelectedOrder = "";
            $("#div_SelectedIcon .dataitem").each(function () {
                if (!$(this).hasClass("ui-draggable-dragging")) //表示正在拖动的项，不传入后台
                {
                    selectedOrder += $(this).attr("argVal") + ",";
                }
            });

            $("#div_unSelectedIcon .dataitem").each(function () {
                unSelectedOrder += $(this).attr("argVal") + ",";
            });

            $.ajax({
                type: "post",
                url: '<%= DZAFCPortal.Config.Base.ClientBasePath%>/AjaxPage/getHomeLinkHandler.ashx',
                data: {
                    op: "save",
                    selectedIds: selectedOrder,
                    unSelectedIds: unSelectedOrder
                },
                beforeSend: function () {
                    //show.html("<img src='/Scripts/Client/images/loading.gif' /> 正在更新");
                },
                success: function () {
                    //关闭当前层
                    window.parent.closeUserConfigLayer();
                },
                error: function () {
                    alert("保存失败！")
                }

            });
        }

        function setDraggable() {
            //设置右侧的项可拖动进行排序
            $("#div_SelectedIcon").sortable({

            });

            //拖曳完停止后提交排序数据到程序后台
            $("#div_SelectedIcon").on("sortstop",
                function (event, ui) {

                });

            //设置左侧项可以拖动到右侧
            SetDraggable($("#div_unSelectedIcon  li"));

            $("#div_unSelectedIcon, #div_SelectedIcon").disableSelection();
        }
    </script>

    <!-- Koncout 绑定链接图标 -->
    <script type="text/javascript">
        var currentIndexApp = new IndexModelApp();

        function IndexModelApp() {
            var self = this;

            //已经选中的链接图标
            self.SelectedLinks = ko.observableArray([]);
            //未（带）选择的链接图标
            self.UnSelectedLinks = ko.observableArray([]);

            //所以可以在左侧的数据
            self.CommonConfigLinks = [];
            //所有的推荐数据
            self.RecommandLinks = [];
        }

        $(function () {
            ////设置滚动条
            //$("#div_unSelectedIcon").niceScroll({ cursorcolor: "#B7B7B7", cursorwidth: "20px", autohidemode: false });

            ko.applyBindings(currentIndexApp, $("#div_showSortIndexIcon")[0]);
            loadLinks();
        });

        function loadLinks() {
            $.ajax({
                cache:false,
                type: "get",
                url: '<%= DZAFCPortal.Config.Base.ClientBasePath%>/AjaxPage/getHomeLinkHandler.ashx',
                dataType: "json",
                data: {
                    op: "linkConfig"
                },
                success: function (datas) {
                    //lert(datas.CommonConfigLinks);
                    //formatLinkArrayUrl(datas.CommonLinks);
                    //formatLinkArrayUrl(datas.CommonConfigLinks);
                    //formatLinkArrayUrl(datas.RecommandLinks);

                    currentIndexApp.RecommandLinks = datas.RecommandLinks;
                    currentIndexApp.CommonConfigLinks = datas.CommonConfigLinks;

                    currentIndexApp.SelectedLinks(datas.CommonLinks);

                    swichTyep(1);
                }
            });
        }

        //切换类型，1表示全部  0表示推荐
        function swichTyep(type, obj) {
            $(".div_showSortIndexIcon_ul li").each(function () {
                $(this).removeClass("activeli");
            });

            if (obj == null || obj == undefined) {
                obj = $(".div_showSortIndexIcon_ul li").first();
            }
            $(obj).addClass("activeli");

            if (type == "1") {
                var items = currentIndexApp.CommonConfigLinks;
                removeLeftDatasInRight(items);

                currentIndexApp.UnSelectedLinks(items);
                setDraggable();
            }
            else {
                var items = currentIndexApp.RecommandLinks;
                removeLeftDatasInRight(items);

                currentIndexApp.UnSelectedLinks(items);
                setDraggable();
            }
        }

        //移除已经存在在左侧的数据
        function removeLeftDatasInRight(items) {
            var selectedOrder = "";
            $("#div_SelectedIcon .dataitem").each(function () {
                if (!$(this).hasClass("ui-draggable-dragging")) //表示正在拖动的项，不传入后台
                {
                    selectedOrder += $(this).attr("argVal") + ",";
                }
            });

            if (!Array.prototype.indexOf) {
                Array.prototype.indexOf = function (elt /*, from*/) {
                    var len = this.length >>> 0;
                    var from = Number(arguments[1]) || 0;
                    from = (from < 0)
                         ? Math.ceil(from)
                         : Math.floor(from);
                    if (from < 0)
                        from += len;
                    for (; from < len; from++) {
                        if (from in this &&
                            this[from] === elt)
                            return from;
                    }
                    return -1;
                };
            }

            var selectedOrderArray = selectedOrder.split(",");
            for (var i = items.length - 1; i >= 0; i--) {
                if (selectedOrderArray.indexOf(items[i].ID) >= 0) {
                    items.splice(i, 1);
                }
            }
        }

        function getIndexIconBackground(icon) {
            return "url('" + icon + "')";
        }

        var EQUIPMENT = -1;
        function formatLinkArrayUrl(items) {
            for (var i = items.length - 1; i >= 0; i--) {
                formatLinkUrl(items[i]);

                if (!isLinkItemVisible(items[i].Url)) {
                    items.splice(i, 1);
                }
            }
        }

        function formatLinkUrl(item) {
            var url = "";

            switch (EQUIPMENT) {
                case 0:
                    url = item.Url; break;
                case 1:
                    url = item.PadUrl; break;
                case 2:
                    url = item.PhoneUrl; break;
            }

            item.Url = url;
        }

        function isLinkItemVisible(url) {
            return !(url == null || url == undefined || url == "");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--  用来配置用户首页图标显示的层 -->
        <div id="div_showSortIndexIcon">
            <div class="div_showSortIndex_span fl" style="margin-left: 20px;">
                未选列表：
            </div>
            <div class="div_showSortIndex_span fl">已选列表：</div>
            <div class="clear"></div>
            <!-- 未选列表 -->
            <ul id="div_unSelectedIcon" class="div_selectIcon" data-bind="foreach: currentIndexApp.UnSelectedLinks" style="margin-left: 20px">
                <li class="fl dataitem " data-bind="attr: { argval: ID, title: Name }" style="height: 90px;">
                    <a class="home_footer_top_text_setting">
                        <img data-bind="attr: { src: Icon }" alt="" />
                        <span class="home_footer_top_text_name" data-bind="text: Name" style="width: 100%; color: #333333;"></span>
                        <i class="removeCurrent" onclick="unChecked(this);"></i>
                    </a>
                </li>
            </ul>
            <ul id="div_SelectedIcon" class="div_selectIcon" data-bind="foreach: currentIndexApp.SelectedLinks">
                <li class="fl dataitem " data-bind="attr: { argval: ID, title: Name }" style="height: 90px;" >
                    <a class="home_footer_top_text_setting">
                        <img data-bind="attr: { src: Icon }" alt="" />
                        <span class="home_footer_top_text_name" data-bind="text: Name" style="width: 100%; color: #333333;"></span>
                        <i class="removeCurrent" onclick="unChecked(this);"></i>
                    </a>
                </li>

            </ul>
            <div class="radius15_bg fl" style="margin-left: 36%; margin-top: 10px; width: 70px;">
                <a href="#" class="btn_work fl" onclick="postSortHanddlerAndColoseLayer();return false;">保 存</a>
            </div>
            <div class="radius15_bg fl" style="margin-left: 50px; margin-top: 10px; width: 70px;">
                   <a href="#" class="btn_work fl" onclick="  window.parent.closeUserConfigLayer();return false;">关 闭</a>
            </div>
        </div>
    </form>
</body>
</html>
