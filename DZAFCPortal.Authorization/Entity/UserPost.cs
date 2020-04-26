using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    // [Table("SYS_User_Post_RL")]
    [NotMapped]
    public class UserPost : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 职务ID
        /// </summary>
        public string PostID { get; set; }
    }
}
