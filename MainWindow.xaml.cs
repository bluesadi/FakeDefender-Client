using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Text;
using Point = OpenCvSharp.Point;
using System.Threading;
using MahApps.Metro.Controls;

namespace FakeDefender_Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {

        public MainWindow(){
            InitializeComponent();
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
