using DZAFCPortal.Config;
using DZAFCPortal.Utility;
using DZAFCPortal.ViewModel.Client;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DZAFCPortal.Facade
{
    public class repOrderSupple
    {
        /// <summary>
        /// 获取订单补充信息
        /// </summary>
        /// <param name="OrderNumber">订单号</param>
        /// <param name="ServiceUnit">要货单位或最终客户</param>
        /// <param name="BeginTime">订单开始日期</param>
        /// <param name="EndTime">订单结束日期</param>
        /// <returns></returns>
        public DataTable GetOrderSupplement(string OrderNumber, string ServiceUnit, string BeginTime, string EndTime, string Department)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] {
                new SqlParameter("@OrderNumber", OrderNumber),
                new SqlParameter("@ServiceUnit", "%"+ServiceUnit+"%") ,
                new SqlParameter("@BeginTime", BeginTime) ,
                new SqlParameter("@EndTime", EndTime) ,
                new SqlParameter("@Department", "%"+Department+"%")
            };
            str.Append(@"   SELECT
                            ex1.orderno AS 销售订单号,
                            ex1.checkpsn AS 审批人,
                            ex1.checkdt AS 审批日期,
                            case  ex1.isspecial when '1' then '是' else '否' end as 是否特批,
                            ex1.limitmax AS 额度,
                            ex1.limitper AS 百分比,
                            c.compname AS 要货单位, 
                            o1.user0002 AS 最终客户, 
                            o1.user0011 AS 订单名称,
                            d.deptname AS 部门名称,
                            e.lastname AS 业务员,
                            o1.countday  AS 账期,
                            o1.locsum AS 含税销售总额,
                            (select sum(untaxlocsum) from orderdet  where orderdet.sysno=o1.sysno) 不含税金额,
                            ISNULL((select SUM(ISNULL(purmst.user0020,0)) from purmst where purmst.exaflg = 1 and purmst.user0020 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ),0) 含税考核成本,
                            ISNULL((select SUM(ISNULL(purmst.user0021,0)) from purmst where purmst.exaflg = 1 and purmst.user0021 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ),0) 不含税考核成本,
                            ISNULL(user0033,0) AS 预估内核收入,
                            ISNULL(o1.user0031, 0) AS 预估内部服务成本, 
                            CASE WHEN ISNULL(countday,0)>55 THEN (countday - 55)*0.00028*(isnull(user0032,0)+isnull(user0042,0)+isnull(user0044,0)+isnull(user0046,0)) ELSE 0 END as 预估资金成本,
                            ISNULL(o1.user0029, 0) AS 预估外部服务成本,
                            ISNULL(o1.user0029, 0)*0.15 AS 管理费,
                            (isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and (tax=0.17 OR tax=0.16)),0)*0.16+isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and tax=0.06),0)*0.06
                             -(ISNULL(user0041,0))*0.16-isnull(user0043,0)*0.06-isnull(user0045,0)*0.16-isnull(user0047,0)*0.06)*0.13 税金及附加,
                            (select sum(untaxlocsum) from orderdet  where orderdet.sysno=o1.sysno) -ISNULL((select SUM(ISNULL(purmst.user0021,0)) from purmst where purmst.exaflg = 1 and purmst.user0021 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ),0) - (ISNULL(o1.user0031, 0)) - (ISNULL(o1.user0029, 0)) - (ISNULL(o1.user0029, 0)*0.15) - ((isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and (tax=0.17 OR tax=0.16)),0)*0.16+isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and tax=0.06),0)*0.06
                            -(ISNULL(user0041,0))*0.16-isnull(user0043,0)*0.06-isnull(user0045,0)*0.16-isnull(user0047,0)*0.06)*0.13) 预估利润,
                            ISNULL((SELECT distinct  ISNULL(CONVERT(VARCHAR(10),zz_v_orderlist_other.开票日期,111),'')+',' FROM zz_v_orderlist_other WHERE zz_v_orderlist_other.销售订单编号=o1.sysno  FOR xml path('')),'') AS 开票日期,
                            ISNULL((select SUM(ISNULL(开票含税金额,0)) from zz_v_orderlist_sinvdet WHERE zz_v_orderlist_sinvdet.销售订单编号=o1.sysno),0) AS 开票总金额合计 ,
                            ISNULL((select SUM(ISNULL(开票含税金额,0)) from zz_v_orderlist_sinvdet WHERE zz_v_orderlist_sinvdet.销售订单编号=o1.sysno AND zz_v_orderlist_sinvdet.税率 = 0.17),0) AS 开票金额17或16 ,
                            ISNULL((select SUM(ISNULL(开票含税金额,0)) from zz_v_orderlist_sinvdet WHERE zz_v_orderlist_sinvdet.销售订单编号=o1.sysno AND zz_v_orderlist_sinvdet.税率 = 0.06),0) AS 开票金额6 ,
                            ex1.prd_dev_ms 微软,
                            ex1.prd_dev_adobe ADOBE,
                            ex1.prd_dev_adsk ADSK,
                            ex1.prd_dev_symc  安全,
                            ex1.prd_dev_huawei 华为爱数,
                            ex1.prd_dev_other 其它,
                            ex1.prd_dev_description 外部服务成本说明,
                            ex1.remarks AS 备注, 
                            ex1.modifiedBy AS 修改人,
                            ex1.modifiedOn AS 修改日期 
                            FROM zz_order_expand ex1
	                        INNER JOIN ordermst o1 ON ex1.orderno = o1.sysno
	                        LEFT JOIN fg_customfile c ON c.compno = o1.compno
	                        LEFT JOIN dept d ON d.deptno = o1.deptno
	                        LEFT JOIN employee e ON e.empno = o1.empno

                            WHERE 1=1
             ");
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                str.Append(" AND ex1.orderno =@OrderNumber");
            }
            if (!string.IsNullOrEmpty(ServiceUnit))
            {
                str.Append(" AND (c.compname LIKE @ServiceUnit OR o1.user0002 LIKE @ServiceUnit)");
            }
            if (!string.IsNullOrEmpty(Department))
            {
                str.Append(" AND d.deptname Like @Department");
            }
            if (!string.IsNullOrEmpty(BeginTime))
            {
                str.Append(" AND (o1.carddt >= @BeginTime)");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                str.Append(" AND (o1.carddt <= @EndTime)");
            }
            str.Append(" ORDER BY ex1.modifiedOn DESC,ex1.checkdt DESC");
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }


        /// <summary>
        /// 订单补充信息添加
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int insertOrderExpand(OrderExpandModel om)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[]
            {
                new SqlParameter("@orderno", om.orderno),
                new SqlParameter("@limitmax", om.limitmax),
                new SqlParameter("@limitper",om.limitper),
                new SqlParameter("@checkpsn", om.checkpsn),
                new SqlParameter("@checkdt", om.checkdt),
                new SqlParameter("@isspecial", om.isspecial),
                new SqlParameter("@remarks", om.remarks),
                new SqlParameter("@createdby", om.createdby),
                new SqlParameter("@createdon", om.createdon),
                new SqlParameter("@modifiedby", om.modifiedby),
                new SqlParameter("@modifiedon", om.modifiedon),

                new SqlParameter("@prd_dev_ms", om.prd_dev_ms),
                new SqlParameter("@prd_dev_adobe", om.prd_dev_adobe),
                new SqlParameter("@prd_dev_adsk", om.prd_dev_adsk),
                new SqlParameter("@prd_dev_symc", om.prd_dev_symc),
                new SqlParameter("@prd_dev_huawei", om.prd_dev_huawei),
                new SqlParameter("@prd_dev_nutanix", om.prd_dev_nutanix),
                new SqlParameter("@prd_dev_other", om.prd_dev_other),
                new SqlParameter("@prd_dev_description", om.prd_dev_description),
            };
            str.Append("INSERT INTO [dbo].[zz_order_expand]");
            str.Append("(orderno,limitmax,limitper,checkpsn,checkdt,isspecial,remarks,createdby,createdon,modifiedby,modifiedon,prd_dev_ms,prd_dev_adobe,prd_dev_adsk,prd_dev_symc,prd_dev_huawei,prd_dev_nutanix,prd_dev_other,prd_dev_description)");
            str.Append(" values (");
            str.Append("@orderno,@limitmax,@limitper,@checkpsn,@checkdt,@isspecial,@remarks,@createdby,@createdon,@modifiedby,@modifiedon,@prd_dev_ms,@prd_dev_adobe,@prd_dev_adsk,@prd_dev_symc,@prd_dev_huawei,@prd_dev_nutanix,@prd_dev_other,@prd_dev_description)");
            int count = new DBHelper(AppSettings.NYA3Conn).ExecuteNonQuery(CommandType.Text, str.ToString(), commandParas);
            return count;
        }


        /// <summary>
        /// 订单补充信息更新
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int updateOrderExpand(OrderExpandModel om)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[]
            {
                new SqlParameter("@orderno", om.orderno),
                new SqlParameter("@limitmax", om.limitmax),
                new SqlParameter("@limitper",om.limitper),
                new SqlParameter("@checkpsn", om.checkpsn),
                new SqlParameter("@checkdt", om.checkdt),
                new SqlParameter("@remarks", om.remarks),
                new SqlParameter("@isspecial", om.isspecial),
                new SqlParameter("@modifiedby", om.modifiedby),
                new SqlParameter("@modifiedon", om.modifiedon),

                new SqlParameter("@prd_dev_ms", om.prd_dev_ms),
                new SqlParameter("@prd_dev_adobe", om.prd_dev_adobe),
                new SqlParameter("@prd_dev_adsk", om.prd_dev_adsk),
                new SqlParameter("@prd_dev_symc", om.prd_dev_symc),
                new SqlParameter("@prd_dev_huawei", om.prd_dev_huawei),
                new SqlParameter("@prd_dev_nutanix", om.prd_dev_nutanix),
                new SqlParameter("@prd_dev_other", om.prd_dev_other),
                new SqlParameter("@prd_dev_description", om.prd_dev_description),
            };
            str.Append(@"UPDATE [dbo].[zz_order_expand] SET limitmax=@limitmax,limitper=@limitper,checkpsn=@checkpsn,
                        checkdt=@checkdt,isspecial=@isspecial,remarks =@remarks, modifiedby=@modifiedby ,
                        modifiedon=@modifiedon,prd_dev_ms =@prd_dev_ms, prd_dev_adobe = @prd_dev_adobe,@prd_dev_adsk=prd_dev_adsk,
                        prd_dev_symc = @prd_dev_symc,prd_dev_huawei =@prd_dev_huawei,prd_dev_nutanix=@prd_dev_nutanix,
                        prd_dev_other = @prd_dev_other , prd_dev_description =@prd_dev_description
                        WHERE orderno=@orderno");
            int count = new DBHelper(AppSettings.NYA3Conn).ExecuteNonQuery(CommandType.Text, str.ToString(), commandParas);
            return count;
        }

        /// <summary>
        /// 订单补充信息更新(扩展)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int updateOrderExpandEst(OrderExpandModel om)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[]
            {
                new SqlParameter("@orderno", om.orderno),
                new SqlParameter("@modifiedby", om.modifiedby),
                new SqlParameter("@modifiedon", om.modifiedon),

                new SqlParameter("@prd_dev_ms", om.prd_dev_ms),
                new SqlParameter("@prd_dev_adobe", om.prd_dev_adobe),
                new SqlParameter("@prd_dev_adsk", om.prd_dev_adsk),
                new SqlParameter("@prd_dev_symc", om.prd_dev_symc),
                new SqlParameter("@prd_dev_huawei", om.prd_dev_huawei),
                new SqlParameter("@prd_dev_nutanix", om.prd_dev_nutanix),
                new SqlParameter("@prd_dev_other", om.prd_dev_other),
                new SqlParameter("@prd_dev_description", om.prd_dev_description),
            };
            str.Append(@"UPDATE [dbo].[zz_order_expand] SET  modifiedby=@modifiedby ,
                        modifiedon=@modifiedon,prd_dev_ms =@prd_dev_ms, prd_dev_adobe = @prd_dev_adobe,@prd_dev_adsk=prd_dev_adsk,
                        prd_dev_symc = @prd_dev_symc,prd_dev_huawei =@prd_dev_huawei,prd_dev_nutanix=@prd_dev_nutanix,
                        prd_dev_other = @prd_dev_other , prd_dev_description =@prd_dev_description
                        WHERE orderno=@orderno");
            int count = new DBHelper(AppSettings.NYA3Conn).ExecuteNonQuery(CommandType.Text, str.ToString(), commandParas);
            return count;
        }

        /// <summary>
        /// 订单补充信息添加(扩展)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int insertOrderExpandEst(OrderExpandModel om)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[]
            {
                new SqlParameter("@orderno", om.orderno),
                new SqlParameter("@limitmax", om.limitmax),
                new SqlParameter("@limitper",om.limitper),


                new SqlParameter("@createdby", om.createdby),
                new SqlParameter("@createdon", om.createdon),
                new SqlParameter("@modifiedby", om.modifiedby),
                new SqlParameter("@modifiedon", om.modifiedon),

                new SqlParameter("@prd_dev_ms", om.prd_dev_ms),
                new SqlParameter("@prd_dev_adobe", om.prd_dev_adobe),
                new SqlParameter("@prd_dev_adsk", om.prd_dev_adsk),
                new SqlParameter("@prd_dev_symc", om.prd_dev_symc),
                new SqlParameter("@prd_dev_huawei", om.prd_dev_huawei),
                new SqlParameter("@prd_dev_nutanix", om.prd_dev_nutanix),
                new SqlParameter("@prd_dev_other", om.prd_dev_other),
                new SqlParameter("@prd_dev_description", om.prd_dev_description),
            };
            str.Append("INSERT INTO [dbo].[zz_order_expand]");
            str.Append("(orderno,limitmax,limitper,createdby,createdon,modifiedby,modifiedon,prd_dev_ms,prd_dev_adobe,prd_dev_adsk,prd_dev_symc,prd_dev_huawei,prd_dev_nutanix,prd_dev_other,prd_dev_description)");
            str.Append(" values (");
            str.Append("@orderno,@limitmax,@limitper,@createdby,@createdon,@modifiedby,@modifiedon,@prd_dev_ms,@prd_dev_adobe,@prd_dev_adsk,@prd_dev_symc,@prd_dev_huawei,@prd_dev_nutanix,@prd_dev_other,@prd_dev_description)");
            int count = new DBHelper(AppSettings.NYA3Conn).ExecuteNonQuery(CommandType.Text, str.ToString(), commandParas);
            return count;
        }

        public DataTable getOrderInfo(string OrderNumber)
        {
            DataTable dt = null;
            StringBuilder str = new StringBuilder();

            SqlParameter[] commandParas = new SqlParameter[] {
                new SqlParameter("@OrderNumber", OrderNumber),
            };

            str.Append(@"   SELECT 
                            d.deptname AS 部门名称,
                            e.lastname AS 业务员,
                            o1.sysno as 销售订单号,
                            o1.user0011 as 订单名称,
							c.compname as 要货单位,
							o1.user0002 as 最终客户 ,
							o1.locsum AS 含税销售总额,
							(select sum(untaxlocsum) from orderdet  where orderdet.sysno=o1.sysno) 不含税金额,
							(select SUM(ISNULL(purmst.user0020,0)) from purmst where purmst.exaflg = 1 and purmst.user0020 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ) 含税考核成本,
                            (select SUM(ISNULL(purmst.user0021,0)) from purmst where purmst.exaflg = 1 and purmst.user0021 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ) 不含税考核成本,
--                            ISNULL(user0033,0) AS 预估内核收入,
							ISNULL(o1.user0031, 0) AS 预估内部服务成本, 
							ISNULL(o1.user0029, 0) AS 预估外部服务成本,
                            ISNULL(o1.user0029, 0)*0.1 AS 管理费,
						   (select sum(untaxlocsum) from orderdet  where orderdet.sysno=o1.sysno) -(select SUM(ISNULL(purmst.user0021,0)) from purmst where purmst.exaflg = 1 and purmst.user0021 IS NOT NULL AND purmst.spurno in (select distinct purdec.spurno from  purdec  where purdec.refsysno = o1.sysno  ) ) - (ISNULL(o1.user0031, 0)) - (ISNULL(o1.user0029, 0)) - (ISNULL(o1.user0029, 0)*0.1) - ((isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and tax=0.17),0)*0.17+isnull((select sum(isnull(untaxlocsum,0)) from orderdet  
                            where orderdet.sysno=o1.sysno and tax=0.06),0)*0.06
                            -(isnull(user0032,0)/1.17)*0.17-isnull(user0043,0)*0.06-isnull(user0045,0)*0.17-isnull(user0047,0)*0.06)*0.13) + ISNULL(user0033,0) 预估利润
							FROM ordermst o1
	                        LEFT JOIN fg_customfile c ON c.compno = o1.compno 
                            LEFT JOIN dept d ON d.deptno = o1.deptno
							LEFT JOIN employee e ON e.empno = o1.empno
                            WHERE 1=1 
            ");
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                str.Append(" AND o1.sysno =@OrderNumber");
            }
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }


        public int isExist(string OrderNumber)
        {
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] {
                new SqlParameter("@OrderNumber", OrderNumber),
            };

            str.Append(@"   SELECT count(*) FROM zz_order_expand WHERE orderno = @OrderNumber");
            int count = (int)new DBHelper(AppSettings.NYA3Conn).ExecuteScalar(CommandType.Text, str.ToString(), commandParas);
            return count;
        }

        public OrderExpandModel getOrderExpandInfo(string OrderNumber)
        {
            DataTable dt = null;
            OrderExpandModel om = new OrderExpandModel();
            StringBuilder str = new StringBuilder();
            SqlParameter[] commandParas = new SqlParameter[] {
                new SqlParameter("@OrderNumber", OrderNumber),
            };

            str.Append(@"   SELECT [orderno]
                                  ,[checkpsn]
                                  ,[checkdt]
                                  ,[limitmax]
                                  ,[limitper]
                                  ,[isspecial]
                                  ,[remarks]
                                  ,[iscancell]
                                  ,[createdon]
                                  ,[createdby]
                                  ,[modifiedon]
                                  ,[modifiedby] 
                                  ,[prd_dev_ms]
                                  ,[prd_dev_adobe]
                                  ,[prd_dev_adsk]
                                  ,[prd_dev_symc]
                                  ,[prd_dev_huawei]
                                  ,[prd_dev_nutanix]
                                  ,[prd_dev_other]
                                  ,[prd_dev_description]
                                   FROM zz_order_expand WHERE orderno = @OrderNumber");
            DataSet ds = new DBHelper(AppSettings.NYA3Conn).ExecuteDataSet(CommandType.Text, str.ToString(), commandParas);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    om.orderno = ds.Tables[0].Rows[0]["orderno"].ToString();
                    om.checkpsn = ds.Tables[0].Rows[0]["checkpsn"].ToString();
                    om.checkdt = ds.Tables[0].Rows[0]["checkdt"].ToString();
                    om.limitmax = decimal.Parse(ds.Tables[0].Rows[0]["limitmax"].ToString());
                    om.limitper = decimal.Parse(ds.Tables[0].Rows[0]["limitper"].ToString());
                    om.isspecial = ds.Tables[0].Rows[0]["isspecial"].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0]["isspecial"].ToString());
                    om.remarks = ds.Tables[0].Rows[0]["isspecial"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["remarks"].ToString();
                    om.createdon = DateTime.Parse(ds.Tables[0].Rows[0]["createdon"].ToString());
                    om.createdby = ds.Tables[0].Rows[0]["createdby"].ToString();
                    om.modifiedon = DateTime.Parse(ds.Tables[0].Rows[0]["modifiedon"].ToString());
                    om.modifiedby = ds.Tables[0].Rows[0]["modifiedby"].ToString();

                    om.prd_dev_ms = ds.Tables[0].Rows[0]["prd_dev_ms"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_ms"].ToString()) : 0;
                    om.prd_dev_adobe = ds.Tables[0].Rows[0]["prd_dev_adobe"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_adobe"].ToString()) : 0;
                    om.prd_dev_adsk = ds.Tables[0].Rows[0]["prd_dev_adsk"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_adsk"].ToString()) : 0;
                    om.prd_dev_symc = ds.Tables[0].Rows[0]["prd_dev_symc"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_symc"].ToString()) : 0;
                    om.prd_dev_huawei = ds.Tables[0].Rows[0]["prd_dev_huawei"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_huawei"].ToString()) : 0;
                    om.prd_dev_nutanix = ds.Tables[0].Rows[0]["prd_dev_nutanix"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_nutanix"].ToString()) : 0;
                    om.prd_dev_other = ds.Tables[0].Rows[0]["prd_dev_other"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[0]["prd_dev_other"].ToString()) : 0;
                    om.prd_dev_description = ds.Tables[0].Rows[0]["prd_dev_description"].ToString();
                }
            }
            else
            {
                return null;
            }
            return om;
        }
    }
}
