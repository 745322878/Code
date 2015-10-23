using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 蓝牙2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StreamSocket streamSocket;

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
        /// 查找蓝牙设备，并找到第一个蓝牙设备进行连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async  void btFinderBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string errMessage = "";
            try
            {
                //查找使用服务发现协议（SDP）并通过既定GUID播发服务的设备
                PeerFinder.AlternateIdentities["Bluetooth:SDP"] = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                //搜索所有配对的设备
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    await new MessageDialog("没有找到相关的蓝牙设备").ShowAsync();
                }
                else
                {
                    //连接到的第一个蓝牙设备
                    PeerInformation selectedPeer = pairedDevices[0];
                    //创建socket连接
                    StreamSocket socket = new StreamSocket();
                    await socket.ConnectAsync(selectedPeer.HostName, selectedPeer.ServiceName);
                    await new MessageDialog("连接上了HostName:" + selectedPeer.HostName + "ServiceName" + selectedPeer.ServiceName).ShowAsync();
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    errMessage = "Bluetooth is turned off";
                }
                else
                {
                    errMessage = ex.Message;
                }
            }
            if (errMessage != "")
            {
                await new MessageDialog(errMessage).ShowAsync();
            }
        }
        /// <summary>
        /// 查找周围所有可以连接的蓝牙设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btFinderAllBluetooth_Click(object sender, RoutedEventArgs e)
        {
            string errMessage = "";
            try
            {
                //这样连接找到的设备对应的PeerInformation.ServiceName将为空
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                //搜索所有配对的设备
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    await new MessageDialog("没有找到相关的蓝牙设备").ShowAsync();
                }
                else
                {
                    streamSocket = new StreamSocket();
                    //第二个参数是一个RFCOMM端口号，范围是1-30
                    await streamSocket.ConnectAsync(pairedDevices[0].HostName, "1");
                }
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x8007048F)
                {
                    errMessage = "Bluetooth is turned off";
                }
                else
                {
                    errMessage = ex.Message;
                }
            }
            if (errMessage != "")
            {
                await new MessageDialog(errMessage).ShowAsync();
            }
        }
    }
}
