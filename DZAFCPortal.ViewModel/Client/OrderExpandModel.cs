using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.ViewModel.Client
{
    public  class OrderExpandModel
    {
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string orderno { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string checkpsn { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public string checkdt { get; set; }


        /// <summary>
        /// 是否特批
        /// </summary>
        public int isspecial { get; set; }

        /// <summary>
        /// 额度
        /// </summary>
        public decimal limitmax { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public decimal limitper { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }

        /// <summary>
        /// 微软产品线
        /// </summary>
        public decimal prd_dev_ms { get; set; }

        /// <summary>
        /// adobe产品线
        /// </summary>
        public decimal prd_dev_adobe { get; set; }

        /// <summary>
        /// adsk产品线
        /// </summary>
        public decimal prd_dev_adsk { get; set; }

        /// <summary>
        /// 安全及存储产品线
        /// </summary>
        public decimal prd_dev_symc { get; set; }

        /// <summary>
        /// 华为及爱数产品线
        /// </summary>
        public decimal prd_dev_huawei { get; set; }

        /// <summary>
        /// Nutanix产品线
        /// </summary>
        public decimal prd_dev_nutanix { get; set; }

        /// <summary>
        /// other产品线
        /// </summary>
        public decimal prd_dev_other { get; set; }


        /// <summary>
        /// 产品拓展费描述
        /// </summary>
        public string prd_dev_description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createdon { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string createdby { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime modifiedon { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string modifiedby { get; set; }
        
    }
}
