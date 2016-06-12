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
using System.Windows.Interop;
using System.Windows.Forms.Integration;
using FoodSafetyMonitoring.Manager;
using System.Data;

namespace FoodSafetyMonitoring
{
    /// <summary>
    /// ChildMenu.xaml 的交互逻辑
    /// 
    /// </summary>
    public partial class ChildMenu : UserControl
    {
        private List<MyChildMenu> childMenus;
        public ChildMenu(List<MyChildMenu> childMenus)
        {
            InitializeComponent();
            this.childMenus = childMenus;
            //定义数组存放：二级菜单外部大控件
            Expander[] expanders = new Expander[] { _expander_0, _expander_1, _expander_2, _expander_3, _expander_4, _expander_5, _expander_6 };

            //先让所有控件都可见
            for (int i = 0; i < 7; i++)
            {
                expanders[i].Visibility = Visibility.Visible;
            }
            //再根据二级菜单的个数隐藏部门控件
            for (int i = childMenus.Count; i < 7; i++)
            {
                expanders[i].Visibility = Visibility.Hidden;
            }
            //加载二、三级菜单
            loadMenu();
        }

        public void loadMenu()
        {
            //定义数组存放：三级菜单的控件
            Grid[] grids = new Grid[] { _grid_0, _grid_1, _grid_2, _grid_3, _grid_4, _grid_5, _grid_6 };
            //定义数组存放：二级菜单的控件
            TextBlock[] texts = new TextBlock[] { _text_0, _text_1, _text_2, _text_3, _text_4, _text_5, _text_6 };
            //先将三级菜单控件进行清空
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].Children.Clear();
            }

