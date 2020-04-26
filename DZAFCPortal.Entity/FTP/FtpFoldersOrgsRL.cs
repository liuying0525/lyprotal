/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/19 16:30:50
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Entity.FTP
{
    [Table("BIZ_FTP_FOLDER_ORG_RL")]
    public class FtpFoldersOrgsRL : BaseEntity
    {
        [Column("FOLDER_ID")]
        public string FolderID { get; set; }
        [Column("ORG_ID")]
        public string OrgID { get; set; }
        [Column("ACCESS_PERMISSION")]
        public int? AccessPermission { get; set; }

    }
}
