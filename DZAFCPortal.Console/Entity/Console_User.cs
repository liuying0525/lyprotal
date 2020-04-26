using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Console
{
    /// <summary>
    /// 员工信息表
    /// </summary>
    [Table("SYS_USER")]
    public partial class Console_User : BaseEntity
    {
        public override string ToString()
        {
            return string.IsNullOrEmpty(DisplayName) ? Account : DisplayName;
        }

        /// <summary>
        /// 账号 
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("ACCOUNT")]
        public string Account { get; set; }

        /// <summary>
        /// 名字 ad:givenName
        /// </summary>
        [Column("FIRSTNAME")]
        public string FirstName { get; set; }

        /// <summary>
        /// 姓 ad:sn
        /// </summary>
        [Column("LASTNAME")]
        public string LastName { get; set; }

        /// <summary>
        /// 显示名 ad:displayName
        /// </summary>
        [Column("DISPLAYNAME")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 工号 
        /// </summary>
        [Column("JOBNUM")]
        public string JobNum { get; set; }

        /// <summary>
        /// 性别：1：男，2：女
        /// </summary>
        [Column("GENDER")]
        public int Gender { get; set; }

        /// <summary>
        /// 职级：1-30
        /// </summary>
        [Column("POSTLEVEL")]
        public int PostLevel { get; set; }

        /// <summary>
        /// 直接上级
        /// </summary>
        [Column("IMMEDIATESUPERVISORID")]
        public string ImmediateSupervisorID { get; set; }

        /// <summary>
        /// 地址 ad:streetAddress
        /// </summary>
        [Column("ADDRESS")]
        public string Address { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [Column("OFFICEPHONE")]
        public string OfficePhone { get; set; }

        /// <summary>
        /// 其他电话
        /// </summary>
        [Column("OTHERPHONE")]
        public string OtherPhone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Column("MOBILEPHONE")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Ip Phone
        /// </summary>
        [Column("IPPHONE")]
        public string IpPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("EMAIL")]
        public string Email { get; set; }

        /// <summary>
        /// 照片地址 
        /// 已变更为字节流 edit by zhanxl at 2016年1月5日10:00:18
        /// </summary>
        [Column("PHOTOURL")]
        public byte[] PhotoUrl { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("ORGANIZATIONID")]
        public string OrganizationID { get; set; }

        /// <summary>
        /// 职务ID
        /// </summary>
        [Column("POSTID")]
        public string PostID { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [Column("NAMEPY")]
        public string NamePY { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        [Required]
        [Column("STATUS")]
        public Int32 Status { get; set; }

        [Column("APPROVESTATUS")]
        public int ApproveStatus { get; set; }

        [Column("LEVELNAME")]
        public string LevelName { get; set; }
        [Column("ENTRYDATE")]
        public DateTime? EntryDate { get; set; }


        //added by zhanxl 2015年4月9日17:19:10
        [Column("ENGLISHNAME")]
        public string EnglishName { get; set; }
        [Column("DEPARTMENT")]
        public string Department { get; set; }
        [Column("OFFICENAME")]
        public string OfficeName { get; set; }
        [Column("POSTNAME")]
        public string PostName { get; set; }




        /// <summary>
        /// 排序号
        /// </summary>
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        /// <summary>
        /// Added by zhanxl 2015年10月23日10:24:31
        /// 存储路径
        /// </summary>
        [Column("ORGPATH")]
        public string OrgPath { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Column("ISSHOW")]
        public bool IsShow { get; set; }

        [Column("ORGPATHNAME")]
        public string OrgPathName { get; set; }

        /// <summary>
        /// lync sip号
        /// </summary>
        [Column("LYNCSIP")]
        public string LyncSip { get; set; }

        /// <summary>
        /// lync分机号
        /// </summary>
        [Column("HOMEPHONE")]
        public string HomePhone { get; set; }

        /// <summary>
        /// 直线号码
        /// </summary>
        [Column("DIRECTPHONE")]
        public string DirectPhone { get; set; }

        /// <summary>
        /// 移动短号
        /// </summary>
        [Column("SHORTMOBILEPHONE")]
        public string ShortMobilePhone { get; set; }


        /// <summary>
        /// 应急电话
        /// </summary>
        [Column("EMERGENCYPHONE")]
        public string EmergencyPhone { get; set; }

        /// <summary>
        /// A3业务员名称
        /// </summary>
        [Column("A3NAME")]
        public string A3Name { get; set; }

        /// <summary>
        /// 上一个部门名称
        /// </summary>
        [Column("LASTDEPTNAME")]
        public string LastDeptName { get; set; }

        /// <summary>
        /// 上一个部门ID
        /// </summary>
        [Column("LASTDEPTID")]
        public string LastDeptID { get; set; }

        /// <summary>
        /// 上级领导账号
        /// </summary>
        [Column("MANAGERACCOUNT")]
        public string ManagerAccount { get; set; }

        /// <summary>
        /// 上级领导姓名
        /// </summary>
        [Column("MANAGERDIPLAYNAME")]
        public string ManagerDiplayName { get; set; }

        [Column("OPENID")]
        public string OpenId { get; set; }
    }
}
