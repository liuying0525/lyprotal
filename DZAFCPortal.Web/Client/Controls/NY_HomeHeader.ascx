<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NY_HomeHeader.ascx.cs" Inherits="DZAFCPortal.Web.Client.Controls.NY_HomeHeader" %>
<div class="ny_header">
    <div class="ny_header_info">
        <div class="ny_header_info_div">
            <img src="/Scripts/Client/images/ny_header_logo.jpg" alt="" class="ny_header_info_logo fl" />
            <div class="ny_header_info_person fr">
                <span class="ny_header_info_person_name"><%= NySoftland.Moss.Helper.GetCurrentDisplayName() %></span>
                <a class="ny_header_info_person_setting">设置<img src="/Scripts/Client/images/ny_header_person_arrow.png" alt="" /></a>
                <asp:Image ID="imgUser" runat="server" CssClass="ny_header_info_person_pic" />
                <div class="ny_header_info_person_setting_div" style="display: none;">
                    <ul class="ny_header_info_person_setting_div_ul">
                        <%--  <li><a href="javascript:void(0);" onclick="OpenEditUserInfo()">修改个人信息</a></li>--%>
                        <%-- <li><a href="javascript:void(0);" onclick="OpenEditPasswordInfo()">修改密码</a></li>--%>
                        <li><a href="javascript:void(0);" onclick="switchDifferentUser();">注销</a></li>
                        <asp:Label runat="server" ID="AdminAccess">
                                    <li class="divide">
                                        <a href='<%= DZAFCPortal.Config.Base.AdminBasePath + "/Main.aspx"%>' target="_blank">后台管理中心
                                        </a>
                                    </li>
                        </asp:Label>
                    </ul>
                </div>
            </div>
        </div>

    </div>
    <div class="ny_header_uldiv">
        <ul class="ny_header_ul">
            <!-- 选中状态：在li中加上class="active" -->
            <li class="ny_header_ul_li active"><a href='<%=DZAFCPortal.Web.Client.Utils.FormatUrl("/Index.aspx") %>'>首页</a></li>
            <asp:Repeater ID="rptNavigator" runat="server" OnItemDataBound="rptNavigator_ItemDataBound">
                <ItemTemplate>
                    <li id='<%#Eval("ID") %>' class="ny_header_ul_li"><a><%#Eval("Title") %></a>
                        <ul class="ny_header_ulsub" style="display: none;">
                            <asp:Repeater runat="server" ID="rptChildNav">
                                <ItemTemplate>
                                    <li>
                                        <%--  <a href='<%#GetSecondaryUrl(Eval("ID").ToString(),Eval("ParentID").ToString()) %>'>--%>
                                        <a href='<%#GetSecondaryUrl(Eval("ID").ToString(),Eval("ParentId").ToString(),Eval("Url").ToString()) %>'>
                                            <%#   DZAFCPortal.Utility.StringExt.CutString(Eval("Title").ToString(),12) %>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
