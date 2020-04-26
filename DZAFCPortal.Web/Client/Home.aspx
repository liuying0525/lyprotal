<%@ Page Title="" Language="C#" MasterPageFile="NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DZAFCPortal.Web.Client.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Client/js/modernizr-custom-v2.7.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ny_container" style="position: relative;">
        <div class="ny_container_row">
            <div class="ny_container_nynews ny_boxshadow fl">
                <div class="ny_container_nynews_title">
                    <span class="ny_container_nynews_title_name">公司动态</span>
                    <a class="ny_container_nynews_title_more" href="#" runat="server" id="aGSXW">+More</a>
                </div>
                <ul class="ny_container_nynews_list_title">
                    <asp:Repeater ID="repResults" runat="server" OnItemDataBound="repResults_ItemDataBound">
                        <ItemTemplate>
                            <li class='<%# GetStyle((Container.ItemIndex+1).ToString()) %>'><%#Eval("Name") %>
                                <ul class="ny_container_nynews_ul" style='<%# GetStyle2((Container.ItemIndex+1).ToString()) %>'>
                                    <asp:Repeater runat="server" ID="rptContent">
                                        <ItemTemplate>
                                            <li><a href='<%#GenerateContentUrl(Eval("ID").ToString()) %>'><%#Eval("Title") %></a><img src="/Scripts/Client/images/ny_container_nynews_newpic.gif" alt="" runat="server" visible='<%#Show(Eval("CreateTime").ToString()) %>' /></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <img src="/Scripts/Client/images/ny_container_nynews_bg.png" alt="" class="ny_container_nynews_bg" />
            </div>
            <div class="ny_container_email ny_boxshadow fl" style="position: relative;">
                <a href="/_layouts/15/NyClient/PersonalWorkItem/MailBox.aspx?TopNavId=1bef1b78-4895-4465-7b3f-3469601416e5&CurNavId=25d43f34-1d76-799f-177e-12346a7de9ff">
                    <span class="ny_container_block_icon_number" id="spanUnreadMailCount" style="display: none"></span>
                    <img src="/Scripts/Client/images/ny_container_email_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">我的邮箱</span>
                </a>

            </div>
            <div class="ny_container_tast ny_boxshadow fl" style="position: relative;">
                <a href="PersonalWorkItem/WF/MyTask.aspx?TopNavId=1bef1b78-4895-4465-7b3f-3469601416e5&CurNavId=c3d683a5-3434-7640-bc1f-86137abd8219">
                    <span class="ny_container_block_icon_number" id="myTaskCount" style="display: none"></span>
                    <img src="/Scripts/Client/images/ny_container_tast_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">我的任务</span>
                </a>

            </div>
        </div>
        <div class="ny_container_row">
            <div class="ny_container_service ny_boxshadow fl">
                <a href="PersonalWorkItem/WF/ServiceApplication.aspx?TopNavId=1bef1b78-4895-4465-7b3f-3469601416e5&CurNavId=8abdacbd-ddfd-d1a3-9762-660e24a8c01b">
                    <img src="/Scripts/Client/images/ny_container_service_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">服务申请</span>
                </a>

            </div>
            <div class="ny_container_picture ny_boxshadow fl" id="slideContainer">
                <div class="flicker-example1 fl" data-block-text="false">
                    <ul>
                        <asp:Repeater ID="repHome" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a <%#ahref(Eval("LinkUlr").ToString())%> target="_blank" title='<%#Eval("Name") %>'>
                                        <img src='<%#Eval("ImageUrl") %>' alt="" class="ny_container_picture_size" />
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="ny_container_report ny_boxshadow fl">
                <a href="/_layouts/15/NyClient/ReportCenter/ModuleList.aspx?TopNavId=1aaae693-4d4b-03f7-a4a5-cfdedf9e1ed1&CurNavId=c552bbb7-6bd3-a99d-3cf4-759f6ff54ff6" runat="server" id="aBBZX" name="KHXX">
                    <img src="/Scripts/Client/images/ny_container_report_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">报表中心</span>
                </a>
            </div>
            <div class="ny_container_document ny_boxshadow fl">
                <a href="/_layouts/15/NyClient/Pages/doc.aspx?CategoryCode=GZZD&rootfolder=%2FDocLib%2F%E7%AE%A1%E7%90%86%E5%88%B6%E5%BA%A6&TopNavId=1d7bd184-4bf3-ebb1-a0c5-62a0e044b2b3&CurNavId=7f6a361b-e879-112a-f6cc-8fac70786ae7" runat="server" id="aWDZX" name="GZZD">
                    <img src="/Scripts/Client/images/ny_container_document_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">文档中心</span>
                </a>
            </div>
        </div>
        <div class="ny_container_row">
            <div class="ny_container_job ny_boxshadow fl">
                <a href="#" runat="server" id="aZCYB" name="ZCYB">
                    <img src="/Scripts/Client/images/ny_container_job_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">职场氧吧</span>
                </a>
            </div>
            <div class="ny_container_safety ny_boxshadow fl">
                <a href="#" runat="server" id="aAQGZ" name="AQGZ">
                    <img src="/Scripts/Client/images/ny_container_safety_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">安全生产</span>
                </a>
            </div>
            <div class="ny_container_dang ny_boxshadow fl">
                <a href="#" runat="server" id="aDZBGZ" name="DZBGZ">
                    <img src="/Scripts/Client/images/ny_container_dang_icon.png" alt="" class="ny_container_block_icon" />
                    <span class="ny_container_block_title">党建工作</span>
                </a>
            </div>
            <div class="ny_container_staff ny_boxshadow fl">
                <div class="ny_container_staff_title">
                    <span class="ny_container_staff_title_name fl">职工之家</span>
                    <a class="ny_container_staff_title_more fr" href="#" runat="server" id="aZGZJ">+More</a>
                </div>
                <ul class="ny_container_staff_ul">
                    <asp:Repeater ID="rptHome" runat="server">
                        <ItemTemplate>
                            <li><a href='<%#GenerateHomeUrl(Eval("Id").ToString(),Eval("Type").ToString()) %>'><%#Eval("Name") %></a><img src="/Scripts/Client/images/ny_container_nynews_newpic.gif" alt="" runat="server" visible='<%#Show(Eval("CreateTime").ToString()) %>' /></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <img src="/Scripts/Client/images/ny_container_nystaff_bg.png" alt="" class="ny_container_staff_bg" />
            </div>
        </div>
    </div>


    <script>
        $(function () {
            var bodyHeight = $(document.body).height();
            if (bodyHeight > 809) {
                var footerMargin = (bodyHeight - 809) / 2 + 22;
                $(".ny_footer").css("margin-top", footerMargin + "px");
                $(".ny_container").css("margin-top", footerMargin + "px");
            }

            getUnreadMailCount();
            getMyTaskCount();


            var _syncIntervalMilliseconds = 1000 * 5;
           <%-- $.ajax({
                cache: false,
                async: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getHomeLinkHandler.ashx',
                data: {
                    Op: "SyncLinks",
                    syncIntervalMillisecond: _syncIntervalMilliseconds
                },
                dataType: "json",
                success: function (data) {
                    //currentCompanyApp.users(data.datas);
                }
            });--%>
        })

        function windowWH() {
            var bodyH = $(document.body).height();
            if (bodyH > 809) {
                var footerM = (bodyH - 809) / 2 + 22;
                $(".ny_footer").css("margin-top", footerM + "px");
                $(".ny_container").css("margin-top", footerM + "px");
            }
            else {
                $(".ny_footer").css("margin-top", "0px");
                $(".ny_container").css("margin-top", "25px");
            }
        }

        $(window).resize(windowWH);

        ///获取未读邮件数量
        function getUnreadMailCount() {

            $.ajax({
                type: "post",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/PersonalWorkItem/Ajax/ExchangeItemHandler.ashx?time=' + new Date(),
                dataType: "json",
                data: {
                    Op: "GetUnreadCount"
                },
                success: function (data) {
                    if (data.IsSucess && data.Datas > 0) {
                        $("#spanUnreadMailCount").text(data.Datas).show();
                    }
                    else {
                        $("#spanUnreadMailCount").hide();
                    }
                }
            });
        }

        //获取我的任务数量
        function getMyTaskCount() {
            $.ajax({
                contentType: "application/json;utf-8",
                type: "post",
                url: '/workflow/Services/Workflow.asmx/GetMyTasks',
                dataType: "json",
                data:
                    JSON.stringify(
                        {
                            account: $('#hidloginaccount').val(),
                            pageSize: 1000,
                            pageIndex: 0,
                            workflowname: "",
                            title: ""
                        }),
                success: function (data) {

                    if (!data.d.HasError && data.d.Data.count > 0) {
                        $("#myTaskCount").text(data.d.Data.count).show();
                    }
                    else {
                        $("#myTaskCount").hide();
                    }
                }
            });
        }



    </script>
</asp:Content>
