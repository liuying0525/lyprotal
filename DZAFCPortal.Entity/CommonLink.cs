using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_COMMON_LINK")]
    public class CommonLink : BaseEntity
    {
        [Column("NAME")]
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        [Column("ICON")]
        /// <summary>
        /// 图标地址
        /// </summary>
        public string Icon { get; set; }
        [Column("MIDDLEICON")]
        /// <summary>
        /// 中等大小的图标
        /// </summary>
        public string MiddleIcon { get; set; }
        [Column("SMALLICON")]
        /// <summary>
        /// 小图标
        /// </summary>
        public string SmallIcon { get; set; }
        [Column("URL")]
        /// <summary>
        /// 点击后跳转地址
        /// </summary>
        public string Url { get; set; }
        [Column("DESCRIPTION")]
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        [Column("APPALYTYPE")]
        /// <summary>
        /// 应用范围：
        /// 0： 所有人  1：特定人员  2: 特定角色
        /// </summary>
        public int AppalyType { get; set; }

        [Column("APPALYORGS")]
        /// <summary>
        /// 应用人群
        /// 存储组织机构ID，多项以 , 隔开
        /// </summary>
        public string AppalyOrgs { get; set; }
        [Column("APPALYUSERS")]
        /// <summary>
        /// 应用人群
        /// 存储用户ID，多项以 , 隔开
        /// </summary>
        public string AppalyUsers { get; set; }
        [Column("APPALYROLES")]
        /// <summary>
        /// 应用角色
        /// 存储角色ID，多项以 , 隔开
        /// </summary>
        public string AppalyRoles { get; set; }
        [Column("APPALYORGPATHS")]
        public string AppalyOrgPaths { get; set; }

        #region--------推荐范围-------------
        [Column("RECOMMENDTYPE")]
        /// <summary>
        /// 推荐范围：
        /// 0： 不做推荐  1：特定人员  2: 特定角色   3：所有人
        /// </summary>
        public int RecommendType { get; set; }
        [Column("RECOMMENDORGS")]
        /// <summary>
        /// 推荐人群
        /// 存储组织机构ID，多项以 , 隔开
        /// </summary>
        public string RecommendOrgs { get; set; }
        [Column("RECOMMENDORGPATHS")]
        public string RecommendOrgPaths { get; set; }
        [Column("RECOMMENDUSERS")]
        /// <summary>
        /// 推荐人群
        /// 存储用户ID，多项以 , 隔开
        /// </summary>
        public string RecommendUsers { get; set; }
        [Column("RECOMMENDROLES")]
        /// <summary>
        /// 推荐角色
        /// 存储角色ID，多项以 , 隔开
        /// </summary>
        public string RecommendRoles { get; set; }
        #endregion
        [Column("ORDERNUM")]
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用
        /// true: 启用
        /// false: 禁用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否默认为常用链接
        /// </summary>
        [Column("ISSHOWINDEX")]
        public bool IsCommonLink { get; set; }
        [Column("TYPE")]
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        [Column("PADURL")]
        /// <summary>
        /// Pad地址
        /// </summary>
        public string PadUrl { get; set; }
        [Column("PHONEURL")]
        /// <summary>
        /// Phone地址
        /// </summary>
        public string PhoneUrl { get; set; }
    }
}
