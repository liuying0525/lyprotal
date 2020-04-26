using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Entity
{
    [NotMapped]
    public class OrganizationRoleUser
    {
        public Organization Organization { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
        public RoleUser OrgRoleUser { get; set; }
        public string OrganizationRoleUserID { get; set; }
    }
}
