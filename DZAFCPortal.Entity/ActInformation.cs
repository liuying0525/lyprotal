using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 活动资讯表
    /// </summary>
    [Table("BIZ_ACTINFORMATION")] 
    public class ActInformation : NySoftland.Core.BaseEntity
    {
        public ActInformation()
        {
            this.IsEnable = true;
        }

        [Column("TITLE")]
        //标题名字
        [MaxLength(64)]
        public string Title { get; set; }

        //发布人
        [MaxLength(32)]
        [Column("PUBLISHER")]
        public string Publisher { get; set; }

        //发布内容
        [Column("PUBCONTENT")]
        public string Pubcontent { get; set; }

        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
