<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="HighlightDetail.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.HighlightDetail" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var proportion = 745 / 745;//690 / 745;
        $(function () {
            var w = $(".ny_detail_abstract_tabscon").width();
            $(".ny_detail_abstract_tabscon img").each(function () {
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
        });

        function thumbs() {
            $.ajax({
                cache:false,
                url: "<%= DZAFCPortal.Config.Base.ClientBasePath %>/StaffHome/AjaxPage/getAjaxThumbs.ashx",
                data: { img: $("#<%=imgThumbs.ClientID %>").attr("class"), id: $("#<%=imgThumbs.ClientID %>").attr("acution") },
                success: function (result) {
                    if (result.split(",")[0] == "1") {
                        if ($("#<%=imgThumbs.ClientID %>").attr("class") == 'ny_mainlist_nametitle_good') {
                            $("#<%=imgThumbs.ClientID %>").attr("class", "ny_mainlist_nametitle_cancelgood");
                            $("#<%=labGood.ClientID %>").text(result.split(",")[1]);
                            $("#<%=labPraise.ClientID %>").text('赞');
                        }
                        else {
                            $("#<%=imgThumbs.ClientID %>").attr("class", "ny_mainlist_nametitle_good");
                            $("#<%=labGood.ClientID %>").text(result.split(",")[1]);
                            $("#<%=labPraise.ClientID %>").text('取消赞');
                        }
                    }
                    else {
                        alert(result);
                    }
                }
            })
        }
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
                        <%--   <span id="status" runat="server">拍卖竞价中</span>--%>
                    </div>
                    <div class="ny_detail_abstract_con_nomar">
                        <span class="ny_detail_abstract_con_title">
                            <asp:Literal ID="litName2" runat="server"></asp:Literal></span>
                        <span>活动时间：<strong><asp:Literal ID="litAuctionTime" runat="server"></asp:Literal></strong></span>
                        <span style="margin-top: 5px;width:60%;float:left;">发布部门：<strong><asp:Literal ID="litPublishDept" runat="server"></asp:Literal></strong></span>
                        <span style="margin-top: 5px;width:40%;float:left;">发布人：<strong><asp:Literal ID="litCreator" runat="server"></asp:Literal></strong></span>
                        <div class="clear"></div>
                        <span style="text-indent: 2em; margin-top: 5px;">
                            <asp:Label ID="labSummary" runat="server" Text="labSummary"></asp:Label>
                        </span>
                        <div class="clear"></div>


                        <a style="float: right" title="点赞" onclick="thumbs()">
                            <img id="imgThumbs" alt=""  runat="server" />
                            <asp:Label ID="labPraise" runat="server" Text="赞" Style="margin-top: 0px;display: inline-block;line-height: 12px;"></asp:Label>
                            (<span class="ny_mainlist_block_con_number_orange" style="display: inline-block; margin: 0px;">
                                <asp:Label ID="labGood" runat="server" Text="Label" Style="margin: 0px; line-height: 12px;"></asp:Label>
                            </span>)
                        </a>
                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                    <div class="ny_detail_abstract_tabs">
                        <ul>
                            <li class="active"><a>活动描述</a></li>
                        </ul>
                    </div>
                    <div class="ny_detail_abstract_tabscon">
                        <div>
                            <asp:Label ID="labContent" runat="server" Text="labContent"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
