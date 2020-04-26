/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/19 13:44:55
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DZAFCPortal.Utility
{
    public class FTPHelper
    {
        #region 构造函数

        /// <summary>
        /// 创建FTP工具
        /// <para>
        /// 默认不使用SSL,使用二进制传输方式,使用被动模式
        /// </para>
        /// </summary>
        /// <param name="host">主机名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        public FTPHelper(string host, string userId, string password, int port)
            : this(host, userId, password, port, null, false, true, true)
        {
        }
        /// <summary>
        /// 创建FTP工具
        /// </summary>
        /// <param name="host">主机名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        /// <param name="enableSsl">允许Ssl</param>
        /// <param name="proxy">代理</param>
        /// <param name="useBinary">允许二进制</param>
        /// <param name="usePassive">允许被动模式</param>
        public FTPHelper(string host, string userId, string password, int port, IWebProxy proxy, bool enableSsl, bool useBinary, bool usePassive)
        {
            this.UserId = userId;
            this.Password = password;
            if (host.ToLower().StartsWith("ftp://"))
            {
                this.Host = host;
            }
            else
            {
                this.Host = "ftp://" + host;
            }
            this.Host += $":{port}";

            this.Port = port;
            this.Proxy = proxy;
            this.EnableSsl = enableSsl;
            this.UseBinary = useBinary;
            this.UsePassive = usePassive;
        }

        #endregion

        #region 配置信息
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; } = string.Empty;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserId { get; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; } = string.Empty;

        /// <summary>
        /// 代理
        /// </summary>
        public IWebProxy Proxy { get; set; } = null;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// EnableSsl
        /// </summary>
        public bool EnableSsl { get; } = false;

        /// <summary>
        /// 被动模式
        /// </summary>
        public bool UsePassive { get; set; } = true;

        /// <summary>
        /// 二进制方式
        /// </summary>
        public bool UseBinary { get; set; } = true;
        #endregion

        #region 创建一个FTP连接
        /// <summary>
        /// 创建一个FTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        /// <returns>FTP请求</returns>
        private FtpWebRequest CreateRequest(string url, string method)
        {
            //建立连接
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(this.UserId, this.Password);
            request.Proxy = this.Proxy;
            request.KeepAlive = false;//命令执行完毕之后关闭连接
            request.UseBinary = UseBinary;
            request.UsePassive = UsePassive;
            request.EnableSsl = EnableSsl;
            request.Method = method;
            return request;
        }
        #endregion

        #region 上传一个文件到远端路径下
        /// <summary>
        /// 把文件上传到FTP服务器的specifiedPath下
        /// </summary>
        /// <param name="localFile">本地文件信息</param>
        /// <param name="remoteFileName">要保存到FTP文件服务器上的名称</param>
        public bool Upload(string specifiedPath, FileInfo localFile, string remoteFileName)
        {
            bool result = false;
            if (localFile.Exists)
            {
                string url = Host.TrimEnd('/') + specifiedPath + remoteFileName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);
                //上传数据
                using (Stream rs = request.GetRequestStream())
                using (FileStream fs = localFile.OpenRead())
                {
                    byte[] buffer = new byte[4096];//4K
                    int count = fs.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        rs.Write(buffer, 0, count);
                        count = fs.Read(buffer, 0, buffer.Length);
                    }
                    fs.Close();
                    result = true;
                }
                return result;
            }
            throw new Exception(string.Format("本地文件不存在,文件路径:{0}", localFile.FullName));
        }


        public void UploadLocalFile(string localPath, string specifiedPath, string remoteFileName)
        {
            //"D:\app\result.txt"

            string url = Host.TrimEnd('/') + specifiedPath + remoteFileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);
            //上传数据
            using (Stream rs = request.GetRequestStream())
            using (FileStream fs = File.Open(localPath, FileMode.Open))
            {
                byte[] buffer = new byte[4096];//4K
                int count = fs.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    rs.Write(buffer, 0, count);
                    count = fs.Read(buffer, 0, buffer.Length);
                }
                fs.Close();
            }

        }


        public void UploadHttpPostFile(HttpPostedFile file, string specifiedPath, string remoteFileName)
        {
            //"D:\app\result.txt"

            string url = Host.TrimEnd('/') + specifiedPath + remoteFileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);
            //上传数据
            using (Stream rs = request.GetRequestStream())
            //using (FileStream fs = File.Open(localPath, FileMode.Open))
            {
                byte[] buffer = new byte[file.ContentLength];//4K

                int count = file.InputStream.Read(buffer, 0, file.ContentLength);
                while (count > 0)
                {
                    rs.Write(buffer, 0, count);
                    count = file.InputStream.Read(buffer, 0, file.ContentLength);
                }
            }

        }

        private void EndGetStreamCallback(IAsyncResult ar)
        {
            //用户定义对象，其中包含该操作的相关信息,在这里得到FtpWebRequest
            FtpWebRequest uploadRequest = (FtpWebRequest)ar.AsyncState;

            //结束由BeginGetRequestStream启动的挂起的异步操作
            //必须调用EndGetRequestStream方法来完成异步操作
            //通常EndGetRequestStream由callback所引用的方法调用
            Stream requestStream = uploadRequest.EndGetRequestStream(ar);

            FileStream fileStream = File.Open(@"D:\app\result.txt", FileMode.Open);

            byte[] buffer = new byte[1024];
            int bytesRead;
            while (true)
            {
                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;

                //本地的文件流数据写到请求流
                requestStream.Write(buffer, 0, bytesRead);
            }

            requestStream.Close();
            fileStream.Close();

            //开始以异步方式向FTP服务器发送请求并从FTP服务器接收响应
            uploadRequest.BeginGetResponse(new AsyncCallback(EndGetResponseCallback), uploadRequest);
        }

        private void EndGetResponseCallback(IAsyncResult ar)
        {
            FtpWebRequest uploadRequest = (FtpWebRequest)ar.AsyncState;

            //结束由BeginGetResponse启动的挂起的异步操作
            FtpWebResponse uploadResponse = (FtpWebResponse)uploadRequest.EndGetResponse(ar);

            Console.WriteLine(uploadResponse.StatusDescription);
            Console.WriteLine("Upload complete");
        }
        #endregion

        #region 从FTP服务器上下载文件
        /// <summary>
        /// 从当前目录下下载文件
        /// <para>
        /// 如果本地文件存在,则从本地文件结束的位置开始下载.
        /// </para>
        /// </summary>
        /// <param name="fileName">服务器上的文件名称</param>
        /// <param name="localName">本地文件名称</param>
        /// <returns>返回一个值,指示是否下载成功</returns>
        public bool Download(string specifiedPath, string fileName, string localName)
        {
            bool result = false;
            using (FileStream fs = new FileStream(localName, FileMode.OpenOrCreate)) //创建或打开本地文件
            {
                //建立连接
                string url = Host.TrimEnd('/') + specifiedPath + fileName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile);
                request.ContentOffset = fs.Length;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    fs.Position = fs.Length;
                    byte[] buffer = new byte[4096];//4K
                    int count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        fs.Write(buffer, 0, count);
                        count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    }
                    response.GetResponseStream().Close();
                }
                result = true;
            }
            return result;
        }

        public void DownloadToClient(string url, string dir)
        {
            WebClient client = new WebClient();

            client.Credentials = new NetworkCredential(this.UserId, this.Password);

            string Path = dir;   //另存为的绝对路径＋文件名 
            try
            {
                client.DownloadFile(new Uri(url), Path);
            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show("文件下载失败，失败原因:" + ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        public Stream GetDownloadFileStream(string fullPath)
        {

            //建立连接
            string url = Host.TrimEnd('/') + fullPath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile);
            request.ContentOffset = 0;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.GetResponseStream();
            }
        }
        #endregion

        #region 重命名FTP服务器上的文件
        /// <summary>
        /// 文件更名
        /// </summary>
        /// <param name="oldFileName">原文件名</param>
        /// <param name="newFileName">新文件名</param>
        /// <returns>返回一个值,指示更名是否成功</returns>
        public bool Rename(string specifiedPath, string oldFileName, string newFileName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath + oldFileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.Rename);
            request.RenameTo = newFileName;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从当前目录下获取文件列表
        /// <summary>
        /// 获取当前目录下文件列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList(string specifiedPath)
        {
            List<string> result = new List<string>();
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.ListDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上获取文件和文件夹列表

        /// <summary>
        /// 获取详细列表 从FTP服务器上获取文件和文件夹列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFileDetails(string specifiedPath)
        {
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.ListDirectoryDetails);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                //var res = ResolveDetailResponceStream(response.GetResponseStream(), Encoding.UTF8);

                ArrayList al = new ArrayList();
                //中文文件名
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                Regex regex = new Regex(ConstValue.FTP_STREAM_RESOLVE_PATTERN);
                IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Match match = regex.Match(line);
                    var modified = DateTime.ParseExact(match.Groups[1].Value, "MM-dd-yy  hh:mmtt", culture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");
                    long size = (match.Groups[2].Value != "<DIR>") ? long.Parse(match.Groups[2].Value) : 0;
                    bool is_dir = match.Groups[2].Value == "<DIR>";
                    string name = match.Groups[3].Value;

                    al.Add(new
                    {
                        name,
                        is_dir,
                        size,
                        modified
                    });

                    //result.Add(string.Format(
                    //    "{0} size = {1,9}  modified = {2}",
                    //    name, size, modified.ToString("yyyy-MM-dd HH:mm")));
                }
                return al;
            }
        }


        public struct DirectoryItem
        {
            /// <summary>
            /// 上级目录路径（相对路径）
            /// 如：/IT部/
            /// </summary>
            public string ParentRelativePath;

            /// <summary>
            /// 当前路径（相对路径）
            /// 如：/IT部/批售/
            /// </summary>
            public string RelativePath
            {
                get
                {
                    return string.Format("{0}/{1}", ParentRelativePath.TrimEnd('/'), Name);
                }
            }

            /// <summary>
            /// 上级目录路径（绝对路径）
            /// 如：ftp://172.16.77.114:7000/IT部/
            /// </summary>
            public string BaseFullUri;

            /// <summary>
            /// 当前路径（绝对路径）
            /// 如：ftp://172.16.77.114:7000/IT部/批售/
            /// </summary>
            public string AbsolutePath
            {
                get
                {
                    return string.Format("{0}/{1}", BaseFullUri.TrimEnd('/'), Name);
                }
            }

            public string ModifiedTime;
            public bool IsDirectory;
            public string Name;
            public string DisplayName;
            public List<DirectoryItem> Items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <remarks>https://social.msdn.microsoft.com/Forums/vstudio/en-US/7758e7a8-e063-4eec-a6c1-c76a59d02852/listing-all-files-from-ftp-server-using-c</remarks>
        public List<DirectoryItem> GetDirectoryInformation(string address)
        {
            FtpWebRequest request = CreateRequest(address, WebRequestMethods.Ftp.ListDirectoryDetails);

            List<DirectoryItem> returnValue = new List<DirectoryItem>();
            string[] list = null;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (string line in list)
            {
                // Windows FTP Server Response Format
                // ModifiedTime    IsDirectory    Name
                string data = line;

                // Parse date
                string date = data.Substring(0, 17);
                DateTime dateTime = DateTime.ParseExact(date, "MM-dd-yy  hh:mmtt", CultureInfo.GetCultureInfo("en-us"), DateTimeStyles.None);
                data = data.Remove(0, 24);

                // Parse <DIR>
                string dir = data.Substring(0, 5);
                bool isDirectory = dir.Equals("<dir>", StringComparison.InvariantCultureIgnoreCase);
                data = data.Remove(0, 5);
                data = data.Remove(0, 10);

                // Parse name
                string name = data;

                // Create directory info
                DirectoryItem item = new DirectoryItem();
                item.BaseFullUri = address;//new Uri(address);
                item.ModifiedTime = dateTime.ToShortDateString();
                item.IsDirectory = isDirectory;
                item.Name = name;


                item.Items = item.IsDirectory ? GetDirectoryInformation(item.AbsolutePath) : null;

                returnValue.Add(item);
            }

            return returnValue;
        }

        public List<DirectoryItem> GetFtpObjectsRecursively(string specifiedPath)
        {
            List<DirectoryItem> returnValue = new List<DirectoryItem>();
            string[] list = null;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.ListDirectoryDetails);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            //var res = ResolveDetailResponceStream(response.GetResponseStream(), Encoding.UTF8);

            IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");

            foreach (string line in list)
            {
                #region DIR信息处理（一）分割法
                //// Windows FTP Server Response Format
                //// ModifiedTime    IsDirectory    Name
                //string data = line;

                //// Parse date
                //string date = data.Substring(0, 17);
                //DateTime dateTime = DateTime.ParseExact(date, "MM-dd-yy  hh:mmtt", culture, DateTimeStyles.None);
                //data = data.Remove(0, 24);

                //// Parse <DIR>
                //string dir = data.Substring(0, 5);
                //bool isDirectory = dir.Equals("<dir>", StringComparison.InvariantCultureIgnoreCase);
                //data = data.Remove(0, 5);
                //data = data.Remove(0, 10);

                //// Parse name
                //string name = data;
                #endregion

                #region DIR信息处理（二）正则法
                Regex regex = new Regex(ConstValue.FTP_STREAM_RESOLVE_PATTERN);
                Match match = regex.Match(line);

                // Parse date
                string modified = DateTime.ParseExact(match.Groups[1].Value, "MM-dd-yy  hh:mmtt", culture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");

                // Parse <DIR>
                bool is_directory = match.Groups[2].Value == "<DIR>";
                string prefix = is_directory ? "[目录]" : "[文件]";

                // Parse name
                string name = match.Groups[3].Value;
                #endregion

                // Create directory info
                DirectoryItem item = new DirectoryItem();
                item.ParentRelativePath = specifiedPath;
                item.BaseFullUri = url; //new Uri(url);
                item.ModifiedTime = modified;
                item.IsDirectory = is_directory;
                item.Name = name;
                item.DisplayName = prefix + name;
                item.Items = item.IsDirectory ? GetFtpObjectsRecursively(item.RelativePath) : null;

                returnValue.Add(item);

            }

            return returnValue;
        }

        #endregion

        #region 从FTP服务器上删除文件
        /// <summary>
        /// 删除FTP服务器上的文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteFile(string specifiedPath, string fileName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath + fileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.DeleteFile);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 在FTP服务器上创建目录
        /// <summary>
        /// 在当前目录下创建文件夹
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否创建成功</returns>
        public bool CreateDirectory(string specifiedPath, string dirName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath + dirName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.MakeDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上删除目录
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteDirectory(string specifiedPath, string dirName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath + dirName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.RemoveDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上获取文件大小
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public long GetFileSize(string specifiedPath, string fileName)
        {
            long result = 0;
            //建立连接
            string url = Host.TrimEnd('/') + specifiedPath + fileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.GetFileSize);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = response.ContentLength;
            }
            return result;
        }
        #endregion

        #region 给FTP服务器上的文件追加内容
        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="localFile">本地文件</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(string specifiedPath, FileInfo localFile, string remoteFileName)
        {
            if (localFile.Exists)
            {
                using (FileStream fs = new FileStream(localFile.FullName, FileMode.Open))
                {
                    return Append(specifiedPath, fs, remoteFileName);
                }
            }
            throw new Exception(string.Format("本地文件不存在,文件路径:{0}", localFile.FullName));
        }
        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="stream">数据流(可通过设置偏移来实现从特定位置开始上传)</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(string specifiedPath, Stream stream, string remoteFileName)
        {
            bool result = false;
            if (stream != null && stream.CanRead)
            {
                //建立连接
                string url = Host.TrimEnd('/') + specifiedPath + remoteFileName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.AppendFile);
                using (Stream rs = request.GetRequestStream())
                {
                    //上传数据
                    byte[] buffer = new byte[4096];//4K
                    int count = stream.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        rs.Write(buffer, 0, count);
                        count = stream.Read(buffer, 0, buffer.Length);
                    }
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 获取FTP服务器上的当前路径
        /// <summary>
        /// 获取FTP服务器上的当前路径
        /// </summary>
        public string CurrentDirectory
        {
            get
            {
                string result = string.Empty;
                string url = Host.TrimEnd('/') + "/";
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.PrintWorkingDirectory);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    string temp = response.StatusDescription;
                    int start = temp.IndexOf('"') + 1;
                    int end = temp.LastIndexOf('"');
                    if (end >= start)
                    {
                        result = temp.Substring(start, end - start);
                    }
                }
                return result;
            }
        }
        #endregion

        #region 检查当前路径上是否存在某个文件
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileName">要检查的文件名</param>
        /// <returns>返回一个值,指示要检查的文件是否存在</returns>
        public bool CheckFileExist(string specifiedPath, string fileName)
        {
            bool result = false;
            if (fileName != null && fileName.Trim().Length > 0)
            {
                fileName = fileName.Trim();
                List<string> files = GetFileList(specifiedPath);
                if (files != null && files.Count > 0)
                {
                    foreach (string file in files)
                    {
                        if (file.ToLower() == fileName.ToLower())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        private ArrayList ResolveDetailResponceStream(Stream responce_stream, Encoding encoding)
        {
            ArrayList al = new ArrayList();
            StreamReader reader = new StreamReader(responce_stream, encoding);//中文文件名
            Regex regex = new Regex(ConstValue.FTP_STREAM_RESOLVE_PATTERN);
            IFormatProvider culture = CultureInfo.GetCultureInfo("en-us");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                Match match = regex.Match(line);
                DateTime modified =
                   DateTime.ParseExact(
                       match.Groups[1].Value, "MM-dd-yy  hh:mmtt", culture, DateTimeStyles.None);
                long size = (match.Groups[2].Value != "<DIR>") ? long.Parse(match.Groups[2].Value) : 0;
                bool is_dir = match.Groups[2].Value == "<DIR>";
                string name = match.Groups[3].Value;

                al.Add(new
                {
                    name,
                    is_dir,
                    size,
                    modified
                });

                //result.Add(string.Format(
                //    "{0} size = {1,9}  modified = {2}",
                //    name, size, modified.ToString("yyyy-MM-dd HH:mm")));
            }

            return al;
        }


    }
}
