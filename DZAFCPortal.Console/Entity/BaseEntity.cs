
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Console
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("INT_ID")]
        public long Int_ID { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [MaxLength(64)]
        [Column("CREATOR")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CREATETIME")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [MaxLength(64)]
        [Column("MODIFIER")]
        public string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("MODIFYTIME")]
        public DateTime ModifyTime { get; set; }
    }
}
