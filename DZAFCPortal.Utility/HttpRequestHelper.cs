/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/15 14:21:15
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Utility
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="req_url"></param>
        /// <param name="json_data"></param>
        /// <param name="method"></param>
        /// <param name="content_type"></param>
        /// <returns></returns>
        public static string SendHttpRequest(string req_url,
                                             string method = "get",
                                             string json_data = "",
                                             string content_type = "application/json",
                                             Dictionary<string, string> headers = null)
        {
            //创建一个HTTP请求  
            HttpWebRequest req = WebRequest.Create(req_url) as HttpWebRequest;
            try
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in headers)
                    {
                        // req.Headers.Add("Authorization", "Bearer " + accessToken);
                        req.Headers.Add(kvp.Key, kvp.Value);
                    }

                }

                //设置请求属性
                req.ContentType = content_type;
                req.Accept = "*/*";
                //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
                req.Timeout = 10 * 1000;//10秒连接不成功就中断 
                req.Method = method;
              
                if (!string.IsNullOrEmpty(json_data))
                {

                    byte[] bytes = Encoding.UTF8.GetBytes(json_data);
                    Stream reqstream = req.GetRequestStream();
                    reqstream.Write(bytes, 0, bytes.Length);
                }
                req.KeepAlive = false;

                //获取返回json数据
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            //读取流
                            return readStream.ReadToEnd();
                        }
                        
                    }
                }
            }
            catch (WebException ex)
            {
                var ex_response = (HttpWebResponse)ex.Response;
                if (ex_response != null)
                {
                    using (StreamReader sr = new StreamReader(ex_response.GetResponseStream(), Encoding.UTF8))
                    {
                        Console.WriteLine(req_url + ";Exception Err:" + ex.Message + ";Err JSON:" + sr.ReadToEnd(), "httperr");
                    }
                }
                req.Abort();
                return null;
            }
        }
    }
}
