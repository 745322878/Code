using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Socket___Tcp协议
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //监听器
        StreamSocketListener listener;
        //socket数据流对象
        StreamSocket socket;
        //输出流的写入数据对象
        DataWriter writer;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
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
        /// <summary>
        /// 服务器监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  async void listenButton_Click(object sender, RoutedEventArgs e)
        {
            if (listener != null)
            {
                await new MessageDialog("监听已经启动了").ShowAsync();
                return;
            }
            listener= new StreamSocketListener();
            //监听后连接的事件OnConnection
            listener.ConnectionReceived += OnConnection;
            //开始监听操作
            try
            {
                await listener.BindServiceNameAsync("22112");
                await new MessageDialog("正在监听").ShowAsync();
            }

            catch (Exception ex)
            {
                listener = null;
                //未知异常
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 监听后连接的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnConnection(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            DataReader reader = new DataReader(args.Socket.InputStream);
            try
            {
                //循环接受数据
                while (true)
                {
                    uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));
                    if (sizeFieldCount != sizeof(uint))
                    {
                        //在socket关闭之前才可以读取全部的数据
                        return;
                    }
                    //读取监听到的信息
                    uint stringLength= reader.ReadUInt32();
                    uint actualStringLength = await reader.LoadAsync(stringLength);
                    if (stringLength != actualStringLength)
                    {
                        //在socket关闭之前才可以读取全部的数据
                        return;
                    }
                    //读取监听到的信息的内容
                    string msg = reader.ReadString(actualStringLength);
                    //将信息显示到界面
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            TextBlock tb = new TextBlock { Text = msg, FontSize = 20 };
                            receive_Msg.Children.Add(tb);
                        });
                }
            }
            catch (Exception ex)
            {
                //未知异常
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 连接Socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void linkButton_Click(object sender, RoutedEventArgs e)
        {
            if (socket != null)
            {
                await new MessageDialog("已经连接了Socket").ShowAsync();
                return;
            }
           
            //创建一个主机名字
            HostName hostName = null;
            string message = "";
            try
            { 
                hostName =new HostName ("localhost");
            }
            catch (ArgumentException )
            {
                message ="主机名不可用";
            }
            if (message != "")
            { 
                await  new MessageDialog (message ).ShowAsync ();
                return ;
            }
            //创建一个StreamSocket对象
            socket = new StreamSocket();
            try
            {
                //开始连接
                await socket.ConnectAsync(hostName, "22112");
                await new MessageDialog("连接成功").ShowAsync();
            }
            catch (Exception ex)
            {
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
          
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sendButton_Click(object sender, RoutedEventArgs e)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        {
            if (listener == null)
            { 
                await new MessageDialog ("监听未启动").ShowAsync ();
                return ;
            }
            if (socket == null)
            {
                await new MessageDialog("未连接Socket").ShowAsync();
                return;
            }
            if (writer == null)
            {
                writer = new DataWriter(socket.OutputStream);
            }
            ///先写入数据的长度，长度的类型为Uint32类型然后在写入数据
            //接收信息字符串
            string stringToEnd = send_Message.Text;
            //将字符串长度以32位整数写入数据流
            writer.WriteUInt32(writer.MeasureString(stringToEnd));
            //将字符串写入输出流
            writer.WriteString(stringToEnd);
            TextBlock te = new TextBlock();
            //te.Text = stringToEnd;
            receive_Msg.Children.Add(te);
            //将数据发送到网络
            try
            {
                await writer.StoreAsync();
                await new MessageDialog("发送成功").ShowAsync();

            }
            catch (Exception ex)
            {
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 关闭Socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (writer != null)
            {
                writer.DetachStream();
                writer.Dispose();
                writer = null;
            }
            if (listener != null)
            {
                listener.Dispose();
                listener = null;

            }
            if (socket !=null)
            {
                socket.Dispose();
                socket = null;
            }
        }
    }
}
