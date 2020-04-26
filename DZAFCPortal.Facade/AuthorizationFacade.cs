using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.ViewModel._01.Authorization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using NySoftland.Core;

namespace DZAFCPortal.Facade
{
    /// <summary>
    /// Authorization view model Generation included:Operation,URL,Module&ModuleGroup
    /// Added by zhanxl 
    /// </summary>
    public class AuthorizationFacade
    {

        private ModuleGroupBLL moduleGroupBll = new ModuleGroupBLL();

        private ModuleBLL moduleBll = new ModuleBLL();

        private OperationBLL operationBll = new OperationBLL();

        private OperationUrlBLL urlBll = new OperationUrlBLL();

        private ApplicationBLL applicationBll = new ApplicationBLL();

        private RoleOperationBLL roleOperationBll = new RoleOperationBLL();

        #region Operation

        /// <summary>
        /// 构造操作管理中的页面展示 数据源
        /// </summary>
        /// <returns></returns>
        public NySoftland.Core.Result GenerateResourceReadOnlyForOperation()
        {
            Result result = new Result();
            List<ModuleGroup_Readonly_Model> moduleGroupList_ViewModel = new List<ModuleGroup_Readonly_Model>();
            try
            {
                var moduleGroupList = moduleGroupBll.GetAllModuleGroup();
                moduleGroupList.ForEach(g =>
                {
                    //初始化Module Group 视图模型 基础属性
                    var moduleGroupObj = new ModuleGroup_Readonly_Model();
                    moduleGroupObj.Instance(g);

                    #region 构造与当前module Group 关联的Modules
                    //Module 实体列表
                    var moduleList = moduleBll.GetModuleListByModuleGroupID(g.ID);

                    //Module 模型列表
                    var moduleList_ViewModel = new List<Module_Readonly_Model>();


                    //Copy(moduleList, moduleList_ViewModel);

                    //使用 Module 实体列表 初始化 Module 模型列表 
                    moduleList.ForEach(m =>
                    {
                        //初始化module 基本属性
                        var module_ViewModel = new Module_Readonly_Model();
                        module_ViewModel.Instance(m);

                        #region 构造与当前module关联的Operations
                        //Operation 模型列表 
                        var operationList_ViewModel = new List<Operation_Readonly_Model>();

                        //Operation 实体列表
                        var operationList = operationBll.GetOperationByModuleID(m.ID);

                        //使用 Operation 实体列表 初始化 Operation 模型列表 
                        operationList.ForEach(o =>
                        {
                            var operation_ViewModel = new Operation_Readonly_Model();
                            operation_ViewModel.Instance(o);
                            operationList_ViewModel.Add(operation_ViewModel);
                        });

                        #endregion

                        //初始化Module 视图模型关联的OperationLst属性
                        module_ViewModel.OperationLst = operationList_ViewModel;

                        moduleList_ViewModel.Add(module_ViewModel);
                    });

                    #endregion

                    //初始化Module Group 视图模型关联的 ModulelLst属性
                    moduleGroupObj.ModulelLst = moduleList_ViewModel;
                    moduleGroupList_ViewModel.Add(moduleGroupObj);
                });


                result.Message = "";
                result.IsSucess = true;
                result.ResultCode = 0;
                //result.Datas = new JavaScriptSerializer().Serialize(moduleGroupList_ViewModel);
                result.Datas = JsonConvert.SerializeObject(moduleGroupList_ViewModel);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = 1;
            }

            return result;
        }

