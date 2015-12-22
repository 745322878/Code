using Socket__ChatRoom.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace Socket__ChatRoom
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ChatUI : Page
    {
        
        int i;
        private static  List<SendData> datas = new List<SendData>();
        StreamSocketListener listener;
        public ChatUI()
        {
            this.InitializeComponent();
         
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Random R = new Random();
            i=R.Next(1, 10000);
        }

        private   void Button_Click(object sender, RoutedEventArgs e)
        {
            Send();
            //创建一个线程，来接受消息
            Task t=new Task((Action)(() =>
                {
                    ReceiveData();
                }));
            t.Start();
        }
        //发送消息
         private async  void Send()
        {
            byte[] buffer;
           
            DataWriter writer = new DataWriter(MainPage.clientsocket.OutputStream);
            string message = i + "说   " + sendtext.Text;
            buffer = Encoding.UTF8.GetBytes(message);
            writer.WriteBytes(buffer);
            //writer.WriteUInt32(writer.MeasureString(message));
            //writer.WriteString(message );
            await writer.StoreAsync();
            //writer.DetachStream();
            //writer.Dispose();

        }

        //放在线程中，接收消息
         private async   void ReceiveData( )
         {
             byte[] data = new byte[1024];           
             DataReader reader=new DataReader(MainPage.clientsocket.InputStream);
             
             try
             {
                 while (true)
                 {
                     reader.InputStreamOptions = InputStreamOptions.Partial;        //采用异步方式
                     await reader.LoadAsync(1024);                                  //获取一定大小的数据流
                     string message = reader.ReadString(reader.UnconsumedBufferLength);
                     //获取字符串，指定为未读取的缓冲区的大小

                     //将后台变化通知到页面
                     await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,()=>
                         {
                             listbox.Items.Add(new SendData { Data = message });
                         });              
                 }
             }
             catch(Exception ex) 
             {
                 Debug.WriteLine(ex.Message);
             }
             
         }
    }
}
