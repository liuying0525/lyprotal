using DZAFCPortal.Config;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using DZAFCPortal.Facade.SSO;
using System.Web.Security;
using System.Web.SessionState;

namespace DZAFCPortal.Web.Client.AjaxPage
{
    /// <summary>
    /// loginHandler 的摘要说明
    /// </summary>
    public class loginHandler : IHttpHandler, IRequiresSessionState
    {
        SSOFacade sso_facade = new SSOFacade();
        public void ProcessRequest(HttpContext context)
        {
            var mode = context.Request.Params["mode"];
            var response_json = "";
            switch (mode)
            {
                case "u_plus_p": response_json = CommonLoginBeta(context); break;
                case "qrcode": response_json = QRLoginWithWechat(context); break;
                case "send_code_via_mail": response_json = SendVerificationCodeBeta(context); break;
                case "verify_code": response_json = VerifyCode(context); break;
                case "submit_modify_pwd": response_json = ForgetPWDSubmitBeta(context); break;
                default:
                    context.Response.Write("error params:" + mode);
                    context.Response.End();
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(response_json);
        }

        #region SSO API 直接返回json

        private string CommonLoginBeta(HttpContext context)
        {
            RSACrypto rsaCrypto = new RSACrypto(ConstValue.AES_KEY_PRIVATE, ConstValue.AES_KEY_PUBLIC);
            var username = rsaCrypto.Decrypt(context.Request["username"]);
            var password = rsaCrypto.Decrypt(context.Request["password"]);

            var model = sso_facade.DoLoginBeta(username, password);

            //登录成功
            if (model.errorNumber == 0 && model.errors.Count == 0)
            {
                var cookie = new HttpCookie(ConstValue.COOKIE_KEY_CURRENT_LOGIN_USER, username);
                cookie.Expires = DateTime.Now.AddMinutes(60 * 8);
                HttpContext.Current.Response.Cookies.Add(cookie);
                //FormsAuthentication..SetAuthCookie(username, false);
                HttpContext.Current.Session.Add($"{ConstValue.SSO.APPLICATION_AUTH_PREFIX_URL}{username}", model.applicationAuths);

                return JsonConvert.SerializeObject(new { IsSucess = true });
            }
            else
            {

                return JsonConvert.SerializeObject(new { IsSucess = false, Message = $"[{model.errorNumber}]{string.Join(";", model.errors)}" });
            }

        }

        private string SendVerificationCodeBeta(HttpContext context)
        {
            var mail = context.Request["mail_address"];

            var access_token = GetApplicationToken(context);

            return sso_facade.SendVerificationCodeViaMailBeta(mail, access_token);
        }

        private string VerifyCode(HttpContext context)
        {
            var mail = context.Request["mail_address"];
            var code = context.Request["vcode"];

            var access_token = GetApplicationToken(context);
            return sso_facade.VerifyCodeViaMailBeta(mail, code, access_token);
        }

        private string ForgetPWDSubmitBeta(HttpContext context)
        {
            var new_pwd = context.Request["new_pwd"];
            var re_new_pwd = context.Request["re_new_pwd"];
            var enduser_token = context.Request["enduser_token"];
            var state = context.Request["state"];

            return sso_facade.ModifyPWDWithFirstLoginBeta(new_pwd, re_new_pwd, enduser_token, state);
        }
        #endregion

        #region Result 对象封装
        private string CommonLogin(HttpContext context)
        {
            RSACrypto rsaCrypto = new RSACrypto(ConstValue.AES_KEY_PRIVATE, ConstValue.AES_KEY_PUBLIC);
            var username = rsaCrypto.Decrypt(context.Request["username"]);
            var password = rsaCrypto.Decrypt(context.Request["password"]);

            var login_result = sso_facade.DoLogin(username, password);

            //登录成功

            if (login_result.IsSucess)
            {
                var cookie = new HttpCookie(ConstValue.COOKIE_KEY_CURRENT_LOGIN_USER, username);
                cookie.Expires = DateTime.Now.AddMinutes(60 * 8);
                HttpContext.Current.Response.Cookies.Add(cookie);
                var ret_model = login_result.Datas as DZAFCPortal.ViewModel.Client.SSOModel.LoginResult_VM;
                //FormsAuthentication..SetAuthCookie(username, false);
                HttpContext.Current.Session.Add($"{ConstValue.SSO.APPLICATION_AUTH_PREFIX_URL}{username}", ret_model.applicationAuths);
            }

            return JsonConvert.SerializeObject(login_result);
        }

        private string SendVerificationCode(HttpContext context)
        {
            var mail = context.Request["mail_address"];

            var access_token = GetApplicationToken(context);

            var res = sso_facade.SendVerificationCodeViaMail(mail, access_token);
            return JsonConvert.SerializeObject(res);
        }

        private string ForgetPWDSubmit(HttpContext context)
        {
            var mail = context.Request["mail_address"];
            var code = context.Request["vcode"];
            var new_pwd = context.Request["new_pwd"];
            var re_new_pwd = context.Request["re_new_pwd"];

            var access_token = GetApplicationToken(context);
            var verify_result = sso_facade.VerifyCodeViaMail(mail, code, access_token);
            if (!verify_result.IsSucess) return JsonConvert.SerializeObject(verify_result);
            else
            {

                var obj = verify_result.Datas as Tuple<string, string>;
                var enduser_token = obj.Item1;
                var state = obj.Item2;

                var modify_result = sso_facade.ModifyPWDWithFirstLogin(new_pwd, re_new_pwd, enduser_token, state);
                return JsonConvert.SerializeObject(modify_result);
            }

        }
        #endregion

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string QRLoginWithWechat(HttpContext context)
        {

            var mail = context.Request["user_mail"];

            DZAFCPortal.Authorization.DAL.UserService userService = new Authorization.DAL.UserService();
            var user = userService.GenericService.FirstOrDefault(u => u.Email == mail);
            if (user != null)
            {
                var cookie = new HttpCookie(ConstValue.COOKIE_KEY_CURRENT_LOGIN_USER, user.Account);
                cookie.Expires = DateTime.Now.AddMinutes(60 * 8);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            context.Response.Redirect($"/Index.aspx");
            context.Response.End();
            return JsonConvert.SerializeObject(new { IsSuccess = (user != null) });
        }
        private string GetApplicationToken(HttpContext context)
        {
            //确认应用token是否存在cookie中
            var exist_token_cookie = context.Request.Cookies.Get(ConstValue.COOKIE_KEY_APPLICATION_OAUTH_TOKEN);
            if (exist_token_cookie == null || exist_token_cookie.Value == null)
            {
                var application_token_obj = sso_facade.RetrieveApplicationAccessToken();
                exist_token_cookie = new HttpCookie(ConstValue.COOKIE_KEY_APPLICATION_OAUTH_TOKEN, application_token_obj.Item1);
                exist_token_cookie.Expires = DateTime.Now.AddSeconds(application_token_obj.Item2);
                HttpContext.Current.Response.Cookies.Add(exist_token_cookie);
            }
            return exist_token_cookie.Value;
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