<%@ Page Title="" Language="C#" MasterPageFile="~/Client/BaseLayouts.Master" AutoEventWireup="true" CodeBehind="CompanyNews.aspx.cs" Inherits="DZAFCPortal.Web.Client.CompanyNews" %>

<%@ Register TagPrefix="webdiyer" Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Scripts/Client/css/platform.css">
    <script src="/Scripts/Client/ThirdLibs/simplePagination/js/jquery.simplePagination.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="set-middle bd description dynamic">
        <div class="set-middle-item">
            <div class="set-middle-top">
                <h4>
                    <asp:Literal runat="server" ID="ltlCategoryTitle"></asp:Literal>
                </h4>
                <div class="platformInput">
                    <input type="text" placeholder="请输入" runat="server" id="txtSearchKey">
                    <asp:LinkButton runat="server" ID="btnSeach" OnClick="btnSeach_Click">
                        <i class="search"></i>
                    </asp:LinkButton>
                </div>

            </div>
            <div class="set-middle-content">
                <ul>
                    <asp:Repeater runat="server" ID="rptContent">
                        <HeaderTemplate>
                            <li>
                                <span>标题</span>
                                <span>发布人</span>
                                <span>发布时间</span>
                            </li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <span><%# Eval("Title") %></span>
                                <span><%# Eval("Publisher") %></span>
                                <span><%# Eval("CreateTime") %></span>
                            </li>

                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="apsnetPaper">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="" LastPageText="" OnPageChanged="AspNetPager1_PageChanged" CssClass="paginator" CurrentPageButtonClass="cpb" NextPageText=">" PrevPageText="<" PageSize="5"></webdiyer:AspNetPager>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
