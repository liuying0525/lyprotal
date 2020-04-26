<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgAndUserChoose.ascx.cs" Inherits="NYPortal.Web.Controls.OrgAndUserChoose" %>

<!-- 弹层 -->
<script src="/Scripts/Admin/ThirdLibs/layer/layer.min.js"></script>
<!-- Ztree 脚本和样式 -->
<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
<script src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
<!-- Ztree End -- >
    
<!-- 分页脚本 -->
<script src="/Scripts/Admin/ThirdLibs/jquery.pager_2014_08.js"></script>

<!-- KnockOut 加载用户数据 -->
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
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
            enable: true,
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
            },
            onCheck: function (event, treeId, treeNode) {
                var model = {
                    ID: treeNode.ID,
                    Name: treeNode.Name,
                    IsChecked: treeNode.IsChecked
                };

                //选中
                IsChecked: treeNode.IsChecked
                if (model.IsChecked) {
                    currentModel.checkedOrgs.push(model);
                }
                else {
                    for (var i = 0; i < currentModel.checkedOrgs().length; i++) {
                        var _temp = currentModel.checkedOrgs()[i];
                        if (_temp.ID == model.ID) {
                            currentModel.checkedOrgs.splice(i, 1);
                            break;
                        }
                    }
                }
            } // onCheck 方法结束
        }
    };

    //发送ajax请求，获取左侧菜单树数据
    $(function () {
        $.ajax({
            type: "get",
            url: '<%=DZAFCPortal.Config.Base.ClientBasePath%>/AjaxPage/getOrganizationHandler.ashx',
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
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Checked",
                    checkedUsers: $('#<%= hfdCheckedUserIds.ClientID%>').val()
                },
                success: function (data) {
                    currentModel.checkedUsers(data);
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

        //组织
        self.orgs = ko.observableArray([]);
        //选中的组织
        self.checkedOrgs = ko.observableArray([]);

        self.checkedUsers.subscribe(function (newValue) {
            var ids = "";
            for (var i = 0; i < newValue.length; i++) {
                ids += newValue[i].ID + ",";
            }

            $('#<%= hfdCheckedUserIds.ClientID%>').val(ids);
        });

        self.checkedOrgs.subscribe(function (newValue) {
            var ids = "";
            for (var i = 0; i < newValue.length; i++) {
                ids += newValue[i].ID + ",";
            }

            $('#<%= hfdCheckedOrgIds.ClientID%>').val(ids);
        });

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
    }

</script>

<!-- 页面弹层 -->
<script type="text/javascript">

    var showLayer;
    function showChooseUserLayer() {
        showLayer = $.layer({
            type: 1,
            title: "组织和人员选择",
            maxmin: true,
            area: [810, 600],
            border: [0, 0.3, '#000'],
            shade: [0.6, '#000'],
            shadeClose: true,
            closeBtn: [0, true],
            fix: true,
            page: {
                dom: "#div_Choose"
            },
            fadeIn: 1000
        });

        return false;
    }
</script>

<style type="text/css">
    .add_save {
        padding: 6px 20px;
        background-color: #fff;
        -webkit-border-radius: 15px;
        -moz-border-radius: 15px;
        -o-border-radius: 15px;
        border-radius: 15px;
        -moz-box-shadow: 0 1px 5px rgba(0,0,0,0.125);
        -webkit-box-shadow: 0 1px 5px rgba(0,0,0,0.125);
        box-shadow: 0 1px 5px rgba(0,0,0,0.125);
        /*text-shadow: 0 -1px 3px rgba(0,0,0,0.125);*/
        border: 1px solid #CECECE;
        color: #009BDF;
        font-weight: bold;
        font-size: 14px;
        -moz-user-select: none; /*火狐*/
        -webkit-user-select: none; /*webkit浏览器*/
        -ms-user-select: none; /*IE10*/
        -khtml-user-select: none; /*早期浏览器*/
        user-select: none;
        text-decoration: none;
        font-family: 'Microsoft YaHei' !important;
    }
</style>

<a class="add_save" onclick="showChooseUserLayer();">选择</a>
<div class="specialPerList">
    <div style="width: 100%">
        <strong>已选组织：</strong>
        <ul data-bind="visible: currentModel.checkedOrgs().length > 0, foreach: currentModel.checkedOrgs" style="padding-left: 30px;">
            <li class="li_remove_icon" data-bind="click: currentModel.removeOrg">
                <span data-bind=" text: Name"></span>
            </li>
        </ul>
    </div>
    <div class="clear"></div>
    <div style="width: 100%;">
        <strong>已选用户：</strong>
        <ul data-bind="visible: currentModel.checkedUsers().length > 0, foreach: currentModel.checkedUsers" style="padding-left: 30px;">
            <li data-bind=" click: currentModel.removeCheckedUser" class="li_remove_icon">
                <span data-bind=" text: Account"></span>
                (<span data-bind=" text: DisplayName"></span>)
            </li>
        </ul>
    </div>
</div>


<div id="div_Choose" class="div_Choose_container" style="display: none">
    <asp:HiddenField ID="hfdCheckedOrgIds" runat="server" />
    <asp:HiddenField ID="hfdCheckedUserIds" runat="server" />
    <div id="bookLeft">
        <ul id="treeOrganization" class="ztree"></ul>
    </div>
    <div id="bookRight">
        <div class="search">
            员工搜索：
                    <input type="text" id="txtKey" class=" form-control txt_width_md" />
            <img src="/Scripts/Admin/images/search.png"
                style="width: 23px; height: 25px; margin-top: -9px; cursor: pointer" onclick="search();" />
            <span id="searchMsg" style="color: red"></span>
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

            <div style="height: 40px;" data-bind="visible: currentModel.users().length <= 0">
                <span>没有数据！</span>
            </div>
            <div id="pager1" class=" jqpager jqpagerUser" style="padding-top: 1px"></div>
        </div>
        <div class="clear"></div>


        <div style="height: 292px; overflow-y: overlay; padding-top: 5px;">
            <div id="div_checkedOrg" class="fl">
                <strong>已选组织：</strong>
                <ul data-bind="visible: currentModel.checkedOrgs().length > 0, foreach: currentModel.checkedOrgs" class="div_checkedOrg_ul">
                    <li class="li_remove_icon" data-bind="click: currentModel.removeOrg">
                        <span data-bind=" text: Name"></span>
                    </li>

                </ul>
            </div>
            <div class="clear"></div>
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
