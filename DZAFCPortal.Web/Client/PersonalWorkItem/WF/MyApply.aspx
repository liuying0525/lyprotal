<%@ Page Title="" Language="C#" MasterPageFile="../../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="MyApply.aspx.cs" Inherits="DZAFCPortal.Web.Client.PersonalWorkItem.WF.MyApply" %>

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
                <span class="fl">我的申请 My Application</span>

            </div>

            <div class="ny_mainlist_block" id="content_apply">
                <div class="ny_mainlist_search_mywork ny_mainlist_search">
                    <span class="ny_mainlist_search_span fl">申请类别:</span>
                    <select class="ny_mainlist_search_select fl" id="wftype" data-bind="options: currentModel.WFNameList, optionsText: 'Description', optionsValue: 'Name', value: selectedWFName, event: { change: OnSelectChange }">
                    </select>
                    <span class="ny_mainlist_search_span fl">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标题:</span>
                    <input type="text" class="ny_mainlist_search_input fl" id="wftitle" />
                    <a class="ny_mainlist_search_icon fl" onclick="getMyApply(1)"></a>
                    <div class="clear"></div>
                </div>
                <!--<span style="display:block;text-align:center;padding:30px">该功能正在开发中，尽请期待...</span>-->
                <div class="ny_mainlist_block_ul_title">
                    <span class="ul_mywork_application_title">标题</span>
                    <span class="ul_mywork_application_type">申请类别</span>
                    <span class="ul_mywork_application_state">当前步骤</span>
                    <span class="ul_mywork_application_per">当前审批人</span>
                    <span class="ul_mywork_application_date">申请时间</span>
                </div>
                <ul class="ny_mainlist_list_ul" data-bind="foreach: currentModel.MyApplyList">
                    <li data-bind="click: function (data, event) { redirectTo(data.Url) }">
                        <a class="ny_mainlist_list_ul_a">
                            <span class="ny_mainlist_mywork_application_title" data-bind="text: Title, attr: { title: Title }"></span>
                            <span class="ny_mainlist_mywork_application_type" data-bind="text: WorkflowName, attr: { title: WorkflowName }"></span>
                            <span class="ny_mainlist_mywork_application_state" data-bind="text: State, attr: { title: State }"></span>
                            <span class="ny_mainlist_mywork_application_per" data-bind="text: Auditors, attr: { title: Auditors }"></span>
                            <span class="ny_mainlist_mywork_application_date" data-bind="text: CreateTime, attr: { title: CreateTime }"></span></a>
                    </li>
                </ul>
                <div id="pager1" class="jqpager jqpagerUser"></div>
            </div>

        </div>
    </div>
    <div class="clear"></div>


    <script type="text/javascript" src="/Scripts/Client/ThirdLibs/knockout-3.2.0.js"></script>
    <script type="text/javascript" src="/Scripts/Client/ThirdLibs/jquery.pager_2014_08.js"></script>
    <script type="text/javascript">
        var currentModel;
        var baseurl = "/workflow/Services/Workflow.asmx/";
        var pagesize = 10;
        $(function () {
            currentModel = new AppViewModel()


            getAllWorkFlowNames();
            //getMyApply(1);

            ko.applyBindings(currentModel, $("#content_apply")[0]);

            //getMyApply($("#tabs_title>li[class='active']:first"), 1);
        });

        getMyApply = function (curIndex) {
            var selval = $("#wftype option:selected").val();

            if (!selval) selval = "";
            var title = $("#wftitle").val();

            $.ajax({
                contentType: "application/json;utf-8",
                type: "post",
                url: "/workflow/Services/Workflow.asmx/GetMyApply",
                //baseurl + "GetMyApply",
                dataType: "json",
                data:
                    JSON.stringify(
                        {
                            account:'<%=CurrentUser.Account%>',
                            pageSize: pagesize,
                            pageIndex: curIndex - 1,
                            workflowname: selval,
                            title: title
                        }),
                success: function (data) {
                    if (!data.d.HasError) {
                        if (data.d.Data.Items.length >= 0) {

                            currentModel.MyApplyList(data.d.Data.Items);

                            var settings = getDefaultPagerSettings();

                            settings.pagenumber = curIndex;
                            settings.recordcount = data.d.Data.count;
                            settings.pagesize = pagesize;
                            settings.buttonClickCallback = getMyApply;
                            settings.numericButtonCount = 3;
                            //设置分页
                            $('#pager1').pager(settings);
                        }

                    }
                    else {

                    }
                }
            });

            return false;
        }

        getAllWorkFlowNames = function () {
            $.ajax({
                contentType: "application/json;utf-8",
                type: "post",
                url: baseurl + "GetAllDefinition",
                dataType: "json",
                data:
                    JSON.stringify(
                        {
                        }),
                success: function (data) {
                    if (!data.d.HasError) {
                        if (data.d.Data.Items.length >= 0) {
                            var ele = {
                                ID: "",
                                Name: "",
                                Description: "全部",
                                Status: "启用"
                            };
                            data.d.Data.Items.unshift(ele);

                            currentModel.WFNameList(data.d.Data.Items);
                        }

                    }
                    else {

                    }
                }
            });

            return false;
        }

        //模拟a标签 跳转target=_blank
        function OpenWithNewTab(url) {
            var el = document.createElement("a");
            document.body.appendChild(el);
            el.href = url; //url 是你得到的连接
            el.target = '_blank'; //指定在新窗口打开
            el.click();
            document.body.removeChild(el);
        }

        function redirectTo(url) {
            window.location.href = url;
        }


        Date.prototype.format = function (format) //author: meizz   //辅助函数
        {
            var o = {
                "M+": this.getMonth() + 1, //month
                "d+": this.getDate(),    //day
                "h+": this.getHours(),   //hour
                "m+": this.getMinutes(), //minute
                "s+": this.getSeconds(), //second
                "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
                "S": this.getMilliseconds() //millisecond
            }
            if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
                (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o) if (new RegExp("(" + k + ")").test(format))
                format = format.replace(RegExp.$1,
                    RegExp.$1.length == 1 ? o[k] :
                        ("00" + o[k]).substr(("" + o[k]).length));
            return format;
        }

        ///格式化日期字符串
        function formatTime(val) {
            var re = /-?\d+/;
            var m = re.exec(val);
            var d = new Date(parseInt(m[0]));
            return d;
        }
    </script>

    <script type="text/javascript">


        function AppViewModel() {

            var self = this;

            //已申请列表
            self.MyApplyList = ko.observableArray([]);

            self.WFNameList = ko.observableArray([]);

            self.selectedWFName = "";


            self.OnSelectChange = function () {

                getMyApply(1);
            }
        }
    </script>
</asp:Content>
