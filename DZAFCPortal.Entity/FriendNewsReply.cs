using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 朋友圈回复表
    /// </summary>
    [Table("BIZ_FRIENDNEWSREPLY")]
    public class FriendNewsReply : NySoftland.Core.BaseEntity
    {
        public FriendNewsReply()
        {
            this.IsEnable = true;
        }

        //用户账号
        [MaxLength(64)]
        [Column("USERACCOUNT")]
        public string UserAccount { get; set; }

        //用户账号(外键)
        [ForeignKey("FriendNews")]
        [Column("FRIENDNEWSID")]
        public string FriendNewsID { get; set; }

        public virtual FriendNews FriendNews { get; set; }

        //回复内容
        [Column("REPLYCONTENT")]
        public string ReplyContent { get; set; }

        //回复留言状态
        [Column("REPLYSTATUS")]
        public int ReplyStatus { get; set; }

        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
