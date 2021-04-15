using System.Windows;
using System.Windows.Controls;

namespace FakeDefender_Client {
    /// <summary>
    /// PageRunning.xaml 的交互逻辑
    /// </summary>
    public partial class PageRunning : Page {
        public PageRunning() {
            InitializeComponent();
        }

        private void ButtonStop(object sender, RoutedEventArgs e) {
            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.SwitchPage(new PageMain());
        }

    }
}
