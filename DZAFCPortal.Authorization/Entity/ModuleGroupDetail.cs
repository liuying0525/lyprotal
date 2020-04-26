using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [Table("SYS_MODULEGROUPDETAIL")]
    public class ModuleGroupDetail : NySoftland.Core.BaseEntity
    {
        public ModuleGroupDetail()
        {
            OrderNum = 0;
        }

        //[ForeignKey("ModuleGroup")]
        [Required]
        [Column("MODULEGROUPID")]
        public string ModuleGroupID { get; set; }
       // public ModuleGroup ModuleGroup { get; set; }

        //[ForeignKey("Module")]
        [Required]
        [Column("MODULEID")]
        public string ModuleID { get; set; }
       // public Module Module { get; set; }

        [Required]
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }


    }
}
