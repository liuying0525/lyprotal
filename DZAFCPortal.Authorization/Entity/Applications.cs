using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 系统表，存放应用系统，目前有Portal，WorkFlow
    /// </summary>
    [Table("SYS_APPLICATION")]
    public class Applications : NySoftland.Core.BaseEntity
    {
        public Applications()
        {
            this.IsEnable = true;
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("NAME")]
        public string Name { get; set; }
        /// <summary>
        /// 系统编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("CODE")]
        public string Code { get; set; }
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        [Required]
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        public virtual List<Module> Modules { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}
