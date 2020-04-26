function DocManager() {
    var docSelf = this;

    //局部变量start
    var ajaxUrl = "ajax/DocMgtHandler.ashx";

    //局部变量 End


    //Ztree 加载 Start
    var setting = {
        data: {
            key: {
                name: "DisplayName",
                children: "Items",

            },
            simpleData: {
                enable: true,
                idKey: "RelativePath",
                pIdKey: "ParentRelativePath",
                rootPId: "/"
            }
        },
        callback: {
            onClick: function (event, treeId, treeNode) {
                if (treeNode.IsDirectory) {
                    docSelf.currentModel.LoadItem(treeNode);
                    docSelf.currentModel.ParentNavID =
                        typeof (treeNode.ParentID) == "undefined" ?
                            treeNode.ID : treeNode.ParentID;
                    docSelf.currentModel.CurrentNavID = treeNode.ID;
                    //docSelf.currentModel.IconPath(treeNode.IconUrl);
                    docSelf.currentModel.isEdit(true);

                    docSelf.currentModel.isOnclick(true);

                    var scope = treeNode.ApplyRoles == "" || treeNode == null ? "1" : "0";
                    docSelf.currentModel.visibleScope(scope);

                    //上级节点名称
                    var parentNode = treeNode.getParentNode();
                    var val = parentNode == null ? "无" : parentNode.Name;
                    docSelf.currentModel.ParentNavName(val);
                }
                else {
                    docSelf.currentModel.isEdit(false);

                    docSelf.currentModel.isOnclick(false);
                }
            },

        },
        view: {
            showIcon: false,
            //showTitle: false,
            txtSelectedEnable: true
        },
        //async: {
        //    enable: true,
        //    url: ajaxUrl,
        //    dataType: "json",
        //    autoParam: ["RelativePath"],
        //    otherParam: { "op": "AsyncLoad" },
        //    dataFilter: filter
        //}

    };

    docSelf.LoadTree = function (treeID) {
        $.ajaxSetup({ cache: false });
        $.ajax({
            type: "get",
            url: ajaxUrl,
            dataType: "json",
            data: {
                op: "loadTree"
            },
            success: function (datas) {
                //var jsonData = JSON.parse(datas.Datas);
                $.fn.zTree.init($("#" + treeID), setting, datas);
            }
        });
    };

    function filter(treeId, parentNode, childNodes) {
        if (!childNodes) return null;
        for (var i = 0, l = childNodes.length; i < l; i++) {
            childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
        }
        return childNodes;
    }
    //Ztree 加载End

    //KnockOut 绑定系列
    docSelf.currentModel = new AppViewModel();
    var emptyGuid = "00000000-0000-0000-0000-000000000000";

    //绑定模型
    //params: roles，所有的角色
    docSelf.BindModel = function (roles) {
        docSelf.currentModel.Roles = JSON.parse(roles);

        ko.applyBindings(docSelf.currentModel);
    }

    //定义模型
    function NavigateModel() {
        var self = this;
        var id = newGuid();
        self.ID = ko.observable(id);
        self.ParentNavID = ko.observable();
        self.Name = ko.observable();
        self.DisplayName = ko.observable();
        self.IsDirectory = ko.observable();

        self.RelativePath = ko.observable();
        self.ModifiedTime = ko.observable(0);
        //self.IconUrl = ko.observable();
        self.IsShow = ko.observable(true);
        //self.Description = ko.observable();
        self.ApplyRoles = ko.observableArray([]);
        //self.IsOpenedNewTab = ko.observable(false);
    }

    function AppViewModel() {
        var self = this;

        self.Roles = [];

        self.ParentNavID = "";

        self.ParentNavName = ko.observable("无");

        self.CurrentNavID = "";

        self.navItem = new NavigateModel();

        //保存状态为修改或者新增
        self.isEdit = ko.observable(false);

        self.isOnclick = ko.observable(false);

        self.IconPath = ko.observable();

        self.visibleScope = ko.observable();

        //新增时清空数据
        self.Clear = function () {
            self.isEdit(false);
            self.isOnclick(true);
            self.navItem.ID("");
            self.navItem.ParentNavID(navJs.currentModel.ParentNavID);
            self.navItem.Name("");
            self.navItem.DisplayName("");
            self.navItem.IsDirectory(true);

            self.navItem.RelativePath("");
            self.navItem.ModifiedTime("");
            //self.navItem.Description("");
            //self.navItem.IsShow(true);
            self.navItem.ApplyRoles([]);
            //self.navItem.OrderNum(0);
            //self.navItem.IsOpenedNewTab(false);

            var selecedNode = $.fn.zTree.getZTreeObj("treeNavigator").getSelectedNodes();
            if (selecedNode.length == 0)
                self.ParentNavName("无");
            else
                self.ParentNavName(selecedNode[0].Name);
        }

        //加载数据
        self.LoadItem = function (data) {
            var checkedRoles = [];
            if (data.ApplyRoles != "" && data.ApplyRoles != null) {
                checkedRoles = data.ApplyRoles.split(",");
            }
            self.navItem.ID(data.ID);
            self.navItem.ParentNavID(data.ParentNavID);
            self.navItem.Name(data.Name);
            self.navItem.DisplayName(data.DisplayName);
            self.navItem.IsDirectory(data.IsDirectory);

            self.navItem.RelativePath(data.RelativePath);

            self.navItem.ModifiedTime(data.ModifiedTime);
            //self.navItem.IsShow(data.IsShow);
            //self.navItem.Description(data.Description);
            self.navItem.ApplyRoles(checkedRoles);
            //self.navItem.OrderNum(data.OrderNum);
            //self.navItem.IsOpenedNewTab(data.IsOpenedNewTab);
        }

        //提交表单
        self.Save = function () {

            var model = {
                ID: self.navItem.ID() == "" ? newGuid() : self.navItem.ID(),
                //Type: self.navItem.Type(),
                ParentID: "",
                Name: self.navItem.Name(),
                DisplayName: self.navItem.DisplayName(),
                DeepNumber: 0,

                RelativePath: self.navItem.RelativePath(),
                IsDirectory: self.IsDirectory(),
                //IsShow: self.navItem.IsShow() == "1" ? true : false,
                ModifiedTime: self.navItem.ModifiedTime(),
                //OrderNum: self.navItem.OrderNum(),
                ApplyRoles: self.navItem.ApplyRoles().join(","),
                //IsOpenedNewTab: self.navItem.IsOpenedNewTab() == "1" ? true : false
            }
            //model.ApplyRoles = self.visibleScope() == 1 ? "" : self.visibleScope


            var saveType;
            if (self.isEdit()) {
                saveType = "Update";
                model.ParentID = navJs.currentModel.ParentNavID;
            }
            else {
                saveType = "Add";
                model.ParentID = navJs.currentModel.CurrentNavID == "" ? emptyGuid : navJs.currentModel.CurrentNavID;
            }
            var modelString = JSON.stringify(model);
            $.getJSON(ajaxUrl + "?timespan=" + new Date().getTime(), { op: "save", model: modelString, savetype: saveType }, function (result) {
                alert(result.Message);
                //保存成功，重新加载左侧树
                if (result.IsSucess) {
                    self.isOnclick(false);
                    docSelf.LoadTree("treeNavigator");
                }
            });
        }

        self.visibleChanged = function () {
            if (self.visibleScope() == "1")
                self.navItem.ApplyRoles([]);
        }
    }
    // Knockout End
}