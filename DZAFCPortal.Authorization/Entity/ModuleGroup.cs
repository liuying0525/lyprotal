using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [Table("SYS_MODULEGROUP")]
    public class ModuleGroup : BaseEntity
    {
        public ModuleGroup()
        {
            OrderNum = 0;
        }
        [Required]
        [MaxLength(64)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        [Column("SUMMARY")]
        public string Summary { get; set; }

        [Required]
        [MaxLength(64)]
        [Column("ICON")]
        public string Icon { get; set; }

        [Required]
        [Column("ORDERNUM")]
        public int OrderNum { get; set; }

        //public List<ModuleGroupDetail> ModuleGroupDetail { get; set; }

        public List<Module> Modules { get; set; }
    }
}
