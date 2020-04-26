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
    [Table("BIZ_FTP_FOLDERS")]
    public class FtpFolders : BaseEntity
    {
        [Column("FOLDER_NAME")]
        public string FolderName { get; set; }

        [Column("PATH")]
        public string Path { get; set; }

    }
}
