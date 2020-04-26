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
    [Table("BIZ_ONLINE_VOTE_OPTIONS")]
    public class OnlineVoteOptions : NySoftland.Core.BaseEntity
    {
        [Column("NY_ONLINEVOTEID")]
        //评选ID(外键)
        [ForeignKey("NY_OnlineVote")]
        public string NY_OnlineVoteID { get; set; }

        public virtual OnlineVote NY_OnlineVote { get; set; }

        
        //评论选项
        [MaxLength(256)]
        [Column("OPTION")]
        public string Option { get; set; }

        [Column("REVIEWSNUM")]
        //评论票数
        public int ReviewsNum { get; set; }
        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
