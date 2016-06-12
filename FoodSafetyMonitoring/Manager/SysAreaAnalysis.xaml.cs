﻿using System;
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
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using Toolkit = Microsoft.Windows.Controls;
using Visifire.Charts;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysAreaAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class SysAreaAnalysis : UserControl
    {
        private DataTable currenttable;
        private string dept_type;
        private int flag = 0;
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        private readonly List<string> analysisThemes = new List<string>() { "-请选择-",
            "检测样本来源产地分布(全国)分析", 
            "检测样本来源产地分布(省内)分析",
            "阳性样本检出来源产地分布(全国)分析",
            "阳性样本检出来源产地分布(省内)分析",
            "疑似阳性样本检出来源产地分布(全国)分析",
            "疑似阳性样本检出来源产地分布(省内)分析"};//初始化变量

        public SysAreaAnalysis(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;
            _analysis_theme.ItemsSource = analysisThemes;
            _analysis_theme.SelectedIndex = 0;
            dtpStartDate.SelectedDate = DateTime.Now;
            dtpEndDate.SelectedDate = DateTime.Now;

            _chart.SizeChanged += new SizeChangedEventHandler(_chart_SizeChanged);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (_analysis_theme.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请先选择分析主题!!!");
                return;
            }

            if (dtpStartDate.SelectedDate.Value.Date > dtpEndDate.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始时间大于结束时间，请重新选择！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataTable table = null;
            string userId = PubClass.userInfo.ID;
            string function = "";

            switch (dept_type)
            {
                case "0": switch (_analysis_theme.Text)
                    {
                        case "检测样本来源产地分布(全国)分析": function = "p_qyfx_provice_produce"; break;
                        case "检测样本来源产地分布(省内)分析": function = "p_qyfx_city_produce"; break;
                        case "阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_yang_produce"; break;
                        case "阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_yang_produce"; break;
                        case "疑似阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_like_yang_produce"; break;
                        case "疑似阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_like_yang_produce"; break;
                        default: break;
                    }
                    break;
                case "1": switch (_analysis_theme.Text)
                    {
                        case "检测样本来源产地分布(全国)分析": function = "p_qyfx_provice_fishery"; break;
                        case "检测样本来源产地分布(省内)分析": function = "p_qyfx_city_fishery"; break;
                        case "阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_yang_fishery"; break;
                        case "阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_yang_fishery"; break;
                        case "疑似阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_like_yang_fishery"; break;
                        case "疑似阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_like_yang_fishery"; break;
                        default: break;
                    }
                    break;
                case "2": switch (_analysis_theme.Text)
                    {
                        case "检测样本来源产地分布(全国)分析": function = "p_qyfx_provice_animal"; break;
                        case "检测样本来源产地分布(省内)分析": function = "p_qyfx_city_animal"; break;
                        case "阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_yang_animal"; break;
                        case "阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_yang_animal"; break;
                        case "疑似阳性样本检出来源产地分布(全国)分析": function = "p_qyfx_provice_like_yang_animal"; break;
                        case "疑似阳性样本检出来源产地分布(省内)分析": function = "p_qyfx_city_like_yang_animal"; break;
                        default: break;
                    }
                    break;
                default: break;
            }

            table = operationContract.ExecuteProQyfx(function, userId, (DateTime) dtpStartDate.SelectedDate,
                                                     (DateTime) dtpEndDate.SelectedDate);
            switch (_analysis_theme.Text)
            {
                case "检测样本来源产地分布(全国)分析": table.Columns[0].ColumnName = "来源省市";
                                                       table.Columns[1].ColumnName = "检测数量"; 
                                                       break;
                case "检测样本来源产地分布(省内)分析": table.Columns[0].ColumnName = "省内来源市(州)";
                                                       table.Columns[1].ColumnName = "检测数量";
                                                       break;
                case "阳性样本检出来源产地分布(全国)分析": table.Columns[0].ColumnName = "来源省市";
                                                           table.Columns[1].ColumnName = "阳性检出数量";
                                                           break;
                case "阳性样本检出来源产地分布(省内)分析": table.Columns[0].ColumnName = "省内来源市(州)";
                                                           table.Columns[1].ColumnName = "阳性检出数量";
                                                           break;
                case "疑似阳性样本检出来源产地分布(全国)分析": table.Columns[0].ColumnName = "来源省市";
                                                           table.Columns[1].ColumnName = "疑似阳性检出数量";
                                                           break;
                case "疑似阳性样本检出来源产地分布(省内)分析": table.Columns[0].ColumnName = "省内来源市(州)";
                                                           table.Columns[1].ColumnName = "疑似阳性检出数量";
                                                           break;
                default: break;
            }

            table.Columns.Add("占比(%)", Type.GetType("System.String"));
            double sum = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sum += Convert.ToDouble(table.Rows[i][1].ToString());
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i][2] = Math.Round(Convert.ToDouble(table.Rows[i][1].ToString()) / sum, 4, MidpointRounding.AwayFromZero) * 100 + "%";
            }
           
            //计算报表总条数
            int row_count = 0;

            if (table.Rows.Count != 0)
            {
                table.Rows.Add(new object[] { "合计", sum, "100%" });

                row_count = table.Rows.Count - 1;
            }
            else
            {
                row_count = 0;
            }

            currenttable = table;

            _title.Text = _analysis_theme.Text;
            _title_2.Text = _analysis_theme.Text;
            //为防止查询时报表宽度为0
            if (flag == 1)
            {
                content.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                content.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/lleft.png"));
                flag = 0;
            }
            _tableview.SetDataTable(table, "", new List<int>());

            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title_1.Text = row_count.ToString();

            if (row_count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //赋值曲线图
            setChart(table);

        }

        void _chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (currenttable != null)
            {
                setChart(currenttable);
            }
        }

        private void setChart(DataTable table)
        {
            _chart.Children.Clear();
            Chart chart = new Chart();
            chart.Background = Brushes.Transparent;
            chart.View3D = true;
            chart.Bevel = true;
            Title title = new Title();
            title.Text = string.Format("数据统计时间：{0}年{1}月{2}日到{3}年{4}月{5}日", dtpStartDate.SelectedDate.Value.Year, dtpStartDate.SelectedDate.Value.Month, dtpStartDate.SelectedDate.Value.Day,
                         dtpEndDate.SelectedDate.Value.Year, dtpEndDate.SelectedDate.Value.Month, dtpEndDate.SelectedDate.Value.Day);
            title.FontFamily = new FontFamily("微软雅黑");
            title.FontSize = 12;
            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.StackedColumn;


            for (int i = 0; i < table.Rows.Count - 1; i++)
            {
                DataPoint point = new DataPoint();
                point.AxisXLabel = table.Rows[i][0].ToString();
                point.YValue = Convert.ToDouble(table.Rows[i][1].ToString());
                point.LabelStyle = LabelStyles.Inside;
                point.LabelText = table.Rows[i][0].ToString();
                point.LabelFontFamily = new FontFamily("微软雅黑");
                point.LabelFontSize = 12;
                dataSeries.DataPoints.Add(point);
            }
            chart.Series.Add(dataSeries);
            chart.Titles.Add(title);
            _chart.Children.Add(chart);
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }

        private void _changeSize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (flag == 0)
            {
                content.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/rright.png"));
                flag = 1;
            }
            else if (flag == 1)
            {
                content.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                content.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/lleft.png"));
                flag = 0;
            }
        }
        private void _changeSize_MouseEnter(object sender, MouseEventArgs e)
        {
            if (flag == 0)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/lleft_pressed.png"));
                _image.Width = 25;
                _image.Height = 25;
            }
            else if (flag == 1)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/rright_pressed.png"));
                _image.Width = 25;
                _image.Height = 25;
            }
        }

        private void _changeSize_MouseLeave(object sender, MouseEventArgs e)
        {
            if (flag == 0)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/lleft.png"));
                _image.Width = 21;
                _image.Height = 21;
            }
            else if (flag == 1)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/rright.png"));
                _image.Width = 21;
                _image.Height = 21;
            }
        }
    }
}
