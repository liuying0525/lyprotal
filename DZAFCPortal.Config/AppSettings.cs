using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Config
{
    public class AppSettings
    {
        /// <summary>
        /// SharePoint站点地址，主要用于找到上传服务器
        /// </summary>
        public static string SharePointSiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SharePointSiteUrl"];
            }
        }



        /**************************************Active Directory***************************************/
        /// <summary>
        /// 默认角色
        /// </summary>
        public static List<string> DefaultRoles
        {
            get
            {
                var defaultRole = ConfigurationManager.AppSettings["DefaultRole"];
                if (String.IsNullOrEmpty(defaultRole))
                {
                    throw new Exception("未配置默认角色，请在配置文件中appSettings添加DefaultRole节点");
                }

                return defaultRole.Split(',').ToList();
            }
        }

        /// <summary>
        /// 默认用户
        /// </summary>
        public static List<string> DefaultAccounts
        {
            get
            {
                var defaultAccount = ConfigurationManager.AppSettings["DefaultAccount"];

                if (String.IsNullOrEmpty(defaultAccount))
                {
                    throw new Exception("未配置默认用户，请在配置文件中appSettings添加DefaultAccount节点");
                }

                return defaultAccount.Split(',').ToList();
            }
        }


        /**********************************************Exchange******************************************/

        public static string OWAUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["OWAUrl"];
            }
        }

        public static string SSSAppID
        {
            get
            {
                return ConfigurationManager.AppSettings["SSSAppID"];
            }
        }

        public static string OWA_DLL_Path
        {
            get
            {
                return ConfigurationManager.AppSettings["OWA_DLL_Path"];
            }
        }

        public static string OWA_Address
        {
            get
            {
                return ConfigurationManager.AppSettings["OWA_Address"];
            }
        }


        public static string DefautDomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["DefautDomainName"];
            }
        }

        /**********************************************SMTP********************************************/
        /// <summary>
        /// 邮件提醒是否启用
        /// </summary>
        public static string IsMailEnabled
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultMailHost"];
            }
        }

        /// <summary>
        /// smtp host
        /// </summary>
        public static string SMTPHost
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultMailHost"];
            }
        }

        /// <summary>
        /// smtp 端口号(默认25)
        /// </summary>
        public static string SMTPPort
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultMailPort"];
            }
        }

        /// <summary>
        /// 默认邮件发送账户
        /// </summary>
        public static string DefaultMailAccount
        {
            get
            {
                return ConfigurationManager.AppSettings["FromAccount"];
            }
        }

        /// <summary>
        /// 默认邮件账户密码
        /// </summary>
        public static string DefaultMailPWD
        {
            get
            {
                return ConfigurationManager.AppSettings["FromPWD"];
            }
        }

        /// <summary>
        /// 默认邮件发送邮件地址
        /// </summary>
        public static string DefaultMailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultMailAddress"];
            }
        }

        /// <summary>
        /// 邮件发送频率
        /// </summary>
        public static string MailFrequency
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFrequency"];
            }
        }

        #region 数据库连接



        /// <summary>
        /// A3系统数据库连接
        /// </summary>
        public static string NYA3Conn
        {
            get
            {
                string conn = ConfigurationManager.ConnectionStrings["NYA3Conn"].ToString();
                return conn;
            }
        }




        /// <summary>
        /// A3系统数据库连接
        /// </summary>
        public static string NYPortalConn
        {
            get
            {
                string conn = ConfigurationManager.ConnectionStrings["DZPortalDB"].ToString();
                return conn;
            }
        }
        #endregion

        #region Windows 服务配置

        public static string CronExp
        {
            get
            {
                return ConfigurationManager.AppSettings["CronExp"];
            }
        }

        /// <summary>
        /// 是否开启自动同步
        /// </summary>
        public static bool EnableAutoSync
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSync"].ToString());
            }
        }

        #endregion

        #region SSO 配置

        public static string SSOHost
        {
            get
            {
                return ConfigurationManager.AppSettings["SSOHost"];
            }
        }

        public static string SSOClientID
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientID"];
            }
        }

        public static string SSOClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientSecret"];
            }
        }

        public static string RootExternalID
        {
            get
            {
                return ConfigurationManager.AppSettings["RootExternalID"];
            }
        }
        #endregion

        public class WechatSettings
        {
            public static string CorpID
            {
                get
                {
                    return ConfigurationManager.AppSettings["CorpID"];
                }
            }

            public static string AgentID
            {
                get
                {
                    return ConfigurationManager.AppSettings["AgentID"];
                }
            }

            public static string AppSecret
            {
                get
                {
                    return ConfigurationManager.AppSettings["AppSecret"];
                }
            }

        }
    }
}
