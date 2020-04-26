using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.ViewModel.Admin
{
    /// <summary>
    /// 用户表 left join 员工资产
    /// </summary>
    public class User_StaffAsset
    {
        #region 用户属性

        public Guid UserID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }
        #endregion

        #region 员工资产属性
        /// <summary>
        /// IP
        /// </summary>
        public string AssetsIP { get; set; }


        /// <summary>
        /// PC资产编号
        /// </summary>
        public string PCNumber { get; set; }

        /// <summary>
        /// 是否贴有资产标签/是否盘点
        /// </summary>
        public bool IsSign { get; set; }

        /// <summary>
        /// PC型号
        /// </summary>
        public string PCType { get; set; }

        /// <summary>
        /// PC序列号
        /// </summary>
        public string PCSerialNumber { get; set; }

        /// <summary>
        /// TelePhone
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 话机型号
        /// </summary>
        public string PhoneType { get; set; }

        /// <summary>
        /// 话机序列号
        /// </summary>
        public string PhoneSerialNumber { get; set; }

        /// <summary>
        /// 话机IP
        /// </summary>
        public string PhoneIP { get; set; }

        /// <summary>
        /// 网络点位
        /// </summary>
        public string NetworkPoint { get; set; }

        /// <summary>
        /// 电话点位
        /// </summary>
        public string PhonePoint { get; set; }
        #endregion
    }
}
