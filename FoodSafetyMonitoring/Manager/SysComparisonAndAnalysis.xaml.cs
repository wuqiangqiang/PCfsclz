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
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using Visifire.Charts;
using Toolkit = Microsoft.Windows.Controls;


namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysComparisonAndAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class SysComparisonAndAnalysis : UserControl
    {
        private string user_flag_tier;
        private string dept_type;
        private readonly List<string> analysisThemes;
        private int flag = 0;
        private DataTable currenttable;
        IOperationContract operationContract = ServiceFactory.GetWcfService<IOperationContract>();
        public SysComparisonAndAnalysis(string depttype)
        {
            InitializeComponent();
            this.dept_type = depttype;
            user_flag_tier = PubClass.userInfo.FlagTier;
            dtpStartDate.SelectedDate = DateTime.Now;
            dtpEndDate.SelectedDate = DateTime.Now;
            analysisThemes = new List<string>() { "-请选择-",
             "各部门检测总量占比分析",
             "各部门阳性样本检出量占比分析",
             "各部门疑似阳性样本检出量占比分析",
            "不同检测项目检测量占比分析",
            "不同检测项目阳性样本检测占比分析",
            "不同检测项目疑似阳性样本检测占比分析"};//初始化变量

            _analysis_theme.ItemsSource = analysisThemes;
            _analysis_theme.SelectedIndex = 0;

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
                        case "各部门检测总量占比分析": function = "p_dbfx_jczl_produce"; break;
                        case "各部门阳性样本检出量占比分析": function = "p_dbfx_jczl_yang_produce"; break;
                        case "各部门疑似阳性样本检出量占比分析": function = "p_dbfx_jczl_like_yang_produce"; break;
                        case "不同检测项目检测量占比分析": function = "p_dbfx_jcxm_produce"; break;
                        case "不同检测项目阳性样本检测占比分析": function = "p_dbfx_jcxmyx_produce"; break;
                        case "不同检测项目疑似阳性样本检测占比分析": function = "p_dbfx_jcxmyx_like_produce"; break;
                        default: break;
                    }
                    break;
                case "1": switch (_analysis_theme.Text)
                    {
                        case "各部门检测总量占比分析": function = "p_dbfx_jczl_fishery"; break;
                        case "各部门阳性样本检出量占比分析": function = "p_dbfx_jczl_yang_fishery"; break;
                        case "各部门疑似阳性样本检出量占比分析": function = "p_dbfx_jczl_like_yang_fishery"; break;
                        case "不同检测项目检测量占比分析": function = "p_dbfx_jcxm_fishery"; break;
                        case "不同检测项目阳性样本检测占比分析": function = "p_dbfx_jcxmyx_fishery"; break;
                        case "不同检测项目疑似阳性样本检测占比分析": function = "p_dbfx_jcxmyx_like_fishery"; break;
                        default: break;
                    }
                    break;
                case "2": switch (_analysis_theme.Text)
                    {
                        case "各部门检测总量占比分析": function = "p_dbfx_jczl_animal"; break;
                        case "各部门阳性样本检出量占比分析": function = "p_dbfx_jczl_yang_animal"; break;
                        case "各部门疑似阳性样本检出量占比分析": function = "p_dbfx_jczl_like_yang_animal"; break;
                        case "不同检测项目检测量占比分析": function = "p_dbfx_jcxm_animal"; break;
                        case "不同检测项目阳性样本检测占比分析": function = "p_dbfx_jcxmyx_animal"; break;
                        case "不同检测项目疑似阳性样本检测占比分析": function = "p_dbfx_jcxmyx_like_animal"; break;
                        default: break;
                    }
                    break;
                default: break;
            }

            table = operationContract.ExecuteProComparisonAnalysis(function, userId,
                                                                   (DateTime) dtpStartDate.SelectedDate,
                                                                   (DateTime) dtpEndDate.SelectedDate);
            switch (_analysis_theme.Text)
            {
                case "各部门检测总量占比分析": table.Columns[0].ColumnName = "部门名称";
                    table.Columns[1].ColumnName = "检测数量";
                    break;
                case "各部门阳性样本检出量占比分析": table.Columns[0].ColumnName = "部门名称";
                    table.Columns[1].ColumnName = "阳性样本数量";
                    break;
                case "各部门疑似阳性样本检出量占比分析": table.Columns[0].ColumnName = "部门名称";
                    table.Columns[1].ColumnName = "疑似阳性样本数量";
                    break;
                case "不同检测项目检测量占比分析": table.Columns.Remove("ItemID");
                    table.Columns[0].ColumnName = "检测项目";
                    table.Columns[1].ColumnName = "检测数量";
                    break;
                case "不同检测项目阳性样本检测占比分析": table.Columns.Remove("ItemID");
                    table.Columns[0].ColumnName = "检测项目";
                    table.Columns[1].ColumnName = "阳性样本数量";
                    break;
                case "不同检测项目疑似阳性样本检测占比分析": table.Columns.Remove("ItemID");
                    table.Columns[0].ColumnName = "检测项目";
                    table.Columns[1].ColumnName = "疑似阳性样本数量";
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

            currenttable = table;

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
            dataSeries.RenderAs = RenderAs.Pie;

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

        void _chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (currenttable != null)
            {
                setChart(currenttable);
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }

        private void _changeSize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (flag == 0)
            {
                content.ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Pixel);
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
