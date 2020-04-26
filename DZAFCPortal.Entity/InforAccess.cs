using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 信息发布访问表
    /// </summary>
    [Table("BIZ_INFORACCESS")]
    public class InforAccess : BaseEntity
    {
        [Column("TITLE")]
        /// <summary>
        /// 访问标题
        /// </summary>
        public string Title { get; set; }

        [Column("ACCESSNAME")]
        /// <summary>
        /// 访问人名字
        /// </summary>
        public string AccessName { get; set; }

        [Column("ACCESSDEPARTMENT")]
        /// <summary>
        /// 访问人部门
        /// </summary>
        public string AccessDepartment { get; set; }

        [Column("TYPE")]
        /// <summary>
        /// 访问人类型 1 访问页面 2 下载附件
        /// </summary>
        public int Type { get; set; }
        [Column("CATEGORYID")]
        /// <summary>
        /// 信息发布访问类别
        /// </summary>
        public string CategoryID { get; set; }

        [Column("ATTACHNAME")]
        /// <summary>
        /// 附件名字
        /// </summary>
        public string AttachName { get; set; }
    }
}
