using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Http_Rss阅读器  //Rss文章内容实体类的封装
{
    /// <summary>
    /// RSS对象类
    /// </summary>
    public class RssItem
    {
        ///初始化一个Rss目录
        public RssItem(string title, string summary, string publishedDate, string url)
        {
            Title = title;
            Summary = summary;
            PublisheDate = publishedDate;
            Url = url;
            PlainSummary =WebUtility.HtmlDecode(Regex.Replace(summary ,"<[^>]+?>",""));
        }
        //标题
        public string Title { get; set; }
        //内容
        public string Summary { get; set; }
        //发表时间
        public string PublisheDate { get; set; }
        //文章地址
        public string Url { get; set; }
        //解析的文本内容
        public string PlainSummary { get; set; }
    }
}
