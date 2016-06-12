using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.dao;
using FoodSafetyMonitoring.Common;
namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysRolePowerManager.xaml 的交互逻辑
    /// </summary>
    public partial class SysRolePowerManager : UserControl
    {
        private int user_flag_tier;
        private DataTable currentTable = new DataTable();
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        public SysRolePowerManager()
        {
            InitializeComponent();
            user_flag_tier = int.Parse(PubClass.userInfo.FlagTier);
            BindListView();
            ShowAllRight();
        }

        private void BindListView()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format(" roleLevel >= {0}", user_flag_tier) );
                DataTable dt = operationContract.GetClientRole(sb.ToString());
                lvlist.DataContext = dt;
                currentTable = dt;
            }
            catch (Exception)
            {
                return;
            }
        }

        public void ShowAllRight()
        {
            DataTable dt = GetMenuTableByMenuRight();
            if (dt != null)
            {
                TreeItem tree = new TreeItem();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["fkSubId"].ToString() == "0")//加载第一级菜单
                    {
                        TreeItem ti = new TreeItem();
                        ti.tag = dt.Rows[i]["subId"].ToString().Trim(); ;
                        ti.text = dt.Rows[i]["subName"].ToString().Trim();
                        ti.parent = tree;
                        LoadTree(dt, ti);//递归函数
                    }
                }
                tvPermissions.ItemsSource = tree.children;
            }
        }

        /// <summary> 根据菜单权限得到系统菜单表
        /// </summary>
        /// <param name="menuCode">菜单权限</param>
        /// <returns></returns>
        public DataTable GetMenuTableByMenuRight()
        {

            try
            {
                DataTable dt = operationContract.GetRoleSub();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void LoadTree(DataTable dt, TreeItem ti)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["fkSubId"].ToString().Trim() == ti.tag.ToString())
                {
                    TreeItem tiTmp = new TreeItem();
                    tiTmp.tag = dt.Rows[i]["subId"].ToString();


                    tiTmp.text = dt.Rows[i]["subName"].ToString();
                    tiTmp.parent = ti;
                    LoadTree(dt,tiTmp);
                }
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            //声明一个变量：用于记录该父菜单是否有子菜单
            int flag = 0;

            string id = (sender as Button).Tag.ToString();

            List<string> list = GetUserRoleMenu(id);

            //循环用户的权限列表
            foreach (string item in list)
            {
                //循环菜单树形结构
                foreach (var ti in tvPermissions.Items)//一级菜单
                {
                    flag = 0;
                    foreach (var tii in ((TreeItem)ti).children)//二级菜单
                    {
                        flag = 1;
                        foreach (var tiii in tii.children)//三级菜单
                        {
                            flag = 2;

                            if ((tiii as TreeItem).tag.ToString() == item)
                            {
                                (tiii as TreeItem).IsChecked = true;
                                goto con_for;
                            }
                        }

                        //只有当二级菜单无子菜单的情况下，才对二级菜单进行打勾
                        if (flag == 1)
                        {
                            if ((tii as TreeItem).tag.ToString() == item)
                            {
                                (tii as TreeItem).IsChecked = true;
                                goto con_for;
                            }
                        }
                    }
                    //只有当父菜单无子菜单的情况下，才对父菜单进行打勾
                    if (flag == 0)
                    {
                        if ((ti as TreeItem).tag.ToString() == item)
                        {
                            (ti as TreeItem).IsChecked = true;
                            goto con_for;
                        }
                    }
                }
                con_for : continue;
            }

            btnSave.Tag = id;
        }

        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="id">角色代码</param>
        private List<string> GetUserRoleMenu(string id)
        {
            List<string> list = new List<string>();
            try
            {
                DataTable dt = operationContract.GetUserRoleMenu(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["subId"].ToString());
                }
            }
            catch (Exception)
            {

            }
            return list;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Tag != null)
            {
                List<string> list_roleMenu = new List<string>();

                foreach (TreeItem ti in tvPermissions.Items)//遍历第一级目录
                {
                    if (ti.IsChecked == true || ti.IsChecked == null)
                    {
                        list_roleMenu.Add(ti.tag.ToString());
                    }

                    foreach (TreeItem tii in ti.children)//遍历第二级目录
                    {
                        if (tii.IsChecked == true || tii.IsChecked == null)
                        {
                            list_roleMenu.Add(tii.tag.ToString());
                        }
 
                        foreach (TreeItem tiii in tii.children)//遍历第三级目录
                        {
                            if (tiii.IsChecked == true)
                            {
                                list_roleMenu.Add(tiii.tag.ToString());
                            }
                        }
                    }
                }

                StringBuilder s_list_roleMenu = new StringBuilder();
                for (int i = 0; i < list_roleMenu.Count; i++)
                {
                    s_list_roleMenu.Append(list_roleMenu[i]);
                    if (i < list_roleMenu.Count - 1)
                    {
                        s_list_roleMenu.Append(",");
                    }
                }

                try
                {
                    int count = operationContract.ExecuteFunRoleSub(btnSave.Tag.ToString(), s_list_roleMenu.ToString());
                    if (count != 0)
                    {
                        Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK);
                        Common.SysLogEntry.WriteLog("角色权限管理", PubClass.userInfo.ShowName, Common.OperationType.Modify, "修改角色权限");
                        return;
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK);
                        return;
                    }
                }
                catch (Exception)
                {
                    Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK);
                    return;
                }
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            this.btnSave.Tag = null;

            foreach (TreeItem item in tvPermissions.Items)
            {
                DiguiClearChecked(item);
            }
        }

        /// <summary>
        /// 递归取消选中
        /// </summary>
        /// <param name="ti"></param>
        private void DiguiClearChecked(TreeItem ti)
        {
            if (ti.children.Count > 0)
            {
                foreach (TreeItem tii in ti.children)
                {
                    if (tii.IsChecked == true)
                    {
                        tii.IsChecked = false;
                    }

                    DiguiClearChecked(tii);
                }
            }
            else
            {
                if (ti.IsChecked == true)
                {
                    ti.IsChecked = false;
                }
            }
        }
    }

    public class TreeItem : INotifyPropertyChanged
    {
        // 构造函数
        public TreeItem()
        {
            children = new ObservableCollection<TreeItem>();
        }

        public object tag
        {
            get;
            set;
        }

        //////////////////////////////////////////////////////////////////////////
        // 节点文字信息
        public string text
        {
            get;
            set;
        }
        // 节点图标路径
        public string itemIcon
        {
            get;
            set;
        }
        // 节点其他信息
        // ...

        public bool isTheChild(TreeItem p)
        {
            bool result = false;
            for (int i = 0; i < p.children.Count; i++)
            {
                if (p.children[i] == this)
                {
                    result = true;
                }
            }
            return result;
        }

        // 父节点
        private TreeItem _parent;
        public TreeItem parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                if (!isTheChild(value))
                {
                    value.children.Add(this);
                    this._parent = value;
                }
            }
        }

        // 子节点
        public ObservableCollection<TreeItem> children
        {
            get;
            set;
        }
        //////////////////////////////////////////////////////////////////////////

        ////////   Check 相关信息   ///////////////////////////////////////////
        bool? _isChecked = false;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
            {
                foreach (TreeItem child in children)
                {
                    child.SetIsChecked(_isChecked, true, false);
                }
            }

            if (updateParent && parent != null)
            {
                parent.VerifyCheckState();
            }

            this.OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.children.Count; ++i)
            {
                bool? current = this.children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }
        ////////////////////////////////////////////////////////////////////////////


        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //////////////////////////////////////////////////////////////////////////
    }
}
