using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 我的同事圈(消息)
    /// </summary>
    [Table("BIZ_FRIENDNEWS")]
    public class FriendNews : NySoftland.Core.BaseEntity
    {
        public FriendNews()
        {
            this.IsEnable = true;
        }  

        /// <summary>
        /// 用户账号
        /// </summary>
        [MaxLength(64)]
        [Column("USERNAME")]
        public string UserName { get; set; }

        /// <summary>
        /// 当前不使用
        /// </summary>
        [MaxLength(256)]
        [Column("ACTNAME")]
        public string ActName { get; set; }

        //PubContent
        [Column("PUBCONTENT")]
        public string PubContent { get; set; }

        //图片地址
        [MaxLength(256)]
        [Column("IMGURL")]
        public string ImgURL { get; set; }

        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }
    }
}
