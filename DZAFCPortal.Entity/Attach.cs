
using NySoftland.Core;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_ATTACH")]
    public class Attach : BaseEntity
    {
        [Column("CONTENTID")]
        /// <summary>
        /// 外键ID
        /// </summary>
        public string ContentID { get; set; }

        [Column("TYPE")]
        /// <summary>
        /// Config.Enums.AttachTypeEnum 枚举
        /// </summary>
        public string Type { get; set; }


        [Column("URL")]
        /// <summary>
        /// 文件的路径
        /// </summary>
        public string Url { get; set; }

        [Column("FILENAME")]
        /// <summary>
        /// 文件的实际名
        /// </summary>
        public string FileName { get; set; }

        [Column("FILESHOWNAME")]
        /// <summary>
        /// 文件的显示名
        /// </summary>
        public string FileShowName { get; set; }

        [Column("EXTENSION")]
        /// <summary>
        /// 扩展名
        /// 包含 .
        /// 例如 .jpg  .txt
        /// </summary>
        public string Extension { get; set; }

        [Column("SIZE")]
        /// <summary>
        /// 文件大小 
        /// </summary>
        public long Size { get; set; }

        [Column("FILE")]
        /// <summary>
        /// 文件的二进制数据
        /// </summary>
        public byte[] File { get; set; }
    }
}
