using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DZAFCPortal.Utility
{
    public class NetWork
    {
        /// <summary>
        /// 通过web请求获取指定URL的内容
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static string GetRequestHtml(string requestUrl)
        {
            string url = requestUrl;

            HttpWebRequest httpReq = (HttpWebRequest)HttpWebRequest.Create(url); ////创建request请求  
            httpReq.Method = "post";  ////设置请求方式  
            HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();  ////返回response数据  

            //请求返回的字符数据
            string data = String.Empty;
            using (Stream myRequestStream = httpRes.GetResponseStream())
            {
                ////取得内容  
                using (StreamReader myStreamRead = new StreamReader(myRequestStream, Encoding.UTF8))
                {
                    data = myStreamRead.ReadToEnd();
                }////读取流  
            }

            return data;
        }


        /// <summary>
        /// 通过正则表达式获取指定标签的内容
        /// </summary>
        /// <param name="htmlTag">标签，例如 div  p  a </param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string GetTagHtml(string htmlTag, string attrName, string attrValue, string html)
        {
            string regex = String.Format("<(?<{0}>[\\w]+)[^>]*\\s{1}=(?<Quote>[\"']?){2}(?(Quote)\\k<Quote>)[\"']?[^>]*>", htmlTag, attrName, attrValue);
            regex += String.Format("((?<Nested><\\k<{0}>[^>]*>)|</\\k<{0}>>(?<-Nested>)|.*?)*</\\k<{0}>>", htmlTag);

            string returnHtml = string.Empty;
            Match m = Regex.Match(html, regex, RegexOptions.IgnoreCase);
            if (m.Success)
            {
                returnHtml = m.Value;
            }

            return returnHtml;
        }

    }
}
