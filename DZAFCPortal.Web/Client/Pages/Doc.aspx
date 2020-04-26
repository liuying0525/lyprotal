﻿<%@ Page Language="C#" MasterPageFile="../NYBaseLayout.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="uc1" TagName="SiteNavigator" Src="../Controls/SiteNavigator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTitleBar" Src="../Controls/NavTitleBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SecendaryLeftNav" Src="../Controls/SecendaryLeftNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopTitleBar" Src="../Controls/TopTitleBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                    <li class=""><a href="javascript:history.go(-1);">返回</a></li>
                </ul>
            </div>
            <div class="ny_mainlist_info">
               <iframe frameborder="0" id="iframepage" height="400px" width="100%" scrolling="no" src="/SitePages/DocLibrary.aspx?RootFolder=<%=Request.Params["RootFolder"] %>"></iframe>
            </div>
        </div>
    </div>
    <style type="text/css">
        .folder-nav-container {
            margin-bottom: 5px;
        }

            .folder-nav-container a {
                text-decoration: underline;
            }

        
    </style>
</asp:Content>