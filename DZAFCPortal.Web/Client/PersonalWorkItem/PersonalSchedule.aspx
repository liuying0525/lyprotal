<%@ Page Title="" Language="C#" MasterPageFile="../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="PersonalSchedule.aspx.cs" Inherits="DZAFCPortal.Web.Client.PersonalWorkItem.PersonalSchedule" %>

<%@ Register Src="../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
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
                <a class="fl ny_mainlist_title_exchangemail" onclick="RedirectToOWA(encodeURIComponent('?cmd=contents&module=calendar&view=monthly'));return false;">
                    <img src="/Scripts/Client/images/ny_share_icon.png" alt="" />点击进入exchange日历
                </a>
            </div>
            <div class="ny_mainlist_block" id="Content_Schedule">
                <div class="ny_ny_mainlist_calendar_select">
                    <a class="ny_ny_mainlist_calendar_select_btn btn_prev" onclick="Search(this,'before');return false;" title="前一天">&lt;&lt;</a>
                    <input class="ny_ny_mainlist_calendar_select_input" type="text" value="" id="timeperiods" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd', readOnly: true, isShowClear: false, onpicked: function (dp) { ProcessQueryWhileTimePicked(dp.cal.getNewDateStr()); } })" />
                    <a class="ny_ny_mainlist_calendar_select_btn btn_next" onclick="Search(this,'next');return false;" title="后一天">&gt;&gt;</a>
                </div>
                <div class="ny_ny_mainlist_calendar_block" data-bind="foreach: currentModel.ScheduleList">
                    <div class="ny_ny_mainlist_calendar_title">
                        <span class="ny_ny_mainlist_calendar_title_day" data-bind="    text: DayNum"></span>
                        <span class="ny_ny_mainlist_calendar_title_week" data-bind="    text: DayOfWeek"></span>
                        <span class="ny_ny_mainlist_calendar_title_month" data-bind="text: MonthNum"></span>

                    </div>
                    <div>
                        <ul class="ny_ny_mainlist_calendar_ul" data-bind="    foreach: $data.DetailList, visible: $data.DetailList.length > 0">
                            <li data-bind="    click: function (data, event) { RedirectToOWA(encodeURIComponent(data.DisplayFormUrl)); }">
                                <a>
                                    <span class="ny_ny_mainlist_calendar_ul_time" data-bind="    text: TimePeriod"></span>
                                    <span class="ny_ny_mainlist_calendar_ul_title" data-bind="text: Title, attr: { title: Title }"></span>
                                    <span class="ny_ny_mainlist_calendar_ul_address" data-bind="text: Place, attr: { title: Place }"></span>
                                </a>
                            </li>
                        </ul>
                        <span class="ny_ny_mainlist_calendar_nodate" data-bind="visible: $data.DetailList.length <= 0">无日程安排</span>
                    </div>
                    <asp:HiddenField runat="server" ID="hfDomainAccount" />

                    <div id="SSSInfo" style="display: none; width: 500px; height: 200px">
                        <table class="table table-bordered bea_domain">
                            <tbody>
                                <tr>
                                    <td style="width: 100px; text-align: right;">域\用户名:</td>
                                    <td>
                                        <input type="text" id="txtAccount" />
                                        <span style="color: red;">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right;">密码:</td>
                                    <td>
                                        <input type="password" id="txtPWD" />
                                        <span style="color: red;">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <input class="btn btn-primary" style="line-height: 14px;" type="button" id="btnSave" value="确定" onclick="SetSSS()" />

                                        <input class="btn btn-danger" style="line-height: 14px;" type="button" id="btnCancel" value="取消" onclick="layer.closeAll()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 20px !important;"><span style="color: red;">*第一次访问或域账号密码修改后，需进行单点登录设置！</span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/Client/ThirdLibs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Scripts/Client/ThirdLibs/knockout-3.2.0.js"></script>
    <script src="/Scripts/Client/ThirdLibs/moment.js"></script>
    <script src="SSSConmon.js"></script>

    <script type="text/javascript">
        var currentModel;
        var _url = '<%=DZAFCPortal.Config.Base.ClientBasePath %>/PersonalWorkItem/Ajax/ExchangeItemHandler.ashx?time=' + new Date();
        var pagesize = 10;
        $(function () {
            $("#txtAccount").val($("#<%=hfDomainAccount.ClientID%>").val());

            currentModel = new AppViewModel()
            ko.applyBindings(currentModel, $("#Content_Schedule")[0]);
            //var t = moment(new Date());
            //var b = GetStartDate(t);
            var b = moment(new Date());
            var e = AddDays(b, 6);
            getCalendar(moment(b).format('YYYY-MM-DD'), moment(e).format('YYYY-MM-DD'));
            //getAllMail($("#tabs_title>li[class='active']:first"), 1);
        });
    </script>

    <script type="text/javascript">
        //获取日程列表
        getCalendar = function (beginDate, endDate) {
            $("#timeperiods").val(beginDate);

            $.ajax({
                type: "post",
                url: _url,
                dataType: "json",
                data: {
                    Op: "GetCalendarList",
                    BeginTime: beginDate,
                    EndTime: endDate,
                },
                success: function (data) {
                    if (data.IsSucess) {
                        if (data.Datas.length >= 0) {
                            currentModel.ScheduleList(data.Datas);
                        }
                    }
                    else {
                        ShowSSSSetLayer();
                    }
                }
            });

            return false;
        }

        function AppViewModel() {

            var self = this;

            //日程列表
            self.ScheduleList = ko.observableArray([]);

        }

        //切换上下周数据
        function Search(trigger, type) {
            var begin = moment($(trigger).siblings("input").val());
            switch (type) {
                case "before":
                    getCalendar(moment(AddDays(begin, -1)).format('YYYY-MM-DD'), moment(AddDays(begin, 5)).format('YYYY-MM-DD'));
                    break;
                case "next":
                    getCalendar(moment(AddDays(begin, 1)).format('YYYY-MM-DD'), moment(AddDays(begin, 7)).format('YYYY-MM-DD'));
                    break;
                default: break;
            }
        }

        //获取基于指定日期所在的周区间的第一天
        function GetStartDate(day) {
            var beginOfWeek = day.day();

            var beginExt = beginOfWeek == 0 ? 6 : 1 - beginOfWeek;//往前添加的查找天数

            var begin = AddDays(day, beginExt)

            return begin;
        }


        // 增加天 
        function AddDays(d, value) {
            return moment(d).date(d.date() + value)
        }

        //时间选择后执行查询
        function ProcessQueryWhileTimePicked(val) {
            var begin = moment(val);
            var end = AddDays(begin, 6);
            getCalendar(begin.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'));
        }

    </script>
</asp:Content>
