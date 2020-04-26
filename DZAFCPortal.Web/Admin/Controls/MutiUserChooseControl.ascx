<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MutiUserChooseControl.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.MutiUserChooseControl" %>
<!-- Create By 唐万祯 多用户选择控件 -->
<!-- Ztree 脚本和样式 -->
<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
<!-- Ztree End -- >
<!-- 分页脚本 -->
<script src="/Scripts/Admin/ThirdLibs/jquery.pager_2014_08.js"></script>
<script src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
<div id="<%=ContainerID %>">

    <a style="cursor: pointer; display: inline-block; border-radius: 5px; border: 1px solid #dddddd; background: #f3f3f3; height: 25px; line-height: 25px; padding: 0 10px;" data-bind="click:showChooseUserLayer">选择</a>
    <!-- Start 用于显示选中的列表 -->
    <div class="specialPerList">
        <div style="width: 100%;">
            <ul data-bind="visible: sureCheckedUsers().length > 0, foreach: sureCheckedUsers" style="padding-left: 30px; white-space: nowrap; -ms-text-overflow: clip; -o-text-overflow: clip; text-overflow: clip; overflow: hidden;">
                <li data-bind="attr: { Title: Account }" style="border-radius: 5px;">
                    <span data-bind=" text: DisplayName"></span>
                </li>
            </ul>
        </div>
    </div>
    <!--End 用于显示选中的列表 -->

    <div id="<%=LayerContainerID %>" class="div_chooseUserContainer" style="display: none">
        <!-- 当前已选中的用户ID，多项以 ,隔开 -->
        <asp:HiddenField ID="hfdCheckedUserIds" runat="server" />
        <!-- 左侧组织节点树 -->
        <div id="bookLeft">
            <ul id="<%=ZtreeID %>" class="ztree"></ul>
        </div>

        <!-- 右侧，用户选择容器 -->
        <div id="bookRight" style="width: 580px; padding-right: 5px;">
            <div class="search">
                员工搜索：
                    <input type="text" class="txtKey form-control txt_width_md" onkeypress="return searchClickEvent();" />
                <img src="/Scripts/Admin/images/search.png" style="width: 30px; height: 30px; cursor: pointer" data-bind="click:$root.search" />
                <span class="errorMsg" style="color: red"></span>

                <a style="margin-top: 0px; float: right;" class="add_save" data-bind=" click: $root.sureChecked">确认选择</a>
                <div class="clear"></div>
            </div>
            <div class="bookAddress_div">
                <ul data-bind="visible: users().length > 0, foreach: users" class="bookAddress">
                    <li data-bind=" click: $root.checkUser">
                        <div data-bind=" attr: { Title: Account }">
                            <span data-bind=" text: DisplayName"></span>
                        </div>
                    </li>
                </ul>

                <div style="height: 40px; text-align: center; padding-top: 10px;" data-bind="visible: users().length <= 0">
                    <span id="span_None_Data"></span>
                </div>
                <div id="pager1" class=" jqpager jqpagerUser" style="padding-top: 1px; margin-top: 5px; margin-left: -1px;" data-bind="visible: users().length > 0"></div>
            </div>
            <div class="clear"></div>

            <div style="height: 266px; overflow-y: auto; margin-top: 5px;">
                <div id="div_checkedUser" class="fl">
                    <strong>已选用户：</strong>
                    <ul data-bind="visible: checkedUsers().length > 0, foreach: checkedUsers" class="div_checkedOrg_ul">
                        <li data-bind=" click: $root.removeCheckedUser, attr: { Title: Account }" class="li_remove_icon">
                            <span data-bind=" text: DisplayName"></span>
                            <a href="#" data-bind="click: $root.removeCheckedUser"></a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>