            for (int i = 0; i < childMenus.Count; i++)
            {
                //二级菜单文字
                texts[i].Text = childMenus[i].name;

                //三级菜单
                int j = 0;
                foreach (DataRow row in childMenus[i].child_childmenu)
                {
                    //插入一行
                    grids[i].RowDefinitions.Add(new RowDefinition());
                    grids[i].RowDefinitions[j].Height = new GridLength(35, GridUnitType.Pixel);

                    //插入button
                    childMenus[i].buttons[j].SetValue(Grid.RowProperty, j);
                    grids[i].Children.Add(childMenus[i].buttons[j]);

                    j = j + 1;
                }
            }
        }

    }

    public class MyChildMenu
    {
        //public Button btn;
        public List<Button> buttons;
        public string name;
        MainWindow mainWindow;
        public TabControl tab;
        public DataRow[] child_childmenu;
        TabItem temptb = null;

        public MyChildMenu(string name, MainWindow mainWindow, DataRow[] child_childmenu)
        {
            this.name = name;
            this.mainWindow = mainWindow;
            this.child_childmenu = child_childmenu;
            this.tab = mainWindow._tab;
            buttons = new List<Button>();


            foreach (DataRow row in child_childmenu)
            {
                Button btn = new Button();
                btn.Content = row["subName"].ToString();
                btn.Tag = row["subId"].ToString();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += new RoutedEventHandler(this.btn_Click);
                buttons.Add(btn);
            }
        }

        private void btn_Click(object sender, System.EventArgs e)
        {
            //(sender as Button).Foreground = Brushes.White;
            int flag_exits = 0;
            foreach (TabItem item in tab.Items)
            {
                if (item.Tag.ToString() == (sender as Button).Tag.ToString())
                {
                    tab.SelectedItem = item;
                    flag_exits = 1;
                    break;
                }
            }
            if (flag_exits == 0)
            {
                mainWindow.grid_Component.Visibility = Visibility.Visible;
                int flag = 0;
                temptb = new TabItem();
                temptb.Tag = (sender as Button).Tag.ToString();
                switch ((sender as Button).Tag.ToString())
                {
                    //首页
                    case "1": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcMainPage();
                        flag = 1;
                        break;

                    //数据采集->农产品->检测样本管理
                    case "F0101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysSampleQueryProduce();
                        flag = 1;
                        break;
                    //数据采集->水产品->检测样本管理
                    case "F0201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysSampleQueryFishery();
                        flag = 1;
                        break;
                    //数据采集->畜产品->检测样本管理
                    case "F0301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysSampleQueryAnimal();
                        flag = 1;
                        break;

                    //数据采集->农产品->检测项目管理
                    case "G0101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysItemQueryProduce();
                        flag = 1;
                        break;
                    //数据采集->水产品->检测项目管理
                    case "G0201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysItemQueryFishery();
                        flag = 1;
                        break;
                    //数据采集->畜产品->检测项目管理
                    case "G0301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysItemQueryAnimal();
                        flag = 1;
                        break;

                    //数据采集->农产品->检测信息录入
                    case "20101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysNewDetectProduce();
                        flag = 1;
                        break;
                    //数据采集->水产品->检测信息录入
                    case "20201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysNewDetectFishery();
                        flag = 1;
                        break;
                    //数据采集->畜产品->检测信息录入
                    case "20301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysNewDetectAnimal();
                        flag = 1;
                        break;
                    
                    //信息查询->农产品->检测信息查询
                    case "30101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysNewDetectProduceQuery();
                        flag = 1;
                        break;
                    //信息查询->水产品->检测信息查询
                    case "30201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysNewDetectFisheryQuery();
                        flag = 1;
                        break;
                    //信息查询->畜产品->检测信息查询
                    case "30301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysNewDetectAnimalQuery();
                        flag = 1;
                        break;
                    
                    //数据统计->农产品->日报表
                    case "40101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysDayReport("0"); 
                        flag = 1;
                        break;
                    //数据统计->农产品->月报表
                    case "40102": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysMonthReport("0"); 
                        flag = 1;
                        break;
                    //数据统计->农产品->自定义报表
                    case "40103": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysYearReport("0"); 
                        flag = 1;
                        break;
                    //数据统计->水产品->日报表
                    case "40201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysDayReport("1");
                        flag = 1;
                        break;
                    //数据统计->水产品->月报表
                    case "40202": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysMonthReport("1");
                        flag = 1;
                        break;
                    //数据统计->水产品->自定义报表
                    case "40203": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysYearReport("1");
                        flag = 1;
                        break;
                    //数据统计->畜产品->日报表
                    case "40301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysDayReport("2");
                        flag = 1;
                        break;
                    //数据统计->畜产品->月报表
                    case "40302": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysMonthReport("2");
                        flag = 1;
                        break;
                    //数据统计->畜产品->自定义报表
                    case "40303": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysYearReport("2");
                        flag = 1;
                        break;

                    //数据分析->农产品->对比分析
                    case "50101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysComparisonAndAnalysis("0"); 
                        flag = 1;
                        break;
                    //数据分析->农产品->趋势分析
                    case "50102": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysTrendAnalysis("0"); 
                        flag = 1;
                        break;
                    //数据分析->农产品->区域分析
                    case "50103": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysAreaAnalysis("0"); 
                        flag = 1;
                        break;
                    //数据分析->农产品->任务完成率分析
                    case "50104": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysTaskReport("0");
                        flag = 1;
                        break;
                    //数据分析->水产品->对比分析
                    case "50201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysComparisonAndAnalysis("1");
                        flag = 1;
                        break;
                    //数据分析->水产品->趋势分析
                    case "50202": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysTrendAnalysis("1");
                        flag = 1;
                        break;
                    //数据分析->水产品->区域分析
                    case "50203": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysAreaAnalysis("1");
                        flag = 1;
                        break;
                    //数据分析->水产品->任务完成率分析
                    case "50204": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysTaskReport("1");
                        flag = 1;
                        break;
                    //数据分析->畜产品->对比分析
                    case "50301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysComparisonAndAnalysis("2");
                        flag = 1;
                        break;
                    //数据分析->畜产品->趋势分析
                    case "50302": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysTrendAnalysis("2");
                        flag = 1;
                        break;
                    //数据分析->畜产品->区域分析
                    case "50303": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysAreaAnalysis("2");
                        flag = 1;
                        break;
                    //数据分析->畜产品->任务完成率分析
                    case "50304": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysTaskReport("2");
                        flag = 1;
                        break;

                    //任务考评->农产品->设置任务量
                    case "60101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new UcSetTask("0");
                        flag = 1;
                        break;
                    //任务考评->农产品->考评报表
                    case "60102": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysTaskCheck("0");
                        flag = 1;
                        break;
                    //任务考评->水产品->设置任务量
                    case "60201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new UcSetTask("1");
                        flag = 1;
                        break;
                    //任务考评->水产品->考评报表
                    case "60202": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysTaskCheck("1");
                        flag = 1;
                        break;
                    //任务考评->畜产品->设置任务量
                    case "60301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new UcSetTask("2");
                        flag = 1;
                        break;
                    //任务考评->畜产品->考评报表
                    case "60302": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysTaskCheck("2");
                        flag = 1;
                        break;

                    //风险预警->农产品->实时风险
                    case "70101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysWarningInfo("0");
                        flag = 1;
                        break;
                    //风险预警->农产品->预警复核
                    case "70102": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysReviewInfoProduce();
                        flag = 1;
                        break;
                    //风险预警->农产品->复核日志
                    case "70103": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysReviewLog("0");
                        flag = 1;
                        break;
                    //风险预警->农产品->预警数据统计
                    case "70104": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysWarningReport("0");
                        flag = 1;
                        break;

                    //风险预警->水产品->实时风险
                    case "70201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysWarningInfo("1");
                        flag = 1;
                        break;
                    //风险预警->水产品->预警复核
                    case "70202": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysReviewInfoFishery();
                        flag = 1;
                        break;
                    //风险预警->水产品->复核日志
                    case "70203": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysReviewLog("1");
                        flag = 1;
                        break;
                    //风险预警->水产品->预警数据统计
                    case "70204": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysWarningReport("1");
                        flag = 1;
                        break;

                    //风险预警->畜产品->实时风险
                    case "70301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysWarningInfo("2");
                        flag = 1;
                        break;
                    //风险预警->畜产品->预警复核
                    case "70302": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysReviewInfoAnimal();
                        flag = 1;
                        break;
                    //风险预警->畜产品->复核日志
                    case "70303": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysReviewLog("2");
                        flag = 1;
                        break;
                    //风险预警->畜产品->预警数据统计
                    case "70304": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysWarningReport("2");
                        flag = 1;
                        break;

                    ////电子出证->畜产品->新建检疫证单(动物)
                    //case "80101": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new UcCreateCertificate(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;
                    ////电子出证->畜产品->电子证查询(动物)
                    //case "80102": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new UcCreateCertificatequery(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;
                    ////电子出证->畜产品->货主信息(动物)
                    //case "80103": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new SysShipperQuery(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;
                    ////电子出证->畜产品->新建检疫证单(产品)
                    //case "80104": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new UcCreateCertificate_product(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;
                    ////电子出证->畜产品->电子证查询(产品)
                    //case "80105": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new UcCreateCertificateProductQuery(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;
                    ////电子出证->畜产品->货主信息(产品)
                    //case "80106": temptb.Header = (sender as Button).Content.ToString();
                    //    temptb.Content = new SysShipperQuery_product(mainWindow.dbOperation);
                    //    flag = 1;
                    //    break;

                    //档案信息->农产品->档案信息管理
                    case "90101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysCompanyQuery("0");
                        flag = 1;
                        break;
                    //档案信息->水产品->档案信息管理
                    case "90201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysCompanyQuery("1");
                        flag = 1;
                        break;
                    //档案信息->畜产品->档案信息管理
                    case "90301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysCompanyQuery("2");
                        flag = 1;
                        break;

                    //检测仪器->农产品->检测仪器管理
                    case "C0101": temptb.Header = (sender as Button).Content.ToString() + "(农产品)";
                        temptb.Content = new SysMachineManager("0");
                        flag = 1;
                        break;
                    //检测仪器->农产品->检测仪器管理
                    case "C0201": temptb.Header = (sender as Button).Content.ToString() + "(水产品)";
                        temptb.Content = new SysMachineManager("1");
                        flag = 1;
                        break;
                    //检测仪器->畜产品->检测仪器管理
                    case "C0301": temptb.Header = (sender as Button).Content.ToString() + "(畜产品)";
                        temptb.Content = new SysMachineManager("2");
                        flag = 1;
                        break;

                    //信息发布->信息发布->信息发布管理
                    case "D0101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysMessageQuery();
                        flag = 1;
                        break;

                    //视频监控->视频监控->视频监控管理
                    case "E0101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysMedia();
                        flag = 1;
                        break;

                    //系统管理->系统管理->部门管理
                    case "A0101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysDeptManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->用户管理
                    case "A0102": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcUserManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->角色管理
                    case "A0103": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysRoleManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->权限管理
                    case "A0104": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysRolePowerManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->修改密码
                    case "A0105": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysModifyPassword();
                        flag = 1;
                        break;
                    //系统管理->系统管理->自定义设置
                    case "A0106": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysLoadPicture();
                        flag = 1;
                        break;
                    //系统管理->系统管理->系统日志
                    case "A0107": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysLogManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->检测试剂耗材管理
                    case "A0108": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysSjHcQuery();
                        flag = 1;
                        break;
                    //系统管理->系统管理->检测师签到查询
                    case "A0109": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysUserSignIn();
                        flag = 1;
                        break;
                    
                    //帮助->帮助->帮助
                    case "B0101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcUnrealizedModul();
                        flag = 1;
                        break;
                    //帮助->帮助->关于
                    case "B0102": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysHelp();
                        flag = 1;
                        break;

                    default: break;
                }
                if (flag == 1)
                {
                    tab.Items.Add(temptb);
                    tab.SelectedIndex = tab.Items.Count - 1;
                }

            }

            //mainWindow.IsEnbleMouseEnterLeave = false;
            //if (uc is IClickChildMenuInitUserControlUI)
            //{
            //    ((IClickChildMenuInitUserControlUI)uc).InitUserControlUI();
            //}
        }
    }
}
