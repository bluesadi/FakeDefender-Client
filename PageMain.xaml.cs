using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.IO;
using System.Net;
using Point = OpenCvSharp.Point;
using System.Threading;
using Window = System.Windows.Window;
using OpenCvSharp;

namespace FakeDefender_Client {
    /// <summary>
    /// PageMain.xaml 的交互逻辑
    /// </summary>
    public partial class PageMain : Page {


        public PageMain() {
            InitializeComponent();
        }

        private void ButtonStart(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.SwitchPage(new PageRunning());
            window.GetScreenMonitor().StartMonitorThread();
        }
    }

}
