using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest req = HttpWebRequest.Create("http://api.map.baidu.com/telematics/v3/weather?location=%E8%A5%BF%E5%AE%89&output=json&ak=Gi27P5bmIinr86htrjU4ESnY");
            req.Method = "GET";
            req.BeginGetResponse(ResponseCallback, req);
            Console.Read();
        }

        private static void ResponseCallback(IAsyncResult result)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
            WebResponse webResponse = httpWebRequest.EndGetResponse(result);
            using (Stream stream=webResponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine(content );
                Console.Read();
            }
        

        }
        public static WeatherData DeserializeJsonToObject<WeatherData>(string jsonString)
        { 
             
        }
        private void GetJson(string jsonString)
        { 
            JsonObject
        }
        //json字符串转化为对象的方法
        private WeatherData GetWeatherDataFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                var ser = new DataContractJsonSerializer(typeof(WeatherData));
                var weatherData = (WeatherData)ser.ReadObject(ms);
                return weatherData;
            }
        }
    }
}
