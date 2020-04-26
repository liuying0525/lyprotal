using DZAFCPortal.Entity;
using DZAFCPortal.Entity.FTP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Repository
{
    public class DZPortalContext : DbContext
    {
        public DZPortalContext()
            : base("name=DZPortalDB")
        {
            //  Database.SetInitializer<EABPortalContext>(new CreateDatabaseIfNotExists());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DZ_PORTAL");
        }


        /// <summary>
        /// 活动资讯表
        /// </summary>
        public DbSet<ActInformation> ActInformations { get; set; }

        /// <summary>
        /// 我的同事圈(消息)
        /// </summary>
        public DbSet<FriendNews> FriendNewses { get; set; }

        /// <summary>
        /// 朋友圈回复表
        /// </summary>
        public DbSet<FriendNewsReply> FriendNewsReplys { get; set; }

        /// <summary>
        /// 我的朋友列表
        /// </summary>
        public DbSet<MyFriends> MyFriendses { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public DbSet<Attach> Attachs { get; set; }

        #region----------知识园地----------
        /// <summary>
        /// 知识园地 问题项
        /// </summary>
        public DbSet<Kn_Issue> Kn_Issues { get; set; }
        /// <summary>
        /// 问题答复
        /// </summary>
        public DbSet<Kn_Fix> Kn_Fixs { get; set; }
        /// <summary>
        /// 配置处理组
        /// </summary>
        public DbSet<Kn_FixGroup> Kn_FixGroups { get; set; }
        /// <summary>
        /// 组中的人员
        /// </summary>
        public DbSet<Kn_FixPerson> Kn_FixPersons { get; set; }

        #endregion


        /// <summary>
        /// 类别
        /// </summary>
        public DbSet<NewsCategory> NewsCategory { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DbSet<News> News { get; set; }

        /// <summary>
        /// 导航
        /// </summary>
        public DbSet<Navigator> Navigator { get; set; }

        /// <summary>
        /// 活动召集
        /// </summary>
        public DbSet<Activities> Activities { get; set; }


        /// <summary>
        /// 活动报名登记表
        /// </summary>
        public DbSet<ActivityParticipants> ActivityParticipants { get; set; }


        /// <summary>
        /// 活动报名登记表（团队）
        /// </summary>
        public DbSet<ActivitiyTeams> ActivitiyTeams { get; set; }

        /// <summary>
        /// 活动风采
        /// </summary>
        public DbSet<Highlights> Highlights { get; set; }

        /// <summary>
        /// 活动风采记录
        /// </summary>
        public DbSet<HighlightsLikeHistory> HighlightsLikeHistory { get; set; }


        /// <summary>
        /// 站点链接
        /// </summary>
        public DbSet<IndexScroll> IndexScroll { get; set; }

        /// <summary>
        /// 在线选评
        /// </summary>
        public DbSet<OnlineVote> OnlineVote { get; set; }


        /// <summary>
        /// 在线评选选项
        /// </summary>
        public DbSet<OnlineVoteOptions> OnlineVoteOptions { get; set; }


        /// <summary>
        /// 投票内容用户关联表
        /// </summary>
        public DbSet<OnlineVoteRecords> OnlineVoteRecords { get; set; }


        /// <summary>
        /// 常用配置表
        /// </summary>
        public DbSet<CommonLink> CommonLink { get; set; }

        /// <summary>
        /// 常用用户配置表
        /// </summary>
        public DbSet<PageLinkUserConfig> PageLinkUserConfig { get; set; }


        /// <summary>
        /// 消息表
        /// </summary>
        public DbSet<UMS_Message> UMS_Message { get; set; }


        /// <summary>
        /// 信息发布访问表
        /// </summary>
        public DbSet<InforAccess> InforAccess { get; set; }


        /// <summary>
        /// 员工信息管理表
        /// </summary>
        public DbSet<EmployeeInfo> EmployeeInfo { get; set; }

        /// <summary>
        /// 物品信息管理表
        /// </summary>
        public DbSet<CorporateAssetsInfo> CorporateAssetsInfo { get; set; }


        #region FTP
        /// <summary>
        /// FTP 文件
        /// </summary>
        public DbSet<FtpFiles> FtpFiles { get; set; }

        /// <summary>
        /// FTP 文件夹
        /// </summary>
        public DbSet<FtpFolders> FtpFolders { get; set; }

        public DbSet<FtpFoldersOrgsRL> FtpFoldersOrgsRL { get; set; }
        #endregion

    }
}
