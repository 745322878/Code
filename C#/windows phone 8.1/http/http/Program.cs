using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace http
{
    class Program
    {                            
        static void Main(string[] args)
        {
            string Url = "http://baike.baidu.com/link?url=N-4aBEM9vA1CqUdW1ohhCvIenKQY969ppQtHsjphluA1dxP5lx6cnbNiAlN6LugQ4-EtpQfZu1fWJq1sDmiFiK";
            string s;
            //获取WebRequest对象
            WebRequest request = HttpWebRequest.Create(Url);
            //发起GetResponse请求
            WebResponse response = request.GetResponse();
            //获取请求返回的内容
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) 
            {
                string content = reader.ReadToEnd();
                stream.Close();
                reader.Close();
            }
            s=(GetPageData(Url, null));
            //Console.WriteLine(s.IndexOf ("第一季 风影夺还之章"));
            //Console.WriteLine(s);
            Input(s);
            Console.ReadLine(); 

        }
        private static void Input(string s)
        { 
            string a=@"<div class=""para"">" ;
            string b = @"</div>";
            int indexa = s.IndexOf("221 归乡") -20;
            int indexb = indexa+3;
            
            for (int i = indexa; i < s.Length; i++)
            {
                indexa=s.IndexOf(a, indexa + 1);
                indexb=s.IndexOf(b, indexb+1);
              
                if (indexb-indexa<=0)
                        break ;
                string se=(s.Substring(indexa, indexb - indexa));
                Console.WriteLine(se.Substring (18));
             }
        }
        //url表示资源定位符，要访问的网址，charset为网页的编码
        //如果传入的charset则自动分析网页编码
        private static string GetPageData(string url, string charset)
        {
            string strWebData = string.Empty;
            //该网站有内容
            if (url != null || url.Trim() != "")
            {
                WebClient wc = new WebClient();
                //网络验证
                wc.Credentials  = CredentialCache.DefaultCredentials;
                //byte接受网页信息
                byte[] myDataBuffer = wc.DownloadData(url);
                //转化为字符串
                strWebData = Encoding.Default.GetString(myDataBuffer);
                //获取网页字符编码描述信息
                Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string webCharSet = charSetMatch.Groups[2].Value;
                if (charset == null || charset == "")
                    charset = "UTF-8";
                else
                    charset = webCharSet;
                if (charset !=null &&charset !=""&&Encoding.GetEncoding (charset)!=Encoding.Default )
                    strWebData =Encoding .GetEncoding (charset ).GetString (myDataBuffer );

               
            }
            return strWebData; 
        }
    }
}
