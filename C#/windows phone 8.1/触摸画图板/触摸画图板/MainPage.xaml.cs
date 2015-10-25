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
using Windows.UI.Xaml.Shapes;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 触摸画图板
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
        private Point prePoint;
        private Point nextPoint;
        private void canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            prePoint = e.GetCurrentPoint(canvas).Position;
        }

        private void canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            nextPoint = e.GetCurrentPoint(canvas).Position;
            //Line l = new Line { X1 = prePoint.X, Y1 = prePoint.Y, X2 = nextPoint.X, Y2 = nextPoint.Y, StrokeThickness=5
            //    ,Stroke=new SolidColorBrush(Colors.Red )};
            Ellipse ellipse = new Ellipse { Width = 100, StrokeThickness = 5, Stroke = new SolidColorBrush(Colors.Red),Fill=new SolidColorBrush(Colors.Yellow )};
            canvas.Children.Add(ellipse);
            //prePoint = nextPoint;
        }
    }
}
