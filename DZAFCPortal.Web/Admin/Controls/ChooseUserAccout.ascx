<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChooseUserAccout.ascx.cs" Inherits="DZAFCPortal.Web.Admin.Controls.ChooseUserAccout" %>
<link href=" /Scripts/Admin/css/chosen/chosen.css" rel="stylesheet" />
<script src="/Scripts/Admin/ThirdLibs/chosen/chosen.jquery.min.js"></script>
<script src="/Scripts/Admin/js/jquery.linq.min.js"></script>

<script type="text/javascript">
    $().ready(function () {
        $('.chosen-select').chosen({
            allow_single_deselect: true,
            max_selected_options: 1,
            placeholder_text_multiple: " "
        });

        $.ajax({
            type: "POST",
            contentType: "application/json;utf-8",
            url: '<%=DZAFCPortal.Config.Base.ClientBasePath %>/AjaxPage/getUserHandler.ashx',
            data: {},
            dataType: "json",
            success: function (rel) {
                if (rel != null) {
                    allusers = rel;
                    $('#<%=selectManagerName.ClientID%>').on('change', function (evt, params) {
                            var assignEngineerName = $('#<%=hidAssignEngineerName.ClientID%>').val();
                            var assignEngineerAccount = $('#<%=hidAssignEngineerAccount.ClientID%>').val();
                            if (params.selected != undefined) {
                                var applicantaccount = params.selected;
                                var queryapplicant = $.Enumerable.From(allusers).Where(function (x) { return x.Account == applicantaccount }).Select().FirstOrDefault();
                                $('#<%=hidAssignEngineerName.ClientID%>').val(assignEngineerName + ',' + queryapplicant.DisplayName);
                                $('#<%=hidAssignEngineerAccount.ClientID%>').val(assignEngineerAccount + ',' + applicantaccount);
                            } else if (params.deselected != undefined) {
                                var applicantdesaccount = params.deselected;
                                var querydesapplicant = $.Enumerable.From(allusers).Where(function (x) { return x.Account == applicantdesaccount }).Select().FirstOrDefault();
                                $('#<%=hidAssignEngineerName.ClientID%>').val(assignEngineerName.replace("," + querydesapplicant.DisplayName, ""));
                                $('#<%=hidAssignEngineerAccount.ClientID%>').val(assignEngineerAccount.replace( params.deselected, ""));
                            }

                        });
                    }
                },
                // error: function (ex) { alert(rel.d.ErrorMessage); }
            });



        })
        function setSelect(reg_name) {
            var arr = reg_name.split(",");
            $.each(arr, function (i, item) {
                $("#<%=selectManagerName.ClientID%> option").each(function () {
                    if ($(this).val() == item) {
                        //$(this).attr("selected", "selected");
                        $("#<%=selectManagerName.ClientID%> option[value='" + item + "']").attr("selected", "selected");
                    }
                });
            })
        }
</script>

<div id="div_Choose" class="div_Choose_container">
    <select multiple="" class="form-control chosen-select" id="selectManagerName" name="selectManagerName" runat="server" data-placeholder="">
    </select>
    <asp:HiddenField ID="hidAssignEngineerName" runat="server" />
    <asp:HiddenField ID="hidAssignEngineerAccount" runat="server" />
</div>
