using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 知识园地 问题主题
    /// </summary>
    [Table("KN_ISSUE")]
    public class Kn_Issue : BaseEntity
    {
        [Column("TITLE")]
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        [Column("PUBLISHER")]
        /// <summary>
        /// 提交人名称
        /// </summary>
        public string Publisher { get; set; }

        [Column("PUBLISHACCOUNT")]
        /// <summary>
        /// 提交人账号 
        /// </summary>
        public string PublishAccount { get; set; }

        [Column("PUBLISHDATE")]
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime PublishDate { get; set; }

        [Column("SUBMITCONTENT")]
        /// <summary>
        /// 提交内容
        /// </summary>
        public string Content { get; set; }

        [Column("ANSWERPEOPLE")]
        /// <summary>
        /// 解答人 
        /// </summary>
        public string AnswerPeople { get; set; }

        [Column("ANSWERPEOPLEACCOUNT")]
        /// <summary>
        /// 解答人 LANID
        /// </summary>
        public string AnswerPeopleAccount { get; set; }

        [Column("ANSWERGROUP")]
        /// <summary>
        /// 解答人员组
        /// 关联读取 ：Kn_AnswerGroup
        /// 数据库中不做外键关联
        /// </summary>
        public string AnswerGroup { get; set; }

        [Column("STATE")]
        /// <summary>
        /// 问题状态，其状态只能递增  0-->1-->2
        /// 既不可直接从 0 转变为 2，若有其他负面状态，请枚举值为 0 一下。
        /// 当前，已解决的条件为 State>=1
        /// 枚举值：
        /// 0 待解决
        /// 1 已解决
        /// 2 精华帖
        /// </summary>
        public int State { get; set; }


        /// <summary>
        /// 问题答复
        /// </summary>
        public virtual List<Kn_Fix> Fixes { get; set; }
    }
}
