using DZAFCPortal.Utility;
using Fxm.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DZAFCPortal.Web.Admin.AjaxPage
{
    /// <summary>
    /// uploadifyHandler 的摘要说明
    /// </summary>
    public class uploadifyHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            try
            {
                //文件上传的目录
                string floder = @"/Uploads/Attach";

                HttpPostedFile file = context.Request.Files["Filedata"];
                string type = context.Request.Params["type"];
                if (file != null && file.ContentLength > 0)
                {
                    string fileShowName = System.IO.Path.GetFileName(file.FileName);
                    string fileName = Guid.NewGuid().ToString() + "_" + fileShowName;
                    var filePath = floder + @"/" + fileName;

                    var datas = new byte[file.ContentLength];
                    var stream = file.InputStream;
                    stream.Read(datas, 0, file.ContentLength);

                    // var rs = Fxm.Utility.FileHelper.UploadToWeb(null, file, floder, fileName);
                    var rs = FileHelper.UploadDocument(datas, filePath);
                    //var rs = new NySoftland.Core.Result();
                    //rs.IsSucess = true;


                    StringBuilder sBuilder = new StringBuilder();
                    sBuilder.Append("<li>");
                    if (rs.IsSucess)
                    //if(true)
                    {
                        sBuilder.AppendFormat("<input name='hid_attach_id' type='hidden' value='' />");
                        sBuilder.AppendFormat("<input name='hid_attach_fileName' type='hidden' value='{0}' />", fileName);
                        sBuilder.AppendFormat("<input name='hid_attach_fileShowName' type='hidden' value='{0}' />", fileShowName);
                        sBuilder.AppendFormat("<input name='hid_attach_fileExtension' type='hidden' value='{0}' />", System.IO.Path.GetExtension(file.FileName));
                        sBuilder.AppendFormat("<input name='hid_attach_fileUrl' type='hidden' value='{0}' />", filePath);
                        sBuilder.AppendFormat("<input name='hid_attach_fileSize' type='hidden' value='{0}' />", file.ContentLength);
                        sBuilder.AppendFormat("<input name='hid_attach_type' type='hidden' value='{0}' />", type);

                        sBuilder.AppendFormat("<a href='javascript:;' onclick='delAttachNode(this);' class='del' title='删除附件'>删除</a>");
                        sBuilder.AppendFormat("<div class='title'><a href='{0}' target='_blank'> <span class='glyphicon glyphicon-paperclip'>{1}</span></a></div>", filePath, file.FileName);
                        sBuilder.AppendFormat("<div class='info'>类型：<span class='ext'> {0}</span> 大小：<span class='size'>{1}</span>", System.IO.Path.GetExtension(file.FileName), Fxm.Utility.FileHelper.FileSizeToString(file.ContentLength));
                    }
                    else
                    {
                        sBuilder.AppendFormat("<a href='javascript:;' onclick='delAttachNode(this);' class='del' title='删除'>删除</a>");
                        sBuilder.Append(fileShowName + " 文件上传失败:"+rs.Message);
                    }
                    sBuilder.Append("</li>");

                    context.Response.Write(sBuilder.ToString());
                }
                else
                {
                    throw new Exception("上传的文件不存在，请检查是否已被删除");
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("上传附件发生异常[uploadifyHandler.ashx]", ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}