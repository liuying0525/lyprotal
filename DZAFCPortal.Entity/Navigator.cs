using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_NAVIGATOR")]
    public class Navigator : NySoftland.Core.BaseEntity
    {
        /// <summary>
        /// 父级导航ID(顶级导航的父级ID为string,Empty)
        /// </summary>
        [Required]
        [Column("PARENTID")]
        public string ParentID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [Column("TITLE")]
        public string Title { get; set; }

        /// <summary>
        /// 英文名字
        /// </summary>
        [Column("ENGLISHNAME")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Column("URL")]
        public string Url { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        [Column("DEEPNUMBER")]
        public int DeepNumber { get; set; }

        /// <summary>
        /// 显示级别
        /// </summary>
        [Column("VIEWLEVEL")]
        public int ViewLevel { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Column("ICONURL")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        /// <summary>
        /// 启用状态(True:启用,False:禁用)
        /// </summary>
        [Column("ENABLED")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 分派角色的Id以,隔开
        /// </summary>
        [Column("APPLYROLES")]
        public string ApplyRoles { get; set; }

        /// <summary>
        /// 点击后是否打开新标签页
        /// </summary>
        [Column("ISOPENEDNEWTAB")]
        public bool IsOpenedNewTab { get; set; }
    }
}
