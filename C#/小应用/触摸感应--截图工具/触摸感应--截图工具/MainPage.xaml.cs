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
using Windows.UI.Xaml.Shapes;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 触摸感应__截图工具
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            button.Click +=button_Click;
            SetPicture();
            
        }

     
        //使用触摸感应选取截图区域
        void SetPicture()
        {
            Rectangle rect = new Rectangle();
            rect.Opacity = 0.5;
            rect.Fill = new SolidColorBrush(Colors.White );
            rect .Height =image1 .Height ;
            rect.Width = image2.Width;
            rect.Stroke = new SolidColorBrush(Colors.Red);
            rect.StrokeThickness = 2;
            
            //添加触摸滑动过程的事件监控
            rect.ManipulationMode = ManipulationModes.All;
            rect.ManipulationDelta +=rect_ManipulationDelta;
            //把Rectangle 控件添加到LayoutRoot上，这时候控件会出现在图片上方
            LayoutRoot.Children.Add(rect);
            LayoutRoot.Height = image1.Height;
            LayoutRoot.Width = image1.Width;
        }

        private void rect_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Rectangle croppingRectangle = (Rectangle)sender;
            if (croppingRectangle.Width >= (int)e.Delta.Translation.X)
            {
                croppingRectangle.Width -= (int)e.Delta.Translation.X;
                
            }
            if (croppingRectangle.Height >= (int)e.Delta.Translation.Y)
            {
                croppingRectangle.Height -= (int)e.Delta.Translation.Y;
            }
        }
        void ClipImage()
        {
            RectangleGeometry geo = new RectangleGeometry();
            //LINQ查询，Query语法
            Rectangle r = (Rectangle)(from c in LayoutRoot.Children 
                                      where c.Opacity == 0.5 
                                      select c).First();
            GeneralTransform gt = r.TransformToVisual(LayoutRoot);
            Point p = gt.TransformPoint(new Point(0, 0));
            geo.Rect = new Rect(p.X, p.Y, r.Width, r.Height);
            image1.Clip = geo;
            r.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            ClipImage();
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(LayoutRoot);
            image2.Source = bitmap;
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
