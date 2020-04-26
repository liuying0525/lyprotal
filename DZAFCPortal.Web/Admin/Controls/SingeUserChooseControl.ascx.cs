using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class SingeUserChooseControl : System.Web.UI.UserControl
    {
        private string chosenType;

        public string ChosenType
        {
            get { return chosenType; }
            set { chosenType = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region--------页面控件ID----------------
        private string containerID = "container_" + Guid.NewGuid().ToString().Replace('-', '_');
        /// <summary>
        /// 容器ID
        /// </summary>
        public string ContainerID
        {
            get
            {
                return containerID + "_" + chosenType;
            }
        }

        private string ztreeID = "ztree_" + Guid.NewGuid().ToString().Replace('-', '_');
        /// <summary>
        /// 树控件ID
        /// </summary>
        protected string ZtreeID
        {
            get
            {
                return ztreeID;
            }
        }

        private string layerContainerID = "layer_" + Guid.NewGuid().ToString().Replace('-', '_');

        /// <summary>
        /// 
        /// </summary>
        protected string LayerContainerID
        {
            get
            {
                return layerContainerID;
            }
        }
        #endregion

        /// <summary>
        /// 组织树根节点，如果不指定，则取全部节点
        /// </summary>
        public string OrgRootID { get; set; }

        #region--------选中的用户取值、赋值--------

        /// <summary>
        /// 选中的用户ID
        /// </summary>
        public string CheckedUserIds
        {
            get
            {
                return hfdCheckedUserIds.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedUserIds.Value = value;
            }
        }

        /// <summary>
        /// 存储用户ID的隐藏域的ClientID
        /// </summary>
        public string hfdUserIdClientID
        {
            get
            {
                return hfdCheckedUserIds.ClientID;
            }
        }


        /// <summary>
        /// 选中的用户Name
        /// </summary>
        public string CheckedUserNames
        {
            get
            {
                return hfdCheckedUserNames.Value.TrimEnd(',');
            }
            set
            {
                hfdCheckedUserNames.Value = value;
            }
        }

        /// <summary>
        /// 存储用户ID的隐藏域的ClientID
        /// </summary>
        public string hfdUserIdClientNames
        {
            get
            {
                return hfdCheckedUserNames.ClientID;
            }
        }
        #endregion
    }
}