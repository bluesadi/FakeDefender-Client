using System;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Threading;

namespace FakeDefender_Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {

        private NotifyIcon icon = null;
        private ScreenMonitor screenMonitor;

        /// <summary>
        /// 初始化MainWindow
        /// 主要是初始化了程序在系统托盘的图标
        /// </summary>
        public MainWindow(){
            InitializeComponent();
            icon = new NotifyIcon();
            icon.Icon = new System.Drawing.Icon("logo.ico");
            icon.DoubleClick += 
                delegate (object sender, EventArgs args) {
                    Show();
                    WindowState = WindowState.Normal;
                    Visibility = Visibility.Visible;
                    icon.Visible = false;
                };
            screenMonitor = new ScreenMonitor();
        }

        public ScreenMonitor GetScreenMonitor() {
            return screenMonitor;
        }

        /// <summary>
        /// 最小化到系统托盘
        /// </summary>
        public void Minimize() {
            WindowState = WindowState.Minimized;
            Visibility = Visibility.Hidden;
            icon.Visible = true;
        }

        /// <summary>
        /// 切换页面
        /// </summary>
        /// <param name="page">切换到的页面</param>
        public void SwitchPage(Page page) {
            Frame.Content = page;
        }



        public Page GetPage() {
            return (Page)Frame.Content;
        }

        private void ButtonMain(object sender, RoutedEventArgs e) {
            Frame.Content = new PageMain();
        }

        private void ButtonSetting(object sender, RoutedEventArgs e) {
            Frame.Content = new PageSetting();
        }

        private void ButtonLogout(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

    }
}
