using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 知识园地   答复人员组中的人员
    /// </summary>
    [Table("KN_FIXPERSON")]
    public class Kn_FixPerson : BaseEntity
    {
        /// <summary>
        /// 组ID 外键
        /// </summary>
        [ForeignKey("FixGroup")]
        [Column("GROUPID")]
        public string GroupID { get; set; }

        public virtual Kn_FixGroup FixGroup { get; set; }

        /// <summary>
        /// 用户ID，关联用户表
        /// </summary>
        [Column("USERID")]
        public string UserID { get; set; }

        [Column("USERACCOUNT")]
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }
    }
}
