using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [NotMapped]
    public class OrganizationManager : NySoftland.Core.BaseEntity
    {
        public string OrganizationID { get; set; }
        public string UserID { get; set; }
    }
}
