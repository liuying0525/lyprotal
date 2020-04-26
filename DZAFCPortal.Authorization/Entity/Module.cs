using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 系统功能表，用于记录系统有哪些功能
    /// </summary>
    [Table("SYS_MODULE")]
    public class Module : NySoftland.Core.BaseEntity
    {
        public Module()
        {
            IsEnable = true;
            IsDelete = false;
            Url = string.Empty;
        }

        /// <summary>
        /// 编号
        /// </summary>
        [MaxLength(64)]
        [Required]
        [Column("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        [Required]
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 模块管理页Url,默认String.Empty
        /// </summary>
        [MaxLength(256)]
        [Required]
        [Column("URL")]
        public string Url { get; set; }

        /// <summary>
        /// 是否启用，默认值true
        /// </summary>
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除,默认值false
        /// </summary>
        [Column("ISDELETE")]
        public bool IsDelete { get; set; }

        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        [Column("APPLICATIONID")]
        public string ApplicationID { get; set; }

        [Column("MODULEGROUP_ID")]
        [ForeignKey("ModuleGroup")]
        public string ModuleGroup_ID { get; set; }

        [Column("PARENTID")]
        public string ParentID { get; set; }

        public ModuleGroup ModuleGroup { get; set; }

        public Applications Application { get; set; }

        public List<Operation> Operations { get; set; }

        // public List<ModuleGroupDetail> ModuleGroupDetail { get; set; }
    }
}
