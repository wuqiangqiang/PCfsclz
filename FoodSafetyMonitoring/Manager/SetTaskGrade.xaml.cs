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
using DBUtility;
using System.Data;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SetTaskGrade.xaml 的交互逻辑
    /// </summary>
    public partial class SetTaskGrade : Window
    {
        private string gradeId;
        private string deptId;
        private DataTable currentTable;
        private SysTaskCheck systaskcheck;
        ISysTaskContract sysTaskContract = ServiceFactory.GetWcfService<ISysTaskContract>();
        public SetTaskGrade(string grade_id, string dept_id, DataTable current_table,SysTaskCheck systaskcheck)
        {
            InitializeComponent();

            this.systaskcheck = systaskcheck; 

            gradeId = grade_id;

            deptId = dept_id;

            currentTable = current_table;

            string grade_up;
            string grade_down;

            switch (gradeId)
            {
                case "1": _grade_up.Text = "100";
                    _grade_up.IsEnabled = false;
                    break;
                case "5": _grade_down.Text = "0";
                    _grade_down.IsEnabled = false;
                    break;
                default: break;
            }
            string[] sArray = sysTaskContract.GetTaskParameter(PubClass.userInfo.DepartmentID, grade_id).Split('_');
            foreach (var s in sArray)
            {
                _grade_up.Text = s[0].ToString();
                _grade_down.Text = s[1].ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_grade_down.Text == "" || _grade_up.Text == "") 
            {
                _txtmsg.Text = "*请输入参数！*";
                return;
            }

            if (int.Parse(_grade_up.Text) <= int.Parse(_grade_down.Text))
            {
                _txtmsg.Text = "*参数下限值必须小于上限值！";
                return;
            }

            if (int.Parse(_grade_up.Text) >100 || int.Parse(_grade_down.Text) > 100)
            {
                _txtmsg.Text = "*参数值必须小于100！";
                return;
            }

            bool exit_flag = sysTaskContract.ExistsGradeId(deptId, gradeId);

            if (exit_flag)
            {                bool n = sysTaskContract.UpdateDeptGrade(_grade_down.Text, _grade_up.Text, PubClass.userInfo.ID,
                                                         DateTime.Now, deptId, gradeId);
                if (n)
                {
                    Toolkit.MessageBox.Show("检测任务指标更新成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    systaskcheck.loadTaskGrade();
                    this.Close();
                }
                else
                {
                    Toolkit.MessageBox.Show("检测任务指标更新失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                bool n = sysTaskContract.AddDeptGrade(deptId, gradeId, _grade_down.Text, _grade_up.Text,
                                                      PubClass.userInfo.ID, DateTime.Now);
                if (n)
                {
                    Toolkit.MessageBox.Show("检测任务指标插入成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    systaskcheck.loadTaskGrade();
                    this.Close();
                }
                else
                {
                    Toolkit.MessageBox.Show("检测任务指标插入失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Grade_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void Grade_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Grade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        //isDigit是否是数字
        public static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))

                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                    return false;
            }
            return true;
        }
    }
}
