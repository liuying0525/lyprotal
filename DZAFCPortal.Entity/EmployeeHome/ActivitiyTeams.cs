using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 公司活动报名登记表(团队)
    /// </summary>
    [Table("BIZ_ACTIVITY_TEAMS")]
    public class ActivitiyTeams : NySoftland.Core.BaseEntity
    {
        //活动ID(外键)
        [ForeignKey("NY_EventSolicitation")]
        [Column("NY_EVENTSOLICITATIONID")]
        public string NY_EventSolicitationID { get; set; }

        public virtual Activities NY_EventSolicitation { get; set; }

        [Column("EVENTNAME")]
        /// <summary>
        /// 团队名字
        /// </summary>
        public string EventName { get; set; }

        [Column("EVENTPERSONNAME")]
        /// <summary>
        /// 团队负责人名字
        /// </summary>
        public string EventPersonName { get; set; }

        [Column("EVENTPHONENUMBER")]
        /// <summary>
        /// 团队负责人电话
        /// </summary>
        public string EventPhoneNumber { get; set; }

        [Column("EVENTEMAIL")]
        /// <summary>
        /// 团队邮箱
        /// </summary>
        public string EventEmail { get; set; }

        [Column("EVENTSEX")]
        /// <summary>
        /// 团队性别
        /// </summary>
        public int EventSex { get; set; }

        [Column("EVENTTEAMACCOUNT")]
        /// <summary>
        /// 团队8位员工号
        /// </summary>
        public string EventTeamAccount { get; set; }

        [Column("USERACCOUNT")]
        ///<summary>
        ///报名用户账号
        /// </summary>
        public string UserAccount { get; set; }

        [Column("TEAMLEVEL")]
        /// <summary>
        /// 称谓
        /// </summary>
        public string TeamLevel { get; set; }
    }
}
