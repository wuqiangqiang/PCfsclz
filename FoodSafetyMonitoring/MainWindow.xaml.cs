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
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using DBUtility;
using FoodSafety.DI;
using FoodSafety.Model;
using FoodSafetyMonitoring.Manager;
using FoodSafetyMonitoring.dao;
using Toolkit = Microsoft.Windows.Controls;
using System.Data;
using System.IO;
using System.Configuration;
using FoodSafety.Service.Contract;
using FoodSafetyMonitoring.Common;

namespace FoodSafetyMonitoring
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public delegate void UserControlCloseEventHandler();
        //public bool IsEnbleMouseEnterLeave = true;
        private string userName;
        public List<MainMenuItem> mainMenus = new List<MainMenuItem>();
        private Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        private string deptId = PubClass.userInfo.DepartmentID;
        public string last_name = "首页";//最后一次点击的主菜单名字，默认是首页

        private int dot = 0;
        IOperationContract operationService = ServiceFactory.GetWcfService<IOperationContract>();

        public MainWindow()
        {
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            this.Width = rc.Width;
            this.Height = rc.Height;
            rcnormal = new Rect((rc.Width - 1366) / 2, (rc.Height - 766) / 2, 1366, 766);
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
           
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }


        private int flag = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                LoadWindow load = new LoadWindow();
                load.Show();

                Application.Current.Resources.Add("省市表", operationService.GetProvinceCity());
                PubClass.ProvinceCityTable = Application.Current.Resources["省市表"] as DataTable;

                UserInfo userInfo = Application.Current.Resources["User"] as UserInfo;
                this.userName = userInfo.ShowName;

                //加载标题
                this._user.Text = this.userName;
                this._date.Text = DateTime.Now.ToLongDateString().ToString();
                DataTable table = operationService.GetUserSupplier(PubClass.supplierid);

                this._bottom.Text = "版权所有:" + table.Rows[0][0].ToString() + "  软著登字第0814101号    版本号：" + ConfigurationManager.AppSettings["version"] + "    技术服务热线：" + table.Rows[0][1].ToString();

                string dept_str = "";
                if (deptId.Length >= 3)
                {
                    dept_str = deptId.Substring(0, 3).ToString();
                }
                else
                {
                    dept_str = deptId;
                }

                this._title.Visibility = Visibility.Visible;

                //加载父菜单和子菜单和首页
                MainMenu_Load();
                this.SizeChanged += new SizeChangedEventHandler(MainWindow_SizeChanged);

                flag = 1;
                timer.Interval = new TimeSpan(1000);
                load.Close();

            }
            //header.UpdateTime();
        }


        //加载父菜单和子菜单
        private void MainMenu_Load()
        {
            //int flag_exits = 0;

            //用户的查看权限
            DataTable table = operationService.GetRolePermission(PubClass.userInfo.ID);
            //一级菜单
            DataRow[] row_mainmenu = table.Select("fkSubId = '0'");
            //定义数组存放：一级菜单图片控件和一级菜单文字控件
            Image[] images = new Image[] { _image_0,_image_1,_image_2,_image_3,_image_4,_image_5,_image_6,
                                           _image_7,_image_8,_image_9,_image_10,_image_11,_image_12,_image_13,_image_14};
            TextBlock[] texts = new TextBlock[] { _text_0, _text_1, _text_2, _text_3, _text_4, _text_5, _text_6,
                                                  _text_7,_text_8,_text_9,_text_10,_text_11,_text_12,_text_13,_text_14};
            Grid[] grids = new Grid[] { _grid_0, _grid_1, _grid_2, _grid_3, _grid_4, _grid_5, _grid_6,
                                                  _grid_7,_grid_8,_grid_9,_grid_10,_grid_11,_grid_12,_grid_13,_grid_14};

            int i = 0;
            foreach (DataRow row in row_mainmenu)
            {
                //二级菜单
                List<MyChildMenu> childMenus = new List<MyChildMenu>();
                DataRow[] row_childmenu = table.Select("fkSubId ='" + row["subId"] + "'", " subId asc");
                //当一级菜单存在，但二级菜单为空时
                if (row_childmenu.Count() == 0)
                {

                }
                else
                {
                    foreach (DataRow row_child in row_childmenu)
                    {
                        DataRow[] row_child_childmenu = table.Select("fkSubId ='" + row_child["subId"] + "'", "subId asc");
                        childMenus.Add(new MyChildMenu(row_child["subName"].ToString(), this, row_child_childmenu));
                    }
                }

                mainMenus.Add(new MainMenuItem(row["subName"].ToString(), images[i], grids[i], row["subUrl"].ToString(), row["subSelectUrl"].ToString(), childMenus, this));
                texts[i].Text = row["subName"].ToString();

                i = i + 1;
            }

            //加载主画面
            grid_mainpage.Visibility = Visibility.Visible;
            TabItem temptb = new TabItem();
            temptb.Header = "首页";
            temptb.Tag = "1";
            temptb.Content = new UcMainPage();
            _tab.Items.Add(temptb);
            _tab.SelectedIndex = _tab.Items.Count - 1;
            _tab.SetValue(Grid.ColumnSpanProperty, 3);
            grid_Component.Children.Remove(_tab);
            grid_mainpage.Children.Add(_tab);
            grid_Menu.Visibility = Visibility.Hidden;
            grid_splitter.Visibility = Visibility.Hidden;
            //grid_Menu.Background = Brushes.White;

            //让首页主菜单呈现选中状态
            //_grid_0.Background = new SolidColorBrush(Color.FromRgb(25, 49, 115));
            _image_0.Source = new BitmapImage(new Uri("pack://application:,," + "/res/firstpage_select.png"));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string header = btn.Tag.ToString();
            foreach (TabItem item in _tab.Items)
            {
                if (item.Header.ToString() == header)
                {
                    _tab.Items.Remove(item);
                    break;
                }
            }
            //判断当前有几个tab页打开，若为0，则隐藏
            if (_tab.Items.Count == 0)
            {
                grid_Component.Visibility = Visibility.Hidden;
            }
        }

        private void layout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //判断鼠标右键按下
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Border bor = sender as Border;
                string header = bor.Tag.ToString();
                foreach (TabItem item in _tab.Items)
                {
                    if (item.Header.ToString() == header)
                    {
                        _tab.Items.Remove(item);
                        break;
                    }
                }
                //判断当前有几个tab页打开，若为0，则隐藏
                if (_tab.Items.Count == 0)
                {
                    grid_Component.Visibility = Visibility.Hidden;
                }
            }
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                max_MouseDown(null, null);
            }

        }

        //退出主窗体
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.CloseWindow();

        }

        public void CloseWindow()
        {
            //if (timer != null && timer.IsEnabled)
            //{
            //    timer.Stop();
            //}
            this.Close();
        }

        //最小化窗口
        private void min_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //最大化或正常窗口
        private void max_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (max.ToolTip.ToString() == "最大化")
            {
                MaxWindow();
            }
            else if (max.ToolTip.ToString() == "还原")
            {
                NormalWindow();
            }
        }

        //最大化窗口
        private void MaxWindow()
        {
            max.ToolTip = "还原";
            rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
            this.Left = 0;//设置位置
            this.Top = 0;
            Rect rc = SystemParameters.WorkArea;//获取工作区大小
            this.Width = rc.Width;
            this.Height = rc.Height;

        }

        //正常窗口
        private void NormalWindow()
        {
            max.ToolTip = "最大化";
            this.Left = rcnormal.Left;
            this.Top = rcnormal.Top;
            this.Width = rcnormal.Width;
            this.Height = rcnormal.Height;
        }

        //移动窗口
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void min_MouseEnter(object sender, MouseEventArgs e)
        {
            min.Source = new BitmapImage(new Uri("pack://application:,," + "/res/min_on.png"));
        }

        private void max_MouseEnter(object sender, MouseEventArgs e)
        {
            max.Source = new BitmapImage(new Uri("pack://application:,," + "/res/max_on.png"));
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void all_MouseLeave(object sender, MouseEventArgs e)
        {
            min.Source = new BitmapImage(new Uri("pack://application:,," + "/res/min.png"));
            max.Source = new BitmapImage(new Uri("pack://application:,," + "/res/max.png"));
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }

        private void _changeSize_MouseDown(object sender, RoutedEventArgs e)
        {
            if (dot == 0)
            {
                grid_mainpage.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Pixel);
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_right.png"));
                dot = 1;
            }
            else if (dot == 1)
            {
                grid_mainpage.ColumnDefinitions[0].Width = new GridLength(210, GridUnitType.Pixel);
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_left.png"));
                dot = 0;
            }
        }

        private void _changeSize_MouseEnter(object sender, MouseEventArgs e)
        {
            if (dot == 0)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_left_pressed.png"));
            }
            else if (dot == 1)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_right_pressed.png"));
            }
        }

        private void _changeSize_MouseLeave(object sender, MouseEventArgs e)
        {
            if (dot == 0)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_left.png"));
            }
            else if (dot == 1)
            {
                _image.Source = new BitmapImage(new Uri("pack://application:,," + "/res/main_right.png"));
            }
        }
    }

    public class MainMenuItem
    {
        public string Name;
        public BitmapImage img_mouseEnter;
        public BitmapImage img_mouseLeave;
        //public BitmapImage img_mouseUnpressed;
        public List<MyChildMenu> childMenus;
        public ChildMenu childMenu;
        public Image img;
        public Grid grid_Menu;
        private MainWindow mainWindow;
        public Grid grid;
        //public int Flag_Exits;

        public MainMenuItem(string name, Image img, Grid grid, string mouseLeaveBackImgPath, string mouseEnterBackImgPath, List<MyChildMenu> childMenus, MainWindow mainWindow)
        {
            this.Name = name;
            this.childMenus = childMenus;
            this.mainWindow = mainWindow;
            grid_Menu = mainWindow.grid_Menu;
            this.childMenu = new ChildMenu(childMenus);
            this.img = img;
            this.grid = grid;
            this.img.Tag = name;
            //this.Flag_Exits = flag_exits;
            img_mouseEnter = new BitmapImage(new Uri("pack://application:,," + mouseEnterBackImgPath));
            img_mouseLeave = new BitmapImage(new Uri("pack://application:,," + mouseLeaveBackImgPath));
            //img_mouseUnpressed = new BitmapImage(new Uri("pack://application:,," + mouseUnpressedBackImgPath));
            //if (Flag_Exits == 1)


            this.img.Source = img_mouseLeave;
            //}
            //else
            //{
            //    this.img.Source = img_mouseUnpressed;
            //    this.img.ToolTip = "无操作权限";
            //}

            this.img.MouseDown += new MouseButtonEventHandler(img_MouseDown);
            this.img.MouseEnter += new MouseEventHandler(img_MouseEnter);
            //this.img.MouseLeave += new MouseEventHandler(img_MouseLeave);

        }


        void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //鼠标点在另外的主菜单上，打开的tab页全部关闭
            if (mainWindow.last_name != Name)
            {
                List<TabItem> items = new List<TabItem> { };
                foreach (TabItem item in mainWindow._tab.Items)
                {
                    items.Add(item);
                }
                foreach (TabItem item in items)
                {
                    mainWindow._tab.Items.Remove(item);
                }

                mainWindow.grid_Component.Visibility = Visibility.Hidden;

                mainWindow.last_name = Name;
            }

            //if (Name == "首页" && Flag_Exits == 1)
            if (Name == "首页")
            {
                int flag = 0;
                foreach (TabItem item in mainWindow._tab.Items)
                {
                    if (item.Tag.ToString() == "1")
                    {
                        mainWindow._tab.SelectedItem = item;
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    mainWindow.grid_Component.Visibility = Visibility.Visible;
                    TabItem temptb = new TabItem();
                    temptb.Header = Name;
                    temptb.Tag = "1";
                    temptb.Content = new UcMainPage();

                    mainWindow._tab.Items.Add(temptb);
                    mainWindow._tab.SelectedIndex = mainWindow._tab.Items.Count - 1;
                }
            }

            //主菜单点击视频监控，若首页已打开则选中该tab，若没有打开则打开该画面
            if (Name == "视频监控")
            {
                int flag = 0;
                foreach (TabItem item in mainWindow._tab.Items)
                {
                    if (item.Tag.ToString() == "2")
                    {
                        mainWindow._tab.SelectedItem = item;
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    mainWindow.grid_Component.Visibility = Visibility.Visible;
                    TabItem temptb = new TabItem();
                    temptb.Header = Name;
                    temptb.Tag = "2";
                    temptb.Content = new SysVideoMonitor();

                    mainWindow._tab.Items.Add(temptb);
                    mainWindow._tab.SelectedIndex = mainWindow._tab.Items.Count - 1;
                }
            }

            //mainWindow.IsEnbleMouseEnterLeave = true;
            //if (Flag_Exits == 1)
            //{
            for (int i = 0; i < grid_Menu.Children.Count; i++)
            {
                grid_Menu.Children.RemoveAt(i);
                i--;
            }
            this.grid_Menu.Children.Add(childMenu);
            //}

            //如果是首页或者视频监控隐藏左侧菜单栏
            if (Name == "首页" || Name == "视频监控")
            {
                //grid_Menu.Background = Brushes.White;
                mainWindow.grid_Menu.Visibility = Visibility.Hidden;
                mainWindow.grid_splitter.Visibility = Visibility.Hidden;
                mainWindow._tab.SetValue(Grid.ColumnSpanProperty, 3);
                mainWindow.grid_Component.Children.Remove(mainWindow._tab);
                mainWindow.grid_mainpage.Children.Remove(mainWindow._tab);
                mainWindow.grid_mainpage.Children.Add(mainWindow._tab);
            }
            else
            {
                if (mainWindow.grid_Component.Children.Count == 0)
                {
                    mainWindow.grid_Menu.Visibility = Visibility.Visible;
                    mainWindow.grid_splitter.Visibility = Visibility.Visible;
                    //grid_Menu.Background = new SolidColorBrush(Color.FromRgb(242, 241, 241));
                    mainWindow.grid_mainpage.Children.Remove(mainWindow._tab);
                    mainWindow.grid_Component.Children.Add(mainWindow._tab);
                }

            }
            //一旦鼠标点击在主菜单图标上，主菜单的图片替换
            for (int i = 0; i < mainWindow.mainMenus.Count; i++)
            {
                mainWindow.mainMenus[i].img.Source = mainWindow.mainMenus[i].img_mouseLeave;
                //mainWindow.mainMenus[i].grid.Background = new SolidColorBrush(Color.FromRgb(25, 86, 162));
            }
            //grid.Background = new SolidColorBrush(Color.FromRgb(25, 49, 115));
            this.img.Source = img_mouseEnter;
        }

        //void img_MouseLeave(object sender, MouseEventArgs e)
        //{

        //    if (Flag_Exits == 1)
        //    {
        //        ((Image)sender).Source = img_mouseLeave;
        //        mainWindow._childmenubar.ImageSource = new BitmapImage(new Uri("pack://application:,," + "/res/childmenu_bar.jpg"));
        //    }
        //}

        public void LoadChildMenu()
        {
            //if (mainWindow.IsEnbleMouseEnterLeave)
            //{
            for (int i = 0; i < grid_Menu.Children.Count; i++)
            {
                grid_Menu.Children.RemoveAt(i);
                i--;
            }
            this.grid_Menu.Children.Add(childMenu);
            //}
            //一旦鼠标移在主菜单图标上，主菜单的图标变成黄色，其余均为正常色
            for (int i = 0; i < mainWindow.mainMenus.Count; i++)
            {
                //if (mainWindow.mainMenus[i].Flag_Exits == 1)
                //{
                mainWindow.mainMenus[i].img.Source = mainWindow.mainMenus[i].img_mouseLeave;
                //}
            }
            //img.Source = img_mouseEnter;
        }

        void img_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (Flag_Exits == 1)
            //{
            // LoadChildMenu();
            //}
        }

    }


}
