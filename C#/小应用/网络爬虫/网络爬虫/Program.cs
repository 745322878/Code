using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 网络爬虫
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string s;
             
            //获取网站网页内容
            WebRequest request = HttpWebRequest.Create("http://222.24.19.3:81/xiyoucs/index.asp");
            WebResponse response = request.GetResponse();
            //获取请求返回的内容
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                //请求返回的字符串内容
                string content = reader.ReadToEnd();
               
                stream .Close ();
                reader.Close();
            }
          
            s =(GetPageData ("http://222.24.19.3:81/xiyoucs/index.asp",null));
            Console.WriteLine(s);
            Input(s);
            Console.ReadLine();
        }
        private static void Input(string s)
        {
            int indexa = 1;
            int indexb = 5;
            string a = "title='";
            string b = "target='_blank'>";
            int j = 0;
            for (int i = indexa; i < s.Length; i++)
            {
                indexa = s.IndexOf(a, indexa + 1);
                indexb = s.IndexOf(b, indexb + 1);
                if (indexb-indexa  == 0)
                    break;
                Console.WriteLine(s.Substring(indexa, indexb - indexa));
            }
        }
            
    
        //url是要访问的网站地址，charSet是目标网页的编码，如果传入的是null或者""，那就自动分析网页的编码
        private static string GetPageData(string url, string charSet)
        {
            string strWebData = string.Empty;
            if (url != null || url.Trim() != "")
            {
                WebClient mc= new WebClient();
                mc.Credentials = CredentialCache.DefaultCredentials;
                byte[] myDataBuffer = mc.DownloadData(url);
                strWebData = Encoding.Default.GetString(myDataBuffer);
               //获取网页字符编码描述信息
                Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string webCharSet=charSetMatch.Groups [2].Value ;
                if (charSet == null || charSet == "")
                {
                    //如果未获取到编码，则设置默认编码
                    if (webCharSet == null || webCharSet == "")
                    {
                        charSet = "UTF-8";
                    }
                    else
                    {
                        charSet = webCharSet;
                    }
                }
                if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
                {
                    strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
                }
            }
            return strWebData;
        }
    }
}
