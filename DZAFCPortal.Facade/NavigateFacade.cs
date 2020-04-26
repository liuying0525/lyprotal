using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.ViewModel;
using DZAFCPortal.Service;
using NySoftland.Core;
using Newtonsoft.Json;

namespace DZAFCPortal.Facade
{
    public class NavigateFacade
    {
        private NavigateService navService = new NavigateService();

        /// <summary>
        /// 加载所有导航
        /// </summary>
        /// <returns></returns>
        public Result LoadAllNavigator()
        {
            Result result = new Result();
            List<NavigateViewModel> navs_ViewModel = new List<NavigateViewModel>();
            try
            {
                var NavList = navService.GenericService.GetAll().OrderBy(n => n.OrderNum).ToList();
                NavList.ForEach(g =>
                {
                    var navObj = new NavigateViewModel();
                    navObj.Instance(g);
                    navs_ViewModel.Add(navObj);
                });

                result.IsSucess = true;
                result.ResultCode = 0;
                result.Datas = JsonConvert.SerializeObject(navs_ViewModel);
                //result.Message = GetNavTypesJson();
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
        /// 保存
        /// </summary>
        /// <param name="type">新增or更新</param>
        /// <param name="jsonToDes">需要反序列化的json字符串</param>
        /// <returns></returns>
        public Result SaveNavigator(string type, string jsonToDes)
        {

            var nav = JsonConvert.DeserializeObject<NavigateViewModel>(jsonToDes);
            Result result = new Result();

            try
            {
                if (nav != null)
                {
                    var m = nav.ToEntity();
                    if (type == "Add")
                    {
                        navService.GenericService.Add(m);
                        navService.GenericService.Save();
                    }
                    else if (type == "Update")
                    {
                        navService.GenericService.Update(m);
                        navService.GenericService.Save();
                    }
                }
                result.IsSucess = true;
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
        /// 删除
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Result Remove(string entityId)
        {
            Result result = new Result();
            try
            {
                navService.GenericService.Delete(entityId);
                navService.GenericService.Save();

                result.IsSucess = true;
                result.Message = "删除成功";
            }
            catch (Exception ex)
            {
                result.Message = "删除失败,ex:" + ex.Message;
                result.IsSucess = false;
            }
            return result;

        }
    }
}
