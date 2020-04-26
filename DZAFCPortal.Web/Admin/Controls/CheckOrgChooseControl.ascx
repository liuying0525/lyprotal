<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckOrgChooseControl.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.CheckOrgChooseControl" %>
<!-- Create By 唐万祯 多组织和用户选择控件 -->
<!-- Ztree 脚本和样式 -->
<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
<!-- Ztree End -- >
<!-- 分页脚本 -->
<script src="/Scripts/Admin/ThirdLibs/jquery.pager_2014_08.js"></script>
<script src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
<style type="text/css">
    .orgContainsChilren, .orgNoneContainsChilren {
        display: block;
        position: absolute;
        top: 5px;
        left: 2px;
        width: 15px;
        height: 15px;
    }

    .orgContainsChilren {
        background: url("/Scripts/Admin/images/smg_orgContainsChilren_icon.png") no-repeat center;
    }

    .orgNoneContainsChilren {
        background: url("/Scripts/Admin/images/smg_orgNoneContainsChilren_icon.png") no-repeat center;
    }
</style>


<div id="<%=ContainerID %>">

    <a style="cursor: pointer; display: inline-block; border-radius: 5px; border: 1px solid #dddddd; background: #f3f3f3; height: 25px; line-height: 25px; padding: 0 10px;" data-bind="click:showChooseUserLayer">选择</a>
    <!-- Start 用于显示选中的列表 -->
    <div class="specialPerList">
        <div style="width: 100%">
            <strong>已选组织：</strong>
            <ul data-bind="visible: sureCheckedOrgs().length > 0, foreach: sureCheckedOrgs" style="padding-left: 30px;">
                <li style="width: 150px; padding-left: 20px;">
                    <span data-bind=" text: Name, attr: { Title: PathName }"></span>
                    
<%--                    <a data-bind="css: { 'orgContainsChilren': IsContainsChildren, 'orgNoneContainsChilren': !IsContainsChildren() }, attr: { Title: IsContainsChildren() ? '包含子部门' : '不包含子部门' }"></a>--%>
                <%-- <a class="remove_icon" data-bind="click: $root.removeOrg" title="移除">
                                <img src="/Scripts/Admin/images/removeCurrent_close.png" alt="" /></a>--%>
                </li>
            </ul>
            <asp:HiddenField ID="hfdchosenDeptName" runat="server" />
        </div>
        <div class="clear"></div>
        <div style="width: 100%;display:none">
            <strong>已选用户：</strong>
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
        <!-- 当前选中的组织ID，多项以,隔开 -->
        <asp:HiddenField ID="hfdCheckedOrgIds" runat="server" />
         <!-- 当前选中的组织名字，多项以,隔开 -->
         <asp:HiddenField ID="hfdCheckedOrgNames" runat="server" />
        <!-- 当前选中的组织Path，多项以,隔开 -->
        <asp:HiddenField ID="hfdCheckedOrgPaths" runat="server" />
        <!-- 左侧组织节点树 -->
        <div id="bookLeft">
            <ul id="<%=ZtreeID %>" class="ztree"></ul>
        </div>

        <!-- 右侧，用户选择容器 -->
        <div id="bookRight" style="width: 550px; padding-right: 5px;">
            <div class="search" style="display:none">
                员工搜索：
                    <input type="text" class="txtKey form-control txt_width_md" />
                <img src="/Scripts/Admin/images/search.png"
                    style="width: 30px; height: 30px; margin-top: -9px; cursor: pointer" data-bind="click:$root.search" />
                <span class="errorMsg" style="color: red"></span>

                <a style="margin-top: 0px; float: right;" class="add_save" data-bind=" click: $root.sureChecked">确认选择</a>
                <div class="clear"></div>
            </div>
            <div class="bookAddress_div" style="display:none" >
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
    
            <!--Start 选人控件中已选列表 -->
            <div style="height: 266px; overflow-y: auto; margin-top: 5px;">
                <div id="div_checkedOrg" class="fl">
                 <%--   <span style="display: block; width: 100%; height: 20px; line-height: 20px; color: #aaaaaa">（
                        <img src="/Scripts/Admin/images/smg_orgContainsChilren_icon.png" alt="" />:当前部门以及所有子部门；
                        <img src="/Scripts/Admin/images/smg_orgNoneContainsChilren_icon.png" alt="" />:仅为当前部门；点击图标切换
                        ）
                    </span>--%>
                    <strong>已选组织：</strong>
                    <ul data-bind="visible: checkedOrgs().length > 0, foreach: checkedOrgs" class="div_checkedOrg_ul">
                        <li style="width: 150px; padding-left: 20px;">
                            <span data-bind=" text: Name, attr: { Title: PathName }"></span>
                            <a data-bind="css: { 'orgContainsChilren': IsContainsChildren, 'orgNoneContainsChilren': !IsContainsChildren() }, attr: { Title: IsContainsChildren() ? '包含子部门' : '不包含子部门' }, click: $root.switchIsContainsChildren"></a>
                            <a class="remove_icon" data-bind="click: $root.removeOrg" title="移除">
                                <img src="/Scripts/Admin/images/removeCurrent_close.png" alt="" /></a>
                        </li>
                    </ul>
                </div>
                 <a style="margin-top: 0px; float: right;" class="add_save" data-bind=" click: $root.sureChecked">确认选择</a>
                <div class="clear"></div>

                <div id="div_checkedUser" class="fl" style="display:none">
                    <strong>已选用户：</strong>
                    <ul data-bind="visible: checkedUsers().length > 0, foreach: checkedUsers" class="div_checkedOrg_ul">
                        <li data-bind=" click: $root.removeCheckedUser, attr: { Title: Account }" class="li_remove_icon">
                            <span data-bind=" text: DisplayName"></span>
                            <a href="#" data-bind="click: $root.removeCheckedUser"></a>
                        </li>
                    </ul>
                </div>
            </div>
            <!--End 选人控件中已选列表 -->
        </div>
    </div>
