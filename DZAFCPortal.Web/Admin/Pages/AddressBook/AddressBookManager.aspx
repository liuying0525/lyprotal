<%@ Page Title="" Language="C#" MasterPageFile="../../BaseLayout.Master" AutoEventWireup="true" CodeBehind="AddressBookManager.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Pages.AddressBook.AddressBookManager" %>

<%@ Register Src="../../Controls/SingeUserChooseControl.ascx" TagPrefix="uc1" TagName="SingeUserChooseControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />


    <!-- 用户自定义的样式和脚本引用 Start-->
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <script src="/Scripts/Admin/js/common.js"></script>



    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
    <!-- 加载左侧树 -->
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
            edit: {
                enable: false,
                showRemoveBtn: true,
                showRenameBtn: false,
                drag: {
                    isCopy: false,
                    isMove: false
                }
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    currentModel.LoadItem(treeNode.ID);

                },
                beforeRemove: function (event, treeNode) {
                    if (confirm("确认删除当前节点？")) {
                        var rs = true;
                        $.ajax({
                            async: false,
                            type: "get",
                            data: {
                                ID: treeNode.ID,
                                op: "remove"
                            },
                            url: ajxUrl,
                            dataType: "json",
                            success: function (result) {
                                if (!result.IsSucess) {
                                    alert(result.Message);
                                    rs = false;
                                }
                            }
                        });
                        return rs;
                    }
                    else return false;
                }
            }
        };

        //发送ajax请求，获取左侧菜单树数据
        $(function () {
            LoadTree();
        });

        function LoadTree() {
            $.ajax({
                type: "get",
                url: ajxUrl,
                dataType: "json",
                data: {
                    op: "Tree",
                    ChannelType: getQueryString("ChannelType")
                <%-- Op: "Tree",
                    checkedOrgs: $('#<%= hfdCheckedOrgIds.ClientID%>').val()--%>
                },
                success: function (data) {
                    $.fn.zTree.init($("#treeCategory"), setting, data);

                    var treeObj = $.fn.zTree.getZTreeObj("treeCategory");
                    //设置要展开的层级
                    //ztree根节点的层级为 0
                    var expandLevel = 1;

                    expandByLevel(treeObj, expandLevel);
                }
            });
        }

        //展开指定层级的节点
        //parms: treeObj  ztree对象
        //expandLevel： 要展开的层级，根节点的层级为0
        function expandByLevel(treeObj, expandLevel) {
            for (var currentLevel = 0; currentLevel <= expandLevel; currentLevel++) {
                var treeNoes = treeObj.getNodesByParam("level", currentLevel);
                for (var i = 0; i < treeNoes.length; i++) {
                    treeObj.expandNode(treeNoes[i], true, false, false);
                }
            }
        }
    </script>
    <!-- End左侧树 -->

    <!-- KnockOut 绑定系列 -->
    <script type="text/javascript">

        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var currentModel = new AppViewModel();
        var ajxUrl = "ajax/addressBookHandler.ashx?version=" + new Date();

        $(function () {
            <%-- currentModel.Roles = JSON.parse('<%=GetRoles() %>');--%>


            ko.applyBindings(currentModel, $("#OrgInfo")[0]);

            adjustPosition();
        });

        //定义模型
        function OrganizationModel() {
            var self = this;

            self.ID = ko.observable();
            self.ParentID = ko.observable(emptyGuid);
            self.ParentName = ko.observable();
            self.Name = ko.observable();
            self.OrderNum = ko.observable(0);
            self.IsEnable = ko.observable(true);
            self.IsShow = ko.observable(true);
            self.Onclick = ko.observable(false);
            self.Description = ko.observable();
            self.BuinessManagerAccount = ko.observable();
            self.DeptManagerAccount = ko.observable();
            self.DeputyDirectorAccount = ko.observable();
            self.A3DeptCode = ko.observable();
            self.ExtendDepartment = ko.observable();
        }

        function AppViewModel() {
            var self = this;
            self.orgItem = new OrganizationModel();


            //加载数据
            self.LoadItem = function (id) {
                $.ajax({
                    type: "get",
                    url: ajxUrl,
                    dataType: "Json",
                    cache: false,
                    data: { op: "Checked", checkedOrgs: id },
                    success: function (data) {
                        self.orgItem.ID(data[0].ID);
                        self.orgItem.ParentID(data[0].ParentID);
                        self.orgItem.ParentName(data[0].Name);
                        self.orgItem.IsEnable(data[0].IsEnable.toString());
                        self.orgItem.OrderNum(data[0].OrderNum);
                        self.orgItem.IsShow(data[0].IsShow.toString());
                        self.orgItem.Onclick(true);
                        self.orgItem.Description(data[0].Description);
                        self.orgItem.A3DeptCode(data[0].A3DeptCode);
                        self.orgItem.ExtendDepartment(data[0].ExtendDepartment);

                        setText("deputyManager", data[0].DeputyManagerName);
                        setText("businessManager", data[0].BusinessManagerName);
                        setText("deptManager", data[0].DeptManagerName);

                        setPropertyValue("deputyManager", "title", data[0].DeputyManagerAccount);
                        setPropertyValue("businessManager", "title", data[0].BusinessManagerAccount);
                        setPropertyValue("deptManager", "title", data[0].DeptManagerAccount);

                        hideOrDisplayCloseBtn('deputyManager', data[0].DeputyManagerAccount)
                        hideOrDisplayCloseBtn('businessManager', data[0].BusinessManagerAccount)
                        hideOrDisplayCloseBtn('deptManager', data[0].DeptManagerAccount)
                    }
                });
            }

            //提交表单
            self.Save = function () {
                var model = {
                    ID: self.orgItem.ID(),
                    ParentID: self.orgItem.ParentID(),
                    OrderNum: self.orgItem.OrderNum(),
                    IsShow: self.orgItem.IsShow(),
                    Description: self.orgItem.Description(),
                    BuinessManagerAccount: getPropertyValue("businessManager", "title"),
                    DeptManagerAccount: getPropertyValue("deptManager", "title"),
                    DeputyDirectorAccount: getPropertyValue("deputyManager", "title"),
                    A3DeptCode: self.orgItem.A3DeptCode(),
                    ExtendDepartment: self.orgItem.ExtendDepartment(),
                }
                var modelString = JSON.stringify(model);
                $.getJSON(ajxUrl, { op: "Save", organizationItem: modelString }, function (result) {
                    alert(result.Message);
                    //保存成功，重新加载左侧树
                    if (result.IsSucess) {

                        //清空当前选中的父类
                        self.ParentID = emptyGuid;
                        LoadTree();
                    }
                });
            }

            //提交表单
            self.Synchronization = function () {
                if (confirm("是否启用同步？")) {
                    var model = {
                        ID: self.orgItem.ID(),
                        ParentID: self.orgItem.ParentID(),
                        OrderNum: self.orgItem.OrderNum(),
                        IsShow: self.orgItem.IsShow()
                    }
                    var modelString = JSON.stringify(model);
                    $.getJSON(ajxUrl, { op: "Synchronization", organizationItem: modelString }, function (result) {
                        alert(result.Message);
                        //保存成功，重新加载左侧树
                        if (result.IsSucess) {

                            //清空当前选中的父类
                            self.ParentID = emptyGuid;
                            LoadTree();
                        }
                    });
                }
            }

            //部门跳转查询
            self.DepartmentSearch = function () {
                window.location.href = "<%= DZAFCPortal.Config.Base.AdminBasePath %>/Authorization/Users/UserList.aspx?Department=" + self.orgItem.ParentName();
            }
        }

        //调整选人控件的位置
        function adjustPosition() {
            $("td[type='leader']").each(function (i, e) {

                var ele = $(e);
                var id = ele.attr("id");

                $("div[name='chooseSingleContainer']").each(function () {
                    if ($(this).attr('id').toString().indexOf(id) >= 0)
                        $(this).appendTo(ele);
                });
            });
        }

        function hideOrDisplayCloseBtn(k, v) {
            if (!v)
                $("#" + k + " div.specialPerList a.chosenManager").hide();
            else
                $("#" + k + " div.specialPerList a.chosenManager").show();
        }

        //根据属性名称及dom id获取指定的值
        function getPropertyValue(k, a) {
            return $("#" + k + " div.specialPerList span:first").attr(a);
        }

        function getText(k) {
            return $("#" + k + " div.specialPerList span:first").text();
        }

        //根据dom id设置指定属性的值
        function setPropertyValue(k, a, v) {
            return $("#" + k + " div.specialPerList span:first").attr(a, v);
        }

        function setText(k, v) {
            return $("#" + k + " div.specialPerList span:first").text(v);
        }
    </script>
    <style>
        .bread_nav {
            padding-bottom: 0px;
            margin: 0px 0 20px 0;
            font-size: 14px;
            height: 40px;
            line-height: 40px;
            padding-left: 20px;
            font-weight: bold;
            border-bottom: 1px solid #808080;
            -moz-box-shadow: 0 0 20px #808080;
            -webkit-box-shadow: 0 0 20px #808080;
            box-shadow: 0 0 20px #808080;
            color: #ffffff;
            background: #075579;
            background-image: -webkit-gradient(linear, left bottom, left top, color-stop(0.32, #007A9C), color-stop(0.66, #075579));
            background-image: -webkit-linear-gradient(#007A9C, #075579);
            background-image: -moz-linear-gradient(top,#007A9C, #075579);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#007A9C', endColorstr='#075579');
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr='#007A9C', endColorstr='#075579')";
            background-image: -ms-linear-gradient(#007A9C, #075579);
            background-image: -o-linear-gradient(#007A9C, #075579);
            background-image: linear-gradient(#007A9C, #075579);
        }

        .center_right {
            width: 74% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container container_Left" style="max-width: 98%">
        <div class="row">
            <div class="bread_nav">
                系统配置 &gt; 组织管理
            </div>
        </div>
        <div class="center_left fl">
            <ul id="treeCategory" class="ztree BEA_tree" style="margin-top: 10px !important;"></ul>
        </div>
        <div class="center_right fr" data-bind=" visible: orgItem.Onclick" id="OrgInfo">
            <span class="BEA_table_title">填写组织信息</span>
            <table class="table table-bordered BEA_table" style="background-color: #f3f3f3; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px; overflow: hidden;">
                <tr>
                    <td>名称：</td>
                    <td>
                        <%--<input type="text" class="form-control" id="Title" data-bind=" value: orgItem.ParentName" />--%>
                        <label data-bind=" text: orgItem.ParentName"></label>
                    </td>
                </tr>
                <tr>
                    <td>排序号：</td>
                    <td>
                        <input type="text" class="form-control" id="OrderNum" data-bind=" value: orgItem.OrderNum" />
                    </td>
                </tr>
                <tr>
                    <td>显示状态：</td>
                    <td>
                        <select data-bind=" value: orgItem.IsShow">
                            <option value="true">显示</option>
                            <option value="false">不显示</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>描述：</td>
                    <td>

                        <input type="text" class="form-control" id="Description" data-bind=" value: orgItem.Description" />
                    </td>
                </tr>
                <tr>
                    <td>A3部门编号：</td>
                    <td>

                        <input type="text" class="form-control" id="A3DeptCode" data-bind=" value: orgItem.A3DeptCode" />
                    </td>
                </tr>
                 <tr>
                    <td>部门映射名称：</td>
                    <td>

                        <input type="text" class="form-control" id="ExtendDepartment" data-bind=" value: orgItem.ExtendDepartment" />
                    </td>
                </tr>
                <tr>
                    <td>分管领导：</td>
                    <td id="deputyManager" type="leader">
                        <%--<input type="hidden" id="deputyManager_placeholder" data-bind=" value: orgItem.Description" />--%>

                    </td>
                </tr>
                <tr>
                    <td>部门经理1：</td>
                    <td id="businessManager" type="leader">
                        <%--<input type="hidden" id="businessManager_placeholder" data-bind=" value: orgItem.Description" />--%>
                    </td>
                </tr>
                <tr>
                    <td>部门经理2：</td>
                    <td id="deptManager" type="leader">
                        <%--<input type="hidden" id="deptManager_placeholder" data-bind=" value: orgItem.Description" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <a class="btn btn-primary" data-bind="click: Save" style="width: 120px; text-align: center; margin: 0 auto">保存</a>
                        <%--<a class="btn btn-primary" data-bind="click: Synchronization" style="width: 120px; text-align: center; margin: 0 auto">同步状态</a>--%>
                        <a class="btn btn-primary" data-bind="click: DepartmentSearch" style="width: 120px; text-align: center; margin: 0 auto" target="iframe_default">部门人员</a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear"></div>
        <uc1:SingeUserChooseControl runat="server" ID="SingeUserChooseControl1" ChosenType="deputyManager" />
        <uc1:SingeUserChooseControl runat="server" ID="SingeUserChooseControl2" ChosenType="businessManager" />
        <uc1:SingeUserChooseControl runat="server" ID="SingeUserChooseControl3" ChosenType="deptManager" />
    </div>
</asp:Content>
