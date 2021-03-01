using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Text;
using Newtonsoft.Json.Linq;
using Point = OpenCvSharp.Point;

namespace FakeDefender_Client {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window {
        public MainWindow(){
            InitializeComponent();
        }
        private Bitmap CaptureScreen() {
            //double w = SystemParameters.PrimaryScreenWidth;
            //double h = SystemParameters.PrimaryScreenHeight;
            double w = 2560;
            double h = 1440;
            Bitmap bitmap = new Bitmap((int)w, (int)h);
            using (Graphics graphics = Graphics.FromImage(bitmap)) {
                graphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size((int)w, (int)h));
            }
            return bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(ip, 23333);
            Mat screenShot = BitmapConverter.ToMat(CaptureScreen());
            try {
                string jsonText = NetUtil.BuildPacket(Guid.NewGuid().ToString(), screenShot);
                client.Connect(point);
                client.Send(Encoding.UTF8.GetBytes(jsonText));
                byte[] buffer = new byte[8 * 1024];
                int n = client.Receive(buffer);
                string recv = Encoding.UTF8.GetString(buffer, 0, n);
                DetectionResult detectionResult = DetectionResult.fromJson(recv);
                foreach (Face face in detectionResult.faces) {
                    Point facePtr1 = new Point(face.x1, face.y1);
                    Point facePtr2 = new Point(face.x2, face.y2);
                    double fakeProb = face.score;
                    FaceMarker faceMarker = new FaceMarker(screenShot);
                    faceMarker.draw(facePtr1, facePtr2, fakeProb);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            Cv2.ImShow("After", screenShot);
            Cv2.WaitKey(0);
        }
    }
}
