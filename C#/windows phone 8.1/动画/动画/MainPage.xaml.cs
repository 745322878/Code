using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 动画
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
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

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    myStoryboard.Begin();
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    storyboard.Begin();
        //}

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //获取单击的按钮事件
            Button btn = sender as Button;
            //获取按钮的RenderTransForm属性
            RotateTransform rotatetransform = btn.RenderTransform as RotateTransform;
            //创建一个DoubleAnimation动画
            DoubleAnimation anima = new DoubleAnimation();
            anima.From = 0;
            anima.To = 360;
            anima.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            //设置动画的Target属性和TargetProperty属性
            Storyboard.SetTarget(anima, rotatetransform);
            Storyboard.SetTargetProperty(anima, "Angle");
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(anima);
            storyboard.Begin();
        }
    }
}
