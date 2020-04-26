<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Main" %>

<%@ Register Src="./Controls/LeftNav.ascx" TagPrefix="uc1" TagName="LeftNav" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <!-- 用户自定义的样式和脚本引用 Start-->
    <link href="/Scripts/Admin/css/leftNav.css" rel="stylesheet" />
    <script src="/Scripts/Admin/js/common.js"></script>
    <!-- 用户自定义的样式和脚本引用 End-->
    <title>东正内网门户</title>

    <script type="text/javascript">
        $(function () {
            $(".nav_list>li>a").click(function () {
                $(this).parent().children("ul").toggle(400);
                $(this).parent().siblings().each(function () {
                    $(this).children("ul").attr("style", "display:none;");
                });
            });
        })


        //    //自适应高度
        //    function iFrameHeight() {
        //        var ifm = document.getElementById("iframepage");
        //        var subWeb = document.frames ? document.frames["iframepage"].document :
        //ifm.contentDocument;

        //        if (ifm != null && subWeb != null) {

        //            ifm.height = subWeb.body.scrollHeight;
        //        }
        //    }
    </script>
    <script>
        //自适应高度
        $(document).ready(function () {
            var Wheight = $(window).height() - 64;
            $("#iframe_default").css("min-height", Wheight + "px");
            $("#iframe_default").css("overflow", "hidden");
        });

        function windowCH() {
            var WCheight = $(window).height() - 64;
            $("#iframe_default").css("min-height", WCheight + "px");
            $("#iframe_default").css("overflow", "hidden");

        }

        $(window).resize(windowCH);

    </script>
</head>
<body>
    <div class="topNav">
        <img src="/Scripts/Admin/images/ny_ad_logo.png" alt="" class="topNav_logoimage fl"/>
      <%--  <img src="/Scripts/Admin/images/ny_header_logo.jpg" alt=""  class="topNav_image fr" />--%>
        后台管理平台
    </div>

    <div class="container_main">
        <div class="container_main_inner">
            <div class="sidebar">
                <uc1:LeftNav runat="server" ID="LeftNav" />
            </div>
            <div class="content_main">
                <iframe id="iframe_default" name="iframe_default" frameborder="0" width="100%"></iframe>
            </div>
            <div style="clear: both;"></div>
        </div>
    </div>
</body>
</html>
