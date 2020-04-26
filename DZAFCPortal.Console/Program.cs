using DZAFCPortal.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Authorization.DAL;
using System.Globalization;
using Newtonsoft.Json;
using DZAFCPortal.Facade.SSO;

namespace DZAFCPortal.Console
{
    class Program
    {
        public static string NumberToChinese(string numberStr)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = numberStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = chineseStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
        }


        static void Main(string[] args)
        {
            Sync2Portal.Process();
            var access_token = Sync2Portal.GetApplicationAccessToken();


            var user_req_url = Sync2Portal.GenerateUserRequestUrl(access_token);
            var json_users = HttpRequestHelper.SendHttpRequest(user_req_url);

            System.Console.WriteLine(json_users);




            SSOFacade facade = new SSOFacade();
            facade.VerifyCodeViaMail("decentcarl@hotmail.com", "223344", "1f22252bd-930b-45d7-9a68-573e2bbc6072");

            var send_data = JsonConvert.SerializeObject(new
            {
                mode = "qrcode",
                user_mail = "zhanxl@dongzhengafc.com"
            });
            //var j = HttpRequestHelper.SendHttpRequest(
            //    "http://172.16.7.33/client/ajaxpage/loginhandler.ashx?mode=qrcode", 
            //    "post", 
            //    send_data);

            var j = HttpRequestHelper.SendHttpRequest(
               "http://172.16.7.33/client/ajaxpage/loginhandler.ashx?mode=qrcode&user_mail=zhanxl@dongzhengafc.com", "get");


          
            System.Console.WriteLine("Complete...");
            #region URL replace

            //var module_srv = new ModuleService();
            //var url_srv = new OperationUrlSerivce();
            ////var nav_srv = new NavigateService();
            //module_srv.GenericService.GetAll(m => m.Url.Contains("/Admin/NYDynamic/")).ToList().ForEach(m =>
            //{
            //    m.Url = m.Url.Replace("/Admin/NYDynamic/NY_DynamicList.aspx", "/Admin/News/NewsList.aspx");

            //});

            //url_srv.GenericService.GetAll(m => m.URL.Contains("/NYDynamic/")).ToList().ForEach(m =>
            //{
            //    m.URL = m.URL.Replace("/NYDynamic/NY_DynamicList.aspx", "/Admin/News/NewsList.aspx");
            //    m.URL = m.URL.Replace("/NYDynamic/NY_EditDynamic.aspx", "/Admin/News/NewsEdit.aspx");
            //});

            //nav_srv.GenericService.GetAll(m => m.Url.ToLower().Contains("/pages/")).ToList().ForEach(m =>
            //{
            //    m.Url = m.Url.Replace("/Pages/", "/Client/Pages/");
            //});

            //module_srv.GenericService.Save();
            //url_srv.GenericService.Save();
            //nav_srv.GenericService.Save();

            #endregion

            var ftp_helper = new FTPHelper("172.16.7.114", "dzafc\\zhanxl", "!!1234abcd", 7000);

            var all_ftp_objects = ftp_helper.GetFtpObjectsRecursively("/");
            //ftp_helper.Upload("/部门111111/", "upload_test.txt");
            //var result = ftp_helper.CreateDirectory("/部门111111/", "部门222222");
            //var file_list = ftp_helper.GetFileList("/");
            //for (int i = 0; i < file_list.Count; i++)
            //{
            //    System.Console.WriteLine(file_list[i]);
            //}

            //var detail_info = ftp_helper.GetFileDetails("/");

            System.Console.ReadLine();
            //RSACrypto rsaCrypto = new RSACrypto(ConstValue.AES_KEY_PUBLIC, ConstValue.AES_KEY_PRIVATE);

            //var password = "1qaz@WSX";
            //var result = GetPOST("https://dzjr.idsmanager.com/public/enduser/login");
        }

        static string GetPOST(string Url)
        {
            try
            {
                var data = new
                {
                    clientId = "aa2598bbaa3360975a80805de17d00bakNlAkgBMmRi",
                    clientSecret = "QWXacsVXq6CrweqmrJCs0cV6NfPmFJRtbDZAsAybVb",
                    username = "zhanxl@dongzhengafc.com",
                    password = "1qaz@WSX"
                };
                string JSONData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
                /*{ "clientId":"aa2598bbaa3360975a80805de17d00bakNlAkgBMmRi","clientSecret":"QWXacsVXq6CrweqmrJCs0cV6NfPmFJRtbDZAsAybVb","username":"zhanxl@dongzhengafc.com","password":"1qaz@WSX"}*/

                //创建一个HTTP请求  
                HttpWebRequest getAccessRequest = WebRequest.Create(Url) as HttpWebRequest;

                //设置请求属性
                getAccessRequest.ContentType = "application/json";
                getAccessRequest.Accept = "*/*";
                //getAccessRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
                getAccessRequest.Timeout = 10 * 1000;//30秒连接不成功就中断 
                getAccessRequest.Method = "post";
                Stream reqstream = getAccessRequest.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);

                //获取返回json数据
                using (HttpWebResponse response = (HttpWebResponse)getAccessRequest.GetResponse())
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
                return null;
            }
        }
    }
}