<div class="clear"></div>
<script>
    $(function () {
        var temprand = parseInt(3 * Math.random());
        path = "/Scripts/Client/images/ny_container_leftul_bg" + temprand + ".png";
        $(".ny_container_leftul_bg").attr("src", path);


        $(".ny_header_info_person_setting").click(function (e) {

            if (e.target == this) {
                e.stopPropagation();
                $(".ny_header_info_person_setting_div").fadeToggle(180);
                $("#overlay").fadeToggle(180);
            }

        });
        $('#overlay').click(function () {
            $(".ny_header_info_person_setting_div").hide();
            $("#overlay").hide();
        });


        $(".ny_change_icon").click(function (e) {
            if (e.target == this) {
                e.stopPropagation();
                if ($(this).hasClass("ny_right_block_close_icon")) {

                    $(this).removeClass("ny_right_block_close_icon");
                    $(".ny_right_block_bg").removeClass("ny_right_block_bg_right20");
                    $(this).addClass("ny_right_block_open_icon");
                    $(".ny_right_block_bg").addClass("ny_right_block_bg_right300");
                    $(".ny_right_block").addClass("ny_right_block_20");

                }
                else {
                    $(this).removeClass("ny_right_block_open_icon");
                    $(".ny_right_block_bg").removeClass("ny_right_block_bg_right300");
                    $(this).addClass("ny_right_block_close_icon");
                    $(".ny_right_block_bg").addClass("ny_right_block_bg_right20");
                    $(".ny_right_block").removeClass("ny_right_block_20");

                }

                $("#overlay2").fadeToggle();
            }

        });

        $('#overlay2').click(function () {
            $(".ny_change_icon").removeClass("ny_right_block_close_icon");
            $(".ny_right_block_bg").removeClass("ny_right_block_bg_right20");
            $(".ny_change_icon").addClass("ny_right_block_open_icon");
            $(".ny_right_block_bg").addClass("ny_right_block_bg_right300");
            $(".ny_right_block").addClass("ny_right_block_20");

            $("#overlay2").hide();
        });

        $('.ny_header_ul>.ny_header_ul_li').mouseenter(function () {
            $(this).find('.ny_header_ulsub').stop(false, true).slideDown(180);
        }).mouseleave(function () {
            $(this).find('.ny_header_ulsub').stop(false, true).slideUp(180);
        });

        $('.ny_container_nynews_list_title>.ny_container_nynews_list_title_li').click(function () {
            $(this).find('.ny_container_nynews_ul').show();
            $(this).addClass("active");
            $(this).siblings().removeClass("active");
            $(this).siblings().find('.ny_container_nynews_ul').hide();

        });

        var bodyHeight = $(document.body).height();
        var ContainerHeight = $(".ny_mainlist").height();
        if (ContainerHeight != null) {
            var bhch = bodyHeight - 247;
            if (bhch > 0) {
                $(".ny_mainlist").css("min-height", bhch + "px");
            }
        }
        else {
            $(".ny_container_leftul_bg").css("display", "none");
        }

    });

    function windowCH() {
        var bodyHHeight = $(document.body).height();
        var ContainerHHeight = $(".ny_mainlist").height();
        if (ContainerHHeight != null) {
            var bhchH = bodyHHeight - 247;
            if (bhchH > 0) {
                $(".ny_mainlist").css("min-height", bhchH + "px");

            }
            else {
                $(".ny_footer").css("margin-top", "0px");
            }
        }

    }

    $(window).resize(windowCH);


    $(document).ready(function () {
        $('.flicker-example1').flicker();
        var TopNavId = getQueryString("TopNavId");

        $(".ny_header_ul>li[id='" + TopNavId.toLowerCase() + "']").addClass("active").siblings("li").removeClass("active");

        $('#tabs_title li').click(function () {
            $(this).addClass("active").siblings().removeClass();
            $('#divShow > div').eq($('#tabs_title li').index(this)).show().siblings().hide();
        });


    });
    function OpenEditUserInfo() {
        $.layer({
            type: 2,
            title: "修改个人信息",
            maxmin: false,
            area: ['500px', '350px'],
            border: [0, 0.3, '#000'],
            shade: [0.6, '#000'],
            shadeClose: true,
            closeBtn: [0, true],
            fix: true,
            iframe: {
                src: '<%=DZAFCPortal.Config.Base.ClientBasePath%>/NY_EditUser.aspx',
                    scrolling: 'no'
                },
                fadeIn: 1000
            });
        }

        function OpenEditPasswordInfo() {
            $.layer({
                type: 2,
                title: "修改个人密码",
                maxmin: false,
                area: ['500px', '350px'],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [0, true],
                fix: true,
                iframe: {
                    src: "<%=DZAFCPortal.Config.Base.ClientBasePath%>/NY_EditPassword.aspx",
                    scrolling: 'no'
                },
                fadeIn: 1000
            });
        }




</script>
