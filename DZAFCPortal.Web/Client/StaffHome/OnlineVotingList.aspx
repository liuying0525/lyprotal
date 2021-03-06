﻿<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="OnlineVotingList.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.OnlineVotingList" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                <ul class="fr" id="tabs_title">
                    <li class="active"><a>最 新</a></li>
                    <li><a>全 部</a></li>
                </ul>
            </div>
            <div class="ny_mainlist_block" id="divShow">
                <div>
                    <ul class="ny_mainlist_block_ul">
                        <asp:Repeater ID="rptOnline" runat="server" OnItemDataBound="rptOnlineOptions_ItemDataBound">
                            <ItemTemplate>
                                <li>
                                    <div class="ny_mainlist_block_pic">
                                        <a href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'>
                                            <img src='<%#Eval("ImageURL") %>' width="100" height="100" alt="" />
                                        </a>
                                        <span><%#OnlineStatus(Eval("BeginTime").ToString(),(Eval("EndTime").ToString())) %></span>
                                    </div>
                                    <div class="ny_mainlist_block_con">
                                        <a href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'><%#Eval("Title") %></a>
                                        <div class="clear"></div>
                                        <div class="ny_mainlist_block_con_time fl">
                                            <span class="fl">评选时间：<strong><%#DateTimeConver(Eval("BeginTime").ToString()) %></strong>至 <strong><%#DateTimeConver(Eval("EndTime").ToString()) %></strong></span>
                                        </div>

                                        <div class="clear"></div>
                                        <div class="ny_mainlist_block_con_info_online">
                                            <asp:Repeater ID="rptOnlineOptions" runat="server">
                                                <ItemTemplate>
                                                    <%#rankSpan((Container.ItemIndex+1).ToString(),Eval("Option").ToString(),Eval("ReviewsNum").ToString(),Eval("NY_OnlineVoteID").ToString())%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <div class="clear" ></div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div style="display: none;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ul class="ny_mainlist_block_ul">
                                <asp:Repeater ID="repPreferent" runat="server" OnItemDataBound="rptPreferent_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <div class="ny_mainlist_block_pic">
                                                <a href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'>
                                                    <img src='<%#Eval("ImageURL") %>' width="100" height="100" alt="" />
                                                </a>
                                                <span><%#OnlineStatus(Eval("BeginTime").ToString(),(Eval("EndTime").ToString())) %></span>
                                            </div>
                                            <div class="ny_mainlist_block_con">
                                                <a href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'><%#Eval("Title") %></a>
                                                <div class="clear"></div>
                                                <div class="ny_mainlist_block_con_time fl">
                                                    <span class="fl">评选时间：<strong><%#DateTimeConver(Eval("BeginTime").ToString()) %></strong>至 <strong><%#DateTimeConver(Eval("EndTime").ToString()) %></strong></span>
                                                </div>

                                                <div class="clear"></div>
                                                <div class="ny_mainlist_block_con_info_online">
                                                    <asp:Repeater ID="rptPreferent" runat="server">
                                                        <ItemTemplate>
                                                            <%#rankSpan((Container.ItemIndex+1).ToString(),Eval("Option").ToString(),Eval("ReviewsNum").ToString(),Eval("NY_OnlineVoteID").ToString())%>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="clear" ></div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <div class="pagenavi productnav">
                                <webdiyer:AspNetPager ID="AspNetPager1" PageSize="10" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
