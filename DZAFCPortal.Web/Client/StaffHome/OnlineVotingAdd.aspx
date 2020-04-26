<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineVotingAdd.aspx.cs" Inherits="DZAFCPortal.Web.Client.StaffHome.OnlineVotingAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        input[type="checkbox"], input[type="checkbox"]:hover {
            height: 13px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
            margin: 0px;
            width: 15px;
            float: left;
            border: 0px;
            background: none;
            -moz-border-radius: 0px;
            -webkit-border-radius: 0px;
            border-radius: 0px;
        }

        .onlineadd_select_list {
            overflow: auto;
            max-height: 570px;
        }

            .onlineadd_select_list li {
                height: auto !important;
                display: block;
                margin-bottom: 10px;
            }

            .onlineadd_select_list input[type="checkbox"] {
                float: left;
                width: 20px;
                margin-top: 3px;
            }

            .onlineadd_select_list label {
                float: right;
                width: 500px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 600px; height: auto">
        <div style="width: 560px;">
            <div class="onlineadd_title">
                <span id="spanTitle" runat="server">评选的标题</span>
            </div>
            <ul class="onlineadd_select_list">
                <li>
                    <div>
                        <span style="display: inline-block; line-height: 20px; color: red;"><asp:Literal ID="litNum" runat="server"></asp:Literal></span>
                        <div class="clear"></div>
                    </div>
                </li>
                <asp:Repeater ID="repOnlineAdd" runat="server">
                    <ItemTemplate>
                        <li>
                            <div>
                                <asp:CheckBox ID="checkOption" runat="server" Text='<%#Eval("Option") %>' />

                                <asp:Label ID="labNum" runat="server" Text='<%#Eval("ReviewsNum") %>' Visible="false"></asp:Label>
                            </div>
                            <div class="clear"></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:Button ID="btnSave" runat="server" Text="提交" CssClass="BEA_Detail_abstract_btn fl" Style="margin: 7px 250px;" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
