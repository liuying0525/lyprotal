/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/5 17:59:36
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Utility
{
    public class ConstValue
    {
        #region Common

        /// <summary>
        /// Guid.Empty 字符串
        /// </summary>
        public const string EMPTY_GUID_STR = "00000000-0000-0000-0000-000000000000";


        #endregion

        public class SSO
        {
            #region 忘记密码
            public class ForgetPWD
            {
                public class ViaMail
                {
                    public const string SEND_CODE_URL = "/api/application/portal/send/email";
                    public const string CONFIRM_CODE_URL = "/api/application/portal/email/confirm_code";
                }
                public class ViaSMS
                {
                    public const string SEND_CODE_URL = "/api/application/portal/sms/send_code";
                    public const string CONFIRM_CODE_URL = "/api/application/portal/sms/confirm_code";
                }
            }
            #endregion

            /// <summary>
            /// 登陆
            /// POST/REST
            /// </summary>
            public const string LOGIN_URL = "/public/enduser/login";
            /// <summary>
            /// 登陆，密码加密
            /// POST/REST
            /// </summary>
            public const string LOGIN_SECURE_URL = "/public/enduser/enc_login";
            /// <summary>
            /// 登出
            /// </summary>
            public const string LOGOUT_URL = "/api/enduser/portal/logout";

            public const string RESET_PASSWORD_URL = "/api/enduser/portal/first_login";

            public const string CHECK_IF_FIRST_LOGIN_URL = "/enduser/portal/first_login";
            
            public const string MODIFY_PASSWORD_URL = "/api/enduser/portal/user_info/modify_password";

            public const string APPLICATION_AUTH_PREFIX_URL = "AUTH_APP_";

            /// <summary>
            /// 获取应用访问的access token
            /// e.g. https://sso.dongzhengafc.com:8081/oauth/token?client_id=d7aaa26837dc16dc12e56a660bd63cca2GxiCra7x8w&client_secret=vYISJ6Wkg7UANQLwoVwhoHI6bPEkzbK1vtmdep1OAi&scope=read&grant_type=client_credentials
            /// </summary>
            public const string RETRIEVE_APP_ACCESS_TOKEN_URL = "/oauth/token";

            /// <summary>
            /// 获取sso 组织
            /// e.g. https://sso.dongzhengafc.com:8081/api/application/organization/list?access_token=74b83852-c374-4b75-9426-55b51c10d02b&id=3&isIncludeChild=true
            /// </summary>
            public const string RETRIEVE_ORGANIZATION_URL = "/api/application/organization/list";

            /// <summary>
            /// 获取sso 用户
            /// e.g. https://sso.dongzhengafc.com:8081/api/application/enterprise/account/all?access_token=74b83852-c374-4b75-9426-55b51c10d02b&ouUuid=cec395a2ae06e06654705d863180643dO7w7hyIJIbq&limit=50
            /// </summary>
            public const string RETRIEVE_USER_URL = "/api/application/enterprise/account/all";
        }

        public class WechatAPI
        {
            /// <summary>
            /// 获取access token
            /// GET
            /// request e.g. https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=wwb7fe0465bb9ec8a4&corpsecret=7N_Tw_EboefYyeWl8b5gCVph31eNqOZvS5Jk-z8oWGg
            /// response e.g. {"errcode": 0,"errmsg": "ok","access_token": "_FfenRsOCzgN3A8f7DiTrT2XQhgP806z-ziSYqJykIDdMYPXbITiS8aSTOuHHEs2WEdJvviKdaTJBI7O4kU7eULvihRO5UU832bicdF84ev-HqVzW735qu_70KY_urnFJWoT1hPwyn6LIZVP7ICZbyb5aWSzouXzSugZtgGxz6IHS24MGg4ZyRg6gLy2xFUW8RWN2ymuwKxGJ43F8X-QEg","expires_in": 7200}
            /// </summary>
            public const string RETRIEVE_ACCESS_TOKEN_URL_TEMP = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

            /// <summary>
            /// 生成二维码
            /// GET
            /// requst e.g. https://open.work.weixin.qq.com/wwopen/sso/qrConnect?appid=wwb7fe0465bb9ec8a4&agentid=1000012&redirect_uri=https://api.dongzhengafc.com/test&state=20190926
            /// response e.g.  https://api.dongzhengafc.com/test/?code=YFP2wc_Y-6d4nblzp5TWDq4YZHvxVzCkM0ZD-3OaLFI&state=20190926&appid=wwb7fe0465bb9ec8a4
            /// </summary>
            public const string GENERATE_QR_CODE_URL_TEMP = "https://open.work.weixin.qq.com/wwopen/sso/qrConnect?appid={0}&agentid={1}&redirect_uri={2}&state={3}";

            /// <summary>
            /// 获取访问用户身份
            /// GET
            /// request e.g. https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=_FfenRsOCzgN3A8f7DiTrT2XQhgP806z-ziSYqJykIDdMYPXbITiS8aSTOuHHEs2WEdJvviKdaTJBI7O4kU7eULvihRO5UU832bicdF84ev-HqVzW735qu_70KY_urnFJWoT1hPwyn6LIZVP7ICZbyb5aWSzouXzSugZtgGxz6IHS24MGg4ZyRg6gLy2xFUW8RWN2ymuwKxGJ43F8X-QEg&code=dg23WnpvxhnEjB7AZ6G4W_uxbVpiRqAeqLk8iWANcS4
            /// response e.g. {"UserId": "zhanxl@dongzhengafc.com","DeviceId": "","errcode": 0,"errmsg": "ok"}
            /// </summary>
            public const string RETRIEVE_WECHAT_USER_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}";
        }

        #region 加密解密

        public const string AES_KEY_PUBLIC = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCk4dIe4Kqke8gc2G9SjhVuwzIi
            fwYJh3LC718R2kBcRnAULFfAB8gL6kNd7bb98hfgQgNiH7ft8KNeCihPjRziZZoe
            uWZelk2sl9rfBUsgnAsuukjUQ+zo2Yzqq9T+p4TF2olhBjlvnsQSDhAH+YhYCp8W
            m5mfKgwkqM3k+cqaaQIDAQAB";

        public const string AES_KEY_PRIVATE = @"
        MIICWwIBAAKBgQCk4dIe4Kqke8gc2G9SjhVuwzIifwYJh3LC718R2kBcRnAULFfA
        B8gL6kNd7bb98hfgQgNiH7ft8KNeCihPjRziZZoeuWZelk2sl9rfBUsgnAsuukjU
        Q+zo2Yzqq9T+p4TF2olhBjlvnsQSDhAH+YhYCp8Wm5mfKgwkqM3k+cqaaQIDAQAB
        AoGABG4w+DVvQGY3FVdXfm9k8gn6seSZ4+2ozsYh1tf3fMNDxbb/UKCk5nUQBFkb
        3qz/qT3820kg5xrdOOyq8qBHE189zUi7hu2K4ro2G8YUbBUbmUEWZtiNizCLHT5D
        WKjlH26WzhAOtXRAqN7kZrmjw6I36zZ+iFvEdwhHiMLC/IECQQDHktVeyUVS5tZW
        pxVpwh/2LXoArJ39E3m2k+XXlyIXDoTyKWGMUCWWTh9f3PwpuAu4+EKrFDPmWlYx
        fly4i4h5AkEA04ADpKKsFC7UWctEa2N5FY7cmkKG9vvHgpVTITP8zUJO58JR7A+9
        ByLG/RjKGQKOnHAa92QBPqHYPj4U7pEFcQJARfXlMmsgECW3sXy09vQEPEpI4H+i
        ipsPKb/C7MagPrDqTfHPrl5SLtSIDxTqmL1Z7qeox0w3vWKhoIwJePABEQJAXpeP
        iY+TxMcZBDwMErd/jSvC8F82u7nqBjZA4sW8mBTC85aOSzTSxyE/vCzdHohtPfxJ
        o0Gf6OI7s8LSW8ySkQJAMRu0ZYHfDHQnNoc8jWHyOUGOW093B0dQ8cz0dr2D7Kf+
        vI3uljdRHtOWOX8SEq+T3ZiuXUq1f9imD4sssAIfVQ==";
        #endregion

        #region FTP

        /// <summary>
        /// 解析windows 操作系统下FTP返回的流的正则
        /// </summary>
        public const string FTP_STREAM_RESOLVE_PATTERN = @"^(\d+-\d+-\d+\s+\d+:\d+(?:AM|PM))\s+(<DIR>|\d+)\s+(.+)$";

        #endregion

        #region Key Name

        public const string COOKIE_KEY_CURRENT_LOGIN_USER = "CURRENT_LOGIN_USER";

        public const string COOKIE_KEY_APPLICATION_OAUTH_TOKEN = "APP_OAUTH_TOKEN";

        #endregion
    }
}
