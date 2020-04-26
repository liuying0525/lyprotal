using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 字典信息主表
    /// </summary>
    [Table("SYS_BNTYPE")]
    public class BnDictType : NySoftland.Core.BaseEntity
    {
        public BnDictType()
        {
            IsEdit = true;
        }


        /// <summary>
        /// 字典编号
        /// </summary>
        [Required]
        [MaxLength(30)]
        //[Index("Index_SYS_BnType_Code", IsUnique = true)]
        [Column("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Required]
        [MaxLength(30)]
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        [Required]
        [Column("ISEDIT")]
        public bool IsEdit { get; set; }


        public virtual List<BnDict> BnDict { get; set; }

        [Column("ASSOCIATEDROLES")]
        //关联角色ID，多个用';'分割
        public string AssociatedRoles { get; set; }
    }
}
