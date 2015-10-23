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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 浏览照片
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
            List<Image> list = new List<Image>();
            list.Add(new Image { ImageSource = "Assets/2.jpg" });
            list.Add(new Image { ImageSource = "Assets/3.jpg" });
            list.Add(new Image { ImageSource = "Assets/4.jpg" });
            filpview.ItemsSource = list;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3.0);
            timer.Tick += ((sender, et) =>
                {
                    if (filpview.SelectedIndex < filpview.Items.Count - 1)
                        filpview.SelectedIndex++;
                    else
                        filpview.SelectedIndex = 0;

                });
            timer.Start();
        }
    }
}
