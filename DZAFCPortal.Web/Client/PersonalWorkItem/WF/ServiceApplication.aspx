<%@ Page Title="" Language="C#" MasterPageFile="../../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="ServiceApplication.aspx.cs" Inherits="DZAFCPortal.Web.Client.PersonalWorkItem.WF.ServiceApplication" %>

<%@ Register Src="../../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="overlay"></div>
    <div id="overlay2"></div>

    <div class="clear"></div>
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
            </div>
            <div class="ny_mainlist_block">
                <ul class="ny_mainlist_block_service_ul">
                    <asp:Repeater runat="server" ID="rpt_content">
                        <ItemTemplate>
                            <li>
                                <a class="ny_mainlist_block_service_ul_a" href='<%#Eval("Url") %>' target="<%#((bool)Eval("IsOpenedNewTab"))?"_blank":"_self" %>">
                                    <img src="<%#Eval("IconUrl") %>" alt="" class="ny_mainlist_block_service_ul_img" />
                                    <span class="ny_mainlist_block_service_ul_span"><%#Eval("Title") %></span>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <!-- <span style="display:block;text-align:center;padding:30px">该功能正在开发中，尽请期待...</span>-->
            </div>

        </div>
    </div>
    <div class="clear"></div>
</asp:Content>
