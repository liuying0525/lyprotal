<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationManagement.aspx.cs" MasterPageFile="../../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Operations.OperationManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- KnockOut 加载用户数据 -->
    <script type="text/javascript" src="/Scripts/Admin/ThirdLibs/knockout-3.2.0.js"></script>
    <!-- 用户或组织选中项操作 -->
    <script type="text/javascript">
        var currentModel = new AppViewModel();

        $(function () {
            //初始化选中的用户数据
            $.ajax({
                async: false,
                type: "get",
                url: '../AjaxPage/OperationHandler.ashx',
                dataType: "json",
                data: {
                    Op: "GetReadonly",
                },
                success: function (data) {
                    var json = eval("(" + data.Datas + ")");
                    currentModel.ModuleGroup(json);
                }
            });
            ko.applyBindings(currentModel);
        });

        // Model
        function AppViewModel() {
            var self = this;
            //列表中待选用户
            self.ModuleGroup = ko.observableArray([]);


            self.ModuleGroup.subscribe(function (newValue) {
                //var ids = "";
                //for (var i = 0; i < newValue.length; i++) {
                //    ids += newValue[i].ID + ",";
                //}
            });


            self.EditOperation = function () {
                var id = this.ID;
                showChooseUserLayer(id);
            }
        }

    </script>

    <script type="text/javascript">

        var showLayer;
        function showChooseUserLayer(moduleID) {
            var url = "EditOperations.aspx?ID=" + moduleID;
            showLayer = $.layer({
                type: 2,
                title: "编辑操作",
                maxmin: true,
                area: [800, 600],
                border: [0, 0.3, '#000'],
                shade: [0.6, '#000'],
                shadeClose: true,
                closeBtn: [0, true],
                fix: true,
                iframe: {
                    src: url,
                    scrolling: 'auto'
                },
                fadeIn: 800,
                close: function () { closeLayerAndRefresh() }

            });

            return false;
        }

        function closeLayerAndRefresh() {
            layer.close(showLayer);

            location.reload();
        }
    </script>

    <style type="text/css">
        input[type=button],
        input[type=reset],
        input[type=submit],
        button {
            min-width: 1em;
            padding: 7px 10px;
            /* [ReplaceColor(themeColor:"ButtonBorder")] */ border: 1px solid #ababab;
            /* [ReplaceColor(themeColor:"ButtonBackground",opacity:"1")] */ background-color: #fdfdfd;
            /* [ReplaceColor(themeColor:"ButtonBackground")] */ background-color: #fdfdfd;
            margin-left: 10px;
            /* [ReplaceFont(themeFont:"body")] */ font-family: "Microsoft Yahei UI","Microsoft Yahei","微软雅黑",SimSun,"宋体", sans-serif;
            font-size: 11px;
            /* [ReplaceColor(themeColor:"ButtonText")] */ color: #444;
        }

        input[type=password], input[type=text], input[type=file], textarea, .ms-inputBox {
            width: 210px;
        }

        .form-horizontal .control-group > .controls > p {
            height: 18px;
            margin-top: 5px;
        }

        .form-horizontal .control-group {
            margin-bottom: 5px;
        }

            .form-horizontal .control-group > .controls > .help-block > label.error {
                color: Red;
            }

        .chkItem {
            margin-left: 20px;
        }

            .chkItem label {
                display: inherit;
                margin-left: 4px;
                color: blue;
            }

            .chkItem input[type="checkbox"] {
                margin: 0;
                margin-bottom: 4px;
                margin-left: 15px;
            }

            .chkItem td {
                border: 0;
                padding: 0;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container container_Left" style="max-width: 98%">
        <div class="row" style="margin-bottom:20px;">
            <div class="bread_nav">
                <asp:Literal runat="server" ID="LiteralSiteMap">
                </asp:Literal>
            </div>
        </div>
        <table class="table table-bordered">
            <tbody data-bind="visible: currentModel.ModuleGroup().length > 0, foreach: currentModel.ModuleGroup">
                <tr>
                    <td>
                        <span data-bind="text: Name"></span>
                        <%--<span class="glyphicon-pencil" style="cursor: pointer"></span>--%>
                    </td>
                    <td>
                        <table class="table table-bordered">
                            <tbody data-bind="visible: $data.ModulelLst.length > 0, foreach: $data.ModulelLst">
                                <tr>
                                    <td style="width: 200px; text-align: right;">
                                        <span data-bind="text: Name"></span>
                                        <span class="glyphicon glyphicon-pencil" style="cursor: pointer" data-bind="click: currentModel.EditOperation"></span>
                                    </td>
                                    <td class="chkItem">
                                        <ul data-bind="visible: $data.OperationLst.length > 0, foreach: $data.OperationLst" style="list-style-type: none">
                                            <li style="float: left">
                                                <span data-bind=" text: Name"></span>&nbsp;
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="footer" style="text-align: right">

            <%--                    <input class="btn-large btn btn-default" style="width: 100px" id="btn_Cancle" type="button" value="取消" />
                    <asp:LinkButton ID="btn_Save" Style="width: 100px" class="btn btn-large btn-primary" Text="保存" runat="server" OnClick="btn_Save_Click">
                         <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
                    </asp:LinkButton>--%>
        </div>


    </div>
</asp:Content>
