<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorize.aspx.cs" MasterPageFile="../BaseLayout.Master" Inherits="DZAFCPortal.Web.Admin.Authorization.Authorize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    <div style="width: 94%; margin: 0 auto;">
        <asp:Panel ID="panel_1" runat="server">
            <h3 style="text-align: center">角色授权</h3>
            <table class="table table-bordered">
                <asp:Repeater ID="rptModuleGroup" runat="server" OnItemDataBound="rptModuleGroup_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td>
                                <table class="table table-bordered">
                                    <asp:Repeater ID="rptModule" runat="server" OnItemDataBound="rptModule_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 160px; text-align: right;"><%#Eval("Name") %>：</td>
                                                <td class="chkItem">
                                                    <asp:CheckBoxList RepeatLayout="Flow" ID="chkList_Item" RepeatDirection="Horizontal" runat="server" CssClass="checkboxList" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="footer" style="text-align: right">

                <input class="btn-large btn btn-default" style="width: 100px" id="btn_Cancle" type="button" value="取消" />
                <asp:LinkButton ID="btn_Save" Style="width: 100px" class="btn btn-large btn-primary" Text="保存" runat="server" OnClick="btn_Save_Click">
                         <span class="glyphicon glyphicon-floppy-disk"> 保存</span>
                </asp:LinkButton>
            </div>
        </asp:Panel>
        <asp:Panel ID="panel_2" runat="server">
            <div style="text-align: center; margin-top: 100px;">
                <h2 style="color: red">
                    <asp:Label ID="lbl_Result" ForeColor="Red" runat="server"></asp:Label></h2>
                <input class="btn-large btn" style="width: 100px" id="btn_Close" type="button" value="离开" />
                <asp:LinkButton ID="Button1" Style="width: 100px" class="btn btn-large" Text="查看权限" Visible="false" runat="server" OnClick="btn_View_Click">
                        <span class="glyphicon glyphicon-list-alt"> 查看权限</span>
                </asp:LinkButton>

            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        $("#btn_Cancle").click(function () {
            window.close();
        });
        $("#btn_Close").click(function () {
            window.close();
        });
    </script>
</asp:Content>
