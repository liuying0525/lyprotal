<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="OnlineVotingDetail.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.OnlineVotingDetail" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/Client/ThirdLibs/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link href="/Scripts/Client/ThirdLibs/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" />
    <script type="text/javascript">
        var proportion = 745 / 745;//690 / 745;
        $(function () {
            $('.ny_detail_abstract_tabs li').click(function () {
                $(this).addClass("active").siblings().removeClass();
                $('.ny_detail_abstract_tabscon > div').eq($('.ny_detail_abstract_tabs li').index(this)).show().siblings().hide();
            });
            $('.ny_menu>ul>li').mouseenter(function () {
                $(this).find('ul').stop(false, true).slideDown(180);
                $(this).addClass("lihover");
            }).mouseleave(function () {
                $(this).find('ul').stop(false, true).slideUp(180);
                $(this).removeClass("lihover");
            });
            $("a[rel=online]").fancybox({
                'hideOnContentClick': true,
                'scrolling': 'no',
                'hideOnContentClick': false,
                padding: 20
            });
            var w = $(".ny_Detail_abstract_tabscon").width();
            $(".NY_Detail_abstract_tabscon img").each(function () {
                var img_w = $(this).width();
                var img_h = $(this).height();
                if (img_w > w || img_w == 0) {
                    var img_w_f = w * proportion;
                    var img_h_f = img_w_f * img_h / img_w;
                    $(this).width(img_w_f);
                    $(this).height(img_h_f);
                    $(this).css('float', 'center');
                }
            })
            $(".ny_Progressbar_title3 li:nth-child(2)").css('margin-left', '33%');
            // $("#fancybox-wrap").width($("#fancybox-wrap").width() + 20);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ny_container">
        <div class="ny_bread">
            <uc1:SiteNavigator runat="server" ID="SiteNavigator" />
        </div>
        <div class="ny_left_sub fl">
            <div class="ny_left_sub_title ny_container_boxshadow ">
                <uc1:NavTitleBar runat="server" ID="NavTitleBar" />
            </div>
            <uc1:SecendaryLeftNav runat="server" ID="SecendaryLeftNav" />
        </div>
        <div class="ny_mainlist ny_container_boxshadow fr">
            <div class="ny_mainlist_title">
                <span class="fl">
                    <uc1:TopTitleBar runat="server" ID="TopTitleBar" />
                </span>
                <ul class="fr">
                    <li><a href="javascript:history.go(-1);">返回</a></li>
                </ul>
            </div>
            <div class="ny_mainlist_block">
                <div class="ny_detail_abstract">
                    <div class="ny_detail_abstract_pic">
                        <asp:Image ID="imageUrl" runat="server" Width="280" Height="200" />
                        <span id="status" runat="server">进行中</span>
                    </div>
                    <div class="ny_detail_abstract_con">
                        <span class="ny_detail_abstract_con_title">
                            <asp:Literal ID="litTitle2" runat="server"></asp:Literal></span>
                        <span style="margin-top: 10px;">评选时间：<strong><asp:Literal ID="litOnlineTime" runat="server"></asp:Literal></strong></span>
                          <span style="margin-top: 5px;width:60%;float:left;">发布部门：<strong><asp:Literal ID="litPublishDept" runat="server"></asp:Literal></strong></span>
                        <span style="margin-top: 5px;width:40%;float:left;">发布人：<strong><asp:Literal ID="litCreator" runat="server"></asp:Literal></strong></span>
                        <div class="clear"></div>
                        <span style="margin-top: 20px; text-indent: 2em">
                            <asp:Label ID="labSummary" runat="server" Text="labSummary"></asp:Label></span>
                        <div class="clear"></div>
                        <asp:Button ID="btnOnline" runat="server" Text="已投票" CssClass="ny_detail_abstract_btn ny_btn_completed fl" Visible="false" Enabled="false" />
                        <a class="ny_detail_abstract_btn fl" rel="online" runat="server" id="aOnline">投票</a>
                        <div class="clear"></div>
                    </div>

                    <div class="clear"></div>
                    <div class="ny_detail_abstract_tabs">
                        <ul>
                            <li class="active"><a>简要介绍</a></li>
                            <li><a>投票详情</a></li>
                        </ul>
                    </div>
                    <div class="ny_detail_abstract_tabscon">
                        <div>
                            <asp:Label ID="labContent" runat="server" Text="labContent"></asp:Label>
                        </div>
                        <div style="display: none">
                            <ul class="ny_online_select_list">
                                <asp:Repeater ID="repOnline" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="ny_online_select fl">
                                                <span><%#Eval("Option") %></span>
                                            </div>
                                            <div class="ny_online_num fr">
                                                <span class="ny_online_progress_num fr"><%#Eval("ReviewsNum") %>票<%#percentage(Eval("ReviewsNum").ToString(),"") %></span>
                                                <div class="progress ny_online_progress fr">
                                                    <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style='width: <%#percentage(Eval("ReviewsNum").ToString(),"pic") %>;'>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clear"></div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
