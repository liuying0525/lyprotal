using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 活动报名登记表
    /// </summary>
    [Table("BIZ_ACTIVITY_PARTICIPANTS")]
    public class ActivityParticipants : NySoftland.Core.BaseEntity
    {
        //活动ID(外键)
        [Column("NY_EVENTSOLICITATIONID")]
        [ForeignKey("NY_EventSolicitation")]
        public string NY_EventSolicitationID { get; set; }

        public virtual Activities NY_EventSolicitation { get; set; }

        //报名用户账号
        [MaxLength(64)]
        [Column("USERACCOUNT")]
        public string UserAccount { get; set; }

        //报名用户名称
        [MaxLength(64)]
        [Column("USERDISPLAYNAME")]
        public string UserDisplayName { get; set; }

        [Column("PHONENUMBER")]
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        [Column("EMAIL")]
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        [Column("SEX")]
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        [Column("EVENTACCOUNT")]
        /// <summary>
        /// 8位员工号
        /// </summary>
        public string EventAccount { get; set; }

        /// <summary>
        /// 外键 团队ID  
        /// </summary>
        [ForeignKey("EventSolitationTeam")]
        [Column("TEAMID")]
        public string TeamID { get; set; }

        public virtual ActivitiyTeams EventSolitationTeam { get; set; }

        [Column("REGISTTIME")]
        //报名时间
        public DateTime RegistTime { get; set; }
        [Column("ISENABLE")]
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
