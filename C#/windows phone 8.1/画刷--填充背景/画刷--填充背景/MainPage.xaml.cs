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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 画刷__填充背景
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //使用SolidColorBrush填充椭圆
            ellipsel.Fill = new SolidColorBrush(Colors.Blue);
            //使用LinearGradientBrush来设置文本框的背景
            LinearGradientBrush l = new LinearGradientBrush();
            l.StartPoint = new Point(0.5, 0);
            l.EndPoint = new Point(0.5, 1);
            GradientStop s1 = new GradientStop();
            s1.Color = Colors.Yellow;
            s1.Offset = 0.25;
            l.GradientStops.Add(s1);
            GradientStop s2 = new GradientStop();
            s2.Color = Colors.Orange;
            s2.Offset = 1.0;
            l.GradientStops.Add(s2);
            textBlock1.Foreground = l ;
            //使用ImageBrush来填充矩形
            ImageBrush i = new ImageBrush();
            i.Stretch = Stretch.UniformToFill;
            i.ImageSource = new BitmapImage(new Uri ("ms-appx:///Assets/1.png",UriKind.Absolute ));
            rectangle1.Fill = i;
            //使用LinerrGradientBrush来设置背景
            LinearGradientBrush rb = new LinearGradientBrush();
            GradientStop s3 = new GradientStop();
            s3.Color = Colors.Yellow;
            s3.Offset = 0.25;
            rb.GradientStops.Add(s3);
            GradientStop s4 = new GradientStop();
            s4.Color = Colors.Orange;
            s4.Offset = 1.0;
            rb.GradientStops.Add(s4);
            GradientStop s5 = new GradientStop();
            s5.Color = Colors.Purple;
            s5.Offset = 0.5;
            rb.GradientStops.Add(s5);
            ellipsel2.Fill = rb;
            LinearGradientBrush rc = new LinearGradientBrush();
            GradientStop s6 = new GradientStop();
            s6.Color = Colors.Pink;
            s6.Offset = 0.5;
            rc.GradientStops.Add(s6);
            btn1.Background = rc;
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
