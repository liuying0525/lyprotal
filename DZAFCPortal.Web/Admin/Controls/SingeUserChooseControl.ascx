<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingeUserChooseControl.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.SingeUserChooseControl" %>

<!-- Create By 唐万祯 单个用户选择控件 -->
<!-- Ztree 脚本和样式 -->
<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
<!-- Ztree End -- >
<!-- 分页脚本 -->
<script src="/Scripts/Admin/ThirdLibs/jquery.pager_2014_08.js"></script>

<div id="<%=ContainerID %>" name="chooseSingleContainer">
    <!-- Start 用于显示选中的项 -->
    <div class="specialPerList">
        <a style="cursor: pointer; display: inline-block; border-radius: 5px; border: 1px solid #dddddd; background: #f3f3f3; height: 25px; line-height: 25px; padding: 0 10px;" data-bind="click:showChooseUserLayer">选择
        </a>
        <span data-bind=" text:sureCheckedUser.DisplayName,attr:{title:sureCheckedUser.Account,name:sureCheckedUser.DisplayName}" name="chosenManager"></span>
        <a class="chosenManager" data-bind="visible:sureCheckedUser.Account()!='',click:clearChosenManager"></a>
    </div>
    <!--End 用于显示选中的项 -->

    <div id="<%=LayerContainerID %>" class="div_chooseUserContainer" style="display: none">
        <!-- 当前已选中的用户ID，多项以 ,隔开 -->
        <asp:HiddenField ID="hfdCheckedUserIds" runat="server" />
        <asp:HiddenField ID="hfdCheckedUserNames" runat="server" />
        <!-- 左侧组织节点树 -->
        <div id="bookLeft">
            <ul id="<%=ZtreeID %>" class="ztree"></ul>
        </div>

        <!-- 右侧，用户选择容器 -->
        <div id="bookRight" style="width: 580px; padding-right: 5px;">
            <div style="padding-top: 10px;">
                已选用户：
                <span data-bind=" text:checkedUser.DisplayName,attr:{title:checkedUser.Account}" name="checkedUser"></span>
                <span id="errorMsg" style="color: red"></span>
                <%--<a style="cursor: pointer; margin-left: 15px;" data-bind=" click:$root.clearCheckedUser,visible:checkedUser.Account()!=''">清空选择</a>--%>
                <a style="margin-top: -10px; float: right;" class="add_save" data-bind=" click: $root.sureChecked">确认选择</a>
                <div class="clear"></div>
                <hr />
            </div>

            <!--Start 员工搜索 -->
            <div class="search">
                员工搜索：
                <input type="text" class="txtKey form-control txt_width_md" onkeypress="return searchClickEvent();" />
                <img src="/Scripts/Admin/images/search.png" style="width: 30px; height: 30px; margin-top: -9px; cursor: pointer" data-bind="click:$root.search" />
                <span class="errorMsg" style="color: red"></span>
            </div>
            <!-- End 员工搜索 -->

            <div class="bookAddress_div">
                <ul data-bind="visible: users().length > 0, foreach: users" class="bookAddress">
                    <li style="background-color: #fcfcfc; text-align: center; padding-left: 0px;" data-bind=" click: $root.checkUser,style: { color: $data.ID == $root.checkedUser.ID() ? 'red' : 'black' }">
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
        var hfdCheckedUserNames = $("#<%= hfdCheckedUserNames.ClientID%>");
        //搜索文本框
        var txtKey= $("#<%= ContainerID%>  .txtKey");
        //组织叔
        var orgTree=$("#<%=ZtreeID %>");
        var errorMsg = $("#<%= ContainerID%>  .errorMsg");
        // 容器中的控件 End

        //用户每一页的数据量
        var currentOrgId="";
        var pagesize = 30;

        //列表中待选用户
        self.users = ko.observableArray([]);
        
        //当前选中的用户
        self.checkedUser ={
            ID: ko.observable(""),
            Account: ko.observable(""),
            DisplayName: ko.observable(""),
            OrgId: ko.observable("")
        };

        //确认已经选中的用户，初始化时或点击确定按钮时赋值
        self.sureCheckedUser ={
            ID: ko.observable(""),
            Account: ko.observable(""),
            DisplayName: ko.observable(""),
            OrgId: ko.observable("")
        };

        //用户操作的方法 Start
        //初始化时，加载已经选中的用户数据
        self.loadSureCheckedUser = function () {
            if ($(hfdCheckedUserIds).val() != "") {
                $.ajax({
                    async: false,
                    type: "get",
                    url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
                    dataType: "json",
                    data: {
                        Op: "Checked",
                        checkedUsers: $(hfdCheckedUserIds).val()
                    },
                    success: function (data) {
                        if(data.length>0)
                        {
                            var json=data[0];

                            self.checkedUser.ID(json.ID);
                            self.checkedUser.Account(json.Account);
                            self.checkedUser.DisplayName(json.DisplayName);
                            self.checkedUser.OrgId(json.OrgId);

                            self.sureCheckedUser.ID(json.ID);
                            self.sureCheckedUser.Account(json.Account);
                            self.sureCheckedUser.DisplayName(json.DisplayName);
                            self.sureCheckedUser.OrgId(json.OrgId);
                        }
                    }
                });
            }
            else {
                self.clearCheckedUser();

                clearSureCheckedUser();
            }
        }

        self.clearCheckedUser = function() {
            self.checkedUser.ID("");
            self.checkedUser.Account("");
            self.checkedUser.DisplayName("");
            self.checkedUser.OrgId("");
        }

        function clearSureCheckedUser() {
            self.sureCheckedUser.ID("");
            self.sureCheckedUser.Account("");
            self.sureCheckedUser.DisplayName("");
            self.sureCheckedUser.OrgId("");
        }

        self.clearChosenManager=function(){
            //$(trigger).siblings("span").text('').attr('title','');
            self.sureCheckedUser.ID("");
            self.sureCheckedUser.Account("");
            self.sureCheckedUser.DisplayName("");
            self.sureCheckedUser.OrgId("");
            //$("#<%=ContainerID%> div.specialPerList a.chosenManager").hide();
        }

       
      

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

        //选中用户操作
        self.checkUser = function () {
            if(self.checkedUser.ID()==this.ID)
            {
                self.clearCheckedUser();
            }
            else
            {
                self.checkedUser.ID(this.ID);
                self.checkedUser.Account(this.Account);
                self.checkedUser.DisplayName(this.DisplayName);
                self.checkedUser.OrgId(this.OrgId);
            }
        }

        //点击确认按钮
        self.sureChecked = function () {   
            var isEmptyChoose;
            if (self.checkedUser.Account() == '') {
                isEmptyChoose = true;
                if(!confirm("没有选中任何组合或用户，确认提交？"))
                {
                    return;
                }
            }
            else
                $(errorMsg).html("");

            self.sureCheckedUser.ID(self.checkedUser.ID());
            self.sureCheckedUser.Account(self.checkedUser.Account());
            self.sureCheckedUser.DisplayName(self.checkedUser.DisplayName());
            self.sureCheckedUser.OrgId(self.checkedUser.OrgId());

            var userIds =self.getCheckedUserAttr("ID");
            var displayName = self.getCheckedUserAttr("DisplayName");
            $(hfdCheckedUserIds).val(userIds);
            $(hfdCheckedUserNames).val(displayName);

            self.clearCheckedUser();
            self.users([]);
            $(txtKey).val("");
            if(!isEmptyChoose){
                $("#<%=ContainerID%> div.specialPerList a.chosenManager").show();
            }
          
            layer.close(showLayer);
        }

        //获取已选中的用户指定的属性
        //parms: attrName：指定ID、Name 等，只能填写一个
        //returns: 返回选中用户指定的属性值字符串，选中多个用户，属性值以,隔开
        self.getCheckedUserAttr =function (attrName) {
            var value = "";
            for(attr in self.checkedUser)
            {
                if(attr.toString()==attrName)
                {
                    var item=self.checkedUser[attr];
                    var _tempValue="";
                    if(typeof(item)=="function"){
                        _tempValue=item();
                    }
                    else _tempValue=item;

                    values=_tempValue;
                    break;
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
        self.showChooseUserLayer= function(){
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
            var n = $("#<%=ContainerID%> div.specialPerList span").text();
            var a = $("#<%=ContainerID%> div.specialPerList span").attr('title');
            
            
            $("#<%=LayerContainerID%> span[name='checkedUser']:first").text(n).attr('title',a);
            self.checkedUser.Account(a);
            self.checkedUser.DisplayName(n);
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
        var app=<%=ContainerID%> ;
       
        app.LoadZtree();

        //初始化加载已选的用户
        app.loadSureCheckedUser();

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

<style type="text/css">
    .chosenManager {
        background-image: url("/Scripts/Admin/images/removeCurrent_close.png");
        background-repeat: no-repeat;
        background-position-x: 0px;
        background-position-y: 4px;
        /*background: url("/Scripts/Admin/images/removeCurrent_close.png")no-repeat 0px 4px;*/
        width: 15px;
        cursor: pointer;
        display: inline-block;
        height: 21px;
        line-height: 25px;
        /*float: left;*/
        margin: 1px 0 0 6px;
    }
</style>
