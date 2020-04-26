<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleOrgChooseControl2.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.SingleOrgChooseControl2" %>
<!-- Ztree 脚本和样式 -->
<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
<%--<script src="/Scripts/Admin/js/chooseSingleOrg.js"></script>--%>



<div id="<%=ContainerID %>" name="chooseOrgContainer">
    <!-- Start 用于显示选中的项 -->
    <div class="specialPerList">
        <a style="float: left; cursor: pointer; display: inline-block; border-radius: 5px; border: 1px solid #dddddd; background: #f3f3f3; height: 25px; line-height: 25px; padding: 0 10px;" onclick="<%=ContainerID%>.showChooseOrgLayer('<%=LayerContainerID%>');">选择</a>
        <span id="<%=SpanID %>" name="chosenDeptName" style="float: left;"><%=ChosenDeptName %></span>
        <asp:HiddenField ID="hfdchosenDeptName" runat="server" />

         <asp:HiddenField ID="hfdchosenDeptNameIds" runat="server" />
   
        <a class="chosenDeptNamecloae" onclick="return <%=ContainerID%>.clearChosenDeptName(this)"></a>
        <div class="clear"></div>
    </div>
    <!--End 用于显示选中的项 -->

    <div id="<%=LayerContainerID %>" class="div_chooseUserContainer" style="display: none">
        <!-- 左侧组织节点树 -->
        <div id="bookLeft">
            <ul id="<%=ZtreeID %>" class="ztree"></ul>
        </div>
    </div>
</div>


<script type="text/javascript">

    $(function () {
        

        var instance=<%=ContainerID%> ;
        instance.hideOrDisplayCloseBtn();
        var opts = {
            url: '<%=DZAFCPortal.Config.Base.AdminBasePath %>/AjaxPage/getOrganizationHandler.ashx',
            rootID: '<%=OrgRootID%>',
            ztreeID: '<%=ZtreeID%>',
        };
        instance.loadTree(opts)
    })

</script>

<script type="text/javascript">
    var <%=ContainerID%> =  new function(){

        var self=this;
        var showLayer;

        self.clearChosenDeptName=function(trigger){
            $(trigger).siblings("span[name='chosenDeptName']").text('');
            $(trigger).parents("div[name='chooseOrgContainer']").siblings("input[type='hidden']").val('');
            $("#<%=hfdchosenDeptName.ClientID%>").val('');
            //$("#ctl00_ContentPlaceHolder1_dropRecruitPosition").empty();
            self.hideOrDisplayCloseBtn();
        }

        self.hideOrDisplayCloseBtn=function(){
            if($("#<%=ContainerID%> div.specialPerList span#<%=SpanID%> ").text()=='')
                $("#<%=ContainerID%> div.specialPerList a.chosenDeptNamecloae").hide();
            else
                $("#<%=ContainerID%> div.specialPerList a.chosenDeptNamecloae").show();
        }

        //加载组织树
        self.loadTree = function (options) {
            var defaults = {
                url: '/_layouts/15/NyAdmin/AjaxPage/getOrganizationHandler.ashx',
                rootID: '',
                ztreeID: '',
            };

            var opts = $.extend(defaults, options);

            var sets = self.generateZtreeSettings();

            $.ajax({
                type: "get",
                url: opts.url,
                dataType: "json",
                data: {
                    Op: "Tree",
                    RootID: opts.rootID
                },
                success: function (data) {
                    $.fn.zTree.init($("#" + opts.ztreeID), sets, data);
                    var treeObj = $.fn.zTree.getZTreeObj(opts.ztreeID);
                    self.expandZtreeNodeByLevel(treeObj, 1);
                }
            });
        };

        self.showChooseOrgLayer = function () {
            showLayer = $.layer({
                type: 1,
                title: "组织选择",
                maxmin: false,
                area: [400, 600],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [0, true],
                fix: true,
                page: {
                    dom: "#<%=LayerContainerID%>"
                },
                fadeIn: 1000
            });

            return false;
        }

        self.generateZtreeSettings = function () {
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
                        $("#<%=SpanID%>").text(treeNode.Name);
                        $("#<%= hfdchosenDeptName.ClientID%>").val(treeNode.Name);
                        $("#<%= hfdchosenDeptNameIds.ClientID%>").val(treeNode.ID);
                        BindRecruitPosition(treeNode.Name);
                        BindPosition();
                        $('#labchosenDeptName').html('*');
                        self.closeLayer();
                        self.hideOrDisplayCloseBtn();
                    }
                }
            };

            return setting;

        };

        self.expandZtreeNodeByLevel = function (treeObj, expandLevel) {
            for (var currentLevel = 0; currentLevel <= expandLevel; currentLevel++) {
                var treeNoes = treeObj.getNodesByParam("level", currentLevel);
                for (var i = 0; i < treeNoes.length; i++) {
                    treeObj.expandNode(treeNoes[i], true, false, false);
                }
            }
        };

        self.closeLayer = function () {
            layer.close(showLayer);
        }

    }
</script>

<style type="text/css">
    #bookLeft {
        width: 380px;
    }



    .chosenDeptNamecloae {
        background-image:url("/Scripts/Admin/images/removeCurrent_close.png");
        background-repeat:no-repeat;
        background-position-x:0px;
        background-position-y:4px;
        width: 15px;
        cursor: pointer;
        display: inline-block;
        height: 21px;
        line-height: 25px;
        /*float: left;*/
        margin: 1px 0 0 6px;
    }
</style>
