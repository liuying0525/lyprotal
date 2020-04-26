/*******************
* 开发人员：zhengwl
 * 创建时间：2016/7/22 9:47:22
 * 描述说明：
 * 
 * 更改历史：
 * 
*******************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("UMS_MESSAGE")]
    public class UMS_Message
    {
        [Column("ID")]
        public long ID { get; set; }

        [Column("ENGINETYPE")]
        /// <summary>
        /// 发送消息方式（默认微信：WeChat）
        /// </summary>
        public string EngineType { get; set; }
        [Column("TO")]
        /// <summary>
        /// 接收人（默认为向关注微信的全部成员发送：@all）
        /// </summary>
        public string To { get; set; }
        [Column("SUBJECT")]
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
        [Column("BODY")]
        /// <summary>
        /// 内容主体
        /// </summary>
        public string Body { get; set; }
        [Column("STATE")]
        /// <summary>
        /// 信息状态（默认添加状态是 等待发送中：Waiting）
        /// </summary>
        public string State { get; set; }
        [Column("ERRORCOUNT")]
        /// <summary>
        /// 发送失败次数（默认为0）
        /// </summary>
        public int ErrorCount { get; set; }
        [Column("CREATETIME")]
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        [Column("SENTTIME")]
        /// <summary>
        /// 发送时间（默认为空）
        /// </summary>
        public DateTime? SentTime { get; set; }
        [Column("MESSAGEINSERVERIDENTITY")]
        /// <summary>
        /// 消息来源标识（默认为空）
        /// </summary>
        public string MessageInServerIdentity { get; set; }
        [Column("SOURCE")]
        /// <summary>
        /// 发送消息所属模块（即公司动态、职工之家和工作流程）
        /// </summary>
        public string Source { get; set; }
        [Column("ESTIMATETIME")]
        /// <summary>
        /// 预计发送时间(消息预计发送的时间)
        /// </summary>
        public DateTime EstimateTime { get; set; }
        [Column("IMAGES")]
        /// <summary>
        /// 图片地址（当前添加数据的图片地址）
        /// </summary>
        public string Images { get; set; }
        [Column("URL")]
        /// <summary>
        /// 链接地址（当前添加数据的详情地址）
        /// </summary>
        public string Url { get; set; }
    }
}
