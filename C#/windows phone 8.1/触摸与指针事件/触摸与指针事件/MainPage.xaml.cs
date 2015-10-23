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

namespace 触摸与指针事件
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CompositeTransform cpTrnsform = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            rect.ManipulationMode = ManipulationModes.TranslateRailsX | ManipulationModes.TranslateRailsY | ManipulationModes.Rotate;
            cpTrnsform = new CompositeTransform();
            cpTrnsform.TranslateX = cpTrnsform.TranslateY = 0;
            cpTrnsform.Rotation = 0;
            cpTrnsform.CenterX=rect.Width /2;
            cpTrnsform.CenterY=rect.Height/2;
            rect.RenderTransform = cpTrnsform;
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
        /// <summary>
        /// 对于UIElement对象的操作和延时完毕时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void rect_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-ManipulationCompleted事件发生",DateTime.Now .ToString ("H:m:s"));
        //}
        ///// <summary>
        ///// 当输入设备在操作期间更改位置时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rect_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Debug.WriteLine("{0}-ManipulationDelta事件发生", DateTime.Now.ToString("H:m:s"));
            cpTrnsform.TranslateX += e.Delta.Translation.X;
            cpTrnsform.TranslateY += e.Delta.Translation.Y;
            cpTrnsform.Rotation += e.Delta.Rotation;
        }
        /// <summary>
        /// 当输入设备在操作期间与UIElement对象失去联系且延时开始时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void rect_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-ManipulationInertiaStarting事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        ///// <summary>
        /// 当输入设备对UIElement 对象开始操作时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void rect_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-ManipulationStarted事件发生", DateTime.Now.ToString("H:m:s"));
        //}
        ///// <summary>
        ///     在首次创建操作处理器时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    ////    private void rect_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
    //    {
    //        Debug.WriteLine("{0}-ManipulationStarting事件发生", DateTime.Now.ToString("H:m:s"));
    //    }

        //private void rect_PointerCanceled(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerCanceled事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerEntered事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerExited事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerMoved事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerPressed事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_PointerReleased(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-PointerReleased事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_RightTapped(object sender, RightTappedRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-RightTapped事件发生", DateTime.Now.ToString("H:m:s"));
        //}

        //private void rect_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    Debug.WriteLine("{0}-Tapped事件发生", DateTime.Now.ToString("H:m:s"));
        //}
    }
}