        /// <summary>
        /// 构造操作管理中的操作编辑 数据源
        /// </summary>
        /// <returns></returns>
        public Result GenerateResourceEditForOperation(string moduleID)
        {
            Result result = new Result();
            var specifiedModule = moduleBll.GetModuleByID(moduleID);

            try
            {
                //初始化module 基本属性
                var module_ViewModel = new Module_Readonly_Model();
                module_ViewModel.Instance(specifiedModule);

                #region 构造与当前module关联的Operations. return operationList_ViewModel
                //Operation 模型列表 
                var operationList_ViewModel = new List<Operation_Readonly_Model>();

                //Operation 实体列表
                var operationList = operationBll.GetOperationByModuleID(moduleID);

                //使用 Operation 实体列表 初始化 Operation 模型列表 
                operationList.ForEach(o =>
                {
                    var operation_ViewModel = new Operation_Readonly_Model();
                    operation_ViewModel.Instance(o);
                    operationList_ViewModel.Add(operation_ViewModel);
                });

                #endregion

                #region 构造基础CRUD Operations. return operationBasedList_ViewModel(因数据库表结构暂时不变更,暂时取消基本操作列)
                ////Operation 模型列表 
                //var operationBasedList_ViewModel = new List<Operation_Readonly_Model>();

                ////Operation 实体列表
                //var operationBasedList = operationBll.GetAllOperation(o => o.IsBasedOp && o.IsEnable && !o.IsDelete);

                ////使用 Operation 实体列表 初始化 Operation 模型列表 
                //operationBasedList.ForEach(o =>
                //{
                //    //基础 Operation中排除已选Operation中存在的
                //    if (operationList.FirstOrDefault(op => op.ID == o.ID) == null)
                //    {
                //        var operationBased_ViewModel = new Operation_Readonly_Model();
                //        operationBased_ViewModel.Instance(o);
                //        operationBasedList_ViewModel.Add(operationBased_ViewModel);
                //    }
                //});

                #endregion

                //初始化Module 视图模型关联的OperationLst属性
                module_ViewModel.OperationLst = operationList_ViewModel;

                //初始化Module 视图模型关联的OperationLst属性
                //module_ViewModel.BaseOperationLst = operationBasedList_ViewModel;

                result.Message = "";
                result.IsSucess = true;
                result.ResultCode = 0;
                result.Datas = JsonConvert.SerializeObject(module_ViewModel);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = 1;
            }

            return result;
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        //public Result SaveOperation(string sourceStr, Guid moduleID)
        //{
        //    Result result = new Result { IsSucess = true };
        //    var opsOriginal = operationBll.GetModuleOperation(moduleID);//原有op
        //    var opsCurrent = JsonConvert.DeserializeObject<List<Operation_Readonly_Model>>(sourceStr);//目前op

        //    try
        //    {
        //        var opsToAdd = (from o in opsCurrent
        //                        where string.IsNullOrEmpty(o.ID)
        //                        select o).ToList();

        //        var opsToDel = (from o in opsOriginal
        //                        where !(from checkedOp in opsCurrent select checkedOp.ID).Contains(o.ID.ToString())
        //                        select o).ToList();

        //        var opsToUpdate = (from o in opsCurrent
        //                           where !string.IsNullOrEmpty(o.ID)
        //                           select o).ToList();


        //        if (opsToAdd.Count != 0)
        //        {
        //            int index = opsOriginal.Count == 0 ? 1 : opsOriginal.OrderByDescending(o => o.OrderNum).FirstOrDefault().OrderNum;
        //            opsToAdd.ToList().ForEach(o =>
        //            {
        //                var newid = Guid.NewGuid();
        //                o.ID = newid.ToString();
        //                operationBll.Add(o.ToEntity(index++));

        //                //与管理员角色关联
        //                string[] defaultRoleNames = System.Configuration.ConfigurationManager.AppSettings["DefaultRole"].Split(';');

        //                foreach (string name in defaultRoleNames)
        //                {
        //                    var curRole = new DZAFCPortal.Authorization.DAL.RoleService().FirstOrDefault(r => r.Name == name);
        //                    if (curRole == null)
        //                        continue;

        //                    RoleOperation rel = new RoleOperation
        //                    {
        //                        OperationID = newid,
        //                        RoleID = curRole.ID

        //                    };

        //                    roleOperationBll.Add(rel);
        //                }

        //            });
        //        }

        //        if (opsToDel.Count != 0)
        //        {
        //            opsToDel.ToList().ForEach(o =>
        //            {
        //                operationBll.Remove(o);
        //                roleOperationBll.RemoveRelationWithOpID(o.ID);// 关联Role关系删除
        //                urlBll.RemoveAll(o.ID); //关联url删除
        //            });
        //        }

        //        if (opsToUpdate.Count != 0)
        //        {
        //            int index = opsOriginal.OrderByDescending(o => o.OrderNum).FirstOrDefault().OrderNum;
        //            opsToUpdate.ToList().ForEach(o =>
        //            {
        //                var entity = operationBll.GetByOpeationID(new Guid(o.ID));
        //                entity.Name = o.Name;
        //                operationBll.Update(entity);
        //            });
        //        }


        //        result.Message = "操作修改保存成功!";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSucess = false;
        //        result.Message = "保存异常,详细信息为" + ex.Message;
        //    }
        //    return result;

        //}

        public Result SaveOperation(string sourceStr, string moduleID)
        {
            var op = JsonConvert.DeserializeObject<Operation_Readonly_Model>(sourceStr);
            Result result = new Result { IsSucess = true };
            try
            {
                if (string.IsNullOrEmpty(op.ID))
                {
                    var newid = Guid.NewGuid().ToString();
                    op.ID = newid;
                    operationBll.Add(op.ToEntity());

                    //与管理员角色关联
                    var defaultRoleNames = DZAFCPortal.Config.AppSettings.DefaultRoles;

                    foreach (string name in defaultRoleNames)
                    {
                        var curRole = new DZAFCPortal.Authorization.DAL.RoleService().GenericService.FirstOrDefault(r => r.Name == name);
                        if (curRole == null)
                            continue;

                        RoleOperation rel = new RoleOperation
                        {
                            OperationID = newid,
                            RoleID = curRole.ID

                        };

                        roleOperationBll.Add(rel);
                    }
                }
                else
                {
                    //update
                    var operationEntity = operationBll.GetByOpeationID(op.ID);
                    operationEntity.Name = op.Name;
                    operationEntity.Code = op.Code;
                    operationEntity.OrderNum = op.OrderNum;
                    operationEntity.IsEnable = op.IsEnable;
                    operationEntity.IsDelete = op.IsDelete;
                    operationEntity.ControlID = op.Code;
                    operationBll.Update(operationEntity);
                }
                result.Message = "Success!";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = "保存异常,详细信息为" + ex.Message;
            }
            return result;
        }

        public Result RemoveOperaion(string sourceStr)
        {
            Result result = new Result { IsSucess = true };
            var opsToDel = JsonConvert.DeserializeObject<List<Operation_Readonly_Model>>(sourceStr);

            try
            {
                if (opsToDel.Count != 0)
                {
                    opsToDel.ForEach(o =>
                    {
                        operationBll.Remove(o.ID);
                        roleOperationBll.RemoveRelationWithOpID(o.ID);// 关联Role关系删除
                        urlBll.RemoveAll(o.ID); //关联url删除
                    });
                    result.Message = "操作修改保存成功!";
                }
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = "保存异常,详细信息为" + ex.Message;
            }
            return result;
        }

        #endregion

        #region Url

        /// <summary>
        /// 构造操作管理中的页面展示 数据源
        /// </summary>
        /// <returns></returns>
        public Result GenerateResourceReadOnlyForURL(string operationID)
        {
            Result result = new Result();
            List<URL_Edit_Model> URLList_ViewModel = new List<URL_Edit_Model>();
            try
            {
                var URLList = urlBll.GetAllUrl(u => u.OperationID == operationID).ToList();
                URLList.ForEach(g =>
                {
                    //初始化Operation URL 视图模型 基础属性
                    var url_ViewModel = new URL_Edit_Model();
                    url_ViewModel.Instance(g);
                    URLList_ViewModel.Add(url_ViewModel);
                });


                result.Message = "";
                result.IsSucess = true;
                result.ResultCode = 0;
                //result.Datas = new JavaScriptSerializer().Serialize(moduleGroupList_ViewModel);
                result.Datas = JsonConvert.SerializeObject(URLList_ViewModel);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = 1;
            }

            return result;
        }


        /// <summary>
        /// 保存URL
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public Result SaveUrl(string sourceStr, string opid)
        {
            var urls = JsonConvert.DeserializeObject<List<URL_Edit_Model>>(sourceStr);
            Result result = new Result { IsSucess = true };
            int index = 1;

            try
            {
                index++;
                urlBll.RemoveAll(opid);

                if (urls.Count == 0)
                    return result;

                urls.ForEach(o =>
                {
                    o.ID = Guid.NewGuid().ToString();
                    urlBll.Add(o.ToEntity());
                });
                result.Message = "URL修改保存成功!";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = string.Format("保存第{0}个url时异常,详细信息为", index) + ex.Message;
            }
            return result;
        }
        #endregion

        #region Module Management

        /// <summary>
        /// 模块管理数据源构造
        /// </summary>
        /// <returns></returns>
        public Result GenerateResourceForModuleManagement(bool getApp)
        {
            Result result = new Result();
            List<ModuleGroup_Edit_Model> moduleGroupList_ViewModel = new List<ModuleGroup_Edit_Model>();
            try
            {
                var moduleGroupList = moduleGroupBll.GetAllModuleGroup();
                moduleGroupList.ForEach(g =>
                {
                    //初始化Module Group 视图模型 基础属性
                    var moduleGroupObj = new ModuleGroup_Edit_Model();
                    moduleGroupObj.Instance(g);

                    #region 构造与当前module Group 关联的Modules
                    //Module 实体列表
                    var moduleList = moduleBll.GetModuleListByModuleGroupID(g.ID);

                    //Module 模型列表
                    var moduleList_ViewModel = new List<Module_Edit_Model>();
                    //Copy(moduleList, moduleList_ViewModel);

                    //使用 Module 实体列表 初始化 Module 模型列表 
                    moduleList.ForEach(m =>
                    {
                        //初始化module 基本属性
                        var module_ViewModel = new Module_Edit_Model();
                        module_ViewModel.Instance(m);
                        moduleList_ViewModel.Add(module_ViewModel);
                    });

                    #endregion

                    //初始化Module Group 视图模型关联的 ModulelLst属性
                    moduleGroupObj.ModulelLst = moduleList_ViewModel;
                    moduleGroupList_ViewModel.Add(moduleGroupObj);
                });


                result.IsSucess = true;
                result.ResultCode = 0;
                result.Datas = JsonConvert.SerializeObject(moduleGroupList_ViewModel);
                if (getApp)
                {
                    result.Message = GetApplicationJson();
                }

            }

            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = 1;
            }

            return result;



        }

