using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace ProgressBar进度条
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //启动进度条，把进度条显示
        private void beginButton(object  sender ,RoutedEventArgs e)
        {
            //将进度条设为可见
            progressBar1.Visibility = Visibility.Visible;
            if (radioButton1.IsChecked == true)
            {
                //进度条为不可重复模式
                progressBar1.IsIndeterminate = false;
                //使用一个定时器，每一秒钟触发一下进度的改变
                DispatcherTimer timer = new DispatcherTimer();          
                timer.Interval = TimeSpan.FromSeconds(1);           //获取timespan时间格式，每隔1秒刷新
                timer.Tick += timer_Tick;                           //进度增加
                timer.Start();                                      //启动定时器     
            }
            else
            { 
                //设置进度条的值为0
                progressBar1.Value = 0;
                //设置进度条为重复模式
                progressBar1.IsIndeterminate = true;
            }
        }
        //定时器的定时触发的事件处理
        async  void timer_Tick(object sender ,object e)
        {
            //如果进度没到达100，则在原来的进度基础上在增加10
            if (progressBar1.Value < 100)
                progressBar1.Value += 10;
            else 
            { 
                //进度完成,移除定时器的定时事件
                (sender as DispatcherTimer).Tick -= timer_Tick;
                //停止定时器的运行
                (sender as DispatcherTimer).Stop();
                await new MessageDialog("进度完成").ShowAsync();
            }
         }
        //取消进度条，把进度条隐藏
        public void cancelButton(object sender, RoutedEventArgs e)
        { 
            //隐藏进度条
            progressBar1.Visibility = Visibility.Collapsed;
        }
        public MainPage()
        {
            this.InitializeComponent();
            //第一次进入页面，设置进度条为不可见
            progressBar1.Visibility = Visibility.Collapsed;

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
    }
}
