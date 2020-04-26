using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Entity;

namespace DZAFCPortal.ViewModel
{
    public class NavigateViewModel : IGenerateModel<Navigator>
    {

        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Url { get; set; }
        public int DeepNumber { get; set; }
        //public string ViewLevel { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int OrderNum { get; set; }
        public bool IsShow { get; set; }
        public string ApplyRoles { get; set; }

        public bool IsOpenedNewTab { get; set; }

        #region IGenerateModel<Navigator> 成员

        public Navigator ToEntity()
        {
            return new Navigator
            {
                ID = this.ID,
                ParentID = this.ParentID,
                Title = this.Name,
                Url = this.Url,
                EnglishName = this.EnglishName,
                DeepNumber = this.DeepNumber,
                ViewLevel = 1,
                Description = this.Description,
                IconUrl = this.IconUrl,
                OrderNum = this.OrderNum,
                Enabled = this.IsShow,
                ApplyRoles = this.ApplyRoles,
                IsOpenedNewTab = this.IsOpenedNewTab
            };
        }

        public void Instance(Navigator entity)
        {
            this.ID = entity.ID;
            this.ParentID = entity.ParentID;
            this.Name = entity.Title;
            this.EnglishName = entity.EnglishName;
            this.Url = entity.Url;
            this.DeepNumber = entity.DeepNumber;
            this.Description = entity.Description;
            this.IconUrl = entity.IconUrl;
            this.OrderNum = entity.OrderNum;
            this.IsShow = entity.Enabled;
            this.ApplyRoles = entity.ApplyRoles;
            this.IsOpenedNewTab = entity.IsOpenedNewTab;
        }

        #endregion
    }
}
