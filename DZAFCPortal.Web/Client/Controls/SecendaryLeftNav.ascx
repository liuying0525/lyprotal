<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SecendaryLeftNav.ascx.cs" Inherits="DZAFCPortal.Web.Client.Controls.SecendaryLeftNav" %>
<script type="text/javascript">
    var CurNavId = getQueryString("CurNavId");
    $(function () {
        $(".ny_left_sub_ul>li[id='" + CurNavId.toLowerCase() + "']").addClass("active").siblings("li").removeClass("active");
    });

    function getQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]); return null;
    }

    function RedirectTo() {

    }
</script>

<ul class="ny_left_sub_ul ny_container_boxshadow">
    <asp:Repeater runat="server" ID="rptCategory">
        <ItemTemplate>
            <li style="cursor: pointer" id='<%#Eval("ID").ToString().ToLower() %>'>
                <%-- <a onclick="location.href='<%#GetSecondaryUrl(Eval("ID").ToString(),Eval("ParentId").ToString()) %>'">--%>
                <a href="<%#GetSecondaryUrl(Eval("ID").ToString(),Eval("ParentId").ToString(),Eval("Url").ToString()) %>" target="<%#((bool)Eval("IsOpenedNewTab"))?"_blank":"_self" %>">
                    <img src="<%#Eval("IconUrl") %>" alt="" />
                    <span><%#Eval("Title") %></span>
                    <span class="ny_left_sub_ul_eng"><%#Eval("EnglishName") %></span>
                </a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
