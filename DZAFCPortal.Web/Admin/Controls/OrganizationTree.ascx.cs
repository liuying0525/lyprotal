using DZAFCPortal.Utility;
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Controls
{
    public partial class OrganizationTree : System.Web.UI.UserControl
    {
        public OrganizationTree()
        {
            IsShowOrgCheckBox = true;
            IsShowUrerNode = true;

            SelectedOrgs = String.Empty;
            SelectedUsers = String.Empty;

            RootID = Guid.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var root = CreateRootTree();

                if (root != null)
                {
                    AppendChildren(ref root);

                    tv.Nodes.Add(root);
                }
            }
        }

        #region-------公共属性----------
        /// <summary>
        /// 选中的组织
        /// </summary>
        public string SelectedOrgs { get; set; }

        /// <summary>
        /// 选中的人员
        /// </summary>
        public string SelectedUsers { get; set; }

        /// <summary>
        /// 根节点，若不指定，则查找最顶级组织节点
        /// </summary>
        public Guid RootID { get; set; }
        /// <summary>
        /// 是否显示用户节点
        /// 默认为 不显示
        /// </summary>
        public bool IsShowUrerNode { get; set; }

        /// <summary>
        /// 标志是否显示组织的复选框
        /// </summary>
        public bool IsShowOrgCheckBox { get; set; }
        #endregion

        #region--------私有属性------------
        private string[] SelectedOrgList
        {
            get
            {
                if (String.IsNullOrEmpty(SelectedOrgs))
                {
                    return new string[0];
                }
                return SelectedOrgs.Split(',');
            }
        }

        private string[] SelectedUserList
        {
            get
            {
                if (String.IsNullOrEmpty(SelectedUsers))
                {
                    return new string[0];
                }
                return SelectedUsers.Split(',');
            }
        }
        #endregion

        #region----------Service 私有变量---------
        DZAFCPortal.Authorization.DAL.OrganizationService oService = new DZAFCPortal.Authorization.DAL.OrganizationService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
        #endregion

        #region-------对于各节点的生成 [私有方法]-------
        /// <summary>
        /// 创建根节点
        /// </summary>
        /// <returns></returns>
        private TreeNode CreateRootTree()
        {
            Organization rootItem;
            if (RootID != null && RootID != Guid.Empty)
                rootItem = oService.GenericService.GetModel(RootID);
            else
                rootItem = oService.GenericService.FirstOrDefault(p => p.ParentID == null || p.ParentID == ConstValue.EMPTY_GUID_STR);
            if (rootItem != null)
            {
                var root = CreateOrgNode(rootItem);

                return root;
            }
            else return null;
        }

        /// <summary>
        /// 递归创建子节点
        /// </summary>
        /// <param name="pNode"></param>
        private void AppendChildren(ref TreeNode pNode)
        {
            var id = GetValueID(pNode.Value).ToString();

            if (IsShowUrerNode)
            {
                var users = userService.GenericService.GetAll(p => p.OrganizationID == id).ToList();
                foreach (var item in users)
                {
                    var cNode = CreateUserNode(item);
                    pNode.ChildNodes.Add(cNode);
                    if (cNode.Checked)
                    {
                        Expand(cNode);
                    }
                }
            }

            var childrenNodes = oService.GenericService.GetAll(p => p.ParentID == id).ToList();
            //如果没有子级部门，递归开始返回
            if (childrenNodes == null || childrenNodes.Count <= 0)
                return;

            foreach (var item in childrenNodes)
            {
                var cNode = CreateOrgNode(item);
                pNode.ChildNodes.Add(cNode);
                if (cNode.Checked)
                {
                    Expand(cNode);
                }

                //开始递归
                AppendChildren(ref cNode);
            }
        }

        /// <summary>
        /// 生成组织节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TreeNode CreateOrgNode(Organization item)
        {
            var node = new TreeNode();
            node.Text = item.Name;
            node.Value = item.ID + ",1";
            node.ImageUrl = @"\Scripts\images\organization_tree.ico";

            if (IsShowOrgCheckBox)
            {
                if (SelectedOrgList.Contains(item.ID))
                    node.Checked = true;
            }

            return node;
        }

        /// <summary>
        /// 生成用户节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TreeNode CreateUserNode(User item)
        {
            var node = new TreeNode();
            node.Text = item.DisplayName;
            node.Value = item.ID + ",2";
            node.ImageUrl = @"\Scripts\images\user_tree.ico";

            if (SelectedUserList.Contains(item.ID))
                node.Checked = true;

            return node;
        }

        private string GetValueID(string value)
        {
            var values = value.Split(',');
            return values[0];
        }

        /// <summary>
        /// 获取值的类型
        /// 1:  组织结构
        /// 2： 人员
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetValueType(string value)
        {
            var values = value.Split(',');
            return int.Parse(values[1]);
        }

        private void Expand(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expanded = true;

                Expand(node.Parent);
            }
        }
        #endregion

        #region--------公共方法---------
        /// <summary>
        /// 获取选中的组织
        /// 多项以 ，  隔开
        /// </summary>
        /// <returns></returns>
        public string GetSelectedOrgs()
        {
            string val = String.Empty;
            foreach (TreeNode node in tv.CheckedNodes)
            {
                //表示为组织结构
                if (GetValueType(node.Value) == 1)
                {
                    val += GetValueID(node.Value) + ",";
                }
            }
            val = val.TrimEnd(',');

            return val;
        }

        /// <summary>
        /// 获取选中的用户ID
        /// 多项以 , 隔开
        /// </summary>
        /// <returns></returns>
        public string GetSelectedUsers()
        {
            string val = String.Empty;
            foreach (TreeNode node in tv.CheckedNodes)
            {
                //表示为组织结构
                if (GetValueType(node.Value) == 2)
                {
                    val += GetValueID(node.Value) + ",";
                }
            }
            val = val.TrimEnd(',');

            return val;
        }
        #endregion

        /// <summary>
        /// 点击，当前项选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tv_SelectedNodeChanged(object sender, EventArgs e)
        {
            var current = tv.SelectedNode;
            current.Checked = !current.Checked;
        }
    }
}