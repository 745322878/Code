using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 天气预报
{
   public class WeatherData
    {
       [DataMember(Name = "error")]
        public int error { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public List<resultsList> results { get; set;  }
        public  class  resultsList
        {
            public string currentCity { get; set;  }
            public string pm25 { get; set; }
            public List<indexList> index { get; set; }
            public List<weather_dataList> weather_data { get; set; }
           

        }
        public struct   indexList
        {
            public string title { get; set; }
            public string zs { get; set; }
            public string tipt { get; set; }
            public string des { get; set; } 
        }
        public class weather_dataList
        {
            public string date { get; set; }
            public string weather { get; set; }
            public string wind { get; set; }
            public string temperature { get; set; }
            public Uri dayPictureUrl { get; set; }
            public Uri nightPictureUrl { get; set; }
        }
     
    }
}
