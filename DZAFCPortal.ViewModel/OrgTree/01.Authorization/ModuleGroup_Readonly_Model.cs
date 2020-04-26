using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应ModuleGroup实体
    /// 适用范围:列表页查看
    /// </summary>
    public class ModuleGroup_Readonly_Model : IGenerateModel<ModuleGroup>
    {
        #region Properties
        /// <summary>
        /// GUID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// ModuleGroup Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 当前ModuleGroup关联的Module List
        /// </summary>
        public List<Module_Readonly_Model> ModulelLst { get; set; }
        #endregion

        #region IGenerateModel<ModuleGroup> 成员

        public ModuleGroup ToEntity()
        {
            return new ModuleGroup
            {
                ID = this.ID,
                Name = this.Name,
                Icon = this.Icon,
                OrderNum = this.OrderNum,
                
            };
        }

        public void Instance(ModuleGroup entity)
        {
            this.ID = entity.ID.ToString();
            this.Name = entity.Name;
            this.Icon = entity.Icon;
            this.OrderNum = entity.OrderNum;
        }

        #endregion
    }
}
