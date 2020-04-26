using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Authorization.BLL;

namespace DZAFCPortal.ViewModel._01.Authorization
{
    /// <summary>
    /// 简介:对应OperationURL实体
    /// 适用范围:编辑
    /// </summary>
    public class URL_Edit_Model : IGenerateModel<OperationUrl>
    {
        /// <summary>
        /// URL ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// URL 
        /// </summary>
        public string UrlPath { get; set; }

        /**********************************前端操作属性****************************************/
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Operation ID
        /// </summary>
        public string OperationID { get; set; }


        #region IGenerateModel<OperationUrl> 成员

        public OperationUrl ToEntity()
        {
            return new OperationUrl
            {
                ID = this.ID,
                URL = this.UrlPath,
                OperationID = this.OperationID
            };
        }

        public void Instance(OperationUrl entity)
        {
            this.ID = entity.ID;
            this.UrlPath = entity.URL;
            this.OperationID = entity.OperationID;
        }
        #endregion

        public URL_Edit_Model()
        {
            IsChecked = false;
        }
    }
}
