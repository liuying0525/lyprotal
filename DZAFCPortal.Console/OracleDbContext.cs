/*-------------------------------------------------------------------------
 * 作者：詹学良
 * 创建时间： 2019/7/30 13:29:57
 * 版本号：v1.0
 *  -------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Console
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() : base("name=DZPortalDB")
        {

        }
        public DbSet<Console_User> Users { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("DZ_PORTAL");
        }
    }
}
