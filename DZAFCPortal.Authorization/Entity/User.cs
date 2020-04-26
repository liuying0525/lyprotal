using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    /// <summary>
    /// 员工信息表
    /// </summary>
    [Table("SYS_USER")]
    public partial class User : NySoftland.Core.BaseEntity
    {
        public override string ToString()
        {
            return String.IsNullOrEmpty(DisplayName) ? Account : DisplayName;
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
        /// 性别：1：男，2：女
        /// </summary>
        [Column("GENDER")]
        public int Gender { get; set; }


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


        [Column("TITLE")]
        public string Title { get; set; }


        //added by zhanxl 2015年4月9日17:19:10
        [Column("ENGLISHNAME")]
        public string EnglishName { get; set; }
        [Column("ORGANIZATIONNAME")]
        public string OrganizationName { get; set; }

        [Column("OFFICENAME")]
        public string OfficeName { get; set; }

        /// <summary>
        /// Added by zhanxl 2015年10月23日10:24:31
        /// 存储路径
        /// </summary>
        [Column("ORGPATH")]
        public string OrgPath { get; set; }

        [Column("ORGPATHNAME")]
        public string OrgPathName { get; set; }

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

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("SORTNUM")]
        public int SortNum { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Column("ISSHOW")]
        public bool IsShow { get; set; }

        [Column("OPENID")]
        public string OpenId { get; set; }

        /// <summary>
        /// 标识该条数据是否同步
        /// 同步开始统一置为False，在新增或者删除组织的时候置为True，同步完成之后再次统一置为False
        /// </summary>
        [Column("ISONSYNCHRONIZE")]
        public bool IsOnSynchronize { get; set; }


        [Column("LOCKED")]
        public bool Locked { get; set; }


        [Column("EXTERNALID")]
        public string ExternalID { get; set; }

        [Column("EXTATTRIBUTES")]
        public string ExtAttributes { get; set; }

        [Column("ORGANIZATIONEXTERNALID")]
        public string OrganizationExtID { get; set; }


        #region Dealer Info
        [Column("DEALERLEVEL")]
        public string DealerLevel { get; set; }
        [Column("DEALERNAME")]
        public string DealerName { get; set; }
        [Column("DEALERADDRESS")]
        public string DealerAddress { get; set; }
        [Column("DEALERCODE")]
        public string DealerCode { get; set; }
        #endregion

    }
}
