using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 职务表
    /// </summary>
    //[Table("SYS_Post")]
    [NotMapped]
    public class Post :NySoftland.Core.BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Code { get; set; }

        [Required]
        public string RoleID { get; set; }

        /// <summary>
        /// 助理
        /// </summary>
        public string AssistantID { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizationID { get; set; }
    }
}
