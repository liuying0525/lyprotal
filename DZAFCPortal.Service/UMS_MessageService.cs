using DZAFCPortal.Entity;
using DZAFCPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class UMS_MessageService 
    {
        DZPortalContext context = new DZPortalContext();

        public void AddMessage(UMS_Message m)
        {
            context.UMS_Message.Add(m);
            context.SaveChanges();
        }
    }
}
