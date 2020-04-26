/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/8/16 9:44:10
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using DZAFCPortal.Config;
using DZAFCPortal.Utility;
using Newtonsoft.Json;
using NySoftland.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZAFCPortal.ViewModel.Client;

namespace DZAFCPortal.Facade.SSO
{
    public class SSOFacade
    {
        /// <summary>
        /// SSO 登录（普通）
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Result DoLogin(string username, string password)
        {
            Result result = new Result();

            try
            {
                var json_data = JsonConvert.SerializeObject(new
                {
                    clientId = AppSettings.SSOClientID,
                    clientSecret = AppSettings.SSOClientSecret,
                    username,
                    password
                });

                var req_url = AppSettings.SSOHost + ConstValue.SSO.LOGIN_URL;

                var ret_json = HttpRequestHelper.SendHttpRequest(req_url, "post", json_data);

                var model = JsonConvert.DeserializeObject<SSOModel.LoginResult_VM>(ret_json);
                result.IsSucess = (model.errorNumber == 0 && model.errors.Count == 0) ? true : false;
                result.ResultCode = model.errorNumber;
                result.Message = string.Join(";", model.errors);
                result.Datas = model;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = -1;
            }

            return result;

        }
        /// <summary>
        /// SSO 登录（普通）
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public SSOModel.LoginResult_VM DoLoginBeta(string username, string password)
        {
            try
            {
                var json_data = JsonConvert.SerializeObject(new
                {
                    clientId = AppSettings.SSOClientID,
                    clientSecret = AppSettings.SSOClientSecret,
                    username,
                    password
                });

                var req_url = AppSettings.SSOHost + ConstValue.SSO.LOGIN_URL;

                var ret_json = HttpRequestHelper.SendHttpRequest(req_url, "post", json_data);
                return JsonConvert.DeserializeObject<SSOModel.LoginResult_VM>(ret_json);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取应用访问的token
        /// </summary>
        /// <returns></returns>
        public Tuple<string, int> RetrieveApplicationAccessToken()
        {

            var app_token_url = $"{AppSettings.SSOHost}{ConstValue.SSO.RETRIEVE_APP_ACCESS_TOKEN_URL}?client_id={AppSettings.SSOClientID}&client_secret={AppSettings.SSOClientSecret}&scope=read&grant_type=client_credentials";

            var ret_json = HttpRequestHelper.SendHttpRequest(app_token_url, "post");

            if (ret_json.Contains("error") || ret_json.Contains("error_description"))
            {
                var ex = JsonConvert.DeserializeObject<SSOModel.ErrorModel.ErrorResult401_VM>(ret_json);
                throw new Exception($"[{ex.error}]{ex.error_description}");
            }
            else
            {
                var model = JsonConvert.DeserializeObject<SSOModel.ApplicationToken_VM>(ret_json);
                return new Tuple<string, int>(model.access_token, model.expires_in);
            }

        }

        #region 忘记密码
        /// <summary>
        /// 忘记密码【邮件验证码】
        /// </summary>
        /// <param name="mail_address">邮件地址</param>
        /// <param name="access_token">token</param>
        /// <returns></returns>
        public Result SendVerificationCodeViaMail(string mail_address, string access_token)
        {
            Result result = new Result();

            try
            {
                var send_code_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.ForgetPWD.ViaMail.SEND_CODE_URL}?access_token={access_token}&email={mail_address}";
                var ret_json = HttpRequestHelper.SendHttpRequest(send_code_req_url, "post");


                if (ret_json.Contains("errorNumber") && ret_json.Contains("errors"))
                {
                    var model = JsonConvert.DeserializeObject<SSOModel.SendCodeViaEmailResult_VM>(ret_json);
                    //正常:{"errorNumber": 0,"errors": []}
                    //异常:{"errorNumber": 802,"errors": ['xxxx 异常']}
                    result.IsSucess = (model.errorNumber == 0 && model.errors.Count == 0) ? true : false;
                    result.ResultCode = model.errorNumber;
                    result.Message = string.Join(";", model.errors);
                }
                //{"error": "invalid_token","error_description": "Invalid access token: 4b4add9c-e5e1-41b1-a43a-5c6dda959b111"}
                //else if (ret_json.Contains("error_description") && ret_json.Contains("error"))
                //{
                //    var ex = JsonConvert.DeserializeObject<SSOModel.ErrorModel.ErrorResult401_VM>(ret_json);
                //    result.IsSucess = false;
                //    result.ResultCode = -1;
                //    result.Message = $"[{ex.error}]{ex.error_description}";
                //}


            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = -1;
            }
            return result;

        }
        /// <summary>
        /// 忘记密码【邮件验证码】
        /// </summary>
        /// <param name="mail_address"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public string SendVerificationCodeViaMailBeta(string mail_address, string access_token)
        {
            try
            {
                var send_code_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.ForgetPWD.ViaMail.SEND_CODE_URL}?access_token={access_token}&email={mail_address}";
                return HttpRequestHelper.SendHttpRequest(send_code_req_url, "post");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        /// <summary>
        /// 校验邮箱验证码
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <param name="code">验证码</param>
        /// <param name="access_token">应用token</param>
        /// <returns></returns>
        public Result VerifyCodeViaMail(string email, string code, string access_token)
        {
            Result result = new Result();

            try
            {
                var veriy_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.ForgetPWD.ViaMail.CONFIRM_CODE_URL}";

                var json_data = JsonConvert.SerializeObject(new
                {
                    email,
                    code
                });

                var ret_json = HttpRequestHelper.SendHttpRequest(veriy_req_url,
                                                                 "post",
                                                                 json_data,
                                                                 "application/json",
                                                                 new Dictionary<string, string> {
                                                                     { "Authorization", $"bearer {access_token}" }
                                                                 });
                //正常:{"errorNumber":0,"errors":[],"accessToken":"b9108dfe-c715-47c3-92ca-7ddede12121f","tokenType":"bearer","scope":"read","expiresIn":20363,"state":"d575429216ca035176324558757fd198R5xZmDuyEOZ"}
                //异常:{"errorNumber": 439,"errors": ["验证码错误"]}
                if (ret_json.Contains("errorNumber") && ret_json.Contains("errors"))
                {
                    var model = JsonConvert.DeserializeObject<SSOModel.ConfirmCodeResult_VM>(ret_json);

                    result.IsSucess = (model.errorNumber == 0 && model.errors.Count == 0) ? true : false;
                    result.ResultCode = model.errorNumber;
                    result.Datas = new Tuple<string, string>(model.accessToken, model.state);
                    result.Message = string.Join(";", model.errors);
                }
                //{"error": "invalid_token","error_description": "Invalid access token: 4b4add9c-e5e1-41b1-a43a-5c6dda959b111"}
                //else if (ret_json.Contains("error_description") && ret_json.Contains("error"))
                //{
                //    var ex = JsonConvert.DeserializeObject<SSOModel.ErrorModel.ErrorResult401_VM>(ret_json);
                //    result.IsSucess = false;
                //    result.ResultCode = -1;
                //    result.Message = $"[{ex.error}]{ex.error_description}";
                //}
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = ex.Message;
                result.ResultCode = 0;

            }
            return result;

        }
        /// <summary>
        /// 校验邮箱验证码
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <param name="code">验证码</param>
        /// <param name="access_token">应用token</param>
        /// <returns></returns>
        public string VerifyCodeViaMailBeta(string email, string code, string access_token)
        {
            try
            {
                var veriy_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.ForgetPWD.ViaMail.CONFIRM_CODE_URL}";

                var json_data = JsonConvert.SerializeObject(new
                {
                    email,
                    code
                });
                //正常:{"errorNumber":0,"errors":[],"accessToken":"b9108dfe-c715-47c3-92ca-7ddede12121f","tokenType":"bearer","scope":"read","expiresIn":20363,"state":"d575429216ca035176324558757fd198R5xZmDuyEOZ"}
                //异常:{"errorNumber": 439,"errors": ["验证码错误"]}
                return HttpRequestHelper.SendHttpRequest(veriy_req_url,
                                                         "post",
                                                         json_data,
                                                         "application/json",
                                                         new Dictionary<string, string> {
                                                             { "Authorization", $"bearer {access_token}" }
                                                         });

                //return JsonConvert.DeserializeObject<SSOModel.ConfirmCodeResult_VM>(ret_json);

                //return new Tuple<bool, string, string>((model.errorNumber == 0 && model.errors.Count == 0), model.accessToken, model.state);


            }
            catch (Exception ex)
            {
                throw;
                //return new Tuple<bool, string, string>((model.errorNumber == 0 && model.errors.Count == 0), model.accessToken, model.state); ;

            }
        }
        /// <summary>
        /// 首次登录改密码、忘记密码
        /// </summary>
        /// <param name="new_pwd">新密码</param>
        /// <param name="re_new_pwd">重复新密码</param>
        /// <param name="acess_token">终端用户token</param>
        /// <param name="state">校验状态</param>
        /// <returns></returns>
        public Result ModifyPWDWithFirstLogin(string new_pwd,
                                              string re_new_pwd,
                                              string access_token,
                                              string state)
        {
            Result result = new Result();

            try
            {
                var modify_pwd_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.RESET_PASSWORD_URL}";
                var json_data = JsonConvert.SerializeObject(new
                {
                    newPassword = new_pwd,
                    reNewPassword = re_new_pwd,
                    state
                });

                var ret_json = HttpRequestHelper.SendHttpRequest(modify_pwd_req_url,
                                                                 "put",
                                                                 json_data,
                                                                 "application/json",
                                                                 new Dictionary<string, string> {
                                                                     { "Authorization", $"bearer {access_token}" }
                                                                 });
                if (ret_json.Contains("errorNumber") && ret_json.Contains("errors"))
                {
                    var model = JsonConvert.DeserializeObject<SSOModel.FirstLoginModifyResult_VM>(ret_json);

                    //正常: {"errorNumber":0,"errors":[],"accessToken":"2249b19d-4803-4358-8203-90cdb2ea9cc2","tokenType":"bearer","scope":"read","expiresIn":43199}
                    //异常1:{"errorNumber":808,"errors":["newPass must be equal with reNewPass"]}
                    //异常2:{"errorNumber":834,"errors":["参数state不正确: b470978585febab2ead81ddbbf65bc557AjJ1m6EQ6s1"]}
                    result.IsSucess = (model.errorNumber == 0 && model.errors.Count == 0) ? true : false;
                    result.ResultCode = model.errorNumber;
                    result.Message = string.Join(";", model.errors);
                }
                //{ "error":"invalid_token","error_description":"Invalid access token: 56374034-5299-463d-9069-3c91024d4b761"}
                //else if (ret_json.Contains("error_description") && ret_json.Contains("error"))
                //{
                //    var ex = JsonConvert.DeserializeObject<SSOModel.ErrorModel.ErrorResult401_VM>(ret_json);
                //    result.IsSucess = false;
                //    result.ResultCode = -1;
                //    result.Message = $"[{ex.error}]{ex.error_description}";
                //}
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = -1;
            }
            return result;

        }
        /// <summary>
        /// 首次登录改密码、忘记密码
        /// </summary>
        /// <param name="new_pwd">新密码</param>
        /// <param name="re_new_pwd">重复新密码</param>
        /// <param name="acess_token">终端用户token</param>
        /// <param name="state">校验状态</param>
        /// <returns></returns>
        public string ModifyPWDWithFirstLoginBeta(string new_pwd,
                                                  string re_new_pwd,
                                                  string access_token,
                                                  string state)
        {
            try
            {
                var modify_pwd_req_url = $"{ AppSettings.SSOHost}{ConstValue.SSO.RESET_PASSWORD_URL}";
                var json_data = JsonConvert.SerializeObject(new
                {
                    newPassword = new_pwd,
                    reNewPassword = re_new_pwd,
                    state
                });
                //正常: {"errorNumber":0,"errors":[],"accessToken":"2249b19d-4803-4358-8203-90cdb2ea9cc2","tokenType":"bearer","scope":"read","expiresIn":43199}
                //异常1:{"errorNumber":808,"errors":["newPass must be equal with reNewPass"]}
                //异常2:{"errorNumber":834,"errors":["参数state不正确: b470978585febab2ead81ddbbf65bc557AjJ1m6EQ6s1"]}
                return HttpRequestHelper.SendHttpRequest(modify_pwd_req_url,
                                                         "put",
                                                         json_data,
                                                         "application/json",
                                                          new Dictionary<string, string> {
                                                              { "Authorization", $"bearer {access_token}" }
                                                          });
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        #endregion

    }
}
