using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [Table("SYS_OPERATIONURL")]
    /// <summary>
    /// 对于某个操作的操作地址
    /// </summary>
    public class OperationUrl : NySoftland.Core.BaseEntity
    {
        [ForeignKey("Operation")]
        [Column("OPERATIONID")]
        public string OperationID { get; set; }

        public Operation Operation { get; set; }

        [Column("URL")]
        public string URL { get; set; }

    }
}
