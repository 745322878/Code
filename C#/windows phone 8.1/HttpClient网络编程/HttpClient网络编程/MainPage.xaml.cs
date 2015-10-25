using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace HttpClient网络编程
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HttpClient httpclient=new HttpClient();
        string content;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            Get();

        }
        async  private  void Get()
         {
             string Address = "http://wenku.baidu.com/link?url=qmrPPsRjENp5vUNG-wfwdYYG20lCqr3oC5-jLgfVLksVCopMgIcnONlXmI56p8aHGphvfLK4_xjZQ-lAmC377iKO9ep-QuAZhquJVYLCGMa";
             var request = HttpWebRequest.Create(Address );
             request.Method = "Get";
             request.BeginGetResponse(ResponseCallback,request );


            //var strinfo = await httpclient.GetStringAsync(Address);
        }

        private void ResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)ar.AsyncState;
            WebResponse webResponse= httpWebRequest.EndGetResponse(ar);
            using (Stream stream = webResponse.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                 content = reader.ReadToEnd();
                 text.Text = content;
            }
            

        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }
    }
}
