using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
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

namespace 蓝牙
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Socket数据流对象
        private StreamSocket socket = null;
         //数据写入对象
        private DataWriter dataWriter;
        //数据读取对象
        private DataReader dataReader;
        public MainPage()
        {
            this.InitializeComponent();
            //页面加载事件
            Loaded += MainPage_Loaded;  
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;
        }
        /// <summary>
        /// 连接请求事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private  async void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {
            //使用UI线程弹出连接请求的接收和拒绝弹窗
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    MessageDialog md = new MessageDialog("是否接收" + args.PeerInformation.DisplayName + "连接请求", "蓝牙连接");
                    UICommand yes = new UICommand("接收");
                    UICommand no = new UICommand("拒绝");
                    md.Commands.Add(yes);
                    md.Commands.Add(no);
                    var result = await md.ShowAsync();
                    if (result == yes)
                    {
                        ConnectToPeer(args.PeerInformation);
                    }
                });
        }
        /// <summary>
        /// 连接并发送信息给对方
        /// </summary>
        /// <param name="peerInformation"></param>
        private async void ConnectToPeer(PeerInformation peer)
        {
            dataReader = new DataReader(socket.InputStream);
            dataWriter = new DataWriter(socket.OutputStream);
            string message = "测试消息";
            uint strLength = dataWriter.MeasureString(message);
            dataWriter.WriteUInt32(strLength);
            dataWriter.WriteString(message);
            uint numBytesWritten = await dataWriter.StoreAsync();
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

        private async void btFindBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            try
            {
                //开始查找对等项
                PeerFinder.Start();
                //等待找到的等待项
                var peers = await PeerFinder.FindAllPeersAsync();
                if (peers.Count == 0)
                {
                    message = "没有发现对等的蓝牙应用";
                }
                else
                {
                    lBluetoothApp.ItemsSource = peers;
                }
            }
            catch(Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    message = "Bluetooth 已关闭，请打开手机的蓝牙开关";
                }
                else
                    message = ex.Message;
            }
            if (message != "")
            {
                await new MessageDialog(message).ShowAsync();
            }
        }
        /// <summary>
        /// 通过蓝牙连接设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btConnect_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            PeerInformation selectedPeer = deleteButton.DataContext as PeerInformation;
            //连接到选择的对等项
            socket = await PeerFinder.ConnectAsync(selectedPeer);
            //使用输出输入流建立数据读写对象
            dataReader = new DataReader(socket.InputStream);
            dataWriter = new DataWriter(socket.OutputStream);
            //开始读取消息
            PeerFinder_StartReader();
        }
        /// <summary>
        /// 开始读取设备
        /// </summary>
        private async  void PeerFinder_StartReader()
        {
            string message = "";
            try
            {
                uint bytesRead = await dataReader.LoadAsync(sizeof(uint));
                if (bytesRead > 0)
                {
                    uint strLength = (uint)dataReader.ReadUInt32();
                    bytesRead = await dataReader.LoadAsync(strLength);
                    if (bytesRead > 0)
                    {
                        string content = dataReader.ReadString(strLength);
                        await new MessageDialog("获取到消息" + content).ShowAsync();
                        //开始读取下一条信息
                        PeerFinder_StartReader();
                    }
                    else
                    {
                        message = "对方已关闭连接";
                    }
                }
                else
                {
                    message = "对方已关闭连接";
                }
            }
            catch (Exception ex)
            {
                message = "读取失败：" + ex.Message;
            }
            if (message != "")
                await new MessageDialog(message).ShowAsync();
        }
       
    }
}
