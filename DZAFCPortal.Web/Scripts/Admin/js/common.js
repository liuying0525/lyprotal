function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return null;
}

function closeWin() {
    //if (confirm("表单数据未保存，确定要关闭本页吗？")) {
    if (confirm("确定要关闭本页吗？")) {
        window.opener = null;
        window.close();
    }
    else { }
}

function closeLayer() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}

//通过 iframe的方式打开一个 layer
//弹层使用 layer.js 插件
function openLayer(htmlUrl, winName, width, height) {
    var showLayer = $.layer({
        type: 2,
        title: winName,
        maxmin: true,
        area: [width, height],
        border: [0, 0.3, '#000'],
        shade: [0.6, '#000'],
        shadeClose: false,
        closeBtn: [0, true],
        fix: true,
        iframe: {
            src: htmlUrl,
            scrolling: 'auto'
        },
        fadeIn: 1000
    });

    return showLayer;
}

function openWin(htmUrl, winName, width, height) {
    var url = htmUrl; //要打开的窗口
    // 居中的算法是：
    // 左右居中： (屏幕宽度-窗口宽度)/2
    // 上下居中： (屏幕高度-窗口高度)/2
    var awidth = width; //窗口宽度,需要设置
    var aheight = height; //窗口高度,需要设置
    var atop = (screen.availHeight - aheight) / 2; //窗口顶部位置,一般不需要改
    var aleft = (screen.availWidth - awidth) / 2; //窗口放中央,一般不需要改

    var param0 = "scrollbars=1,alwaysRaised=1,status=0,menubar=0,resizable=2,location=0"; //新窗口的参数
    //新窗口的左部位置，顶部位置，宽度，高度
    var params = "top=" + atop + ",left=" + aleft + ",width=" + awidth + ",height=" + aheight + "," + param0;
    win = window.open(url, winName, params); //打开新窗口
    win.focus(); //新窗口获得焦点
}

function openFullWin(htmUrl, winName) {
    var url = htmUrl; //要打开的窗口

    var awidth = screen.availWidth;//窗口宽度,需要设置
    var aheight = screen.availHeight; //窗口高度,需要设置
    var atop = 0; //窗口顶部位置,一般不需要改
    var aleft = 0; //窗口放中央,一般不需要改

    var param0 = "scrollbars=1,alwaysRaised=1,status=0,menubar=0,resizable=2,location=0"; //新窗口的参数
    //新窗口的左部位置，顶部位置，宽度，高度
    var params = "top=" + atop + ",left=" + aleft + ",width=" + awidth + ",height=" + aheight + "," + param0;
    win = window.open(url, winName, params); //打开新窗口
    win.focus(); //新窗口获得焦点
}

//输入验证
function validateCheckBoxList(id, msgId) {
    var rs = false;

    if ($('[id$=' + id + '] input:checked').length > 0) {
        rs = true;

        $("#" + msgId).html("");
    }
    else {
        $("#" + msgId).html("必须勾选一项");
    }

    return rs;
}

function SetLayerRadius() {
    var iframe = $("iframe.xubox_iframe");
    iframe.css({ "border-bottom-left-radius": "15px", "border-bottom-right-radius": "15px" });
    iframe.next("div").css({ "border-top-left-radius": "15px", "border-top-right-radius": "15px" });
    iframe.parents("div.xubox_layer").css('border-radius', '15px');
    iframe.parents("div.xubox_main").css('border-radius', '15px');
}


//判断是否在手机上运行
function IsMobilePhone() {
    var basePrincipal = ["Android", "iPhone", "iPod", "Windows Phone", "MQQBrowser"];
    var advancePrincipal = ["MI PAD", "SM-T320"];

    var flag = false;
    var agent = navigator.userAgent;

    if (agent.indexOf("Windows NT") <= -1 || (agent.indexOf("Windows NT") > -1 && agent.indexOf("compatible; MSIE 9.0;") > -1)) {
        if (agent.indexOf("Windows NT") <= -1 && agent.indexOf("Macintosh") <= -1) {
            for (i = 0; i < basePrincipal.length; i++) {
                var item = basePrincipal[i];
                if (agent.indexOf(item) > -1) {
                    if (item != "Android")//&& item != "MI"
                    {
                        flag = true;
                        break;
                    }
                    else {
                        for (j = 0; j < advancePrincipal.length; i++) {
                            var _tempItem = advancePrincipal[j];
                            if (agent.indexOf(_tempItem) <= -1) {
                                flag = true;
                                break;
                            }
                        } //end advancePrincipal for
                    }// end else
                }
            }//end basePrincipal for
        }
    }

    return flag;
}

//判断是否在电脑上运行
function IsWindows() {
    var flag = false;
    var agent = navigator.userAgent;

    if (agent.indexOf("Mobile") <= 1)
        flag = true;

    return flag;
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

function singDifferentUser() {
    LoginAsAnother('\u002f_layouts\u002fcloseConnection.aspx?loginasanotheruser=true', 0);
}

function getDefaultPagerSettings() {
    return {
        firsttext: '首页',
        prevtext: '上页',
        nexttext: '下页',
        lasttext: '尾页',
        recordtext: '共{0}页，{1}条记录',
        numericButtonCount: 8
    };
}


function VerSpeChar(name) {
    var regular = /[!\*\'\(\)\;\:\@\&\=\+\$\,\/\?\%\#\[\]\.\。\！\￥\，\？\；\：\【\】\（\）"]/;
    if (!regular.test(name.substring(0, name.lastIndexOf(".")))) {
        return false;
    }
    else {
        return true;
    }
}


