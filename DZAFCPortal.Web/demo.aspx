<%@ Page Title="" Language="C#" MasterPageFile="~/Client/BaseLayouts.Master" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="DZAFCPortal.Web.demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="rightOne">
        <div class="onePart">
            <div class="title">
                <div class="name">热点新闻</div>
                <div class="getMore">更多</div>
            </div>
            <div class="oneContent">
                <img src="/Scripts/Client/images/bosses.png" class="oneImg" alt="" />
                <div class="oneNews redNews">
                    <div class="newsInfo"><span class="newsInfoRed">置顶</span>上海东正汽车金融股份有限公司顺利上市</div>
                    <div class="newsTime">2019年x月x日</div>
                </div>
                <div class="newDesc">概览概览概览概览概览概概览概览概览概览概览概览概览概览概览概览概览概览概览概览概览概览概览览概览概览概览概览概览概览概览概览概览概览概览</div>
                <div class="oneNews">
                    <div class="newsInfo">上海东正汽车金融股份有限公司顺利上市</div>
                    <div class="newsTime">2019年x月x日</div>
                </div>
            </div>
        </div>
        <div class="onePart">
            <div class="title">
                <div class="name">职工之家</div>
                <div class="getMore">更多</div>
            </div>
            <div class="oneContent">
                <dl class="oneDl">
                    <dt>内容内容内容内内容内容内容</dt>
                    <dd>2019年x月x日</dd>
                    <dt>内容内容内容内容内容内容内</dt>
                    <dd>2019年x月x日</dd>
                    <dt>内容内容内容内容内容内容内</dt>
                    <dd>2019年x月x日</dd>
                    <dt>内容内容内容内容内容内容内</dt>
                    <dd>2019年x月x日</dd>
                    <dt>内容内容内容内容内容内容内</dt>
                    <dd>2019年x月x日</dd>
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
                <dt>2019-01-01 10:00</dt>
                <dd>李四2019-01-01 请假8小时申请</dd>
                <dt>2019-01-01 10:00</dt>
                <dd>李四2019-01-01 请假8小时申请</dd>
                <dt>2019-01-01 10:00</dt>
                <dd>李四2019-01-01 请假8小时申请</dd>
                <dt>2019-01-01 10:00</dt>
                <dd>李四2019-01-01 请假8小时申请</dd>
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
        <div class="timeContent" style="height: 200px;">
            <div class="threeTime">
                <div class="month">AUG</div>
                <div class="today">今日天气：晴</div>
            </div>
            <div class="threeWeather">
                <div class="day">08</div>
                <div class="timeBox">
                    <div class="nong"><span>农历</span><span>五月十五</span></div>
                    <div class="yang"><span>2019年7月3日</span><span>周五</span></div>
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
                    <label for="part">部门</label><input type="text" id="part" />
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

</asp:Content>
