///显示SSS设置层
function ShowSSSSetLayer() {

    $.layer({
        type: 1, //0-4的选择,（1代表page层）
        area: ['500', '300'],
        border: [0, 0.3, '#000'],
        maxmin: false,
        title: '单点登录设置',
        skin: 'layui-layer-molv', //墨绿风格
        shade: [0.6, '#000'],
        shadeClose: true,
        closeBtn: [0, true],
        fix: true,
        page: {
            dom: "#SSSInfo"
        },
    });

    return false;
}

//设置sso
function SetSSS() {
    var _data = {
        Op: "SetSSS",
        Account: $("#txtAccount").val(),
        PWD: $("#txtPWD").val()
    };
    $.ajax({
        type: "post",
        url: _url,
        dataType: "json",
        data: _data,
        success: function (data) {
            if (data.IsSucess) {
                alert("用户凭据配置成功。");
                layer.closeAll();
                getAllMail(1);
            }
            else {
                alert("账户无法验证，请重新输入!");
                //alert(data.Message);
            }
        }
    });
}

function ConfirmSSS() {

    $.ajax({
        type: "post",
        url: _url,
        dataType: "json",
        data: { "Op": "ConfirmSSS" },
        success: function (data) {
            if (!data.IsSucess)
                ShowSSSSetLayer();
        }
    });

}

//跳转OWA
function RedirectToOWA(params) {
    //var params = mailId;
    //if (mailId == "")
    //    params = "";
    //else
    //    params = "#viewmodel=ReadMessageItem&ItemID=" + mailId;
    var url = '/_layouts/15/NyClient/MailTransit.aspx?query=' + params;
    OpenWithNewTab(url);
}

//模拟a标签 跳转target=_blank
function OpenWithNewTab(url) {
    var el = document.createElement("a");
    document.body.appendChild(el);
    el.href = url; //url 是你得到的连接
    el.target = '_blank'; //指定在新窗口打开
    el.click();
    document.body.removeChild(el);
}