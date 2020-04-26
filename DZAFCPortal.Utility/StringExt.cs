using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DZAFCPortal.Utility
{
    public static class StringExt
    {
        /// <summary>
        /// 截取字符串长度，一个中文算一个字符，其他两个算一个
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(this string inputStr, int len)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                return string.Empty;
            }
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = string.Empty;
            byte[] s = ascii.GetBytes(inputStr);

            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                tempString += inputStr.Substring(i, 1);
                if (tempLen >= (len * 2)) break;
            }
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputStr);
            if (GetStringLength(inputStr) > len) tempString += "…";
            return tempString;
        }

        /// <summary>
        /// 统计字符串长度中文算一个,其他2个算一个
        /// </summary>
        /// <param name="str"></param>
        /// <returns>字符串长度</returns>
        public static int GetStringLength(this string str)
        {

            int chineseLeterCount = 0;
            int ortherLeterCount = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) && Char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
                {
                    chineseLeterCount++;//统计汉字
                }
                else
                {
                    ortherLeterCount++;
                }
            }
            return chineseLeterCount + ortherLeterCount / 2;
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string RemoveHTML(this string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// 将字符串转换为HTML编码的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HTMLEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 将已经为HTTP传输进行过 HTML 编码的字符串转换为以解码的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HTMLDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 对URl字符串进行编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseInt(this string str, out int val)
        {
            return int.TryParse(str, out val);
        }


        /// <summary>
        /// 将布尔类型的数据转换为  是、否 的文本形式
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string BoolToString(bool val)
        {
            return val ? "是" : "否";
        }

        /// <summary>
        /// 将时间类型的数据转换为短时间的字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToShortString(object date)
        {
            if (date == null) return String.Empty;

            return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
        }
    }
}
