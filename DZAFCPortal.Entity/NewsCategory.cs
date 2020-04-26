using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_CATEGORY")]
    public class NewsCategory : BaseEntity
    {

        [Required]
        [MaxLength(64)]
        [Column("NAME")]
        public string Name { get; set; }

        [Column("CODE")]
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }


        [Column("ORDERNUM")]
        [Required]
        public int OrderNum { get; set; }

        [Column("ISSHOWINDEXAREA")]
        /// <summary>
        /// 是否在公司动态显示
        /// </summary>
        public bool IsShowIndexArea { get; set; }
        [Column("INDEXIMGURL")]
        /// <summary>
        /// 默认图片
        /// </summary>
        public string IndexImgUrl { get; set; }
    }
}
