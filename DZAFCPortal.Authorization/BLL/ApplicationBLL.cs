using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.BLL
{
    public class ApplicationBLL
    {
        ApplicationService service = new ApplicationService();
        public Applications GetApplicationsByName(string name)
        {
            return service.GenericService.FirstOrDefault(p => p.Name == name);
        }

        public Applications GetApplicationsByCode(string name)
        {
            return service.GenericService.FirstOrDefault(p => p.Code == name);
        }

        public List<Applications> GetAll()
        {
            var source = service.GenericService.GetAll();
            if (source == null || source.Count() == 0)
            {
                return new List<Applications>();
            }
            return source.ToList();
        }
    }
}
