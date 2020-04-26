<%@ Page Title="" Language="C#" MasterPageFile="../../NYBaseLayout.Master" AutoEventWireup="true" CodeBehind="NY_CompanyMailList.aspx.cs" Inherits="DZAFCPortal.Web.Client.NY_CommunicationPlatform.Company.NY_CompanyMailList" %>

<%@ Register Src="../../Controls/SiteNavigator.ascx" TagPrefix="uc1" TagName="SiteNavigator" %>
<%@ Register Src="../../Controls/NavTitleBar.ascx" TagPrefix="uc1" TagName="NavTitleBar" %>
<%@ Register Src="../../Controls/SecendaryLeftNav.ascx" TagPrefix="uc1" TagName="SecendaryLeftNav" %>
<%@ Register Src="../../Controls/TopTitleBar.ascx" TagPrefix="uc1" TagName="TopTitleBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/Scripts/Client/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/Client/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
    <script src="/Scripts/Client/ThirdLibs/knockout-3.2.0.js"></script>
    <script src="/Scripts/Client/ThirdLibs/jquery.pager_2014_08.js"></script>
    <!-- KnockOut Start -->
    <script type="text/javascript">

        var currentCompanyApp = new companyModelApp();
        function companyModelApp() {
            var self = this;

            self.users = ko.observableArray([]);
            self.detailUser = {
                DisplayName: ko.observable(""),
                OfficePhone: ko.observable(""),
                MobilePhone: ko.observable(""),
                IpPhone: ko.observable(""),
                Email: ko.observable(""),
                PhotoUrl: ko.observable(""),
                Organization: ko.observable(""),
                Account: ko.observable(""),
                LyncSip: ko.observable(""),
                Department: ko.observable(""),
                ManagerDisplayName: ko.observable(""),
                LevelName: ko.observable(""),
                HomePhone: ko.observable(""),
                Emailhref: ko.observable(""),
                DirectPhone: ko.observable(""),
                ShortMobilePhone: ko.observable(""),
                EmergencyPhone: ko.observable("")
            };

            //获取Lync拨号号码，将用户电话号码格式化
            self.getLyncPhone = function (phone) {
                return "Callto:tel:+19" + phone;
            }

            self.getEmail = function (email) {
                return "mailto:" + email;
            }

            self.phoneShow = function (IpPhone, HomePhone) {
                var rs = true;
                if (IpPhone == '' || HomePhone == '' || IpPhone == null || HomePhone == null) {
                    rs = false;
                }
                return rs;
            }

            //判断是否允许Lync通讯
            //只有当前登录用户有注册Lync，并且点击的用户也有注册Lync
            //结果才返回 true，表示可以进行lync通讯
            self.enableLyncContract = function (lyncSip) {
                var currentUserSip = "<%=CurrentUser.LyncSip%>";
                
                var rs = false
                if (currentUserSip && lyncSip)
                    rs = true;

                return rs;
            }

            //判断是否允许拨打lync电话通讯
            //只有当前登录用户有注册Lync，并且点击的用户的手机号码不为空
            //结果才返回 true，表示可以使用lync拨打电话
          <%--  self.enbaleLyncPhoneContract = function (mobilePhone) {
                var currentUserSip = "<%=CurrentUser.LyncSip%>";

                var rs = false;
                if (currentUserSip && mobilePhone)
                    rs = true;

                return rs;
            }--%>

            self.LoadDetailUser = function () {
                self.detailUser.DisplayName(this.DisplayName);
                self.detailUser.OfficePhone(this.OfficePhone);
                self.detailUser.MobilePhone(this.MobilePhone);
                self.detailUser.IpPhone(this.IpPhone);
                self.detailUser.Email(this.Email);
                self.detailUser.Emailhref(self.getEmail(this.Email));
                self.detailUser.PhotoUrl(this.PhotoUrl);
                self.detailUser.ManagerDisplayName(this.ManagerDisplayName)

                var orgPathName = getShowPathName(this.OrgPathName, 100);
                self.detailUser.Organization(orgPathName);
                self.detailUser.Account(this.Account);
                self.detailUser.LyncSip(this.LyncSip);
                self.detailUser.Department(this.Department);
                self.detailUser.LevelName(this.LevelName);
                self.detailUser.HomePhone(this.HomePhone);
                self.detailUser.DirectPhone(this.DirectPhone);
                self.detailUser.ShortMobilePhone(this.ShortMobilePhone);
                self.detailUser.EmergencyPhone(this.EmergencyPhone);
                showUserDetailLayer();
            }
        }

        //分页的回传方法
        loadCompanyUsers = function (currentIndex) {
            $.ajax({
                cache: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/NY_CommunicationPlatform/Ajax/addressBookHandler.ashx',
                data: {
                    Op: "bookAddress",
                    searchKey: $("#txtKey").val(),
                    orgId: currentOrgId,
                    isContainsChildren: isContainsChildren ? 1 : 0,
                    selectType: $("#selectType").val(),
                    pagesize: pagesize,
                    currentIndex: currentIndex
                },
                dataType: "json",
                beforeSend: function () {
                    $("#div_Message").css("display", "none");
                    $("#searchMsg").html("");
                },
                success: function (data) {
                    currentCompanyApp.users(data.datas);

                    if (data.datas.length <= 0) {
                        $("#div_Message").css("display", "block");
                    }

                    //在电脑端显示分页按钮的个数，加上中间的一个总数为5个
                    var numericButtonCount = 4;
                    //if (!IsWindows()) {
                    //    numericButtonCount = 0;
                    //}

                    var pagerSettings = initPagerSettings();
                    pagerSettings.pagenumber = currentIndex;
                    pagerSettings.recordcount = data.recordCount;
                    pagerSettings.pagesize = pagesize;
                    pagerSettings.buttonClickCallback = loadCompanyUsers;
                    pagerSettings.numericButtonCount = numericButtonCount;

                    //设置分页
                    $('#pager1').pager(pagerSettings);
                }
            });
        }

        var pagesize = 10;
        //当前的组织ID，如果为包含子级isContainsChildren=true，则该值去组织的Path属性
        var currentOrgId = "";
        //是否包含子级，当点击搜索按钮时，该值设置为true
        //当点击组织树节点时，设置为false
        var isContainsChildren = true;

        //开始搜索
        function searchClickEvent() {
            if (event.keyCode == 13) {
                search();
                return false;
            }
        }

        function search() {
            if ($("#txtKey").val() == "") {
                $("#searchMsg").html("请输入关键字！");

                return;
            }
            else {
                isContainsChildren = true;

                var treeObj = $.fn.zTree.getZTreeObj("treeOrganization");
                var selectedNodes = treeObj.getSelectedNodes();
                if (selectedNodes.length > 0) {
                    currentOrgId = selectedNodes[0].Path;
                }
                else currentOrgId = "";
                loadCompanyUsers(1);

                //设置没有数据时的提示信息
                $("#div_Message").html("该关键字没有查询相关用户！");

                $("#searchMsg").html("");
            }
        }

        //根据屏幕宽度和高度设置每页显示的数量
        function initPageSize() {
            //每行显示的数据量
            var colCount = 3;

            pagesize = getPageSizeByScreenHeight(colCount, 100, 160);
        }

        //方法定义完毕后，执行绑定！
        //注：由于使用了单页加载，$(function()) 方法需要放在执行脚本最后
        $(function () {
            ko.applyBindings(currentCompanyApp, $("#Content_Company")[0]);

            //initPageSize();
        });
    </script>
    <!-- KnockOut End -->

    <!-- 加载左侧树形菜单 -->
    <script type="text/javascript">
        var setting = {
            data: {
                key: {
                    name: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "ParentID"
                }
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    var treeObj = $.fn.zTree.getZTreeObj("treeOrganization");
                    //展开当前节点
                    treeObj.expandNode(treeNode, true, false, false);
                    //清空搜索框
                    $("#txtKey").val("");
                    //修改没有数据时的提示信息
                    if (treeNode.isParent) {
                        //设置没有数据时的提示信息
                        $("#div_Message").html("请到下属部门查看相关用户！");
                    }
                    else {
                        //设置没有数据时的提示信息
                        $("#div_Message").html("该部门下没有查询到用户！");
                    }

                    //设置信息
                    var searchRangeHtml = "<span title='" + getShowPathName(treeNode.PathName, 100) + "'> ";
                    searchRangeHtml += getShowPathName(treeNode.PathName, 3);
                    searchRangeHtml += "</span> ";
                    $("#span_search_range").html(searchRangeHtml);
                    //设置当前搜索不包含子级
                    isContainsChildren = false;
                    //设置当前节点ID
                    currentOrgId = treeNode.ID;

                    //调用方法，加载当前节点下用户数据
                    loadCompanyUsers(1);
                }
            }
        };

        //获取要显示的PathName
        //部门通讯录同样存在该方法
        function getShowPathName(pathName, level) {
            var showName = "";
            //显示的层级，从后往前
            var showLevel = level;

            if (pathName) {
                var nameArray = pathName.split('/');
                //移除空的项
                for (var i = nameArray.length - 1; i >= 0; i--) {
                    if (!nameArray[i]) {
                        nameArray.splice(i, 1);
                    }
                }

                //表示数组的长度大于要显示的长度，删除多余的部分
                if (showLevel < nameArray.length) {
                    nameArray.splice(0, nameArray.length - showLevel);
                }

                //填充的字符
                var fixText = ">>";
                for (var i = 0; i < nameArray.length; i++) {
                    showName += nameArray[i] + fixText;
                }
                showName = showName.substring(0, showName.length - fixText.length);
            }

            return showName;
        }

        //获取当前节点的组织ID，如果包含子级，则获取节点的Path，否则返回ID
        //如果未选中,返回空
        function getSelectedNodeOrgId() {
            var treeObj = $.fn.zTree.getZTreeObj("treeOrganization");
            var nodes = treeObj.getSelectedNodes();

            var org = "";
            if (nodes.length > 0) {
                var node = nodes[0];

                if (isContainsChildren) {
                    org = node.Path;
                }
                else org = node.ID;
            }
        }

        $(function () {
            $.ajax({
                cache: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/NY_CommunicationPlatform/Ajax/addressBookHandler.ashx',
                dataType: "json",
                data: {
                    op: "orgTree"
                },
                success: function (data) {
                    $.fn.zTree.init($("#treeOrganization"), setting, data);
                    var treeObj = $.fn.zTree.getZTreeObj("treeOrganization");

                    var node = $.fn.zTree.getZTreeObj("treeOrganization").getNodes()[0];
                    //node.Name = "上海广播电视台";
                    //treeObj.updateNode(node);
                    $("#span_search_range").text(node.Name);

                    expandZtreeNodeByLevel(treeObj, 0);
                }
            });
        });

    </script>
    <!-- 左侧树形菜单 End -->
    <script type="text/javascript">

        function showUserDetailLayer() {
            $.layer({
                type: 1,
                title: "员工信息",
                maxmin: false,
                move: '.xubox_title',
                area: [370, 400],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [0, true],
                fix: true,
                page: {
                    dom: "#div_company_user_detail"
                },
                fadeIn: 1000,

            });

            return false;
        }

        function closeUserDetailLayer() {
            layer.closeAll();
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
                    <uc1:TopTitleBar runat="server" ID="TopTitleBar" />
                </span>
                <ul class="fr">
                    <li class=""><a href="javascript:history.go(-1);">返回</a></li>
                </ul>
            </div>
            <div class="ny_mainlist_block">

                <div class="ny_adressbook_left fl">
                    <div class="ny_adressbook_left_bg">
                        <!-- 以下的ul中放树形结构 -->
                        <ul id="treeOrganization" class="ztree company_ztree"></ul>
                        <!-- end 以上的ul中放树形结构 -->
                    </div>
                </div>

                <div class="ny_adressbook_right fr" id="Content_Company">
                    <div style="display:none">
                        搜索范围：<span id="span_search_range"></span>
                    </div>
                    <div class="ny_adressbook_right_search">
                        员工搜索：<input type="text" style="margin-bottom: 0px;" id="txtKey" onkeypress="return searchClickEvent();" />
                        <select style="margin-bottom: 0px;" id="selectType">
                            <option value="userName">用户名称</option>
                            <option value="email">电子邮箱</option>
                            <option value="ipPhone">固话分机</option>
                            <option value="homePhone">Lync分机</option>
                            <option value="mobilePhone">手机号码</option>
                        </select>

                        <img src="/Scripts/Client/images/search.png" alt="" style="width: 30px; height: 30px; margin-left: 5px; cursor: pointer" onclick="search();" />
                        <span id="searchMsg" style="color: red"></span>
                        <div class="clear"></div>

                    </div>
                    <!-- 列表页 -->
                    <div class="ny_adressbook_right_list">
                        <ul class="ny_adressbook_ul" data-bind="visible: currentCompanyApp.users().length > 0, foreach: currentCompanyApp.users">
                            <li class="ny_adressbook_ul_li">
                                <%--<div class="ny_adressbook_ul_a_state state_white"></div>--%>
                                <img alt="" class="ny_adressbook_ul_a_img" data-bind="attr: { src: PhotoUrl }, click: currentCompanyApp.LoadDetailUser " />
                                <div class="ny_adressbook_ul_a_div">
                                    <a class="ny_adressbook_ul_a_div_name" data-bind=" text: DisplayName, click: currentCompanyApp.LoadDetailUser"></a>
                                    <span class="ny_adressbook_ul_a_div_job" data-bind="text: LevelName"></span>
                                    <div class="clear"></div>
                                    <span class="ny_adressbook_ul_a_div_dept" data-bind=" text: Department"></span>
                                    <span class="ny_adressbook_ul_a_div_mphone"><a data-bind=" text: IpPhone, attr: { title: IpPhone }" style="cursor:text"></a><a data-bind="visible: currentCompanyApp.phoneShow(IpPhone, HomePhone)" style="cursor:text">/</a><a data-bind="    text: HomePhone, attr: { title: HomePhone }" style="cursor:text"></a></span>
                                    <span class="ny_adressbook_ul_a_div_ophone"><a data-bind=" text: MobilePhone, attr: { title: MobilePhone }" style="cursor:text"></a></span>
                                    <span class="ny_adressbook_ul_a_div_email"><a data-bind="text: Email, attr: {href: currentCompanyApp.getEmail(Email), title: Email }"></a></span>
                                </div>
                            </li>
                        </ul>
                        <div class="clear"></div>
                        <div id="pager1" class="jqpager" data-bind="visible: currentCompanyApp.users().length > 0"></div>
                        <div id="div_Message" style="margin-left: 35%; display: none; margin-top: 35px;">该部门下没有查询到用户！</div>
                        <%-- <div class="pagenavi">
                            <a href="#" class="page">首页</a>
                            <span class="current">1</span>
                            <a href="#" class="page">2</a>
                            <a href="#" class="page">3</a>
                            <a href="#" class="nextpostslink">尾页</a>
                        </div>

                        <div id="div_Message" style="margin-left: 35%; display: none; margin-top: 35px;">该部门下没有查询到用户！</div>--%>
                    </div>
                    <div class="ny_adressbook_ul_li_info" style="display: none" id="div_company_user_detail">
                      <%--  <div class="ny_adressbook_ul_li_info_state state_white"></div>--%>
                        <img data-bind="attr: { src: detailUser.PhotoUrl }" alt="" class="ny_adressbook_ul_li_info_img" />
                        <div class="ny_adressbook_ul_li_info_nameinfo">
                            <span data-bind=" text: detailUser.DisplayName"></span>
                            <span data-bind=" text: detailUser.Account"></span>
                              <a data-bind="    attr: { href: detailUser.LyncSip }, visible: currentCompanyApp.enableLyncContract(detailUser.LyncSip())" title="办公通讯">
                                <img id="imgLyncSip" src="/Scripts/Client/images/ny_adressbook_massage_icon.png" alt="" /></a>
                        </div>
                        <div class="clear"></div>

                        <div class="ny_adressbook_ul_li_more">
                            <span>职&emsp;&emsp;务：<strong data-bind=" text: detailUser.LevelName"></strong>
                            </span>
                            <span>部门名称：<strong data-bind=" text: detailUser.Department"></strong>
                            </span>
                            <span>固话分机：<strong data-bind=" text: detailUser.IpPhone"></strong>
                            </span>
                             <span>Lync分机：<strong data-bind=" text: detailUser.HomePhone"></strong>
                            </span>
                            <span>手机号码：<strong data-bind=" text: detailUser.MobilePhone"></strong>
                                <%--    <a data-bind="attr: { href: currentCompanyApp.getLyncPhone(detailUser.MobilePhone()) }, visible: currentCompanyApp.enbaleLyncPhoneContract(detailUser.MobilePhone())" title="拨打电话">
                                    <img id="imgLyncPhone" src="/Scripts/Client/images/smg_adressbook_call_icon.png" alt="" />
                                </a>--%>
                            </span>
                            <span>直线号码：<strong data-bind=" text: detailUser.DirectPhone"></strong>
                            </span>
                            <span>移动短号：<strong data-bind=" text: detailUser.ShortMobilePhone"></strong>
                            </span>
                            <span>应急电话：<strong data-bind=" text: detailUser.EmergencyPhone"></strong>
                            </span>
                            <span>电子邮箱：<a data-bind="text: detailUser.Email, attr: { href: detailUser.Emailhref, title: detailUser.Email }" style="font-weight:bold"></a>
                            </span>
                        </div>
                        <a href="#" class="ny_normal_btn fl" onclick="closeUserDetailLayer();return false;">关 闭</a>

                    </div>

                </div>

                <div class="clear"></div>

            </div>

        </div>
    </div>
</asp:Content>
