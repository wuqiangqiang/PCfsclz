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
using System.Windows.Shapes;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// NewOrModifyItemProduce.xaml 的交互逻辑
    /// </summary>
    public partial class NewOrModifyItemFishery: Window
    {
        private string itemId;
        SysItemQueryFishery item;
        private int flag = 0;
        private System.Data.DataTable dt_flag = new System.Data.DataTable();
        private string userId = PubClass.userInfo.ID;
        private string deptId = PubClass.userInfo.DepartmentID;
        IDetectContract detectContract = ServiceFactory.GetWcfService<IDetectContract>();

        public NewOrModifyItemFishery(SysItemQueryFishery item_query, string item_id)
        {
            InitializeComponent();
            flag = 0;
            this.itemId = item_id;
            this.item = item_query;

            //赋值是否启用下拉选择框
            dt_flag.Columns.Add(new DataColumn("flagid"));
            dt_flag.Columns.Add(new DataColumn("flagname"));
            var row2 = dt_flag.NewRow();
            row2["flagid"] = "1";
            row2["flagname"] = "启用";
            dt_flag.Rows.Add(row2);
            var row3 = dt_flag.NewRow();
            row3["flagid"] = "0";
            row3["flagname"] = "禁用";
            dt_flag.Rows.Add(row3);
            ComboboxTool.InitComboboxSource(_openflag, dt_flag, "lr");

            DataTable table = detectContract.GetDetItemAll("f", item_id);

            if(table.Rows.Count != 0)
            {
                this._itemname.Text = table.Rows[0][0].ToString();
                //赋值是否启用
                for (int i = 0; i < _openflag.Items.Count; i++)
                {
                    if ((_openflag.Items[i] as Label).Tag.ToString() == table.Rows[0][1].ToString())
                    {
                        _openflag.SelectedItem = _openflag.Items[i];
                        break;
                    }
                }
            }

        }

        public NewOrModifyItemFishery(SysItemQueryFishery item_query)
        {
            InitializeComponent();
            flag = 1;
            this.item = item_query;

            //赋值是否启用下拉选择框
            dt_flag.Columns.Add(new DataColumn("flagid"));
            dt_flag.Columns.Add(new DataColumn("flagname"));
            var row2 = dt_flag.NewRow();
            row2["flagid"] = "1";
            row2["flagname"] = "启用";
            dt_flag.Rows.Add(row2);
            var row3 = dt_flag.NewRow();
            row3["flagid"] = "0";
            row3["flagname"] = "禁用";
            dt_flag.Rows.Add(row3);
            ComboboxTool.InitComboboxSource(_openflag, dt_flag, "lr");
            _openflag.SelectedIndex = 1;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_itemname.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入检测项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_openflag.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择是否启用！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (flag == 0)
            {

                bool flag1 = detectContract.UpdateDetect("f", _itemname.Text.Trim(),
                                                        (_openflag.SelectedItem as Label).Tag.ToString(), itemId);
                if (flag1)
                {
                    Toolkit.MessageBox.Show("检测项目修改成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    item.refresh();
                    this.Close();
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("检测项目修改失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else 
            {
                bool flag2 = detectContract.AddDetect("f", _itemname.Text,(_openflag.SelectedItem as Label).Tag.ToString(), userId,DateTime.Now);
                if(flag2)
                {
                    Toolkit.MessageBox.Show("检测项目新增成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    item.refresh();
                    this.Close();
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("检测项目新增失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }
    }
}
