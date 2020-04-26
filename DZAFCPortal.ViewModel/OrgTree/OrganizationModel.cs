using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DZAFCPortal.Authorization.DAL;

namespace DZAFCPortal.ViewModel.OrgTree
{
    public class OrganizationModel
    {
        private UserService userService = new UserService();


        public string ID { get; set; }

        public string Name { get; set; }

        public string ParentID { get; set; }

        public bool IsChecked { get; set; }

        public string Path { get; set; }

        public string PathName { get; set; }

        public int OrderNum { get; set; }

        public bool IsEnable { get; set; }

        public bool IsShow { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string Description { get; set; }


        public string DeputyManagerAccount { get; set; }

        public string DeputyManagerName { get { return userService.getUserName(this.DeputyManagerAccount); } }

        public string BusinessManagerAccount { get; set; }

        public string BusinessManagerName { get { return userService.getUserName(this.BusinessManagerAccount); } }

        public string DeptManagerAccount { get; set; }

        public string DeptManagerName { get { return userService.getUserName(this.DeptManagerAccount); } }

        public string A3DeptCode { get; set; }

        /// <summary>
        /// 部门映射名称
        /// </summary>
        public string ExtendDepartment { get; set; }
    }
}