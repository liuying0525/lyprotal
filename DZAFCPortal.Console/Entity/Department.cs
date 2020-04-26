/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/7/30 13:31:37
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Console
{
    [Table("A_DEPARTMENT")]
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}
