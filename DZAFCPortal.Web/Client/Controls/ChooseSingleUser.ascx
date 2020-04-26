<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChooseSingleUser.ascx.cs" Inherits="DZAFCPortal.Web.Client.Controls.ChooseSingleUser" %>
<!-- Ztree 脚本和样式 -->
<link href="/Scripts/Client/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/Client/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
<script src="/Scripts/Client/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
<!-- Ztree End -- >
    
 <!-- 分页脚本 -->
<script src="/Scripts/Client/ThirdLibs/jquery.pager_2014_08.js"></script>
<!-- KnockOut 加载用户数据 -->
<script type="text/javascript">
    //分页的回传方法
    pageCallback = function (currentIndex) {
        $.ajax({
            type: "get",
            url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
            data: {
                Op: "choose",
                searchKey: $("#txtKey").val(),
                checkedUsers: getQueryString("myFriends"),
                orgId: orgId,
                pagesize: pagesize,
                currentIndex: currentIndex
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

    var pagesize = 15;
    //选中的组织树
    var orgId = "";

    //通过组织加载用户信息
    function loadUser(org) {
        currentModel.IsFirstLoad(false);
        orgId = org;
        pageCallback(1);
    }

    function search() {
        if ($("#txtKey").val() == "") {
            $("#searchMsg").html("请输入关键字！");

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
        ko.applyBindings(currentModel, $("#div_Choose")[0]);
    });

    // Model
    function AppViewModel() {
        var self = this;
        //是否首次加载，首次加载不显示 没有数据项
        self.IsFirstLoad = ko.observable(true);
        //列表中待选用户
        self.users = ko.observableArray([]);

        //选中的用户
        self.checkedUser = {
            ID: ko.observable(""),
            Account: ko.observable(""),
            DisplayName: ko.observable(""),
            OrgId: ko.observable("")
        }

        //组织
        self.orgs = ko.observableArray([]);

        //选中用户操作
        self.checkUser = function () {
            self.checkedUser.ID(this.ID);
            self.checkedUser.Account(this.Account);
            self.checkedUser.DisplayName(this.DisplayName);
            self.checkedUser.OrgId(this.OrgId);

            $("#errorMsg").html("");
        }

        //点击确认按钮
        self.sureChecked = function () {
            if (self.checkedUser.ID() == "") {
                $("#errorMsg").html("没有选中用户！");

                return;
            }
            else $("#errorMsg").html("");

            self.sureUserCallback();

            //$.ajax({
            //    type: "get",
            //    url: 'ajax/myFriends.ashx',
            //    dataType: "json",
            //    data: {
            //        Op: "add",
            //        checkedAccount: self.checkedUser.Account()
            //    },
            //    success: function (result) {
            //        alert(result.Message);

            //        if (result.IsSucess)
            //            parent.layer.closeAll()
            //    }
            //});
        }

        self.sureUserCallback = function () { }

        self.clear = function () {
            self.checkedUser.ID("");
            self.checkedUser.Account("");
            self.checkedUser.DisplayName("");
            self.checkedUser.OrgId("");
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
</script>
<style>
    hr {
        margin-top: 10px;
        margin-bottom: 10px;
    }

    .search {
        margin-left: 0px !important;
    }
</style>

<!------------------注意------------------------->
<!----  获取选中用户的脚本为 currentModel.checkedUser.Account()  currentModel.checkedUser.ID() -->
<!----  指定确认事件为： currentModel.sureUserCallback =(此处填写方法名称)   -->
<!------------------------------------------->

<div id="div_Choose" class="div_Choose_container">
<%--    <div id="bookLeft">
        <ul id="treeOrganization" class="ztree"></ul>
    </div>--%>
    <div id="bookRight" style="width:600px;margin-left:20px;">
        <div>
            已选用户：<span style="line-height: 29px; color: red; font-weight: bold; text-decoration: underline;" data-bind="text: currentModel.checkedUser.DisplayName "></span>
            <a class="BEA_btn_submit fr" style="line-height: 17px;" data-bind=" click: sureChecked">确&nbsp;&nbsp;认</a> <span id="errorMsg" style="color: red"></span>
            <div class="clear"></div>
            <hr />
        </div>

        <div class="search">
            用户搜索：
                    <input type="text" id="txtKey" class=" form-control " style="width: 200px; display: inline; height: 30px;" />
            <img src="/Scripts/Client/images/search.png"
                style="width: 30px; height: 30px; margin-top: 0px; cursor: pointer" onclick="search();" />
            <span id="searchMsg" style="color: red"></span>
        </div>
        <div class="bookAddress_div">
            <ul data-bind="visible: currentModel.users().length > 0, foreach: currentModel.users" class="bookAddress" style="height: 412px">
                <li data-bind=" click: currentModel.checkUser">
                    <div>
                        <span data-bind=" text: Account" style="margin-right: 5px;"></span>
                        (<span data-bind=" text: DisplayName"></span>)
                    </div>
                </li>
            </ul>

            <div style="height: 40px; text-align: center; padding-top: 10px;" data-bind="visible: !currentModel.IsFirstLoad() && currentModel.users().length <= 0">
                <span style="color: red">没有数据！</span>
            </div>
            <div class="clear"></div>
            <div id="pager1" class=" jqpager jqpagerUser" style="padding-top: 1px; margin: 5px 0 0 5px;" data-bind="visible: currentModel.users().length > 0"></div>
        </div>
        <div class="clear"></div>
    </div>
</div>
