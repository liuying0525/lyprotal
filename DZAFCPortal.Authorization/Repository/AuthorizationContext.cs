
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace DZAFCPortal.Authorization.Repository
{
    public partial class AuthorizationContext : DbContext
    {
        public AuthorizationContext()
            : base("name=AuthorizationDB")
        {
            //Database.SetInitializer<AuthorizationContext>(new CreateDatabaseIfNotExists());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("DZ_PORTAL");

        }


        #region 系统权限

        public DbSet<Applications> Application { get; set; }

        public DbSet<Module> Module { get; set; }
        public DbSet<Operation> Operation { get; set; }
        //public DbSet<OrganizationPost> OrganizationPost { get; set; }
        // public DbSet<Post> Post { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleOperation> RoleOperation { get; set; }
        public DbSet<RoleUser> RoleUser { get; set; }
        public DbSet<User> User { get; set; }
        //   public DbSet<UserPost> UserPost { get; set; }

        public DbSet<ModuleGroup> ModuleGroup { get; set; }
        public DbSet<ModuleGroupDetail> ModuleGroupDetail { get; set; }

        /// <summary>
        /// 组织表
        /// </summary>
        public DbSet<Organization> Organization { get; set; }

        /// <summary>
        /// 字典表
        /// </summary>
        public DbSet<BnDict> BnDict { get; set; }

        /// <summary>
        /// 字典类别
        /// </summary>
        public DbSet<BnDictType> BnDictType { get; set; }

        /// <summary>
        /// 上级领导设置
        /// 关联用户与上级领导
        /// </summary>
        public DbSet<ImmediateSupervisorSettings> ImmediateSupervisorSettings { get; set; }
        #endregion
    }
}
