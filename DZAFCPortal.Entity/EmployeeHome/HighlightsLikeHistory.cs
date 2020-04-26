using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 活动风采点赞记录
    /// </summary>
    [Table("BIZ_HIGHLIGHTS_LIKE_HISTORY")]
    public class HighlightsLikeHistory : NySoftland.Core.BaseEntity
    {
        /// <summary>
        ///  活动风采外键
        /// </summary>
        [ForeignKey("NY_AuctionItem")]
        [Column("NY_AUCTIONITEMID")]
        public string NY_AuctionItemID { get; set; }

        public virtual Highlights NY_AuctionItem { get; set; }
        [Column("AUCTIONACCOUNT")]
        /// <summary>
        /// 活动风采账号
        /// </summary>
        public string AuctionAccount { get; set; }

        [Column("AUCTIONNAME")]
        /// <summary>
        /// 活动风采名称
        /// </summary>
        public string AuctionName { get; set; }
        [Column("HISTORYTHUMBS")]
        /// <summary>
        /// 活动点赞记录
        /// </summary>
        public int HistoryThumbs { get; set; }
    }
}
