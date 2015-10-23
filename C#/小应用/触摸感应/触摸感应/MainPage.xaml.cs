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

namespace 触摸感应
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point start = new Point();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            ellipse.PointerEntered+=ellipse_PointerEntered;
            ellipse.PointerExited+=ellipse_PointerExited;
        }
        private void ellipse_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //获取点击处相对于image控件的坐标，作为开始点
            start = e.GetCurrentPoint(ellipse ).Position;
        }
        private void ellipse_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //获取结束的坐标点，也是相对于image控件的坐标点
            Point end = e.GetCurrentPoint(ellipse ).Position;
            double angle = 0;
            if (Math.Abs(end.X - start.X) < 1 && Math.Abs(end.Y - start.Y) < 1)
            {
                angle = 0;
            }
            else if (end.X > start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 360 - Math.Atan((end.Y - start.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI);
                }
                else
                {
                    angle = Math.Atan((start.Y - end.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI);
                }
            }
            else if (end.X < start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 180 + Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X) * 180 / Math.PI);
                }
                else
                {
                    angle = 180 - Math.Atan((start.Y - end.Y) * 1.0 / (start.X - end.X) * 180 / Math.PI);
                }
            }
            if (angle <= 45 || angle > 315)
            {
                //count++;
                //if (count < App.musicPhoto.Count)
                //{

                //    Image imgItem = new Image();

                //    imgItem.Width = 300;
                //    imgItem.Height = 370;
                //    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                //    image1.Children.Clear();
                //    this.image1.Children.Add(imgItem);
                //}
                //else
                //{
                //    count = App.musicPhoto.Count + 1;
                //}
                textBlock.Text = "从左向右滑";
            }
            else if (angle >= 135 && angle < 225)
            {
                textBlock .Text="从右向左滑";
                //count--;
                //if (count >= 0)
                //{
                //    Image imgItem = new Image();

                //    imgItem.Width = 300;
                //    imgItem.Height = 370;
                //    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                //    image1.Children.Clear();
                //    this.image1.Children.Add(imgItem);
                //}
                //else
                //{
                //    count = -1;
                //}
            }
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
