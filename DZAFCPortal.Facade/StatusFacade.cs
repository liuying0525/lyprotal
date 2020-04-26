using DZAFCPortal.Service;
using DZAFCPortal.Authorization.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Facade
{
    public class StatusFacade
    {
        /// <summary>
        /// 活动召集状态
        /// </summary>
        /// <param name="BookBeginTime"></param>
        /// <param name="BookEndTime"></param>
        /// <param name="ActBeginTime"></param>
        /// <param name="ActEndTime"></param>
        /// <returns></returns>
        public string Status(string BookBeginTime, string BookEndTime, string ActBeginTime, string ActEndTime)
        {
            DateTime bookBeginTime = Convert.ToDateTime(BookBeginTime);
            DateTime bookEndTime = Convert.ToDateTime(BookEndTime);
            DateTime actBeginTime = Convert.ToDateTime(ActBeginTime);
            DateTime actEndTime = Convert.ToDateTime(ActEndTime);
            string status = "";
            if (DateTime.Now > bookBeginTime && DateTime.Now < bookEndTime)
            {
                status = "报名进行中";
            }
            else if (DateTime.Now > bookEndTime)
            {
                status = "报名结束";
            }
            else
            {
                status = "预告";
            }
            return status;
        }

        public string OnlineStatus(string BeginTime, string EndTime)
        {
            DateTime beginTime = Convert.ToDateTime(BeginTime);
            DateTime endTime = Convert.ToDateTime(EndTime);
            string status = "";
            if (DateTime.Now > beginTime && DateTime.Now < endTime)
            {
                status = "投票进行中";
            }
            else if (DateTime.Now > endTime)
            {
                status = "投票结束";
            }
            else
            {
                status = "预告";
            }
            return status;
        }

        public string PromotionStatus(string BeginTime, string EndTime)
        {
            DateTime beginTime = Convert.ToDateTime(BeginTime);
            DateTime endTime = Convert.ToDateTime(EndTime);
            string status = "";
            if (DateTime.Now > beginTime && DateTime.Now < endTime)
            {
                status = "活动进行中";
            }
            else if (DateTime.Now > endTime)
            {
                status = "活动结束";
            }
            else
            {
                status = "预告";
            }
            return status;
        }


        public string AuctionStatus(string BeginTime, string EndTime)
        {
            DateTime beginTime = Convert.ToDateTime(BeginTime);
            DateTime endTime = Convert.ToDateTime(EndTime);
            string status = "";
            if (DateTime.Now > beginTime && DateTime.Now < endTime)
            {
                status = "拍卖竞价中";
            }
            else if (DateTime.Now > endTime)
            {
                status = "拍卖结束";
            }
            else
            {
                status = "拍卖开始";
            }
            return status;
        }
        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public string DateTimeConver(string datetime)
        {
            DateTime dte = Convert.ToDateTime(datetime);
            string datetimeNew = dte.ToString("yyyy-MM-dd HH:mm"); ;
            return datetimeNew;
        }

        /// <summary>
        /// 判断New图标是否显示
        /// </summary>
        /// <param name="Createtime"></param>
        /// <returns></returns>
        public bool Show(string Createtime)
        {
            bool result = false;
            DateTime dt = Convert.ToDateTime(Createtime);
            if (dt.AddDays(7) > DateTime.Now)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 显示按钮状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="eventRegisterCount">已报名人数</param>
        /// <param name="eventMaxCount">最大报名人数</param>
        /// <param name="guid">活动id</param>
        /// <returns></returns>
        public eventStatus EventStatus(string status, int eventRegisterCount, int eventMaxCount, string guid)
        {
            eventStatus et = new eventStatus();
            ActivityParticipantsService esSerivce = new ActivityParticipantsService();
            et.Enable = false;
            et.ClassName = "ny_detail_abstract_btn ny_btn_forbid fl";
            if (status == "预告")
            {
                et.Name = "报名未开始";
            }
            else if (status == "报名进行中")
            {
                string EventAccount = UserInfo.Account;
                if (esSerivce.isRegist(guid, EventAccount))
                {
                    et.Name = "已报名";
                    et.ClassName = "ny_detail_abstract_btn ny_btn_completed fl";
                }
                else if (eventRegisterCount == eventMaxCount)
                {
                    et.Name = "名额已满";
                }
                else
                {
                    et.Name = "报名";
                    et.ClassName = "ny_detail_abstract_btn fl";
                    et.Enable = true;
                }
            }
            else
            {
                et.Name = "报名已结束";
            }
            return et;
        }

        /// <summary>
        /// 返回投票html文字格式
        /// </summary>
        /// <param name="top">排名</param>
        /// <param name="Option">选项</param>
        /// <param name="ReviewsNum">投票数</param>
        /// <param name="OnlineVoteID">选项ID</param>
        /// <returns></returns>
        public string rankSpan(string top, string Option, string ReviewsNum, string OnlineVoteID)
        {
            StringBuilder span = new StringBuilder();
            //string span = "";

            string Sum = GetSum(OnlineVoteID);
            if (top == "1")
            {
                span.Append("<div class='ny_mainlist_block_con_info_online_top1'>");
                span.Append("<span class='ny_mainlist_block_con_info_online_no'>");
                span.Append("Top 1 ：</span><span  class='ny_mainlist_block_con_info_online_sel'>");
                span.Append(Option.Replace("<p>", "").Replace("</p>", ""));
                span.Append("</span><span class='ny_mainlist_block_con_info_online_num'>");
                span.Append(ReviewsNum + "票" + percentage(ReviewsNum, Sum) + "</span></div>");
            }
            else
            {
                span.Append("<div class=''>");
                span.Append("<span class='ny_mainlist_block_con_info_online_no'>");
                span.Append("Top " + top + " ：</span><span  class='ny_mainlist_block_con_info_online_sel'>");
                span.Append(Option.Replace("<p>", "").Replace("</p>", ""));
                span.Append("</span><span class='ny_mainlist_block_con_info_online_num'>");
                span.Append(ReviewsNum + "票" + percentage(ReviewsNum, Sum) + "</span></div>");
            }
            return span.ToString();
        }

        /// <summary>
        /// 投票百分比转换
        /// </summary>
        /// <param name="ReviewsNum">已投票数</param>
        /// <param name="Sum">投票总数</param>
        /// <returns></returns>
        public string percentage(string ReviewsNum, string Sum)
        {
            float result = 0;
            if (ReviewsNum != "0" && Sum != "0")
            {
                result = float.Parse(ReviewsNum) / float.Parse(Sum);
                return "(" + (result * 100).ToString("0.00") + "%)";
            }
            else
            {
                return "(0%)";
            }
        }

        /// <summary>
        /// 获取得票总和
        /// </summary>
        /// <param name="OnlineVoteID"></param>
        public string GetSum(string OnlineVoteID)
        {
            OnlineVoteOptionsService optionService = new OnlineVoteOptionsService();
            string Sum = optionService.GenericService.GetAll().Where(p => p.NY_OnlineVoteID == OnlineVoteID).Sum(p => p.ReviewsNum).ToString();
            return Sum;
        }

        public class eventStatus
        {
            public string Name { get; set; }

            public string ClassName { get; set; }

            public bool Enable { get; set; }
        }
    }
}