</div>


<script type="text/javascript">
    //定义模型
    var <%=ContainerID%> =new function () {
        var self = this;
        // 容器中的控件 Start
        //存储确认选中的用户Id，多项以,隔开
        var hfdCheckedUserIds = $("#<%= hfdCheckedUserIds.ClientID%>");
        //存储确认选中的组织Id,多项以,隔开
        var hfdCheckedOrgIds=$("#<%=hfdCheckedOrgIds.ClientID%>");
        //存储确认选中的组织名字,多项以,隔开
        var hfdCheckedOrgNames=$("#<%=hfdCheckedOrgNames.ClientID%>");
        //存储确认的选中的组织Path，多项以,隔开
        var hfdCheckedOrgPaths=$("#<%=hfdCheckedOrgPaths.ClientID%>");

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
                    async: false,
                    type: "get",
                    url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getUserHandler.ashx',
                    dataType: "json",
                    data: {
                        Op: "Checked",
                        checkedUsers: $(hfdCheckedUserIds).val()
                    },
                    success: function (data) {
                        //self.checkedUsers(data);
                        self.sureCheckedUsers(data);
                    }
                });
            }
            else {
                self.checkedUsers([]);
                self.sureCheckedUsers([]);
            }
        }
        //end function loadSureCheckedUsers

        //分页获取用户的回传方法
        self.loadUserPageCallback = function (currentIndex) {
            $.ajax({
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getUserHandler.ashx',
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
        //end function loadUserPageCallback()

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
            //选中的用户列表移除
            self.checkedUsers.remove(this);
            if (this.OrgId == currentOrgId)
                self.users.push(this);
        }


        //点击确认按钮
        self.sureChecked = function () {          
            if (self.checkedOrgs().length<=0&&self.checkedUsers().length <= 0) {
                //$(errorMsg).html("没有选中任何组合或用户");
                if(!confirm("没有选中任何组合或用户，确认提交？"))
                {
                    return;
                }
            }
            else $(errorMsg).html("");

            self.sureCheckedUsers(self.checkedUsers());
            self.sureCheckedOrgs(self.checkedOrgs());

            var userIds =self.getCheckedUserAttr("ID");
            $(hfdCheckedUserIds).val(userIds);

            //#region 获取选中的组织Ids和Paths
            var orgIds = "";
            var orgPaths = "";
            var orgNames = "";
            for (var i = 0; i < self.sureCheckedOrgs().length; i++) {
                var item = self.sureCheckedOrgs()[i];

                orgIds += item.ID + ",";
                orgNames += item.Name + ",";
                if (item.IsContainsChildren()) {
                    orgPaths += item.Path + ",";
                }
            }
            $(hfdCheckedOrgIds).val(orgIds);
            $(hfdCheckedOrgPaths).val(orgPaths);
            $(hfdCheckedOrgNames).val(orgNames);
            $("#<%= hfdchosenDeptName.ClientID%>").val(orgNames);
            //#endregion 

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
                enable: true,
                chkStyle: "checkbox",
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
                    $(txtKey).val("");

                    currentOrgId=treeNode.ID;

                    //self.loadUserPageCallback(1);
                },
                //#region 树形控件选中||取消选择事件
                onCheck: function (event, treeId, treeNode) {
                    var model = {
                        ID: treeNode.ID,
                        Name: treeNode.Name,
                        Path: treeNode.Path,
                        PathName:treeNode.PathName,
                        IsChecked: treeNode.IsChecked,
                        IsContainsChildren: ko.observable(true)
                    }

                    var checkedOrgs=<%=ContainerID%>.checkedOrgs;
                    //选中
                    if (model.IsChecked) {
                        checkedOrgs.push(model);
                    }
                    else {
                        for (var i = 0; i < checkedOrgs().length; i++) {
                            var _temp = checkedOrgs()[i];
                            if (_temp.ID == model.ID) {
                                checkedOrgs.remove(_temp);
                                break;
                            }
                        }
                    }
                } //end function onCheck()
            },
            //End function callback()
        
        };// end settings

        //组织节点数据
        self.orgs = ko.observableArray([]);
        self.checkedOrgs= ko.observableArray([]);
        self.sureCheckedOrgs= ko.observableArray([]);

        //加载组织树
        self.LoadZtree=function(){
            $.ajax({
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getOrganizationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Tree",
                    checkedOrgs: $('#<%= hfdCheckedOrgIds.ClientID%>').val(),
                    checkedOrgPaths: $("#<%=hfdCheckedOrgPaths.ClientID%>").val(),
                    RootID: '<%= OrgRootID%>'
                },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        data[i].IsContainsChildren = ko.observable(data[i].IsContainsChildren);
                    }
                    
                    self.orgs(data);

                    $.fn.zTree.init($(orgTree), setting, self.orgs());

                    var treeObj = $.fn.zTree.getZTreeObj("<%=ZtreeID%>");
                    self.expandZtreeNodeByLevel(treeObj, 1);
                }
            });
        }
        self.expandZtreeNodeByLevel = function (treeObj, expandLevel) {
            for (var currentLevel = 0; currentLevel <= expandLevel; currentLevel++) {
                var treeNoes = treeObj.getNodesByParam("level", currentLevel);
                for (var i = 0; i < treeNoes.length; i++) {
                    treeObj.expandNode(treeNoes[i], true, false, false);
                }
            }
        };
        self.loadSureCheckedOrgs=function(){
            $.ajax({
                type: "get",
                url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getOrganizationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "Checked",
                    checkedOrgs: $('#<%= hfdCheckedOrgIds.ClientID%>').val(),
                    checkedOrgPaths: $("#<%=hfdCheckedOrgPaths.ClientID%>").val(),
                },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        data[i].IsContainsChildren = ko.observable(data[i].IsContainsChildren);
                    }
                    
                    self.checkedOrgs(data);
                    self.sureCheckedOrgs(data);
                }
            });
        }
        
        self.removeOrg = function () {
            self.checkedOrgs.remove(this);

            //设置树节点非选中
            var zTree = $.fn.zTree.getZTreeObj("<%=ZtreeID%>");
            var node = zTree.getNodeByParam("ID", this.ID);
            node.IsChecked = false;
            zTree.updateNode(node); //更新改节点UI
        }

        //切换是否包含子级
        self.switchIsContainsChildren = function () {
            var rs = !this.IsContainsChildren();

            this.IsContainsChildren(rs);
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

        //清空隐藏控件的值
        self.clear=function(){
            $(hfdCheckedOrgPaths).val("");
            $(hfdCheckedOrgIds).val("");
            $(hfdCheckedUserIds).val("");
            $(hfdCheckedOrgNames).val("");
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


    $(function () {
        var app=<%=ContainerID%>;

        app.LoadZtree();

        //初始化加载已选的用户
        app.loadSureCheckedUsers();
        //初始化加载已选的组织
        app.loadSureCheckedOrgs();

        ko.applyBindings( app, $("#<%=ContainerID %>")[0]);
    });

</script>
