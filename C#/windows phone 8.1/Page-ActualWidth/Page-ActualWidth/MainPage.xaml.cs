using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Page_ActualWidth
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
            this.SizeChanged += MainPage_SizeChanged;
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;
            //进入顺序
            /*
             * 1.构造函数
             * 2.OnNavigatedTo()
             * 3.Loaded()
           */

            //离开顺序
            /*
             * 1.OnNavigatedFrom()
             * 2.UnLoaded();
             */
        }

        void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UIElement element = new UIElement
            {
                Height = this.ActualHeight,
                Width = this.ActualWidth
            };
            double  width1 = page.ActualWidth;
            double height1 = page.ActualHeight;
            double width2 = grid.ActualWidth;
            double height2 = grid.ActualHeight;
        }

       
        void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Unloaded事件执行！！！");

        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Loaded事件执行！！！");

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
            Debug.WriteLine("OnNavigatedTo事件执行！！！");

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("OnNavigatedFrom事件执行!!!");
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = (Window.Current.Content) as Frame;
            frame.Navigate(typeof(BlankPage1));
        }

     
    }
}
