using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 系统操作表
    /// </summary>
    [Table("SYS_OPERATION")]
    public class Operation : NySoftland.Core.BaseEntity
    {
        public Operation()
        {
            IsEnable = true;
            IsDelete = false;

            Urls = new List<OperationUrl>();
        }


        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("CODE")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 控件ID
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("CONTROLID")]
        //[Index("Index_ControlID", IsUnique = true)]
        public string ControlID { get; set; }

        ///// <summary>
        ///// 操作地址
        ///// </summary>
        //[MaxLength(512)]
        // public string Url { get; set; }
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        [Column("ISDELETE")]
        public bool IsDelete { get; set; }

        [Column("MODULEID")]
        [Required]
        [ForeignKey("Module")]
        public string ModuleID { get; set; }

        public Module Module { get; set; }
        // public List<Role> Roles { get; set; }


        public List<OperationUrl> Urls { get; set; }

        /// <summary>
        /// 是否属于基本的操作(CRUD) added by zhanxl 2015年1月15日14:32:46
        /// </summary>
        //public bool IsBasedOp { get; set; }
    }
}
