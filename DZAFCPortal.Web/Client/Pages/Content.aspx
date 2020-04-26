<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="DZAFCPortal.Web.Client.Pages.Content" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return decodeURI(r[2]); return null;
        }
        function AttachInfoAdd(attachName) {
            $.ajax({
                //要用post方式 
                type: "post",
                //方法所在页面和方法名 
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getHomeLinkHandler.ashx',
                data: {
                    op: "InfoAccessAdd",
                    attachName: attachName,
                    ContentId: getQueryString("ContentId")
                },
                //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字 
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //返回的数据用data.d获取内容 
                    //alert(data);
                },
                error: function (err) {
                    alert(err);
                }
            });
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
                     <uc1:TopTitleBar runat="server" ID="TopTitleBar" /></span>
                <ul class="fr">
                    <li class=""><a href="javascript:history.go(-1);">返回</a></li>
                </ul>
            </div>
            <div class="ny_mainlist_info">
                <div class="ny_mainlist_namedate">
                    <span class="ny_mainlist_nametitle fl">
                        <asp:Literal runat="server" ID="literalTitle"></asp:Literal></span>
                    <span class="ny_mainlist_nametitle_span">发布时间：<asp:Literal runat="server" ID="literalPublishTime"></asp:Literal></span>
                    <span class="ny_mainlist_nametitle_span">发布人：<asp:Literal runat="server" ID="literalPublisher"></asp:Literal></span>
                    <span class="ny_mainlist_nametitle_span">点击量：<asp:Literal runat="server" ID="literalNum"></asp:Literal></span>
                  <%--  <a class="ny_mainlist_nametitle_good fr" title="点赞">
                        赞
                    </a>--%>
                </div>
                <div class="clear"></div>
                <div class="ny_mainlist_detailtext">
                    
                    <asp:Label ID="lblContent" runat="server" Text="" CssClass="contenthtml"></asp:Label>
                    
                    <ul class="ny_attachmentlist" id="fileUl" runat="server">
                        <li style="list-style-type: none;">
                            <span class="ny_attachmentlist_title">附件清单：</span>
                        </li>
                        <asp:Repeater runat="server" ID="rptAttachList">
                            <ItemTemplate>
                                <li>
                                    <a target="_blank" href='<%#downUrl(Eval("Url").ToString())%>' onclick="AttachInfoAdd('<%#RemoveGuid(Eval("FileName").ToString())%>')"><%#RemoveGuid(Eval("FileName").ToString())  %></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>

        </div>
    </div>
</asp:Content>