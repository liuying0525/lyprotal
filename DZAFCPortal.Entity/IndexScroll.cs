using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 滚动图片
    /// </summary>
    [Table("BIZ_INDEX_SCROLL")]
    public class IndexScroll : NySoftland.Core.BaseEntity
    {
        public IndexScroll()
        {
            Name = string.Empty;
            ShortName = string.Empty;
            Code = string.Empty;
            ImageUrl = string.Empty;
            LinkUlr = string.Empty;
            EnableState = true;
        }

        [Column("NAME")]
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        [Column("SHORTNAME")]
        /// <summary>
        /// 名字缩写
        /// </summary>
        public string ShortName { get; set; }
        [Column("CODE")]
        public string Code { get; set; }
        [Column("PAGELINKTYPE")]
        /// <summary>
        /// 类别 见枚举 NYProtal.Config.Enums.PageLinkEnum
        /// </summary>
        public int PageLinkType { get; set; }
        [Column("IMAGEURL")]
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; }
        [Column("LINKURL")]
        /// <summary>
        /// 链接跳转路径
        /// </summary>
        public string LinkUlr { get; set; }

        [Column("ORDERNUM")]
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("ENABLESTATE")]
        /// <summary>
        /// 启用状态
        /// </summary>
        public bool EnableState { get; set; }
    }
}
