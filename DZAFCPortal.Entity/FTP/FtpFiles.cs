/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/19 16:28:56
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
    [Table("BIZ_FTP_FILES")]
    public class FtpFiles : BaseEntity
    {
        [Column("FILE_NAME")]
        public string FileName { get; set; }
        [Column("PATH")]
        public string Path { get; set; }
        [Column("PARENT_FOLDER_ID")]
        public string ParentFolderID { get; set; }
    }
}
