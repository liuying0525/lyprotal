<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="ActivityDetail.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.ActivityDetail" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/base.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/layouts.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/flickerplate.css" rel="stylesheet" />
    <link href="/Scripts/Client/css/pagenavi.css" rel="stylesheet" />

    <script src="/Scripts/Client/ThirdLibs/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link href="/Scripts/Client/ThirdLibs/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" />
    <script type="text/javascript">
        var proportion = 745 / 745;//690 / 745;
        $(function () {
            $("a[rel=events]").fancybox({
                'hideOnContentClick': true,
                'scrolling': 'no',
                'hideOnContentClick': false,
                padding: 20
            });
            $('.ny_detail_abstract_tabs li').click(function () {
                $(this).addClass("active").siblings().removeClass();
                $('.ny_detail_abstract_tabscon > div').eq($('.ny_detail_abstract_tabs li').index(this)).show().siblings().hide();
            });
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

        function Confirm() {
            var result = false;
            var r = confirm("您确定报名参加此活动？");
            if (r) {
                result = true;
            }
            return result;
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
                <!--<div class="ny_progressbar">
                    <div class="progress ">
                        <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 50%;">
                        </div>
                    </div>
                    <ul class="ny_progressbar_title ny_progressbar_title3">
                        <li class="active fl">预告</li>
                        <li class="active fl">报名进行中</li>
                        <li class=" fr">报名结束</li>
                    </ul>
                    <div class="clear"></div>
                </div>-->
                <div class="ny_detail_abstract">
                    <div class="ny_detail_abstract_pic">
                        <asp:Image ID="imageUrl" runat="server" Width="280" Height="200" />
                        <span id="status" runat="server">报名进行中</span>
                    </div>
                    <div class="ny_detail_abstract_con">
                        <span class="ny_detail_abstract_con_title">
                            <asp:Literal ID="litName2" runat="server"></asp:Literal>
                        </span>
                        <span>报名时间：<strong><asp:Literal ID="litBookTime" runat="server"></asp:Literal></strong>
                        </span>
                        <span>活动时间：<strong><asp:Literal ID="litActTime" runat="server"></asp:Literal></strong>
                        </span>
                         <span style="margin-top: 5px;width:60%;float:left;">发布部门：<strong><asp:Literal ID="litPublishDept" runat="server"></asp:Literal></strong></span>
                        <span style="margin-top: 5px;width:40%;float:left;">发布人：<strong><asp:Literal ID="litCreator" runat="server"></asp:Literal></strong></span>
                        <div class="clear"></div>
                        <span style="text-indent: 2em">
                            <asp:Label ID="labSummary" runat="server" Text="labSummary"></asp:Label>
                        </span>
                        <div class="clear"></div>
                        <!--满额后，报名按钮的a标签中class加上ny_btn_forbid;活动简介的字数最好控制在100字以内，超出部分是不显示的-->
                        <div class="clear"></div>
                        <%--                        <asp:Button ID="btnReg" runat="server" Text="已报名" CssClass="ny_detail_abstract_btn ny_btn_completed fl" Visible="false" Enabled="false" />--%>
                        <%--<a runat="server" class="ny_detail_abstract_btn nny_btn_forbid fl" rel="events" id="a_Events" onclick="">报名</a>--%>
                        <asp:Button ID="btnRegistration" runat="server" Text="报名" CssClass="ny_detail_abstract_btn fl" OnClick="btnRegistration_Click" OnClientClick="return Confirm()" />
                        <asp:Label ID="labNum" runat="server" Text="labNum" CssClass="ny_detail_abstract_num fl"></asp:Label>
                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                    <div class="ny_detail_abstract_tabs">
                        <ul>
                            <li class="active"><a>活动描述</a></li>
                            <li><a>报名方式</a></li>
                            <li><a>报名记录</a></li>
                        </ul>
                    </div>
                    <div class="ny_detail_abstract_tabscon">
                        <div>
                            <asp:Label ID="labContent" runat="server" Text="labContent"></asp:Label>
                        </div>
                        <div style="display: none">
                            <asp:Label ID="labActWay" runat="server" Text="labActWay"></asp:Label>
                        </div>
                        <div style="display: none" runat="server" id="person">
                            <table class="ny_detail_abstract_table">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">序号</th>
                                        <th style="width: 30%">部门</th>
                                        <th style="width: 25%">姓名</th>
                                        <%-- <th style="width: 7%">性别</th>--%>
                                        <%-- <th style="width: 15%">电话</th>--%>
                                        <%-- <th style="width: 20%">邮箱</th>--%>
                                        <th style="width: 35%">报名时间</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptRegister" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1%></td>
                                                <td><%#Eval("EventAccount")%></td>
                                                <%-- <td><%#Eval("EventAccount")%></td>--%>
                                                <td><%#Eval("UserDisplayName")%></td>
                                                <%-- <td><%#(Eval("Sex", "{0}") == "0") ? "男" : "女"%></td>--%>
                                                <%--  <td><%#Eval("PhoneNumber")%></td>--%>
                                                <%-- <td><%#Eval("Email")%></td>--%>
                                                <td><%#Eval("RegistTime")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div style="display: none" runat="server" id="team">
                            <table class="BEA_Detail_abstract_table">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">队伍</th>
                                        <th style="width: 10%">角色</th>
                                        <th style="width: 10%">员工号</th>
                                        <th style="width: 10%">姓名</th>
                                        <th style="width: 7%">性别</th>
                                        <th style="width: 15%">电话</th>
                                        <th style="width: 20%">邮箱</th>
                                        <th style="width: 25%">报名时间</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptTeam" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("EventName")%></td>
                                                <td><%#Eval("TeamLevel")%></td>
                                                <td><%#Eval("EventTeamAccount")%></td>
                                                <td><%#Eval("EventPersonName")%></td>
                                                <td><%#(Eval("EventSex", "{0}") == "0") ? "男" : "女"%></td>
                                                <td><%#Eval("EventPhoneNumber")%></td>
                                                <td><%#Eval("EventEmail")%></td>
                                                <td><%#Eval("CreateTime")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
