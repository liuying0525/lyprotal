<%@ Page Title="" Language="C#" MasterPageFile="../BaseLayout.Master" AutoEventWireup="true" CodeBehind="GlobalAddressBook.aspx.cs" Inherits="DZAFCPortal.Web.Client.AddressBook.GlobalAddressBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- 弹层脚本和样式 start -->
    <script src="/Scripts/Client/ThirdLibs/layer/layer.min.js"></script>
    <link href="/Scripts/Client/ThirdLibs/layer/skin/layer.css" rel="stylesheet" />

    <!-- Ztree 脚本和样式 -->
    <link href="/Scripts/Client/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/Client/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <script src="/Scripts/Client/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
    <!-- Ztree End -- >
  
 <!-- 分页脚本 -->
    <script src="/Scripts/Client/ThirdLibs/jquery.pager_2014_08.js"></script>

    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Client/ThirdLibs/knockout-3.2.0.js"></script>
    <script type="text/javascript">
        //分页的回传方法
        pageCallback = function (currentIndex) {
            $.ajax({
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
                data: {
                    Op: "addressbook",
                    orgId: orgId,
                    pagesize: pagesize,
                    currentIndex: currentIndex,

                    Account: currentModel.searchModel.Account(),
                    PostName: currentModel.searchModel.PostName(),
                    Department: currentModel.searchModel.Department(),
                    OfficeName: currentModel.searchModel.OfficeName(),
                    Address: currentModel.searchModel.Address(),
                    OfficePhone: currentModel.searchModel.OfficePhone(),
                },
                dataType: "json",
                success: function (data) {
                    //为视图的 users 重新赋值
                    currentModel.users(data.datas);

                    //设置分页
                    $('#pager1').pager({
                        pagenumber: currentIndex,
                        recordcount: data.recordCount,
                        pagesize: pagesize,
                        buttonClickCallback: pageCallback,
                        firsttext: '首页',
                        prevtext: '前一页',
                        nexttext: '下一页',
                        lasttext: '尾页',
                        recordtext: '共{0}页，{1}条记录',
                        numericButtonCount: 0
                    });
                }
            });
        }

        var pagesize = 10;
        //选中的组织树
        var orgId = "";

        //通过组织加载用户信息
        function loadUser(org) {
            orgId = org;
            pageCallback(1);

            $("#searchMsg").html("");
        }

        function searchUsers() {
            var searchModel = currentModel.searchModel;

            if (searchModel.Account() == "" && searchModel.PostName() == ""
                 && searchModel.Department() == "" && searchModel.OfficeName() == ""
                && searchModel.Address() == "" && searchModel.OfficePhone() == "") {

                $("#searchMsg").html("请输入关键字！");

                currentModel.users([]);
                currentModel.checkedUser.ID("");

                return;
            }
            else {
                loadUser("");

                $("#searchMsg").html("");
            }
        }
    </script>
    <!-- KnockOut End -->

    <!-- 加载左侧树形菜单 -->
    <script type="text/javascript">
        var setting = {
            check: {
                enable: false
            },
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
                    $("#txtKey").val("");

                    loadUser(treeNode.ID);
                }
            }
        };

        //发送ajax请求，获取左侧菜单树数据
        $(function () {
            <%-- $.ajax({
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getOrganizationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Tree"
                },
                success: function (data) {
                    currentModel.orgs(data);

                    $.fn.zTree.init($("#treeOrganization"), setting, currentModel.orgs());
                }
            });--%>
        });

    </script>
    <!-- 左侧树形菜单 End -->

    <!-- 用户或组织选中项操作 -->
    <script type="text/javascript">
        var currentModel = new AppViewModel();

        $(function () {
            ko.applyBindings(currentModel);
        });

        // Model
        function AppViewModel() {
            var self = this;
            //列表中待选用户
            self.users = ko.observableArray([]);

            //选中的用户
            self.checkedUser = {
                ID: ko.observable(""),
                Account: ko.observable(""),
                DisplayName: ko.observable(""),
                OrgId: ko.observable(""),

                OfficePhone: ko.observable(""),
                Email: ko.observable(""),
                FirstName: ko.observable(""),
                LastName: ko.observable(""),
                Department: ko.observable(""),
                OfficeName: ko.observable(""),
                PostName: ko.observable(""),
                Address: ko.observable(""),
            }

            //搜索模型
            self.searchModel = {
                Account: ko.observable(""),
                PostName: ko.observable(""),
                Department: ko.observable(""),
                OfficeName: ko.observable(""),
                Address: ko.observable(""),
                OfficePhone: ko.observable("")
            };

            //组织
            self.orgs = ko.observableArray([]);

            //选中用户操作
            self.checkUser = function () {
                self.checkedUser.ID(this.ID);
                self.checkedUser.Account(this.Account);
                self.checkedUser.DisplayName(this.DisplayName);
                self.checkedUser.OrgId(this.OrgId);

                self.checkedUser.OfficePhone(this.OfficePhone);
                self.checkedUser.Email(this.Email);
                self.checkedUser.FirstName(this.FirstName);
                self.checkedUser.LastName(this.LastName);
                self.checkedUser.Department(this.Department);
                self.checkedUser.OfficeName(this.OfficeName);
                self.checkedUser.PostName(this.PostName);
                self.checkedUser.Address(this.Address);
                //openUserAddressBook();
            }

            self.sureUserCallback = function () { }

            self.clear = function () {
                self.checkedUser.ID("");
                self.searchModel.Account("");
                self.checkedUser.DisplayName("");
                self.checkedUser.OrgId("");
                self.checkedUser.OfficePhone("");
                self.checkedUser.Email("");
                self.checkedUser.FirstName("");
                self.checkedUser.LastName("");
                self.checkedUser.Department("");
                self.checkedUser.OfficeName("");
                self.checkedUser.PostName("");
                self.checkedUser.Address("");

            }

            self.clearSearch = function () {
                self.searchModel.Account("");
                self.searchModel.OfficePhone("");
                self.searchModel.Department("");
                self.searchModel.OfficeName("");
                self.searchModel.PostName("");
                self.searchModel.Address("");
            }
        }
    </script>
    <%--回车事件--%>
    <script type="text/javascript">
        $(function () {
            $(".search").keypress(function (e) {
                if (e.which == 13) {
                    $(".search img").click(search());
                }
            });
            $(window).keydown(function (e) {
                if (e.which == 13) {
                    return;
                }
            })
        });

        function openUserAddressBook() {
            $.layer({
                type: 1,
                title: "组织通讯录",
                maxmin: false,
                area: [800, 600],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [1, true],
                fix: true,
                page: {
                    dom: "#div_addressbook"
                },
                fadeIn: 1000
            });

            //currentModel.clear();
            //指定回传事件
            //currentModel.sureUserCallback = sureUserCallBack;
        }

    </script>
    <style>
        hr {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .search {
            margin-left: 0px !important;
        }

        .BEA_AB_left {
            padding: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="BEA_bg">
        <div class="BEA_container">
            <div class="BEA_mainwidth">
                <div class="BEA_Bread">
                    <span>首页</span>&gt;<span>组织通讯录</span>
                </div>
                <div class="BEA_main BEA_block_shadow_main">
                    <div class="BEA_mainlist_title">
                        <span class="fl">组织通讯录 Address Book</span>
                    </div>
                    <div class="BEA_mainlist_News" style="padding-top: 20px;">
                        <%--<div class="BEA_AB_left fl">
                            <ul id="treeOrganization" class="ztree"></ul>
                        </div>--%>
                        <div class="BEA_AB_search_div">
                            <table class="BEA_AB_search_table">
                                <tr>
                                    <td class="table_title_left">用户名/LANID：</td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.Account" class="search form-control  " />
                                    </td>
                                    <td class="table_title_left">部门：
                                    </td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.Department" class="search form-control  " />
                                    </td>
                                    <td class="table_title_left">地址：
                                    </td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.Address" class="search form-control  " />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="table_title_left">职务：
                                    </td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.PostName" class="search form-control  " />
                                    </td>
                                    <td class="table_title_left">办公室：
                                    </td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.OfficeName" class="search form-control  " />
                                    </td>
                                    <td class="table_title_left">电话：
                                    </td>
                                    <td>
                                        <input type="text" data-bind="value: currentModel.searchModel.OfficePhone" class="search form-control  " />
                                    </td>
                                </tr>

                            </table>
                            <div class="BEA_AB_search">
                                <a href="#" class="BEA_AB_search_btn" onclick="searchUsers();return false;">
                                    <img src="/Scripts/Client/images/AB_search.png" alt="" style="margin-top: -6px; margin-left: -4px;" />搜索
                                </a>
                                <a href="#" class="BEA_AB_search_btn" data-bind="click: currentModel.clearSearch">清 空</a>
                            </div>
                        </div>

                        <!--搜索错误消息 -->
                        <div id="searchMsg" class="fl" style="color: red; width: 100%;padding-top: 10px;text-align: center;"></div>
                        <div class="clear"></div>
                        <div class="BEA_AB_middle fl" id="bookRight">
                            <%-- <div>
                                    已选用户：<span style="line-height: 29px; color: red; font-weight: bold; text-decoration: underline;" data-bind="text: currentModel.checkedUser.DisplayName "></span>
                                    <a class="BEA_btn_submit fr" style="line-height: 17px;" data-bind=" click: sureChecked">确&nbsp;&nbsp;认</a> <span id="errorMsg" style="color: red"></span>
                                    <div class="clear"></div>
                                    <hr />
                                </div>--%>


                            <div class="bookAddress_div">
                                <ul data-bind="visible: currentModel.users().length > 0, foreach: currentModel.users" class="bookAddress BEA_AB_middle_ul">
                                    <li data-bind=" click: currentModel.checkUser">
                                        <div>
                                            <span data-bind=" text: Account" style="margin-right: 5px;"></span>
                                            (<span data-bind=" text: DisplayName"></span>)
                                        </div>
                                    </li>
                                </ul>

                                <%--<div style="height: 40px; text-align: center; padding-top: 10px;" data-bind="visible: currentModel.users().length <= 0">
                                    <span style="color: red">没有数据！</span>
                                </div>--%>
                                <div class="clear"></div>
                                <div id="pager1" class=" jqpager jqpagerUser" style="padding-top: 1px; margin: 5px 0 0 5px;" data-bind="visible: currentModel.users().length > 0"></div>
                            </div>
                            <div class="clear"></div>

                        </div>

                        <div class="BEA_AB_right fr" data-bind="with: currentModel.checkedUser, visible: currentModel.checkedUser.ID() != ''">

                            <span data-bind="text: DisplayName " class="BEA_AB_right_title"></span>
                            <%--<span data-bind="text: PostName " class="BEA_AB_right_title2"></span>--%>
                            <table class="BEA_AB_right_table">
                                <tbody>

                                    <tr>
                                        <td class="firstc">姓(L):</td>
                                        <td class="lastc"><span data-bind="text: LastName "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">名(F):</td>
                                        <td class="lastc"><span data-bind="text: FirstName "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">显示名:</td>
                                        <td class="lastc" colspan="3"><span data-bind="text: DisplayName "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">职务(E):</td>
                                        <td class="lastc"><span data-bind="text: PostName "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">部门(D):</td>
                                        <td class="lastc"><span data-bind="text: Department "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">办公室(O):</td>
                                        <td class="lastc"><span data-bind="text: OfficeName "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">电话(H):</td>
                                        <td class="lastc"><span data-bind="text: OfficePhone "></span></td>
                                    </tr>
                                    <tr>
                                        <td class="firstc">地址(R):</td>
                                        <td class="lastc" colspan="3"><span data-bind="text: Address "></span></td>
                                    </tr>
                                </tbody>

                            </table>
                        </div>



                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
            </div>
        </div>

    </div>




</asp:Content>
