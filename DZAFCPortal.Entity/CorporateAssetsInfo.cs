using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 物品信息管理表
    /// </summary>
    [Table("BIZ_CORPORATEASSETSINFO")]
    public class CorporateAssetsInfo: BaseEntity
    {
        [Column("CODE")]
        /// <summary>
        /// 物品编码
        /// </summary>
        public string Code { get; set; }

        [Column("NAME")]
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }

        [Column("COMPANY")]
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Company { get; set; }

        [Column("TYPE")]
        /// <summary>
        /// 物品类型
        /// </summary>
        public string Type { get; set; }

        [Column("PRESERVER")]
        /// <summary>
        /// 保管人
        /// </summary>
        public string Preserver { get; set; }

        [Column("ENTRUSTED")]
        /// <summary>
        /// 委托代管人
        /// </summary>
        public string Entrusted { get; set; }

        [Column("ISENABLED")]
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        [Column("ISBORROW")]
        /// <summary>
        /// 是否外借
        /// </summary>
        public bool IsBorrow { get; set; }

        [Column("REMARK")]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}