using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [Table("SYS_ORGANIZATION")]
    public class Organization : NySoftland.Core.BaseEntity
    {
        public Organization()
        {
            Description = string.Empty;
            IsEnable = true;
            IsDelete = false;
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("CODE")]
        public string Code { get; set; }


        /// <summary>
        /// 组织名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        [Column("NAME")]
        public string Name { get; set; }


        [MaxLength(64)]
        [Column("SHORTNAME")]
        public string ShortName { get; set; }

        /// <summary>
        /// 是否虚拟组织
        /// </summary>
        [Required]
        [Column("ISVIRTUAL")]
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 描述，最大长度500,默认值string.empty
        /// </summary>
        [MaxLength(500)]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 上级部门ID(uuid)
        /// </summary>
        [Column("PARENTID")]
        public string ParentID { get; set; }

        /// <summary>
        /// 是否启用，默认值 true
        /// </summary>
        [Required]
        [Column("ISENABLE")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除的数据，默认值false
        /// </summary>
        [Required]
        [Column("ISDELETE")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("SORTNUM")]
        public int SortNum { get; set; }


        /// <summary>
        /// Added by zhanxl 2015年10月23日10:24:31
        /// 存储路径
        /// </summary>
        [Column("PATH")]
        public string Path { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Column("ISSHOW")]
        public bool IsShow { get; set; }

        [Column("PATHNAME")]
        public string PathName { get; set; }


        /// <summary>
        /// 标识该条数据是否同步
        /// 同步开始统一置为False，在新增或者删除组织的时候置为True，同步完成之后再次统一置为False
        /// </summary>
        [Column("ISONSYNCHRONIZE")]
        public bool IsOnSynchronize { get; set; }


        [Column("ORGANIZATIONTYPE")]
        public string OrganizationType { get; set; }

        [Column("REGIONID")]
        public string RegionID { get; set; }

        [Column("EXTATTRIBUTES")]
        public string ExtAttributes { get; set; }

        [Column("DEPTMANAGERIDS")]
        public string DeptManagerIDs { get; set; }

        [Column("EXTERNALID")]
        public string ExternalID { get; set; }

        [Column("PARENTEXTERNALID")]
        public string ParentExternalID { get; set; }

        [Column("LEVELNUMBER")]
        public int LevelNumber { get; set; }
    }
}
