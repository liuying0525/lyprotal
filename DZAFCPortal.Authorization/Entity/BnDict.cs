using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 字典信息字表 
    /// </summary>
    [Table("SYS_BNDICT")]
    public class BnDict : NySoftland.Core.BaseEntity
    {
        public BnDict()
        {
            IsVisible = true;
        }

        /// <summary>
        /// 编号
        /// </summary>
        [MaxLength(64)]
        [Column("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 父级ID
        /// 如为最高级别，请使用 string.Empty();
        /// </summary>
        [Column("PARENTID")]
        public string ParentID { get; set; }

        /// <summary>
        /// 排序字段，数值越小越靠前
        /// </summary>
        [Required]
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("DISPLAYNAME")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        [Required]
        [Column("ISVISIBLE")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        [Column("BNDICTTYPEID")]
        public string BnDictTypeID { get; set; }

        public virtual BnDictType BnDictType { get; set; }
    }
}
