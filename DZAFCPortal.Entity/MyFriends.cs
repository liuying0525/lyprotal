using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 我的朋友列表
    /// </summary>
    [Table("BIZ_MYFRIENDS")]
    public class MyFriends : NySoftland.Core.BaseEntity
    {
        public MyFriends()
        {
            this.IsEnable = true;
        }

        //用户账号
        [MaxLength(64)]
        [Column("USERACCOUNT")]
        public string UserAccount { get; set; }

        //朋友账号
        [Column("MYFRIENDACCOUNT")]
        public string MyFriendAccount { get; set; }

        //public virtual MyFriends FriendID { get; set; }

        [Column("APPLYSTATE")]
        /// <summary>
        /// 好友批准状态
        /// -99 批准不通过
        /// 0 等待批准
        /// 99 批准通过
        /// </summary>
        public int ApplyState { get; set; }

        [Column("ISENABLE")]
        public bool IsEnable { get; set; }
    }
}
