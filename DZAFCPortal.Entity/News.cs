using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_NEWS")]
    public class News: NySoftland.Core.BaseEntity
    {
        public News()
        {
            this.IsEnable = true;
        }
        //标题名字
        [MaxLength(128)]
        [Column("TITLE")]
        public string Title { get; set; }

        //发布人
        [MaxLength(64)]
        [Column("PUBLISHER")]
        public string Publisher { get; set; }

        //发布内容
        [Column("CONTENT")]
        public string Content { get; set; }

        ////类别ID(外键)
        //[ForeignKey("NY_Category")]
        [Column("CATEGORYID")]
        public string CategoryID { get; set; }

        /// <summary>
        /// 类别路径
        /// </summary>
        [Column("CATEGORYPATH")]
        public string CategoryPath { get; set; }

        //简介
        [MaxLength(2048)]
        [Column("SUMMARY")]
        public string Summary { get; set; }

        //首页图片
        [MaxLength(256)]
        [Column("INDEXIMGURL")]
        public string IndexImgUrl { get; set; }

        //排序号
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        //发布部门
        [MaxLength(64)]
        [Column("PUBLISHDEPT")]
        public string PublishDept { get; set; }

        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        [Column("VIEWNUM")]
        public int ViewNum { get; set; }
    }
}

