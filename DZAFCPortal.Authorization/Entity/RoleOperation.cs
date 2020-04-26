using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 角色功能关系表
    /// </summary>
    [Table("SYS_ROLE_OPERATION_RL")]
    public class RoleOperation : NySoftland.Core.BaseEntity
    {

        /// <summary>
        ///  角色ID
        /// </summary>
        //  [ForeignKey("Role")]
        [Column("ROLEID")]
        public string RoleID { get; set; }

        //  public Role Role { get; set; }


        /// <summary>
        /// 操作ID
        /// </summary>
        // [ForeignKey("Operation")]
        [Column("OPERATIONID")]
        public string OperationID { get; set; }
        //  public Operation Operation { get; set; }
    }
}
