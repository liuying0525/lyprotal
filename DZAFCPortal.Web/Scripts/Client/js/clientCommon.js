function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return null;
}

function initPagerSettings() {
    settings = {
        firsttext: '首页',
        prevtext: '前一页',
        nexttext: '下一页',
        lasttext: '尾页',
        recordtext: '共{0}页，{1}条记录',
        numericButtonCount: 8
    };

    return settings;
}

function closeWin() {
    window.opener = null;
    window.open('', '_self');
    window.close();
}

function switchDifferentUser() {
    // LoginAsAnother('\u002f_layouts\u002fcloseConnection.aspx?loginasanotheruser=true', 0);
    window.location = "/_layouts/closeConnection.aspx?loginasanotheruser=true";
}


var dateFormat = "yyyy-MM-dd HH:mm";


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

function expandZtreeNodeByLevel(treeObj, expandLevel) {
    for (var currentLevel = 0; currentLevel <= expandLevel; currentLevel++) {
        var treeNoes = treeObj.getNodesByParam("level", currentLevel);
        for (var i = 0; i < treeNoes.length; i++) {
            treeObj.expandNode(treeNoes[i], true, false, false);
        }
    }
}

//获取默认的分页配置[juqery.pager]
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

function windowRH() {
    var Rdivheighth = $(".report_topdiv").height();
    var Rtabletoph = Rdivheighth + 75;
    $(".report_teblediv").css("top", Rtabletoph + "px");
}

function InitSortableOption() {
    // Basic table with sort
    $('.stickysortTable').stickySort({ sortable: true });
}
