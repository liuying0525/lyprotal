using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Authorization.BLL
{
    public class ModuleGroupBLL
    {
        ModuleGroupService service = new ModuleGroupService();

        public ModuleGroup GetModelByID(string id)
        {
            return service.GenericService.GetModel(id);
        }

        public List<ModuleGroup> GetAllModuleGroup()
        {
            var groups = service.GenericService.GetAll();
            if (groups == null || groups.Count() == 0)
                return new List<ModuleGroup>();

            return groups.OrderBy(g => g.OrderNum).ToList();
        }

        public void AddModuleGroup(ModuleGroup model)
        {
            service.GenericService.Add(model);
            service.GenericService.Save();
        }

        public void UpdateModuleGroup(ModuleGroup model)
        {
            service.GenericService.Update(model);
            service.GenericService.Save();
        }

        public void RemoveModuleGroup(string id)
        {
            service.GenericService.Delete(id);
            service.GenericService.Save();
        }

    }
}
