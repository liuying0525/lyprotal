<%@ Page Language="C#" MasterPageFile="../NYBaseLayout.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="uc1" TagName="SiteNavigator" Src="../Controls/SiteNavigator.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTitleBar" Src="../Controls/NavTitleBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SecendaryLeftNav" Src="../Controls/SecendaryLeftNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopTitleBar" Src="../Controls/TopTitleBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .folder-nav-container {
            margin-bottom: 5px;
        }

            .folder-nav-container a {
                text-decoration: underline;
            }
    </style>
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
                <WebPartPages:WebPartZone id="g_9A79F09219E84643AEB42A0DCA1731AE" runat="server" title="区域 1"><ZoneTemplate>
<WebPartPages:ListViewWebPart runat="server" __MarkupType="xmlmarkup" WebPart="true" __WebPartId="{3D406F4E-43D9-47DC-8C32-C4F64B3585C2}" >
<WebPart xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.microsoft.com/WebPart/v2">
  <Title>Test</Title>
  <FrameType>Default</FrameType>
  <Description />
  <IsIncluded>true</IsIncluded>
  <PartOrder>2</PartOrder>
  <FrameState>Normal</FrameState>
  <Height />
  <Width />
  <AllowRemove>true</AllowRemove>
  <AllowZoneChange>true</AllowZoneChange>
  <AllowMinimize>true</AllowMinimize>
  <AllowConnect>true</AllowConnect>
  <AllowEdit>true</AllowEdit>
  <AllowHide>true</AllowHide>
  <IsVisible>true</IsVisible>
  <DetailLink>/Lists/Test</DetailLink>
  <HelpLink />
  <HelpMode>Modeless</HelpMode>
  <Dir>Default</Dir>
  <PartImageSmall />
  <MissingAssembly>无法导入此 Web 部件。</MissingAssembly>
  <PartImageLarge>/_layouts/15/images/itevent.png?rev=23</PartImageLarge>
  <IsIncludedFilter />
  <ExportControlledProperties>false</ExportControlledProperties>
  <ConnectionID>00000000-0000-0000-0000-000000000000</ConnectionID>
  <ID>g_3d406f4e_43d9_47dc_8c32_c4f64b3585c2</ID>
  <WebId xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">00000000-0000-0000-0000-000000000000</WebId>
  <ListViewXml xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">&lt;View Name=&quot;{3D406F4E-43D9-47DC-8C32-C4F64B3585C2}&quot; MobileView=&quot;TRUE&quot; Type=&quot;CALENDAR&quot; Hidden=&quot;TRUE&quot; TabularView=&quot;FALSE&quot; RecurrenceRowset=&quot;TRUE&quot; DisplayName=&quot;&quot; Url=&quot;/SitePages/Calendar.aspx&quot; Level=&quot;1&quot; BaseViewID=&quot;2&quot; ContentTypeID=&quot;0x&quot; MobileUrl=&quot;_layouts/15/mobile/viewdaily.aspx&quot; ImageUrl=&quot;/_layouts/15/images/events.png?rev=23&quot;&gt;&lt;Toolbar Type=&quot;Standard&quot; /&gt;&lt;ViewHeader /&gt;&lt;ViewBody /&gt;&lt;ViewFooter /&gt;&lt;ViewEmpty /&gt;&lt;ParameterBindings&gt;&lt;ParameterBinding Name=&quot;NoAnnouncements&quot; Location=&quot;Resource(wss,noXinviewofY_LIST)&quot; /&gt;&lt;ParameterBinding Name=&quot;NoAnnouncementsHowTo&quot; Location=&quot;Resource(wss,noXinviewofY_DEFAULT)&quot; /&gt;&lt;/ParameterBindings&gt;&lt;ViewFields&gt;&lt;FieldRef Name=&quot;EventDate&quot; /&gt;&lt;FieldRef Name=&quot;EndDate&quot; /&gt;&lt;FieldRef Name=&quot;fRecurrence&quot; /&gt;&lt;FieldRef Name=&quot;EventType&quot; /&gt;&lt;FieldRef Name=&quot;WorkspaceLink&quot; /&gt;&lt;FieldRef Name=&quot;Title&quot; /&gt;&lt;FieldRef Name=&quot;Location&quot; /&gt;&lt;FieldRef Name=&quot;Description&quot; /&gt;&lt;FieldRef Name=&quot;Workspace&quot; /&gt;&lt;FieldRef Name=&quot;MasterSeriesItemID&quot; /&gt;&lt;FieldRef Name=&quot;fAllDayEvent&quot; /&gt;&lt;/ViewFields&gt;&lt;ViewData&gt;&lt;FieldRef Name=&quot;Title&quot; Type=&quot;CalendarMonthTitle&quot; /&gt;&lt;FieldRef Name=&quot;Title&quot; Type=&quot;CalendarWeekTitle&quot; /&gt;&lt;FieldRef Name=&quot;Location&quot; Type=&quot;CalendarWeekLocation&quot; /&gt;&lt;FieldRef Name=&quot;Title&quot; Type=&quot;CalendarDayTitle&quot; /&gt;&lt;FieldRef Name=&quot;Location&quot; Type=&quot;CalendarDayLocation&quot; /&gt;&lt;/ViewData&gt;&lt;Query&gt;&lt;Where&gt;&lt;DateRangesOverlap&gt;&lt;FieldRef Name=&quot;EventDate&quot; /&gt;&lt;FieldRef Name=&quot;EndDate&quot; /&gt;&lt;FieldRef Name=&quot;RecurrenceID&quot; /&gt;&lt;Value Type=&quot;DateTime&quot;&gt;&lt;Month /&gt;&lt;/Value&gt;&lt;/DateRangesOverlap&gt;&lt;/Where&gt;&lt;/Query&gt;&lt;/View&gt;</ListViewXml>
  <ListName xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">{0B8B3BA4-93C6-4240-AE5A-20522D7BC079}</ListName>
  <ListId xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">0b8b3ba4-93c6-4240-ae5a-20522d7bc079</ListId>
  <ViewFlag xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">8921097</ViewFlag>
  <ViewFlags xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">Html Hidden RecurrenceRowset Calendar Mobile</ViewFlags>
  <ViewContentTypeId xmlns="http://schemas.microsoft.com/WebPart/v2/ListView">0x</ViewContentTypeId>
</WebPart>
</WebPartPages:ListViewWebPart>
</ZoneTemplate></WebPartPages:WebPartZone>

            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#scriptWPQ1>div").hide();
            var param = getUrlParam('TopNavId');
            var param2 = getUrlParam('CurNavId');
            var param3 = getUrlParam('CategoryCode');
            if (param != null && param != '' && param2 != null && param2 != '' && param3 != null && param3 != '') {
                $(".ms-listlink").each(function (idx, obj) {
                    var clickfun = $(obj).attr("onclick");
                    var url = clickfun.split('?');
                    if (url.length > 0)
                        $(obj).attr("onclick", url[0] + "?CategoryCode=" + param3 + "&TopNavId=" + param + "&CurNavId=" + param2 + "&" + url[1]);
                });
            }
        });

        //获取url中的参数
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
    </script>
</asp:Content>