<script type="text/javascript">
    //定义模型
    var <%=ContainerID%> = new function () {
        var self = this;
        // 容器中的控件 Start
        //存储确认选中的用户Id，多项以,隔开
        var hfdCheckedUserIds = $("#<%= hfdCheckedUserIds.ClientID%>");
        //搜索文本框
        var txtKey= $("#<%= ContainerID%>  .txtKey");
        //组织叔
        var orgTree=$("#<%=ZtreeID %>");
        var errorMsg = $("#<%= ContainerID%>  .errorMsg");
        // 容器中的控件 End

        //当前选中的组织
        var currentOrgId="";
        //用户每一页的数据量
        var pagesize = 30;

        //列表中待选用户
        self.users = ko.observableArray([]);
        //当前选中的用户
        self.checkedUsers = ko.observableArray([]);
        //确认已经选中的用户，初始化时或点击确定按钮时赋值
        self.sureCheckedUsers = ko.observableArray([]);

        //用户操作的方法 Start
        //初始化时，加载已经选中的用户数据
        self.loadSureCheckedUsers = function () {
            if ($(hfdCheckedUserIds).val() != "") {
                $.ajax({
                    cache:false,
                    async: false,
                    type: "get",
                    url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
                    dataType: "json",
                    data: {
                        Op: "Checked",
                        checkedUsers: $(hfdCheckedUserIds).val()
                    },
                    success: function (data) {
                        self.checkedUsers(data);
                        self.sureCheckedUsers(data);
                    }
                });
            }
            else {
                self.checkedUsers([]);
                self.sureCheckedUsers([]);
            }
        }

        //分页获取用户的回传方法
        self.loadUserPageCallback = function (currentIndex) {
            $.ajax({
                cache:false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
                data: {
                    Op: "choose",
                    searchKey: $(txtKey).val(),
                    checkedUsers: self.getCheckedUserAttr("ID"),
                    orgId: currentOrgId,
                    pagesize: pagesize,
                    currentIndex: currentIndex
                },
                dataType: "json",
                success: function (data) {
                    //为视图的 users 重新赋值
                    self.users(data.datas);

                    //设置分页
                    $('#<%= ContainerID%>  #pager1').pager({
                        pagenumber: currentIndex,
                        recordcount: data.recordCount,
                        pagesize: pagesize,
                        buttonClickCallback: self.loadUserPageCallback,
                        firsttext: '首页',
                        prevtext: '前一页',
                        nexttext: '下一页',
                        lasttext: '尾页',
                        recordtext: '共{0}页，{1}条记录',
                        numericButtonCount: 0
                    });

                    $("#<%= ContainerID%>  .span_None_Data").text("没有数据！");
                }
            });
        }

        //选中用户操作
        self.checkUser = function () {
            var model = {
                ID: this.ID,
                Account: this.Account,
                DisplayName: this.DisplayName,
                OrgId: this.OrgId,
                OrgPath: this.OrgPath,
                LyncSip: this.LyncSip
            }

            self.checkedUsers.push(model);
            self.users.remove(this);
        }

        //移除选中用户项操作
        self.removeCheckedUser = function () {
            //var model = {
            //    ID: this.ID,
            //    Account: this.Account,
            //    DisplayName: this.DisplayName,
            //    OrgId: this.OrgId,
            //    OrgPath: this.OrgPath,
            //    LyncSip: this.LyncSip
            //}

            //选中的用户列表移除
            self.checkedUsers.remove(this);
            if (this.OrgId == currentOrgId)
                self.users.push(this);
        }

        //点击确认按钮
        self.sureChecked = function () {          
            if (self.checkedUsers().length <= 0) {
                if(!confirm("没有选中任何组合或用户，确认提交？"))
                {
                    return;
                }
            }
            else $(errorMsg).html("");

            self.sureCheckedUsers(self.checkedUsers());

            var userIds =self.getCheckedUserAttr("ID");
            $(hfdCheckedUserIds).val(userIds);

            layer.close(showLayer);
        }

        //获取已选中的用户指定的属性
        //parms: attrName：指定ID、Name 等，只能填写一个
        //returns: 返回选中用户指定的属性值字符串，选中多个用户，属性值以,隔开
        self.getCheckedUserAttr =function (attrName) {
            var values = "";
            for (var i = 0; i < self.checkedUsers().length; i++) {
                var item=self.checkedUsers()[i];
                for(attr in item)
                {
                    if(attr.toString()==attrName)
                    {
                        var _tempValue="";
                        if(typeof(attr)=="function"){
                            _tempValue=item[attr]();
                        }
                        else _tempValue=item[attr];

                        values+=_tempValue+",";
                        break;
                    }
                }
            }

            return values;
        }

        //用户操作的方法 End

        //组织的系列操作 Start
        var setting = {
            check: {
                enable: false
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
                    var treeObj = $.fn.zTree.getZTreeObj("<%=ZtreeID%>");
                    //展开当前节点
                    treeObj.expandNode(treeNode, true, false, false);
                    $(txtKey).val("");

                    currentOrgId=treeNode.ID;

                    self.loadUserPageCallback(1);
                }
            }
        };

        //组织节点数据
        self.orgs = ko.observableArray([]);
        //加载组织树
        self.LoadZtree=function(){
            $.ajax({
                cache: false,
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getOrganizationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Tree",
                    RootID: '<%= OrgRootID%>'
                },
                success: function (data) {
                    self.orgs(data);

                    $.fn.zTree.init($(orgTree), setting, self.orgs());

                    var treeObj = $.fn.zTree.getZTreeObj("<%=ZtreeID%>");
                    expandZtreeNodeByLevel(treeObj, 1);
                }
            });
        }

        //组织的系列操作 End

        //搜索方法
        self.search=function(){
            if ($(txtKey).val() == "") {
                $(errorMsg).html("请输入关键字！");

                return;
            }
            else {
                currentOrgId="";
                self.loadUserPageCallback(1);

                $(errorMsg).html("");
            }
        }

        var showLayer;
        self.showChooseUserLayer=function(){
            showLayer = $.layer({
                type: 1,
                title: "组织和人员选择",
                maxmin: false,
                area: [810, 600],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [0, true],
                fix: true,
                page: {
                    dom: "#<%=LayerContainerID %>"
                },
                fadeIn: 1000
            });

            return false;
        }
    }

    //回车搜索
    function searchClickEvent () {
        var app=<%=ContainerID%>;

        if (event.keyCode == 13) {
            app.search();
            return false;
        }
    }
    $(function () {
        var app=<%=ContainerID%>;

        app.LoadZtree();

        //初始化加载已选的用户
        app.loadSureCheckedUsers();

        ko.applyBindings( app, $("#<%=ContainerID %>")[0]);
    });

    function expandZtreeNodeByLevel(treeObj, expandLevel) {
        for (var currentLevel = 0; currentLevel <= expandLevel; currentLevel++) {
            var treeNoes = treeObj.getNodesByParam("level", currentLevel);
            for (var i = 0; i < treeNoes.length; i++) {
                treeObj.expandNode(treeNoes[i], true, false, false);
            }
        }
    }
</script>
