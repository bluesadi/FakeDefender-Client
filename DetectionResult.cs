using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FakeDefender_Client {
    class Face {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
        public double score { get; set; }
    }

    class DetectionResult {

        public string uuid { get; set; }
        public int faceNum { get; set; }
        public List<Face> faces { get; set; }

        public static DetectionResult fromJson(string jsonText) {
            DetectionResult result = JsonConvert.DeserializeObject<DetectionResult>(jsonText);
            return result;
        }
    }
}
