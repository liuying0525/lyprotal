using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 上级领导设置
    /// 关联用户与上级领导
    /// </summary>
    [Table("SYS_USER_MANAGER_RL")]
    public class ImmediateSupervisorSettings : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        [Required]
        [Column("ACCOUNT")]
        public string Account { get; set; }

        /// <summary>
        ///上级领导ID （关联用户表 User(ID)）
        /// </summary>
        [Required]
        [Column("USERID")]
        public string UserID { get; set; }
    }
}
