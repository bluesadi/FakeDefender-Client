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

        private void Minimize() {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
            window.Visibility = Visibility.Hidden;
        }

        private string SendPredictRequest(string jsonText) {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string result = null;

            request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/predict");
            request.Method = "POST";
            request.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(jsonText);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream()) {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
                result = reader.ReadToEnd();
            }

            return result;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            Minimize();
            Thread.Sleep(500);
            Mat screenShot = BitmapConverter.ToMat(CaptureScreen());
            try {

                string jsonText = NetUtil.BuildPacket(Guid.NewGuid().ToString(), screenShot);
                string result = SendPredictRequest(jsonText);
                DetectionResult detectionResult = DetectionResult.fromJson(result);
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
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Normal;
            Cv2.ImShow("After", screenShot);
            Cv2.WaitKey(0);
        }
    }

}
