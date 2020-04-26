<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../BaseLayout.Master" CodeBehind="ModuleManagement.aspx.cs" Inherits="DZAFCPortal.Web.Admin.Authorization.Modules.ModuleManagement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>

    <!-- Ztree 脚本和样式 -->
    <link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/zTreeStyle/zTreeStyle.css" type="text/css" />
    <%--<link rel="stylesheet" href="/Scripts/Admin/ThirdLibs/zTree_v3/css/demo.css" type="text/css" />--%>
    <!-- 用户自定义的样式 Start-->
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <!-- 用户自定义的样式和脚本引用 End-->

    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.core-3.5.js"></script>
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.excheck-3.5.min.js"></script>
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/zTree_v3/js/jquery.ztree.exedit-3.5.min.js"></script>


    <script type="text/javascript">
        function newGuid() {
            var guid = "{";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid + "}";
        }

        function Save(_url, _data) {
            $.ajax({
                async: false,
                type: "get",
                url: _url,
                dataType: "json",
                data: _data,
                success: function (data) {
                    if (data.IsSucess) {
                        alert("保存成功!");
                    }
                    else
                        alert("保存失败!")

                    location.reload();
                }
            });
        }



        function showCode(str) {
            var code = $("#code");
            code.empty();
            for (var i = 0, l = str.length; i < l; i++) {
                code.append("<li>" + str[i] + "</li>");
            }
        }
    </script>

    <script type="text/javascript">

        var currentModel;
        $(function () {
            //初始化选中的用户数据
            $.ajax({
                async: false,
                type: "get",
                url: '../AjaxPage/ModuleManageHandler.ashx',
                dataType: "json",
                data: {
                    Op: "GetEdit",
                    GetApp: true
                },
                success: function (data) {
                    var json = eval("(" + data.Datas + ")");
                    currentModel = new AppViewModel(json);//ko 初始化
                    ko.applyBindings(currentModel);
                    $.fn.zTree.init($("#treeModuleManage"), setting, currentModel.ModuleGroups());//初始化z-tree

                    //赋值Applications
                    var applications = eval("(" + data.Message + ")");
                    currentModel.AppPlatform = ko.observableArray(applications);

                }
            });

        });

        // Main Model
        function AppViewModel(source) {
            var self = this;

            self.ModuleGroups = ko.observableArray(source);


            self.AppPlatform = ko.observableArray([]);

            //当前Group
            self.CurrentModuleGroup = ko.observable();

            //当前Module
            self.CurrentModule = ko.observable();

            //是否ModuleGroup
            self.IsModuleGroup = ko.observable();

            //当前节点的父级ID
            self.ParentID = ko.observable();

            //保存状态为修改或者新增
            self.isEdit = ko.observable(true);

            //是否显示"新增模块按钮"
            self.isShow = ko.computed(function () {
                return typeof (self.ParentID()) == 'undefined' ? false : true;
            });

            //新增Module Group
            self.AddParent = function () {
                self.IsModuleGroup(true);
                self.isEdit(false);
                var model = {
                    Name: "",
                    ID: newGuid(),
                    Icon: "",
                    OrderNum: "",
                    Summary: "",
                    IsModuleGroupType: true,
                    ModulelLst: ""
                };
                self.CurrentModuleGroup(model);
            }

            //新增Module
            self.AddChild = function () {

                self.IsModuleGroup(false);
                self.isEdit(false);
                var model = {
                    ID: newGuid(),
                    Name: "",
                    OrderNum: "",
                    Url: "",
                    Code: "",
                    IsEnable: true,
                    IsDelete: false,
                    ModuleGroup_ID: self.ParentID(),
                    ApplicationID: "",
                    IsModuleGroupType: false

                };
                self.CurrentModule(model);
            }

            self.SaveParent = function () {
                var saveType;

                var curParent = self.CurrentModuleGroup();
                if (self.isEdit()) {
                    saveType = "Update";
                    self.ModuleGroups().forEach(function (item) {
                        if (curParent.ID == item.ID) {
                            item.Name = curParent.Name;
                            item.Icon = curParent.Icon;
                            item.OrderNum = curParent.OrderNum;
                            item.Summary = curParent.Summary;
                        }
                    });
                }
                else {
                    saveType = "Add";
                    self.ModuleGroups().push(curParent);
                }

                var json = { Source: ko.utils.stringifyJson(curParent) };
                json.SaveType = saveType;
                json.Op = "SaveModuleGroup";

                var url = '../AjaxPage/ModuleManageHandler.ashx';
                Save(url, json);

            }

            self.SaveChild = function () {
                //alert(self.isEdit() ? "编辑状态" : "新增");
                var saveType;

                if (self.isEdit())
                    saveType = "Update";
                else
                    saveType = "Add";

                var json = { Source: ko.utils.stringifyJson(self.CurrentModule()) };
                json.SaveType = saveType;
                json.Op = "SaveModule";

                var url = '../AjaxPage/ModuleManageHandler.ashx';
                Save(url, json);

            }
        }

        // z-tree settings 
        var setting = {
            check: {
                enable: false,
                chkboxType: { "Y": "s", "N": "s" }
            },
            data: {
                key: {
                    name: "Name",
                    children: "ModulelLst"
                },
            },
            edit: {
                enable: true,
                removeTitle: "移除",
                showRemoveBtn: true,
                showRenameBtn: false

            },

            callback: {
                onClick: function (event, treeId, treeNode) {
                    var _ismoduleGroup = treeNode.IsModuleGroupType;
                    currentModel.IsModuleGroup(_ismoduleGroup);//更新状态:当前节点是父节点or子节点
                    currentModel.isEdit(true);//更新状态:当前操作为编辑

                    var pid;

                    if (_ismoduleGroup) {
                        //alert("这是父节点");
                        var model = {
                            ID: treeNode.ID,
                            Name: treeNode.Name,
                            Icon: treeNode.Icon,
                            OrderNum: treeNode.OrderNum,
                            Summary: treeNode.Summary,
                            IsModuleGroupType: treeNode.IsModuleGroupType
                        };
                        currentModel.CurrentModuleGroup(model);
                        pid = treeNode.ID;
                    }
                    else {
                        //alert("这是子节点");
                        var model = {
                            ID: treeNode.ID,
                            Name: treeNode.Name,
                            Url: treeNode.Url,
                            OrderNum: treeNode.OrderNum,
                            Code: treeNode.Code,
                            IsEnable: treeNode.IsEnable,
                            IsDelete: treeNode.IsDelete,
                            ModuleGroup_ID: treeNode.ModuleGroup_ID,
                            ApplicationID: treeNode.ApplicationID,
                            IsModuleGroupType: treeNode.IsModuleGroupType
                        };
                        currentModel.CurrentModule(model);

                        pid = treeNode.getParentNode().ID;
                    }

                    currentModel.ParentID(pid);
                },

                beforeRemove: function (treeId, treeNode) {
                    if (!confirm("确认删除?"))
                        return false;

                    if (!treeNode.IsDelete && typeof (treeNode.IsDelete) != "undefined") {
                        alert("当前记录不可删除,请编辑后重新操作!")
                        return false;
                    }
                },

                onRemove: function (event, treeId, treeNode) {
                    var model = {
                        Op: "Remove",
                        ID: treeNode.ID,
                        IsModuleGroupType: treeNode.IsModuleGroupType
                    };

                    //删除
                    $.ajax({
                        async: false,
                        type: "get",
                        url: '../AjaxPage/ModuleManageHandler.ashx',
                        dataType: "json",
                        data: model,
                        success: function (data) {
                            //var json = $.parseJSON(data.Datas);
                            ////currentModel = new AppViewModel(json);//ko 初始化
                            //ko.applyBindings(currentModel);
                            //$.fn.zTree.init($("#treeModuleManage"), setting, currentModel.ModuleGroups());//初始化z-tree
                            if (data.IsSucess) {
                                alert("success");
                            }
                            else
                                alert(data.Message);

                        }
                    });

                }

            }
        };

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

        .mikuai_span {
            display: inline-block;
            width: 80px;
            margin: 10px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container container_Left" style="max-width: 98%">
        <div class="row" style="margin-bottom: 20px;">
            <div class="bread_nav">
                权限管理 > 模块管理
            
            </div>
        </div>

        <div style="width: 30%; float: left">
            <button data-bind="click: AddParent" class="btn-primary btn">新增模块组</button>
            <button data-bind=" click: AddChild, visible: isShow() " class="btn-primary btn">新增模块</button>
            <ul id="treeModuleManage" class="ztree smg_cms_tree" style="margin-top: 20px;"></ul>
        </div>

        <div style="width: 70%; float: left">
            <table>
                <tr>
                    <td data-bind="visible: currentModel.IsModuleGroup()">

                        <ul data-bind=" with: CurrentModuleGroup" id="ul_mg" style="list-style-type: none; background-color: #f3f3f3; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px; border: 1px solid #ddd; padding: 10px; margin-top: 45px;">
                            <li>
                                <span class="mikuai_span">名称:</span>
                                <input type="text" data-bind="value: Name" />
                            </li>
                            <li>
                                <span class="mikuai_span">排序号:</span>
                                <input type="text" data-bind="value: OrderNum" />
                            </li>
                            <li>
                                <span class="mikuai_span">描述:</span>
                                <input type="text" data-bind="value: Summary" />
                            </li>
                            <li>
                                <button data-bind="click: currentModel.SaveParent" class="btn-primary btn">保存</button>
                            </li>
                        </ul>


                    </td>
                    <td data-bind="visible: !currentModel.IsModuleGroup()">


                        <ul data-bind=" with: CurrentModule" style="list-style-type: none" id="ul_m">
                            <li>
                                <span class="mikuai_span">名称:</span>
                                <input type="text" data-bind="value: Name" />
                            </li>
                            <li>
                                <span class="mikuai_span">排序号:</span>
                                <input type="text" data-bind="value: OrderNum" />
                            </li>

                            <li>
                                <span class="mikuai_span">URL:</span>
                                <input type="text" data-bind="value: Url" style="width: 500px" />
                            </li>
                            <li>
                                <span class="mikuai_span">编号:</span>
                                <input type="text" data-bind="value: Code" />
                            </li>
                            <li>
                                <span class="mikuai_span">是否启用:</span>
                                <input type="checkbox" data-bind="checked: IsEnable" />
                            </li>
                            <li>
                                <span class="mikuai_span">可被删除:</span>
                                <input type="checkbox" data-bind="checked: IsDelete" />
                            </li>
                            <li>
                                <p>
                                    应用平台:
                                    <select data-bind="options: currentModel.AppPlatform, optionsText: 'Name', optionsValue: 'ID', value: ApplicationID, optionsCaption: '请选择...'"></select>
                                </p>
                                <%--   <div data-bind="visible: selectedCountry">
                            <!-- Appears when you select something -->
                            当前已选:<span data-bind="text: currentModel.CurrentApplication() "></span>.
                        </div>--%>
                            </li>
                            <li>
                                <button data-bind="click: currentModel.SaveChild" class="btn btn-default" style="width: 120px; text-align: center; margin: 10px 200px">保存</button></li>
                        </ul>


                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>
