using DZAFCPortal.Config;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Facade
{
    public class WFAttachSQL
    {
        /// <summary>
        /// 流程附件查询
        /// </summary>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetWFAttach(string id, string Type)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] { new SqlParameter("@ID", id), new SqlParameter("@ProcessType", Type) };
            str.Append("SELECT * FROM [dbo].[WF_Attach]");
            if (!string.IsNullOrEmpty(id))
            {
                str.Append(" WHERE ID=@ID");
            }
            DataSet ds = new DBHelper(AppSettings.NYPortalConn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
