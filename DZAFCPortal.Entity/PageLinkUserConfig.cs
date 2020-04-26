using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 用户自定义的链接信息
    /// 包括 修改链接排序、可见信息
    /// </summary>
    [Table("BIZ_PAGE_LINK_USER_CONFIG")]
    public class PageLinkUserConfig : NySoftland.Core.BaseEntity
    {
        [Column("LINKID")]
        public string LinkID { get; set; }
        [Column("USERID")]
        public string UserID { get; set; }
        [Column("ORDERNUM")]
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
        [Column("TYPE")]
        /// <summary>
        /// 具体见 PageLinkTypeEnum 枚举说明
        /// 扩展 Type=99 表示显示在首页的配置类别
        /// 该类别下的数据集合 PageLinkTypeEnum 中所有类型
        /// </summary>
        public int Type { get; set; }
        [Column("ISENABLE")]
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsEnable { get; set; }
        [Column("ISSHOWINDEX")]
        /// <summary>
        /// 是否显示在首页
        /// </summary>
        public bool IsShowIndex { get; set; }
    }
}
