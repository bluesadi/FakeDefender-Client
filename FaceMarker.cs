using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDefender_Client {
    class FaceMarker {

        private Mat targetMat;

        public FaceMarker(Mat targetMat) {
            this.targetMat = targetMat;
        }

        public void draw(Point facePtr1, Point facePtr2, double fakeProb) {
            Scalar color = fakeProb > 0.5 ? Scalar.Red : Scalar.Green;
            Cv2.Rectangle(this.targetMat, facePtr1, facePtr2, color, 3);
            Point textPtr = new Point(facePtr1.X, facePtr2.Y + 22);
            Cv2.PutText(this.targetMat, "Fake: " + String.Format("{0:N2}", fakeProb), textPtr, HersheyFonts.HersheyDuplex, 1, color, 2);
        }
    }
}
