<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChooseMutiUser.ascx.cs" Inherits="DZAFCPortal.Web.Client.Controls.ChooseMutiUser" %>

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
                Op: "choose",
                searchKey: $("#txtKey").val(),
                checkedUsers: $('#<%= hfdCheckedUserIds.ClientID%>').val(),
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

    var pagesize = 20;
    //选中的组织树
    var orgId = "";

    //通过组织加载用户信息
    function loadUser(org) {
        orgId = org;
        pageCallback(1);
    }

    function search() {
        if ($("#txtKey").val() == "") {
            $("#errorMsg").html("请输入关键字！");

            return;
        }
        else {
            loadUser("");

            $("#errorMsg").html("");
        }
    }
</script>
<!-- KnockOut End -->

<!-- 加载左侧树形菜单 -->
<script type="text/javascript">
    var setting = {
        check: {
            enable: false,
            chkboxType: { "Y": "", "N": "" }
        },
        data: {
            key: {
                name: "Name",
                checked: "IsChecked"
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
        $.ajax({
            type: "get",
            url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getOrganizationHandler.ashx',
            dataType: "json",
            data: {
                Op: "Tree",
                checkedOrgs: $('#<%= hfdCheckedOrgIds.ClientID%>').val()
            },
            success: function (data) {
                currentModel.orgs(data);

                $.fn.zTree.init($("#treeOrganization"), setting, currentModel.orgs());
            }
        });
    });

</script>
<!-- 左侧树形菜单 End -->

<!-- 用户或组织选中项操作 -->
<script type="text/javascript">
    var currentModel = new AppViewModel();
    $(function () {
        //初始化选中的用户数据
        if ($('#<%= hfdCheckedUserIds.ClientID%>').val() != "") {
            $.ajax({
                async: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath%>/AjaxPage/getUserHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Checked",
                    checkedUsers: $('#<%= hfdCheckedUserIds.ClientID%>').val()
                },
                success: function (data) {
                    currentModel.checkedUsers(data);
                    currentModel.initLoadUsers(data);
                }
            });
        }

        //初始化已选中的组织数据
        if ($('#<%= hfdCheckedOrgIds.ClientID%>').val() != "") {
            $.ajax({
                async: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getOrganizationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Checked",
                    checkedOrgs: $('#<%= hfdCheckedOrgIds.ClientID%>').val()
                },
                success: function (data) {
                    currentModel.checkedOrgs(data);
                    currentModel.initLoadOrgs(data);
                }
            });
        }

        ko.applyBindings(currentModel);
    });

    // Model
    function AppViewModel() {
        var self = this;
        //列表中待选用户
        self.users = ko.observableArray([]);
        //选中的用户
        self.checkedUsers = ko.observableArray([]);
        //初始已经选中的用户
        self.initLoadUsers = ko.observableArray([]);

        //组织
        self.orgs = ko.observableArray([]);

        //选中用户操作
        self.checkUser = function () {
            var model = {
                ID: this.ID,
                Account: this.Account,
                DisplayName: this.DisplayName,
                OrgId: this.OrgId
            }

            self.checkedUsers.push(model);
            self.users.remove(this);
        }

        //移除选中用户项操作
        self.removeCheckedUser = function () {
            var model = {
                ID: this.ID,
                Account: this.Account,
                DisplayName: this.DisplayName,
                OrgId: this.OrgId
            }

            //选中的用户列表移除
            self.checkedUsers.remove(this);
            if (this.OrgId == orgId)
                self.users.push(model);
        }

        self.removeOrg = function () {
            self.checkedOrgs.remove(this);

            //设置树节点非选中
            var zTree = $.fn.zTree.getZTreeObj("treeOrganization");
            var node = zTree.getNodeByParam("ID", this.ID);
            node.IsChecked = false;
            zTree.updateNode(node); //更新改节点UI
        }

        //点击确认按钮
        self.sureChecked = function () {
            if (self.checkedUsers().length <= 0 && self.checkedOrgs().length <= 0) {
                $("#errorMsg").html("没有选中任何用户或组织机构！");

                return;
            }
            else $("#errorMsg").html("");

            self.initLoadUsers(self.checkedUsers());
            var userIds = "";
            for (var i = 0; i < self.initLoadUsers().length; i++) {
                userIds += self.initLoadUsers()[i].ID + ",";
            }
            $('#<%= hfdCheckedUserIds.ClientID%>').val(userIds);


            self.initLoadOrgs(self.checkedOrgs());
            var orgIds = "";
            for (var i = 0; i < self.initLoadOrgs().length; i++) {
                orgIds += self.initLoadOrgs()[i].ID + ",";
            }
            $('#<%= hfdCheckedUserIds.ClientID%>').val(orgIds);

            layer.closeAll();
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
    <asp:HiddenField ID="hfdCheckedOrgIds" runat="server" />
    <asp:HiddenField ID="hfdCheckedUserIds" runat="server" />
    <div id="bookLeft">
        <ul id="treeOrganization" class="ztree"></ul>
    </div>
    <div id="bookRight" style="width: 580px; padding-right: 5px;">
        <div class="search">
            用户搜索：
                    <input type="text" id="txtKey" class=" form-control txt_width_md" />
            <img src="/Scripts/Client/images/search.png"
                style="width: 30px; height: 30px; margin-top: -9px; cursor: pointer" onclick="search();" />
            <span id="errorMsg" style="color: red"></span>

            <a style="margin-top: 0px; float: right;" class="add_save" data-bind=" click: sureChecked">确认选择</a>
            <div class="clear"></div>
        </div>
        <div class="bookAddress_div">
            <ul data-bind="visible: currentModel.users().length > 0, foreach: currentModel.users" class="bookAddress">
                <li data-bind=" click: currentModel.checkUser">
                    <div>
                        <span data-bind=" text: Account"></span>
                        (<span data-bind=" text: DisplayName"></span>)
                    </div>
                </li>
            </ul>

            <div style="height: 40px; text-align: center; padding-top: 10px;" data-bind="visible: currentModel.users().length <= 0">
                <span>没有数据！</span>
            </div>
            <div id="pager1" class=" jqpager jqpagerUser" style="padding-top: 1px; margin-top: 5px;" data-bind="visible: currentModel.users().length > 0"></div>
        </div>
        <div class="clear"></div>

        <div style="height: 266px; overflow-y: overlay; margin-top: 5px;">
            <div id="div_checkedUser" class="fl">
                <strong>已选用户：</strong>
                <ul data-bind="visible: currentModel.checkedUsers().length > 0, foreach: currentModel.checkedUsers" class="div_checkedOrg_ul">
                    <li data-bind=" click: currentModel.removeCheckedUser" class="li_remove_icon">
                        <span data-bind=" text: Account"></span>
                        (<span data-bind=" text: DisplayName"></span>)
                          <a href="#" data-bind="click: currentModel.removeCheckedUser"></a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</div>
