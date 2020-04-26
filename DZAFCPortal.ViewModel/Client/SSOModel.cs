/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/11/18 13:27:27
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.ViewModel.Client
{
    public class SSOModel
    {
        /// <summary>
        /// 调用登录接口返回的数据模型
        /// </summary>
        public class LoginResult_VM
        {
            public int errorNumber { get; set; }

            public List<string> errors { get; set; }

            public string accessToken { get; set; }

            public string tokenType { get; set; }

            public string scope { get; set; }

            public string expiresIn { get; set; }

            public string needModifyPwd { get; set; }

            public string state { get; set; }
            public string needBindPhone { get; set; }
            public List<ApplicationAuths_VM> applicationAuths { get; set; }
            public string refreshToken { get; set; }
            public string secret { get; set; }


        }

        /// <summary>
        /// 拥有访问权限的应用
        /// </summary>
        public class ApplicationAuths_VM
        {
            public string name { get; set; }
            public string applicationId { get; set; }
            public string applicationUuid { get; set; }
            public string idpApplicationId { get; set; }
            public string logoUuid { get; set; }
            public string logoUrl { get; set; }
            public string startUrl { get; set; }
            public string createTime { get; set; }
            public string description { get; set; }
            public string enabled { get; set; }
            public List<string> supportDeviceTypes { get; set; }
            public string existAccountLinking { get; set; }
            public string enableTwoFactor { get; set; }
            public string classifyUuid
            {
                get; set;
            }

            public List<string> applicationUsernames { get; set; }

            public string display { get; set; }
        }

        /// <summary>
        /// 获取应用访问的access token
        /// </summary>
        public class ApplicationToken_VM
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string scope { get; set; }
        }

        /// <summary>
        /// 发送邮件验证码结果
        /// </summary>
        public class SendCodeViaEmailResult_VM {
            public int errorNumber { get; set; }

            public List<string> errors { get; set; }
        }

        /// <summary>
        /// 验证码校验结果
        /// </summary>
        public class ConfirmCodeResult_VM
        {
            public int errorNumber { get; set; }

            public List<string> errors { get; set; }

            public string accessToken { get; set; }

            public string tokenType { get; set; }
            public string scope { get; set; }
            public int expiresIn { get; set; }
            public string state { get; set; }
        }

        /// <summary>
        /// 忘记密码、首次登陆改密码结果
        /// </summary>
        public class FirstLoginModifyResult_VM {
            public int errorNumber { get; set; }
            public List<string> errors { get; set; }
            public string accessToken { get; set; }
            public string tokenType { get; set; }
            public string scope { get; set; }
            public int expiresIn { get; set; }
        }

        public class ErrorModel
        {

            /// <summary>
            /// 验证码校验结果[异常]
            /// </summary>
            public class ErrorResult200_VM
            {
                public int errorNumber { get; set; }

                public List<string> errors { get; set; }

            }

            /// <summary>
            /// 获取应用访问的access token[异常]
            /// </summary>
            public class ErrorResult401_VM
            {
                public string error { get; set; }
                public string error_description { get; set; }
            }
        }
    }
}
