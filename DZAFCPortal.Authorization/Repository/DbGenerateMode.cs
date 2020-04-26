using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Authorization.Repository
{
    public enum DbGenerateType
    {
        CreateIfDbNotExists,
        Always,
        Changed
    }
    public class DbGenerateMode
    {
        /// <summary>
        /// 初始化数据库生成模式
        /// </summary>
        /// <param name="type"></param>
        public static void InitDbGenerateModel(DbGenerateType type)
        {
            if (type == DbGenerateType.CreateIfDbNotExists)
            {
                Database.SetInitializer<AuthorizationContext>(new CreateDatabaseIfNotExists());
            }
            else if (type == DbGenerateType.Always)
            {
                Database.SetInitializer<AuthorizationContext>(new DropCreateDatabaseAlways());
            }
            else if (type == DbGenerateType.Changed)
            {
                Database.SetInitializer<AuthorizationContext>(new DropCreateDatabaseIfModelChanges());
            }
        }
    }

    public class CreateDatabaseIfNotExists : System.Data.Entity.CreateDatabaseIfNotExists<AuthorizationContext>
    {
        protected override void Seed(AuthorizationContext context)
        {
            InitData.InitDbData(context);
        }
    }

    public class DropCreateDatabaseAlways : System.Data.Entity.DropCreateDatabaseAlways<AuthorizationContext>
    {
        protected override void Seed(AuthorizationContext context)
        {
            InitData.InitDbData(context);
        }
    }

    public class DropCreateDatabaseIfModelChanges : System.Data.Entity.DropCreateDatabaseIfModelChanges<AuthorizationContext>
    {
        protected override void Seed(AuthorizationContext context)
        {
            InitData.InitDbData(context);
        }
    }


}
