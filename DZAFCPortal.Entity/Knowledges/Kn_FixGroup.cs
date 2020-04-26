using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 知识园地  答复问题的人员组
    /// </summary>
    [Table("KN_FIXGROUP")]
    public class Kn_FixGroup : BaseEntity
    {
        public Kn_FixGroup()
        {
            FixPersons = new List<Kn_FixPerson>();
        }

        [Column("CODE")]
        /// <summary>
        /// 组编号，当前未启用
        /// </summary>
        public string Code { get; set; }

        [Column("NAME")]
        /// <summary>
        /// 组名称
        /// </summary>
        public string Name { get; set; }

        [Column("ORDERNUM")]
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        [Column("ENABLESTATE")]
        /// <summary>
        /// 启用状态
        /// </summary>
        public bool EnableState { get; set; }

        /// <summary>
        /// 组内人员
        /// </summary>
        public virtual List<Kn_FixPerson> FixPersons { get; set; }
    }
}
