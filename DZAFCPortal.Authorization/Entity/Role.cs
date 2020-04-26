using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{

    [Table("SYS_ROLE")]
    public class Role : NySoftland.Core.BaseEntity
    {
        public Role()
        {
            IsDelete = false;
            IsEnable = true;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        [Required]
        [Column("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(512)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("ISDELETE")]
        public bool IsDelete { get; set; }

        [Column("APPLICATIONID")]
        public string ApplicationID { get; set; }

        public Applications Application { get; set; }

        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 角色类型 0 前台权限 1 后台权限 
        /// </summary>
        [Column("ROLETYPES")]
        public int RoleTypes { get; set; }
    }
}
