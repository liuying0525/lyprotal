using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    [Table("SYS_ROLE_USER_RL")]
    public class RoleUser : NySoftland.Core.BaseEntity
    {

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("ORGANIZATIONID")]
        public string OrganizationID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        [ForeignKey("Role")]
        [Column("ROLEID")]
        public string RoleID { get; set; }

        public Role Role { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        [ForeignKey("User")]
        [Column("USERID")]
        public string UserID { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 是否默认组织
        /// </summary>
        [Column("ISDEFAULT")]
        public bool IsDefault { get; set; }


    }
}
