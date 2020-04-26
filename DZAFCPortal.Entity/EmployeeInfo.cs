using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Entity
{
    [Table("BIZ_EMPLOYEEINFOR")]
    public class EmployeeInfo : NySoftland.Core.BaseEntity
    {
        [Column("NAME")]
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        [Column("NAMEACCOUNT")]
        /// <summary>
        /// 账号
        /// </summary>
        public string NameAccount { get; set; }
        [Column("SEX")]
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        [Column("BIRTHDATE")]
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }
        [Column("DEPTNAME")]
        /// <summary>
        /// 部门名字
        /// </summary>
        public string DeptName { get; set; }
        [Column("POSITION")]
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        [Column("RECRUITNATURE")]
        /// <summary>
        /// 招聘性质
        /// </summary>
        public string RecruitNature { get; set; }
        [Column("EDUCATION")]
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        [Column("EDUCATIONALLEVEL")]
        /// <summary>
        /// 学位
        /// </summary>
        public string EducationalLevel { get; set; }
        [Column("GRADUATEDSCHOOL")]
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduatedSchool { get; set; }
        [Column("PROFESSION")]
        /// <summary>
        /// 专业
        /// </summary>
        public string Profession { get; set; }

        [Column("GRADUATIONTIME")]
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationTime { get; set; }
        [Column("GRADUATESCHECKTIME")]
        /// <summary>
        /// 应届生时间
        /// </summary>
        public DateTime? GraduatesCheckTime { get; set; }
        [Column("INTERNSHIPSTARTTIME")]
        /// <summary>
        /// 实习开始日期
        /// </summary>
        public DateTime? InternshipStartTime { get; set; }
        [Column("INTERNSHIPENDTIME")]
        /// <summary>
        /// 实习结束日期
        /// </summary>
        public DateTime? InternshipEndTime { get; set; }
        [Column("TRIALSTARTTIME")]
        /// <summary>
        /// 试用开始日期
        /// </summary>
        public DateTime? TrialStartTime { get; set; }
        [Column("TRIALENDTIME")]
        /// <summary>
        /// 试用结束日期
        /// </summary>
        public DateTime? TrialEndTime { get; set; }
        [Column("ENABLE")]
        /// <summary>
        /// 启用状态 1-在职 0-离职 2-其他
        /// </summary>
        public Int32 Enable { get; set; }
        [Column("ORDERNUM")]
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
        [Column("OUTDUTYDATE")]
        /// <summary>
        /// 离职日期
        /// </summary>
        /// <remarks>added by zhanxl at 20171116</remarks>
        public DateTime? OutdutyDate { get; set; }
        [Column("POLITICALSTATUS")]
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalStatus { get; set; }

    }
}
