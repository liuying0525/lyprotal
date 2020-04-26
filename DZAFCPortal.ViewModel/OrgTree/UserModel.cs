using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.ViewModel.OrgTree
{
    public class UserModel
    {
        public string ID { get; set; }

        public string Account { get; set; }

        public string DisplayName { get; set; }

        public string Organization { get; set; }

        public string OrgId { get; set; }

        public string MobilePhone { get; set; }

        public string OfficePhone { get; set; }

        public string Email { get; set; }

        //added by zhanxl 2015年5月13日17:26:03
        /// <summary>
        /// 名字
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }

        public string Department { get; set; }

        public string OfficeName { get; set; }

        public string PostName { get; set; }

        public string Address { get; set; }


        public string IpPhone { get; set; }

        public string LyncSip { get; set; }


        public string OrgPathName { get; set; }

        public string PhotoUrl { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// Lync分机号
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// 直线号码
        /// </summary>
        public string DirectPhone { get; set; }

        /// <summary>
        /// 移动短号
        /// </summary>
        public string ShortMobilePhone { get; set; }


        /// <summary>
        /// 应急电话
        /// </summary>
        public string EmergencyPhone { get; set; }
    }
}