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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace ScrollViewer滚动试图
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //往下滚动的定时触发器
        private DispatcherTimer tmrDown;
        //往上滚动的定时触发器
        private DispatcherTimer tmrUp;
        public MainPage()
        {
            this.InitializeComponent();
            //添加图片到scrolViwer里的stackpanel
            for (int i = 0; i < 30; i++)
            {
                Image imgItem = new Image();
                imgItem.Width = 200;
                imgItem.Height = 200;
                if (i % 4 == 0)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///1.jpg", UriKind.RelativeOrAbsolute)));
                }
                else if (i % 4 == 1)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///android.png", UriKind.RelativeOrAbsolute)));
                }
                else if (i % 4 == 2)
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///windowsmobile.png", UriKind.RelativeOrAbsolute)));
                }
                else
                {
                    imgItem.Source = (new BitmapImage(new Uri("ms-appx:///mp3.png", UriKind.RelativeOrAbsolute)));
                }
                this.stkpnllImage.Children .Add (imgItem );
            }
            ////初始化tmrDown定时触发器
            //tmrDown =new DispatcherTimer ();
            ////每500毫秒跑一次
            //tmrDown.Interval = TimeSpan.FromSeconds(10);
            ////加入每次tick事件
            //tmrDown.Tick +=tmrDown_Tick;
            
            //tmrUp =new DispatcherTimer ();
            ////每500毫秒跑一次
            ////tmrUp .Interval =new TimeSpan (500);
            //tmrUp.Interval = TimeSpan .FromSeconds(10);
            ////加入每次tick事件
            //tmrUp.Tick +=tmrUp_Tick;
            //this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        //void tmrUp_Tick(object sender ,object e)
        //{
            
        //    //tmrDown.Stop();
        //    //tmrUp.Start();
        //    //将VerticalOffset -10将出现图片即将往上滚动的效果
        //    //scrollViewer1.ChangeView(scrollViewer1.VerticalOffset - 10.5f, scrollViewer1.HorizontalOffset,scrollViewer1 .ZoomFactor );
        //    scrollViewer1.ChangeView(scrollViewer1 .HorizontalOffset,scrollViewer1.VerticalOffset,10f);
        //}
        //void tmrDown_Tick(object sender ,object  e)
        //{
        //    //tmrUp.Stop();           
        //    //tmrDown.Start();
        //    scrollViewer1.ChangeView(scrollViewer1.HorizontalOffset, scrollViewer1.VerticalOffset, 10f);
        //}
        ////往上按钮事件
        //private  void btnUp_Click(object sender ,RoutedEventArgs e)
        //{
        //    ////先停下往下的定时触发器
        //    //tmrUp.Stop();
        //    //tmrDown.Stop();
        //    //////tmrUp定时触发器开始
        //    //tmrUp.Start();
        //    scrollViewer1.ScrollToVerticalOffset(scrollViewer1.VerticalOffset + 10.5f);
           
        //}
        ////往下按钮事件
        //private  void btnDown_Click(object sender ,RoutedEventArgs  e)
        //{
        //    scrollViewer1.ScrollToVerticalOffset(scrollViewer1.VerticalOffset - 10.5f); ;
        //}
        //private  void btnStop_Click(object sender ,RoutedEventArgs  e)
        //{
        //    tmrUp.Stop ();
        //    tmrDown .Stop ();
        //}

      
    }

}

