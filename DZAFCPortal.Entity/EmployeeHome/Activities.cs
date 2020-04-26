using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 活动召集
    /// </summary>
    [Table("BIZ_ACTIVITIES")]
    public class Activities : NySoftland.Core.BaseEntity
    {
        [Column("NAME")]
        //活动名称
        public string Name { get; set; }
        [Column("MAXPERSONCOUNT")]
        //活动名额
        public int MaxPersonCount { get; set; }
        [Column("SUMMARY")]
        //活动简介
        [MaxLength(2048)]
        public string Summary { get; set; }
        [Column("STATUS")]
        //活动状态
        public int Status { get; set; }
        [Column("ACTBEGINTIME")]
        //活动开始时间
        public DateTime ActBeginTime { get; set; }
        [Column("ACTENDTIME")]
        //活动结束时间
        public DateTime ActEndTime { get; set; }
        [Column("IMAGEURL")]
        //活动图片路径
        public string ImageURL { get; set; }
        [Column("BOOKBEGINTIME")]
        //活动预订开始时间
        public DateTime BookBeginTime { get; set; }
        [Column("BOOKENDTIME")]
        //活动预订结束时间
        public DateTime BookEndTime { get; set; }
        [Column("ACTDESCRIPTION")]
        //活动描述
        public string ActDescription { get; set; }
        [Column("ACTWAY")]
        //报名方式
        public string ActWay { get; set; }
        [Column("PUBLISHDEPT")]
        //发起部门
        public string PublishDept { get; set; }
        [Column("TYPE")]
        /// <summary>
        /// 类型 1：个人
        /// 2：团队
        /// </summary>
        public int Type { get; set; }
        [Column("TEAMMAXPERSON")]
        /// <summary>
        /// 团队人数
        /// </summary>
        public int TeamMaxPerson { get; set; }
        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
