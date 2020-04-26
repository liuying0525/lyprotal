<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DZAFCPortal.Web.Index" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>首页</title>
    <link rel="stylesheet" href="/Scripts/Client/css/index.css" />
    <style type="text/css">
        a {
            text-decoration: unset !important;
            color: black !important;
        }
    </style>

    <script src="/Scripts/jquery/jquery.min.js"></script>
    <script src="/Scripts/Lib/knockout-3.2.0.js"></script>

    <script type="text/javascript">
        function IndexViewModel() {
            var self = this;

            self.MainData = ko.observable();
        }
        var currentModel = new IndexViewModel();


        $(function () {
            $.ajax({
                type: "post",
                url: "Client/AjaxPage/IndexHandler.ashx",
                dataType: "json",
                data: { op: "Init" },
                success: function (data) {
                    console.log(data.Datas);
                    currentModel.MainData(data.Datas);
                    ko.applyBindings(currentModel);
                }
            });
        });
    </script>
</head>
<body>
    <div class="indexHeader">
        <img src="/Scripts/Client/images/indexLogo.png" class="indexLogo leftP" alt="" />
        <img src="/Scripts/Client/images/indexOut.png" alt="" class="indexOut rightP" />
        <div class="userInfos rightP">
            <p><asp:Literal runat="server" ID="lblCurrentUser"></asp:Literal></p>
            <p><asp:Literal runat="server" ID="lblPosition"></asp:Literal></p>
        </div>
        <div class="indexUser rightP">
            <img src="/Scripts/Client/images/indexUser.png" alt="" />
        </div>
        <div class="rightP">
            <div class="messages">IT通知：关于CRM维护通知</div>
        </div>
        <div class="indexInfo rightP">
            <img src="/Scripts/Client/images/indexInfo.png" alt="" />
            <div class="messagesNum">33</div>
        </div>
    </div>
    <div class="content">
        <div class="left">
            <ul class="types">
                <li class="type activeType" onclick="activeImgFunc(0)">
                    <img src="/Scripts/Client/images/icon-index40.png" alt="" />系统首页</li>
                <li class="type" onclick="activeImgFunc(1)">
                    <img src="/Scripts/Client/images/icon-index0.png" alt="" />工作平台</li>
                <li class="type" onclick="activeImgFunc(2)">
                    <img src="/Scripts/Client/images/icon-index1.png" alt="" />公司动态</li>
                <li class="type" onclick="activeImgFunc(3)">
                    <img src="/Scripts/Client/images/icon-index2.png" alt="" />文档管理</li>
                <li class="type" style="margin: 0;" onclick="activeImgFunc(4)">
                    <img src="/Scripts/Client/images/icon-index3.png" alt="" />高管门户</li>
            </ul>
        </div>
        <div class="right">
            <div class="rightOne">
                <div class="onePart" data-bind="with:currentModel.MainData().News">
                    <div class="title">
                        <div class="name">热点新闻</div>
                        <a class="getMore" data-bind="attr: { href: more_url}">更多</a>
                    </div>
                    <div class="oneContent" data-bind="foreach: data">
                        <%--<img src="/Scripts/Client/images/bosses.png" class="oneImg" alt="" />--%>
                        <img data-bind="style: { display: $index() == 0 ? 'block' : 'none' },attr: { src: image_url}" class="oneImg" alt="" />
                        <div class="oneNews" data-bind="css: { redNews: $index() == 0 }">
                            <div class="newsInfo">
                                <span class="newsInfoRed" data-bind="style: { display: $index() == 0 ? 'block' : 'none' }">置顶</span>
                                <a data-bind="html:title, attr: { href: url}"></a>
                            </div>
                            <div class="newsTime" data-bind="html:modify_time"></div>
                        </div>
                    </div>
                </div>
                <div class="onePart" data-bind="with:currentModel.MainData().StaffHome">
                    <div class="title">
                        <div class="name">职工之家</div>
                        <a class="getMore" data-bind="attr: { href: more_url}">更多</a>
                    </div>
                    <div class="oneContent">
                        <dl class="oneDl" data-bind="foreach: data">
                            <dt>
                                <a data-bind="html: title, attr: { href: url}"></a>
                            </dt>
                            <dd data-bind="html:modify_time"></dd>
                        </dl>
                    </div>
                </div>
            </div>
            <div class="rightTwo">
                <div class="twoTypes">
                    <div class="twoType activeTwoType" onclick="activeTypeFunc(0)"><span>我的待办(9)</span></div>
                    <div class="twoType" onclick="activeTypeFunc(1)"><span>我的待阅(3)</span></div>
                    <div class="twoType" onclick="activeTypeFunc(2)"><span>我的流程(3)</span></div>
                </div>
                <div class="oneContent">
                    <dl class="oneDl twoDl">
                        <dt style="text-align: center;">时间</dt>
                        <dd style="text-align: center;">任务名</dd>
                        <dt>2019-01-01  10:00</dt>
                        <dd>李四2019-01-01  请假8小时申请</dd>
                        <dt>2019-01-01  10:00</dt>
                        <dd>李四2019-01-01  请假8小时申请</dd>
                        <dt>2019-01-01  10:00</dt>
                        <dd>李四2019-01-01  请假8小时申请</dd>
                        <dt>2019-01-01  10:00</dt>
                        <dd>李四2019-01-01  请假8小时申请</dd>
                    </dl>
                </div>
                <div class="onePart">
                    <div class="title">
                        <div class="name">常用流程</div>
                    </div>
                    <div class="oneContent">
                        <div class="twoLc">
                            <img src="/Scripts/Client/images/lc0.png" alt="" />
                            <p>人事</p>
                        </div>
                        <div class="twoLc">
                            <img src="/Scripts/Client/images/lc1.png" alt="" />
                            <p>财务</p>
                        </div>
                        <div class="twoLc">
                            <img src="/Scripts/Client/images/lc2.png" alt="" />
                            <p>IT</p>
                        </div>
                        <div class="twoLc">
                            <img src="/Scripts/Client/images/lc3.png" alt="" />
                            <p>法务</p>
                        </div>
                        <div class="twoLc">
                            <img src="/Scripts/Client/images/lc4.png" alt="" />
                            <p>其他</p>
                        </div>
                    </div>
                </div>
                <div class="onePart">
                    <div class="title">
                        <div class="name">知识库</div>
                        <div class="getMore">更多</div>
                    </div>
                    <div class="oneContent">
                        <div class="twoZs">
                            <div class="zsTitle">我的部门</div>
                            <div class="zsText"><span>IT部文档</span></div>
                        </div>
                        <div class="twoZs">
                            <div class="zsTitle">法务部门</div>
                            <div class="zsText"><span>法务文档</span></div>
                            <div class="zsText"><span>法务文档</span></div>
                        </div>
                        <div class="twoZs">
                            <div class="zsTitle">行政部门</div>
                            <div class="zsText"><span>行政文档</span></div>
                        </div>
                        <div class="twoZs">
                            <div class="zsTitle">公共文档</div>
                            <div class="zsText"><span>人事文档</span></div>
                            <div class="zsText"><span>人事文档</span></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="rightThree">
                <div class="timeContent" style="height: 200px;" data-bind="with:currentModel.MainData().DateInfo">
                    <div class="threeTime">
                        <div class="month" data-bind="html:month_abbr"></div>
                        <div class="today">今日天气：晴</div>
                    </div>
                    <div class="threeWeather">
                        <div class="day" data-bind="html:day_num">08</div>
                        <div class="timeBox">
                            <div class="nong"><span>农历</span><span data-bind="text:chinese_date"></span></div>
                            <div class="yang"><span data-bind="text:date_full"></span><span data-bind="text:week"></span></div>
                        </div>
                    </div>
                </div>
                <div class="oneContent">
                    <div class="title">通讯录查询</div>
                    <div class="threeTxl">
                        <div class="threeItem">
                            <label for="name">姓名</label><input type="text" id="name" />
                        </div>
                        <div class="threeItem">
                            <button class="search">查询</button>
                        </div>
                    </div>
                    <div class="title">系统导航</div>
                    <div class="threeXts">
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt0.png" alt="" />
                            <p>CRM系统</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt1.png" alt="">
                            <p>H3系统</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt2.png" alt="" />
                            <p>报表中心</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt3.png" alt="" />
                            <p>批售系统</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt4.png" alt="" />
                            <p>263邮件</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt5.png" alt="" />
                            <p>面签后台</p>
                        </div>
                        <div class="threeXt">
                            <img src="/Scripts/Client/images/xt6.png" alt="" />
                            <p>贷后催收</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var imgList = [
            { active: '/Scripts/Client/images/icon-index40.png', normal: '/Scripts/Client/images/icon-index4.png' },
            { active: '/Scripts/Client/images/icon-index00.png', normal: '/Scripts/Client/images/icon-index0.png' },
            { active: '/Scripts/Client/images/icon-index10.png', normal: '/Scripts/Client/images/icon-index1.png' },
            { active: '/Scripts/Client/images/icon-index20.png', normal: '/Scripts/Client/images/icon-index2.png' },
            { active: '/Scripts/Client/images/icon-index30.png', normal: '/Scripts/Client/images/icon-index3.png' }
        ];
        function activeImgFunc(e) {
            let eles = document.getElementsByClassName('type');
            console.log(eles.length);
            for (let i = 0; i < eles.length; i++) {
                eles[i].className = 'type';
                eles[i].firstChild.src = imgList[i]['normal'];
            }
            let ele = eles[e];
            ele.className = 'activeType';
            ele.setAttribute('class', 'type activeType');
            ele.firstChild.src = imgList[e]['active'];
        }
        function activeTypeFunc(e) {
            let eles = document.getElementsByClassName('twoType');
            for (let i = 0; i < eles.length; i++) {
                eles[i].className = 'twoType';
            }
            let ele = eles[e];
            ele.className = 'twoType activeTwoType';
        }
    </script>
</body>
</html>
