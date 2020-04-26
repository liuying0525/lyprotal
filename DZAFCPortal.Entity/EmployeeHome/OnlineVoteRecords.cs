using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 投票内容用户关联表
    /// </summary>
    [Table("BIZ_ONLINE_VOTE_RECORDS")]
    public class OnlineVoteRecords : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 评选用户ID(外键)
        /// </summary>
        [ForeignKey("NY_OnlineVoteOptions")]
        [Column("NY_ONLINEVOTEADMINID")]
        public string NY_OnlineVoteAdminID { get; set; }


        public virtual OnlineVoteOptions NY_OnlineVoteOptions { get; set; }

        [Column("ONLINEVOTEID")]
        /// <summary>
        /// 在线评选ID
        /// </summary>
        public string OnlineVoteID { get; set; }

       
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(64)]
        [Column("USERNAME")]
        public string UserName { get; set; }

        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
