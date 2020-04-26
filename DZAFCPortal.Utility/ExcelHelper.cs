using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DZAFCPortal.Utility
{
    public class ExcelHelper
    {
        public static void ExportExcel(Object sourceList, string ExcelName, List<System.Web.UI.WebControls.BoundField> columns)
        {
            System.Web.UI.WebControls.GridView dgExport = null;
            using (System.IO.StringWriter strWriter = new System.IO.StringWriter())
            {
                System.Web.UI.HtmlTextWriter htmlWriter = null;
                if (sourceList != null)
                {
                    //设置编码和附件格式 
                    //System.Web.HttpUtility.UrlEncode(ExcelName, System.Text.Encoding.UTF8);//作用是方式中文文件名乱码
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(ExcelName + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"), System.Text.Encoding.UTF8) + ".xls");
                    HttpContext.Current.Response.ContentType = "application/ms-excel";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.Charset = "utf-8";

                    htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                    //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
                    dgExport = new System.Web.UI.WebControls.GridView();
                    foreach (var column in columns)
                    {
                        dgExport.Columns.Add(column);
                    }
                    dgExport.DataSource = sourceList;
                    dgExport.AllowPaging = false;
                    dgExport.AutoGenerateColumns = false;
                    dgExport.DataBind();

                    //下载到客户端 
                    dgExport.RenderControl(htmlWriter);
                    HttpContext.Current.Response.Write(strWriter.ToString());
                }
            }
            HttpContext.Current.Response.End();
        }

        public static void ExportExcelWithDataTable(DataTable dt, string excelName)
        {
            System.Web.UI.WebControls.GridView dgExport = null;
            using (System.IO.StringWriter strWriter = new System.IO.StringWriter())
            {
                System.Web.UI.HtmlTextWriter htmlWriter = null;

                //设置编码和附件格式 
                //System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)作用是方式中文文件名乱码
                HttpContext.Current.Response.AddHeader("content-disposition",
                                            "attachment;filename=" +
                                            System.Web.HttpUtility.UrlEncode(
                                                excelName,
                                                System.Text.Encoding.UTF8) + ".xls");
                HttpContext.Current.Response.ContentType = "application nd.ms-excel";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Charset = "utf-8";

                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
                dgExport = new System.Web.UI.WebControls.GridView();

                dgExport.DataSource = dt.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.DataBind();

                //下载到客户端 
                dgExport.RenderControl(htmlWriter);
                HttpContext.Current.Response.Write(strWriter.ToString());
            }
            HttpContext.Current.Response.End();
        }
    }
}
