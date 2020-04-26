using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class ModuleBLL
    {
        ModuleService service = new ModuleService();
        public List<Module> GetModuleList(string appName)
        {
            Applications app = new ApplicationBLL().GetApplicationsByCode(appName);
            if (app == null)
            {
                return null;
            }
            return service.GenericService.GetAll(p => p.IsDelete == false && p.IsEnable == true && p.ApplicationID == app.ID).OrderBy(p => p.OrderNum).ToList();
        }

        /// <summary>
        /// 根据Module Group ID获取module edit by zhanxl
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Module> GetModuleListByModuleGroupID(string groupID)
        {
            var modules = service.GenericService.GetAll(m => m.ModuleGroup_ID == groupID && m.IsEnable);
            if (modules == null || modules.Count() == 0)
                return new List<Module>();

            return modules.OrderBy(m => m.OrderNum).ToList();
        }


        public Module GetModuleByID(string id)
        {
            return service.GenericService.GetModel(id);
        }


        public void AddModule(Module model)
        {
            service.GenericService.Add(model);
            service.GenericService.Save();
        }

        public void UpdateModule(Module model)
        {
            service.GenericService.Update(model);
            service.GenericService.Save();
        }

        public void RemoveModule(string id)
        {
            service.GenericService.Delete(id);
            service.GenericService.Save();
        }


    }
}