        /// <summary>
        /// 保存模块组
        /// </summary>
        /// <param name="type">新增or更新</param>
        /// <param name="jsonToDes">需要反序列化的json字符串</param>
        /// <returns></returns>
        public Result SavgModuleGroup(string type, string jsonToDes)
        {
            var group = JsonConvert.DeserializeObject<ModuleGroup_Edit_Model>(jsonToDes);
            Result result = new Result { IsSucess = true };

            try
            {
                if (group != null)
                {
                    var moduleGroup = group.ToEntity();
                    if (type == "Add")
                    {
                        moduleGroupBll.AddModuleGroup(moduleGroup);
                    }
                    else if (type == "Update")
                    {
                        moduleGroupBll.UpdateModuleGroup(moduleGroup);
                    }

                }
                result.Message = "保存成功!";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = "保存异常,详细信息为" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 保存模块
        /// </summary>
        /// <param name="type">新增or更新</param>
        /// <param name="jsonToDes">需要反序列化的json字符串</param>
        /// <returns></returns>
        public Result SaveModule(string type, string jsonToDes)
        {
            var module = JsonConvert.DeserializeObject<Module_Edit_Model>(jsonToDes);
            Result result = new Result { IsSucess = true };

            try
            {
                if (module != null)
                {
                    var m = module.ToEntity();
                    if (type == "Add")
                    {

                        moduleBll.AddModule(m);
                    }
                    else if (type == "Update")
                    {
                        moduleBll.UpdateModule(m);
                    }

                }
                result.Message = "保存成功!";
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.Message = "保存异常,详细信息为" + ex.Message;
            }
            return result;
        }

        public Result Remove(string entityId, bool isModuleGroup)
        {

            Result result = new Result();
            try
            {
                if (isModuleGroup)
                    moduleGroupBll.RemoveModuleGroup(entityId);
                else
                    moduleBll.RemoveModule(entityId);

                result.IsSucess = true;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = ex.InnerException.Message;
                result.IsSucess = false;
            }
            return result;

        }
        #endregion

        #region Application

        /// <summary>
        /// 获取所有的Application
        /// </summary>
        /// <returns></returns>
        public Result GenerateResourceForApplication()
        {
            Result result = new Result();
            List<Application_Simple_Model> applicationList_ViewModel = new List<Application_Simple_Model>();
            try
            {
                var applicationLst = applicationBll.GetAll();
                applicationLst.ForEach(a =>
                {
                    var app = new Application_Simple_Model();
                    app.Instance(a);
                    applicationList_ViewModel.Add(app);
                });

                result.Message = "";
                result.IsSucess = true;
                result.ResultCode = 0;
                //result.Datas = new JavaScriptSerializer().Serialize(moduleGroupList_ViewModel);
                result.Datas = JsonConvert.SerializeObject(applicationList_ViewModel);
            }

            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucess = false;
                result.ResultCode = 1;
            }

            return result;
        }

        /// <summary>
        /// 获取所有的Application ,返回json字符串,私有方法
        /// </summary>
        /// <returns></returns>
        private string GetApplicationJson()
        {
            List<Application_Simple_Model> applicationList_ViewModel = new List<Application_Simple_Model>();

            var applicationLst = applicationBll.GetAll();
            applicationLst.ForEach(a =>
            {
                var app = new Application_Simple_Model();
                app.Instance(a);
                applicationList_ViewModel.Add(app);
            });
            return JsonConvert.SerializeObject(applicationList_ViewModel);
        }


        #endregion

    }
}
