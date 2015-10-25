using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Web.Syndication;

namespace Http_Rss阅读器           //Rss服务类封装网络请求的方法
{
    /// <summary>
    /// 获取网络Rss服务类
    /// </summary>
    public static  class RssService
    {
        ///获取Rss目录列表
        public static void GetRssItems(string rssFeed, Action<IEnumerable<RssItem>> onGetRssItemsCompleted = null, Action<string> onError = null,
            Action onFinally = null)
        {
            var request = HttpWebRequest.Create(rssFeed);
            request.Method = "GET";
            request.BeginGetResponse((result) =>
            {
                try
                {
                    HttpWebRequest httpWebResquest = (HttpWebRequest)result.AsyncState;
                    WebResponse webResponse = httpWebResquest.EndGetResponse(result);
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string content = reader.ReadToEnd();
                            //将网络获取的信息转化成RSS实体类
                            List<RssItem> rssItems = new List<RssItem>();
                            SyndicationFeed feeds = new SyndicationFeed();
                            feeds.Load(content);
                            foreach (SyndicationItem f in feeds.Items)
                            {
                                RssItem rssItem = new RssItem(f.Title.Text, f.Summary.Text, f.PublishedDate.ToString(), f.Links[0].Uri.AbsoluteUri);
                                rssItems.Add(rssItem);
                            }
                            if (onGetRssItemsCompleted != null)
                            {
                                onGetRssItemsCompleted(rssItems);
                            }
                        }

                    }
                }
                catch (WebException webEx)
                {
                    string exceptionInfo = "";
                    switch (webEx.Status)
                    {
                        case WebExceptionStatus.ConnectFailure:
                            exceptionInfo = "ConnectFaliure:远程服务器连接失败";
                            break;
                        case WebExceptionStatus.MessageLengthLimitExceeded:
                            exceptionInfo = "MessageLengthLimitExceeded:网络请求的信息长度受到限制";
                            break;
                        case WebExceptionStatus.Pending:
                            exceptionInfo = "Pending:内部异步请求挂起";
                            break;
                        case WebExceptionStatus.RequestCanceled:
                            exceptionInfo = "RequestCaneled:该请求将被取消";
                            break;
                        case WebExceptionStatus.SendFailure:
                            exceptionInfo = "SendFailure:发送失败，未能将完整请求发送到";
                            break;
                        case WebExceptionStatus.UnknownError:
                            exceptionInfo = "UnknownError:未知错误";
                            break;
                        case WebExceptionStatus.Success:
                            exceptionInfo = "Success :请求成功";
                            break;
                        default:
                            exceptionInfo = "未知网络异常";
                            break;
                    }
                    if (onError != null)
                        onError(exceptionInfo);
                }
                catch (Exception e)
                {
                    if (onError != null)
                        onError("异常" + e.ToString());
                }
                finally
                {
                    if (onFinally != null)
                        onFinally();
                }
            }, request);
        }
    }
}
