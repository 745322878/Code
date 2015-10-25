using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 两次退出程序
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //DispatcherTimer timer = null;
        //int i;
        //TextBlock t;
        //int flag = 0;
        public MainPage()
        {
            this.InitializeComponent();
           
            this.NavigationCacheMode = NavigationCacheMode.Required;

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            myStoryboard.Completed += myStoryboard_Completed;
            
        }

        void myStoryboard_Completed(object sender, object e)
        {
            ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            TextBlock t = new TextBlock();
            t.Text = "再按一次退出应用";
            t.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            t.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            t.FontSize = 30;
            t.Foreground = new SolidColorBrush(Colors.Black);
            t.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ContentPanel.Children.Add(t);
            if (ContentPanel.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                ContentPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
               
                myStoryboard.Begin();
                t.Visibility = Windows.UI.Xaml.Visibility.Visible;
                return;
            }
            if (ContentPanel.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                App.Current.Exit();
            }
        }
        //    //if (flag == 1)
        //    //{
        //    //    App.Current.Exit();
        //    //}
        //    //myStoryboard.Begin();
           
        //    //rect.Fill.Opacity = 1;
        //    //t = new TextBlock();
        //    //t.Text = "再按一次推出应用";
        //    //t.Foreground = new SolidColorBrush(Colors.Black);
        //    //t.FontSize = 30;
        //    //t.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
        //    //t.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
        //    //text.Children.Add(t);

        //    //timer = new DispatcherTimer();
        //    //timer.Interval = TimeSpan.FromSeconds(1);
        //    //timer.Tick += timer_Tick;
        //    //timer.Start(); 
        //}

        //private void timer_Tick(object sender, object e)
        //{
        //    i++;
        //    if (i < 3)
        //    {
        //        flag = 1;
        //    }
        //    if (i > 3)
        //    {
        //        rect.Fill.Opacity = 0;
        //        t.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //        timer.Stop();
        //        i = 0;
        //        flag = 0;
        //    }
        //}
    }
}
