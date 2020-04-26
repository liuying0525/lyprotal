using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 活动风采
    /// </summary>
    [Table("BIZ_HIGHLIGHTS")]
    public class Highlights : NySoftland.Core.BaseEntity
    {
        [Column("AUCTIONNAME")]
        /// <summary>
        /// 活动名称
        /// </summary>
        public string AuctionName { get; set; }
        [Column("SUMMARY")]
        /// <summary>
        /// 活动简介描述
        /// </summary>
        public string Summary { get; set; }
        [Column("ITEMSUMMARY")]
        /// <summary>
        /// 物品描述
        /// </summary>
        public string ItemSummary { get; set; }
        [Column("AUCTIONBEGINTIME")]
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime AuctionBeginTime { get; set; }
        [Column("AUCTIONENDTIME")]
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime AuctionEndTime { get; set; }

        [Column("INDEXIMGURL")]
        /// <summary>
        /// 活动图片
        /// </summary>
        public string IndexImgUrl { get; set; }
        [Column("PUBLISHDEPT")]
        /// <summary>
        /// 发布部门
        /// </summary>
        public string PublishDept { get; set; }
    }
}
