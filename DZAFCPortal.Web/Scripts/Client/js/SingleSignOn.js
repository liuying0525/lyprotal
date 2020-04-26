
var _code;  //办公链接code
var _isMail; //当前验证对象是否邮箱

var username;//sso lanid
var pwd;//sso password
var domain;//sso domain
var domainIndex; //domain Index

//distinguished name
var dc_beacn = "BEACN*10.168.128.2:389*DC=beacn,DC=chn,DC=hkbea,DC=com*beacn.chn.hkbea.com";
var dc_chn = "CHN*10.162.1.27:389*DC=chn,DC=hkbea,DC=com*chn.hkbea.com";


//added by zhanxl 2015年4月16日14:46:52
//打开窗口录入sso
function Open() {

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
            dom: "#DomainAccount"
        },
    });

    return false;
}

//设置sso
function SetSSO(_url) {
    var _data = { Account: $("#txtAccount").val(), PWD: $("#txtPWD").val(), Domain: $("input[type='radio']:checked").val() };
    $.ajax({
        type: "post",
        url: _url,
        dataType: "json",
        data: _data,
        success: function (data) {
            if (data.IsSucess) {
                alert("单点登录配置成功");
                layer.closeAll();
                if (_isMail)//设置成功选择跳转owa或者链接处理页
                    RedirectOWA();
                else
                    RedirectToDetailOfficeLink(data.Datas);
            }
            else
                alert(data.Message);
        }
    });
}

//判断sso是否存在当前用户凭据
function ConfirmSSO(_url, trigger) {
    _code = $(trigger).attr("code");

    if (typeof (_code) == "undefined")  //scenario1:Mail Confirm
    {
        _isMail = true;
        $.ajax({
            type: "get",
            url: _url,
            dataType: "json",
            data: {
            },
            success: function (data) {
                if (!data.IsSucess)
                {
                    Open();
                }
                else {
                    RedirectOWA();//验证通过跳转owa
                }
            }
        });
    }
    else {
        _isMail = false;
        //scenario2:None SSO Link
        if ($.trim(_code) == "") {
            var linkUrl = $(trigger).attr("url");
            OpenWithNewTab(linkUrl);
            return false;
        }
        else //scenario3:SSO Link   
        {
            $.ajax({
                type: "get",
                url: _url,
                dataType: "json",
                data: {
                },
                success: function (data) {
                    if (!data.IsSucess) {
                        Open();
                        return false;
                    }
                    else {
                        RedirectToDetailOfficeLink(data.Datas);
                    }
                }
            });
        }
    }
}

//各系统中转中心
function RedirectToDetailOfficeLink(userinfo) {
    var url = "";

    //alert("进入单点登录！当前code为：" + _code);
    //username = userinfo.Username;
    //pwd = userinfo.Password;
    //domain = userinfo.Domain;
    //domainIndex = domain.toLowerCase() == "chn" ? 0 : 1;      //判断域index 费用管理需传递index


    switch (_code) {
        case "Study":
            GetToStudy();
            break;
        case "Leave":
            GetToLeave();
            break;
        case "PM":
            GetToPM();
            break;
        case "Expense":
            GetToExpense();
            break;
    }

    return false;
}

//跳转到owa
function RedirectOWA() {
    var url = '/_layouts/15/BEAClient/MailTransit.aspx?query=';
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



/************************************  跳转各系统  *******************************************************/
//学习管理
function GetToStudy() {
    var url = "/_layouts/15/BEAClient/SSO/SSO_Study.aspx";
    OpenWithNewTab(url);
    //var data = { username: username, pwd: pwd, domain: domain };
    //$.ajax({
    //    type: "post",
    //    url: url,
    //    dataType: "json",
    //    data: data,
    //    success: function () {
    //    }
    //});
}

//电子休假
function GetToLeave() {
    var url = "/_layouts/15/BEAClient/SSO/SSO_Leave.aspx";
    OpenWithNewTab(url);
    //var leaveUrl = "http://els.intranet.hkbea.com.cn:8080/e_leave/lonAction_loginAD.action?loginType=login&userID=" + username + "&userPassword=" + pwd + "&beadomain=" + domain;
    //OpenWithNewTab(leaveUrl);
}

//项目管理
function GetToPM() {
    var url = "/_layouts/15/BEAClient/SSO/SSO_Project.aspx";
    OpenWithNewTab(url);

    //var url = "/_layouts/15/BEAClient/SSO/SSO_Project.aspx";
    //var data = { username: username, pwd: pwd };
    //$.ajax({
    //    type: "post",
    //    url: url,
    //    dataType: "json",
    //    data: data,
    //    success: function () {

    //    }
    //});

}

//费用管理
function GetToExpense() {
    var url = "/_layouts/15/BEAClient/SSO/SSO_Expense.aspx";
    OpenWithNewTab(url);
    //var expenseUrl = "http://cem.intranet.hkbea.com.cn/expense/bea/login/start?aboutlogin-bea=aaa&userID=" + username + "&userPassword=" + pwd + "&beadomain=" + domainIndex;
    //OpenWithNewTab(expenseUrl);
}