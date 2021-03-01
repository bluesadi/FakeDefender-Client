using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.IO;

namespace FakeDefender_Client {
    class NetUtil {
        public static string BuildPacket(string uuid, Mat image) {
            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("uuid");
            jsonWriter.WriteValue(uuid);
            jsonWriter.WritePropertyName("image");
            jsonWriter.WriteValue(Convert.ToBase64String(image.ToBytes()));
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
            return stringWriter.GetStringBuilder().ToString();
        }
    }
}
