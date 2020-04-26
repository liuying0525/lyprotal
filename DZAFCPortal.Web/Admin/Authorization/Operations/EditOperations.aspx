<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOperations.aspx.cs" MasterPageFile="../../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Operations.EditOperations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Admin/js/KO.validate.js"></script>

    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/Admin/css/smgWeb.css" rel="stylesheet" />
    <!-- 分页脚本 -->
    <script src="/Scripts/Admin/ThirdLibs/jquery.pager_2014_08.js"></script>

    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>

    <script type="text/javascript"> 

        function Save(_processurl, _data, callback) {
            $.ajax({
                async: false,
                type: "get",
                url: _processurl,
                dataType: "json",
                data: { "Source": _data },
                success: callback
            });
        }

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

        function cb(data) {
            if (data.IsSucess) {
                alert(data.Message);
                //currentModel.SelectedType(false);
                Load();
            }
            else
                alert(data.Message);
        }

        function RefreshParent() {
            parent.closeLayerAndRefresh();
        }
    </script>

    <script type="text/javascript">
        var opID;
        currentModel = new AppViewModel();
        moduleID = getQueryString("ID");
        //空操作
        EmptyOperation = function () {
            this.ID = ""
            this.Name = "",
            this.IsChecked = false,
            this.ModuleID = moduleID,
            this.Code = "",
            this.OrderNum = "",
            this.IsEnable = false,
            this.IsDelete = false
        };

        Load = function () {
            $.ajax(
               {
                   async: false,
                   type: "get",
                   url: '../AjaxPage/OperationHandler.ashx',
                   dataType: "json",
                   data: {
                       Op: "GetEdit",
                       ModuleID: moduleID
                   },
                   success: function (data) {
                       var json = eval("(" + data.Datas + ")");
                       currentModel.ChosenOperations(json.OperationLst);

                       //Clear
                       currentModel.RollbackOperations.removeAll();
                       currentModel.RemovedOperations.removeAll();


                       json.OperationLst.forEach(function (item) {
                           var model = {
                               ID: item.ID,
                               Name: item.Name,
                               IsChecked: item.IsChecked,
                               ModuleID: item.ModuleID,
                               Code: item.Code,
                               OrderNum: item.OrderNum,
                               IsEnable: item.IsEnable,
                               IsDelete: item.IsDelete
                           }
                           currentModel.RollbackOperations.push(model);
                       });
                   }
               });
        }

           $(function () {
               //初始化选中的用户数据
               Load()
               ko.applyBindings(currentModel);
           });
           // Model
           function AppViewModel() {
               var self = this;

               /*************************************** 属性定义 ********************************************/
               //self.CurrentModuleID = ko.observable(moduleID);

               //当前操作
               self.CurrentOperation = ko.observable();

               //已选操作
               self.ChosenOperations = ko.observableArray([]);

               //待复原操作
               self.RollbackOperations = ko.observableArray([]);

               //删除的Operaions
               self.RemovedOperations = ko.observableArray([]);

               //URL 信息
               self.URLInfo = ko.observableArray([]);

               self.SelectedType = ko.observable("");

               /*************************************** 方法定义 ********************************************/
               //新增一个空操作 待编辑
               self.InsertOp = function () {
                   self.SelectedType("LoadBase");

                   var empty = new EmptyOperation();
                   self.CurrentOperation(empty);
               }

               //移除所有选中的Operation
               self.BatchRemoveOp = function () {
                   //Operation to del
                   var OpSource = self.ChosenOperations()
                   for (var i = OpSource.length - 1; i >= 0; i--) {
                       var temp = OpSource[i];
                       if (temp.IsChecked) {
                           self.ChosenOperations.remove(temp);
                           self.RemovedOperations.push(temp);
                       }
                   }
               }

               //恢复Operation
               self.RecoveryOperation = function () {
                   self.ChosenOperations.removeAll();

                   self.RollbackOperations().forEach(function (val) {
                       val.IsChecked = false;
                       self.ChosenOperations.push(val);
                   })
               }

               //操作全选
               self.CheckAllOp = function () {
                   self.OpFlag = ko.observable(true);
                   self.ChosenOperations().forEach(function (op) {
                       op.IsChecked = true;
                   })
               }

               //加载当前操作的基本信息
               self.LoadBaseOpInfo = function () {
                   currentModel.SelectedType("LoadBase");

                   if ($.trim(this.ID) == "") {
                       return;
                   }

                   var model = {
                       ID: this.ID,
                       Name: this.Name,
                       ModuleID: this.ModuleID,
                       Code: this.Code,
                       OrderNum: this.OrderNum,
                       IsEnable: this.IsEnable,
                       IsDelete: this.IsDelete
                   };

                   self.CurrentOperation(model);


               }

               //加载当前操作下的URL配置
               /*后期如果修改表结构,operation重用的话,则还需要传一个参数ModuleID*/
               self.Configure = function () {
                   if ($.trim(this.ID) == "") {
                       currentModel.SelectedType("")
                       return;
                   }

                   currentModel.SelectedType("LoadUrl");

                   //赋值当前操作
                   var model = {
                       ID: this.ID,
                       Name: this.Name,
                       ModuleID: this.ModuleID,
                       Code: this.Code,
                       OrderNum: this.OrderNum,
                       IsEnable: this.IsEnable,
                       IsDelete: this.IsDelete
                   };
                   self.CurrentOperation(model);




                   $.ajax({
                       async: false,
                       type: "get",
                       url: '../AjaxPage/URLHandler.ashx',
                       dataType: "json",
                       data: {
                           Op: "LoadURL",
                           OperationID: this.ID
                       },
                       success: function (data) {
                           var json = eval("(" + data.Datas + ")");
                           currentModel.URLInfo(json);
                       }
                   });
               }

               //移除指定单个url
               self.RemoveSingleUrl = function () {

                   currentModel.URLInfo.remove(this);
               }

               //移除所有选中的URL
               self.BatchRemoveURL = function () {
                   //URL to del
                   var URLSource = self.URLInfo();
                   for (var i = URLSource.length - 1; i >= 0; i--) {
                       var temp = URLSource[i];
                       if (temp.IsChecked) {
                           self.URLInfo.remove(temp);
                       }
                   }
               }

               //插入新URL
               self.InsertUrl = function () {
                   var id = newGuid();
                   var model = {
                       ID: id,
                       UrlPath: "",
                       IsChecked: true,
                       OperationID: self.CurrentOperation().ID
                   };

                   self.URLInfo.push(model);
               }

               //URL全选
               self.CheckAllUrl = function () {
                   //URL to del
                   self.URLInfo().forEach(function (url) {
                       url.IsChecked = true;
                   })

               }

               //保存URL
               self.SaveURL = function () {
                   var source = ko.utils.stringifyJson(self.URLInfo);
                   Save('../AjaxPage/URLHandler.ashx?Op=SaveURL&OpID=' + self.CurrentOperation().ID, source, cb);
               }


               //保存操作基本信息
               self.SaveOp = function () {
                   var source = ko.utils.stringifyJson(self.CurrentOperation());
                   Save('../AjaxPage/OperationHandler.ashx?Op=SaveOperation&ModuleID=' + moduleID,
                         source,
                         function (data) {
                             cb(data);
                             var empty = new EmptyOperation();
                             self.CurrentOperation(empty);
                             self.SelectedType("LoadBase");
                         });
               }

               //删除操作
               self.RemovedOp = function () {
                   var source = ko.utils.stringifyJson(self.RemovedOperations());
                   Save('../AjaxPage//OperationHandler.ashx?Op=RemoveOperation',
                         source,
                         function (data) {
                             cb(data);
                             var empty = new EmptyOperation();
                             self.CurrentOperation(empty);
                             self.SelectedType("");
                         });
               }

               self.t = function (form) {
                   alert("Json URL: " + ko.utils.stringifyJson(self.URLInfo));
                   alert("Json Ops: " + ko.utils.stringifyJson(self.ChosenOperations));
               }

           }

    </script>

    <style type="text/css">
        .pitchOn {
            background-color: darkgrey;
        }

        ul {
            padding-left: 0px;
        }

        li {
            padding-bottom: 7px;
        }

        ._span_block {
            display: block;
            padding-bottom: 5px;
        }

        .iconFR {
            float: right;
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 style="text-align: center">操作编辑</h3>
    <div class="container">
        <div class="heigth_20">
        </div>
        <table class="table table-bordered">
            <thead>
                <tr style="max-height: 50px">
                    <%--<th style="width: 12%">基础操作</th>--%>
                    <th style="width: 30%">已选操作
                            <span data-bind="click: currentModel.InsertOp" class="glyphicon glyphicon-plus iconFR" style="cursor: pointer;" id="addOperation" title="新增"></span>
                        <span data-bind="click: currentModel.BatchRemoveOp" class="glyphicon glyphicon-trash iconFR" style="cursor: pointer" id="DelChosen" title="移除选中"></span>
                        <span data-bind="click: currentModel.RemovedOp" class="glyphicon glyphicon-floppy-disk iconFR" style="cursor: pointer" title="保存已选操作"></span>
                    </th>
                    <th style="width: 35%" data-bind="visible: SelectedType() == 'LoadBase'">基本信息管理</th>
                    <th style="width: 35%" data-bind="visible: SelectedType() == 'LoadUrl'">URL配置
                            <span data-bind="click: currentModel.InsertUrl" class="glyphicon glyphicon-plus iconFR" style="cursor: pointer;" id="addUrl" title="新增URL"></span>
                        <span data-bind="click: currentModel.BatchRemoveURL" class="glyphicon glyphicon-trash iconFR" style="cursor: pointer" id="delUrls" title="移除选中"></span>
                        <span data-bind="click: currentModel.SaveURL" class="glyphicon glyphicon-floppy-disk iconFR" style="cursor: pointer" title="保存当前配置"></span>
                    </th>
                </tr>
            </thead>

            <tr style="min-height: 360px">
                <%--已选操作--%>
                <td>
                    <ul data-bind="visible: currentModel.ChosenOperations().length > 0, foreach: currentModel.ChosenOperations" style="list-style-type: none" id="ul_chosenOp">
                        <li name="li_chosenOp">
                            <input type="checkbox" data-bind=" checked: IsChecked, enable: !IsDelete " />
                            <span data-bind="text: Name" style="width: 140px"></span>
                            <span title="配置URL" class="glyphicon glyphicon-book" data-bind="click: currentModel.Configure" style="cursor: pointer; padding-left: 20px"></span>
                            <span title="查看基本信息" class="glyphicon glyphicon-pencil" data-bind="click: currentModel.LoadBaseOpInfo" style="cursor: pointer; padding-left: 10px"></span>
                        </li>
                    </ul>
                    <span class="glyphicon glyphicon-repeat " style="cursor: pointer; float: right" data-bind="click: currentModel.RemovedOp" title="恢复初始"></span>
                </td>

                <%--基本信息--%>
                <td data-bind="visible: SelectedType() == 'LoadBase'">
                    <div style="vertical-align: top">
                        <ul data-bind=" with: CurrentOperation" style="list-style-type: none" id="ul1">
                            <li style="float: left;">
                                <span class="_span_block">名称:<input data-bind="value: Name" style="width: 200px;" /></span>
                                <span class="_span_block">编码:<input data-bind="value: Code" style="width: 200px;" /></span>
                                <span class="_span_block">排序号:<input data-bind="value: OrderNum" style="width: 200px;" /></span>
                                <span class="_span_block">是否启用:<input type="checkbox" data-bind="checked: IsEnable" /></span>
                                <span class="_span_block">允许删除:<input type="checkbox" data-bind="checked: IsDelete" /></span>
                            </li>
                        </ul>

                    </div>
                    <hr />
                    <button style="cursor: pointer" data-bind="click: currentModel.SaveOp">&nbsp;保存</button>
                </td>

                <%--URL配置--%>
                <td data-bind="visible: SelectedType() == 'LoadUrl'">

                    <ul data-bind="visible: currentModel.URLInfo().length > 0, foreach: currentModel.URLInfo" style="list-style-type: none" id="ul_urlConfig">
                        <li style="float: left;">
                            <input type="checkbox" data-bind="checked: IsChecked" />
                            <input data-bind="value: UrlPath" style="width: 400px;" />&nbsp; 
                                <%--<span class='glyphicon glyphicon-ok' style='cursor: pointer; line-height: 26px' data-bind='visible: $data.UrlPath == "", click: currentModel.t.bind($data, this)'></span>--%>
                            <span class="glyphicon glyphicon-remove" style="cursor: pointer; line-height: 26px" data-bind="click: currentModel.RemoveSingleUrl"></span>
                        </li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
