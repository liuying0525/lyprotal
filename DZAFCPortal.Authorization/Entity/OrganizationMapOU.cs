using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    //[Table("SYS_OrganizationMapOU")]
    [NotMapped]
    public class OrganizationMapOU
    {
        [Key]
        [MaxLength(128)]
        public string OrganizationCode { get; set; }

        [MaxLength(128)]
        public string OUCode { get; set; }
    }
}
