using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 组织职务表
    /// 用于记录组织节点下面有哪些职务供用户去选择
    /// </summary>
    //[Table("SYS_Organization_Post_RL")]
    [NotMapped]
    public class OrganizationPost : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizationID { get; set; }

        /// <summary>
        /// 职务ID
        /// </summary>
        public string PostID { get; set; }
    }
}
