using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    /// <summary>
    /// 知识园地 问题答复
    /// </summary>
    [Table("KN_FIX")]
    public class Kn_Fix : BaseEntity
    {
        /// <summary>
        /// 问题ID，外键
        /// </summary>
        [ForeignKey("Issue")]
        [Column("ISSUEID")]
        public string IssueID { get; set; }


        public virtual Kn_Issue Issue { get; set; }

        [Column("ANSWERCONTENT")]
        /// <summary>
        /// 答复内容
        /// </summary>
        public string Content { get; set; }

        [Column("ANSWERPEOPLE")]
        /// <summary>
        /// 解答人 
        /// </summary>
        public string AnswerPeople { get; set; }

        [Column("ANSWERPEOPLEACCOUNT")]
        /// <summary>
        /// 解答人 LANID
        /// </summary>
        public string AnswerPeopleAccount { get; set; }

        [Column("FIXDATE")]
        /// <summary>
        /// 解答时间
        /// </summary>
        public DateTime FixDate { get; set; }
    }
}
