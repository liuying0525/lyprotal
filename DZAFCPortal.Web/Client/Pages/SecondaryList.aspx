<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="SecondaryList.aspx.cs" Inherits="DZAFCPortal.Web.Client.Pages.SecondaryList" %>
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
            <div class="ny_mainlist_block">
                <span class="ny_mainlist_block_ul_title">
                    <span class="ny_mainlist_block_ul_title_title">标题</span>
                     <span class="ny_mainlist_block_ul_title_person">发布人</span>
                    <span class="ny_mainlist_block_ul_title_date">发布时间</span>
                </span>
                <div id="divShow">
                <div>
                    <ul class="ny_mainlist_list_ul">
                        <asp:Repeater runat="server" ID="rptContentTitle">
                            <ItemTemplate>
                                <li>
                                    <a class="ny_mainlist_list_ul_a" href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'>
                                        <span class="ny_mainlist_list_ul_a_tilte"><%#Eval("Title") %><img src="/Scripts/Client/images/ny_container_nynews_newpic.gif" alt="" runat="server"  visible='<%#Show(Eval("CreateTime").ToString()) %>' /></span>
                                           <span class="ny_mainlist_list_ul_a_person"><%#Eval("Publisher") %></span>
                                        <span class="ny_mainlist_list_ul_a_date"><%# Convert.ToDateTime(Eval("CreateTime")).ToString("yyyy-MM-dd HH:mm") %></span>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div style="display: none">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ul class="ny_mainlist_list_ul">
                                <asp:Repeater runat="server" ID="rptContentTitles">
                                    <ItemTemplate>
                                        <li>
                                            <a class="ny_mainlist_list_ul_a" href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'>
                                                <span class="ny_mainlist_list_ul_a_tilte"><%#Eval("Title") %><img src="/Scripts/Client/images/ny_container_nynews_newpic.gif" alt="" runat="server"  visible='<%#Show(Eval("CreateTime").ToString()) %>'/></span>
                                                <span class="ny_mainlist_list_ul_a_person"><%#Eval("Publisher") %></span>
                                                <span class="ny_mainlist_list_ul_a_date"><%# Convert.ToDateTime(Eval("CreateTime")).ToString("yyyy-MM-dd HH:mm") %></span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <div class="pagenavi">
                                <webdiyer:AspNetPager ID="AspNetPager1" PageSize="10" runat="server" AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText="下一页" PrevPageText="上一页"></webdiyer:AspNetPager>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
