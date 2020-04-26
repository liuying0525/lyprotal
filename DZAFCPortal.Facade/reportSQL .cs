using DZAFCPortal.Config;
using DZAFCPortal.Entity;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Facade
{
    public class reportSQL
    {

        /// <summary>
        /// 估算费用查询
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetFeeQuery(string beginDate, string endDate)
        {
            beginDate = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd") + " 00:00";
            endDate = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd") + " 23:59";
            DataTable dt = null;
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@beginDate", beginDate), new SqlParameter("@endDate", endDate) };
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.StoredProcedure, "zz_gen_ygjsfwf_data_2015", commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 流程类型查询
        /// </summary>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetWFDefinition(string id, string Type)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@ID", id), new SqlParameter("@ProcessType", Type) };
            str.Append("SELECT * FROM [dbo].[WF_Definition]");
            if (!string.IsNullOrEmpty(id))
            {
                str.Append(" WHERE ID=@ID");
            }
            else if (!string.IsNullOrEmpty(Type))
            {
                str.Append(" WHERE Name=@ProcessType");
            }
            str.Append(" ORDER BY OrderNum");
            DataSet ds = new DBHelper(AppSettings.NYPortalConn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 流程类型删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int delWFDefinition(string id)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@ID", id) };
            str.Append("DELETE  FROM [dbo].[WF_Definition] WHERE ID=@ID");
            int count = new DBHelper(AppSettings.NYPortalConn).ExecuteNonQuery(CommandType.Text, str.ToString(), commandParas);
            return count;
        }




        /// <summary>
        /// 内核单未办结查询
        /// </summary>
        /// <param name="OrderNumber">销售订单编号</param>
        /// <param name="Title">标题</param>
        /// <param name="ServiceUnit">要货单位</param>
        /// <param name="Type">检索类型 </param>
        /// <param name="Department">部门</param>
        /// <param name="Manager"> 客户经理 </param>
        /// <returns></returns>
        public DataSet GetInternalAccounting(string OrderNumber, string Title, string ServiceUnit, string Type, string Department, string Manager, int CurrentPageIndex, int PageSize)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();
            //SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@ID", id), new SqlParameter("@ProcessType", Type) };
            str.Append(@"   SELECT [AccountingDate],[AccountCost],[PayCustomerName],[PayDeptName],[Applicant],[ApplyDeptName],[OrderNumber],[ServiceUnit],[ServiceDate],
            [ServiceContent],b.[ID]  AS WFInstanceID,b.[Title],b.[CreateName] AS Creator,b.[CreateTime],c.[AssignToName],b.[State]
            FROM [dbo].[FD_InternalAccounting] a left join  [dbo].[WF_Instance] b on a.ID = b.FormCode 
            LEFT JOIN [dbo].[WF_Task] c on b.ID = c.[InstanceId] 
            WHERE b.[StateId] !=1 AND c.[TaskState] = 'Open'  AND b.[Title] like '%NH%' ");
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                str.Append(" AND a.OrderNumber ='" + OrderNumber + "'");
            }
            if (!string.IsNullOrEmpty(Title))
            {
                str.Append(" AND b.Title like '%" + Title + "%'");
            }
            if (!string.IsNullOrEmpty(ServiceUnit))
            {
                str.Append(" AND a.ServiceUnit like '%" + ServiceUnit + "%'");
            }
            if (Type == "1")
            {
                if (!string.IsNullOrEmpty(Department))
                {
                    str.Append(" AND a.ApplyDeptName like '%" + Department + "%'");
                }
                if (!string.IsNullOrEmpty(Manager))
                {
                    str.Append(" AND a.Applicant like '%" + Manager + "%'");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Department))
                {
                    str.Append(" AND a.PayDeptName like '%" + Department + "%'");
                }
                if (!string.IsNullOrEmpty(Manager))
                {
                    str.Append(" AND a.PayCustomerName like '%" + Manager + "%'");
                }
            }
            str.Append(" ORDER BY a.FormNumber");

            SqlParameter[] parameters = {
             new SqlParameter("@sqlstr",str.ToString()),
             new SqlParameter("@currentpage",CurrentPageIndex),
             new SqlParameter("@pagesize",PageSize),
            };

            DataSet ds = new DBHelper(AppSettings.NYPortalConn).ExecuteDataSet(CommandType.StoredProcedure, "SqlPager", parameters);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return ds;
        }

        /// <summary>
        /// 内核单报表未办结查询
        /// </summary>
        /// <param name="OrderNumber">销售订单编号</param>
        /// <param name="Title">标题</param>
        /// <param name="ServiceUnit">要货单位</param>
        /// <param name="Type">检索类型 </param>
        /// <param name="Department">部门</param>
        /// <param name="Manager"> 客户经理 </param>
        /// <returns></returns>
        public DataTable GetAllInternalAccounting(string OrderNumber, string Title, string ServiceUnit, string Type, string Department, string Manager)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] {
                new SqlParameter("@OrderNumber", OrderNumber), 
                new SqlParameter("@Title", "%"+Title+"%") ,
                new SqlParameter("@ServiceUnit", "%"+ServiceUnit+"%") ,
                new SqlParameter("@Department", "%"+Department+"%") ,
                new SqlParameter("@Manager", "%"+Manager+"%") ,
            };
            str.Append(@"   SELECT [AccountingDate],[AccountCost],[PayCustomerName],[PayDeptName],[Applicant],[ApplyDeptName],[OrderNumber],[ServiceUnit],[ServiceDate],
            [ServiceContent],b.[ID]  AS WFInstanceID,b.[Title],b.[CreateName] AS Creator,b.[CreateTime],c.[AssignToName],b.[State]
            FROM [dbo].[FD_InternalAccounting] a left join  [dbo].[WF_Instance] b on a.ID = b.FormCode 
            LEFT JOIN [dbo].[WF_Task] c on b.ID  = c.[InstanceId] 
            WHERE b.[StateId] !=1 AND c.[TaskState] = 'Open' AND b.[Title] like '%NH%' ");
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                str.Append(" AND a.OrderNumber =@OrderNumber");
            }
            if (!string.IsNullOrEmpty(Title))
            {
                str.Append(" AND b.Title like @Title");
            }
            if (!string.IsNullOrEmpty(ServiceUnit))
            {
                str.Append(" AND a.ServiceUnit like @ServiceUnit");
            }
            if (Type == "1")
            {
                if (!string.IsNullOrEmpty(Department))
                {
                    str.Append(" AND a.ApplyDeptName like @Department");
                }
                if (!string.IsNullOrEmpty(Manager))
                {
                    str.Append(" AND a.Applicant like @Manager");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Department))
                {
                    str.Append(" AND a.PayDeptName like @Department");
                }
                if (!string.IsNullOrEmpty(Manager))
                {
                    str.Append(" AND a.PayCustomerName like @Manager");
                }
            }
            str.Append(" ORDER BY a.FormNumber");
            DataSet ds = new DBHelper(AppSettings.NYPortalConn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }



        /// <summary>
        /// 下单台账查询
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetUnderSingle(string beginDate, string endDate)
        {
            beginDate = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd") + " 00:00";
            endDate = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd") + " 23:59";
            DataTable dt = null;
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@beginDate", beginDate), new SqlParameter("@endDate", endDate) };
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.StoredProcedure, "zz_gen_cgtz_data", commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 下单资金成本
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetOrderCost(string beginDate, string endDate)
        {
            beginDate = Convert.ToDateTime(beginDate).ToString("yyyy-MM-dd") + " 00:00";
            endDate = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd") + " 23:59";
            DataTable dt = null;
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@beginDate", beginDate), new SqlParameter("@endDate", endDate) };
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.StoredProcedure, "zz_gen_cgzjcb_data", commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
