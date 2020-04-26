using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 在线评选选项
    /// </summary>
    [Table("BIZ_ONLINE_VOTE")]
    public class OnlineVote : NySoftland.Core.BaseEntity
    {
        public virtual List<OnlineVoteOptions> NY_VoteOptions { get; set; }

        //标题
        [MaxLength(64)]
        [Column("TITLE")]
        public string Title { get; set; }

        //活动简介
        [Column("SUMMARY")]
        public string Summary { get; set; }

        //投票开始时间
        [Column("BEGINTIME")]
        public DateTime BeginTime { get; set; }

        //投票结束时间
        [Column("ENDTIME")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [MaxLength(256)]
        [Column("IMAGEURL")]
        public string ImageURL { get; set; }

        //发起部门
        [MaxLength(64)]
        [Column("PUBLISHDEPT")]
        public string PublishDept { get; set; }


        /// <summary>
        /// 活动详情
        /// </summary>
        [Column("ONLINEDESCRIPTION")]
        public string OnlineDescription { get; set; }

        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }


        /// <summary>
        /// 可选数量
        /// </summary>
        [Column("VOTENUM")]
        public int VoteNum { get; set; }

        /// <summary>
        /// 选择类型 最多可选：MaxNum，必须选：MustNum
        /// </summary>
        [Column("VOTETYPE")]
        public string VoteType { get; set; }
    }
}
