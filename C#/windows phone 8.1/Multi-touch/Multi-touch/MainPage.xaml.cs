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

namespace Multi_touch
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<double> leftposition =new List<double> ();
        List<double> rightposition = new List<double>();
        List<double> doublevalues = new List<double>();
       // List<double> doublevalues1 = new List<double>();

       // DispatcherTimer timer = new DispatcherTimer();
 
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            //timer.Interval = TimeSpan.FromSeconds(0.01);
            //timer.Tick += timer_Tick;

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

        /*   private void Page_Title_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
           {
               list.Items.Add("ManipulationStarting触发表示Manipulation系列事件开始");
               list.Items.Add("----------------------------------------------------");
           }*/

      /*  private void Page_Title_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            list.Items.Add("ManipulationStarted表示你的手指刚刚接触到屏幕");
            list.Items.Add("接触点X:" + e.Position.X + "  Y:" + e.Position.Y);
            list.Items.Add("----------------------------------------------------");
            position.Add(e.Position.X);
            position.Add(e.Position.Y);
        }

        private void Page_Title_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
           
            list.Items.Add("ManipulationDelta事件发生");
            list.Items.Add("变化 Translation X:" + e.Delta.Translation.X + "  Y:" + e.Delta.Translation.Y);

           // singlevalues.Add(e.Delta.Translation.X, e.Delta.Translation.Y);
            doublevalues.Add(e.Delta.Translation.Y);
             if (position.Count == 0 || position.Count == 4)
            {

                Debug.WriteLine("多指滑动");
            }
            else
            {
                Debug.WriteLine("单指滑动");
            }       
            //list.Items.Add("累增 Translation X:" + e.Cumulative.Translation.X + "  Y:" + e.Cumulative.Translation.Y);
            //list.Items.Add("线速度 Linear X:" + e.Velocities.Linear.X + "  Y:" + e.Velocities.Linear.Y);
            //list.Items.Add("----------------------------------------------------");
        }
        /*private void Page_Title_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            list.Items.Add("ManipulationInertiaStarting 发生延迟");
            list.Items.Add("变化 Translation X:" + e.Delta.Translation.X + "  Y:" + e.Delta.Translation.Y);
            list.Items.Add("累增 Translation Y:" + e.Cumulative.Translation.X + "  Y:" + e.Cumulative.Translation.Y);
            list.Items.Add("线速度 Linear X:" + e.Velocities.Linear.X + "  Y:" + e.Velocities.Linear.Y);
            list.Items.Add("----------------------------------------------------");
        }*/
        /*
        private void Page_Title_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

            list.Items.Add("ManipulationCompleted手指离开了屏幕");
            list.Items.Add("总的变化 Translation X:" + e.Cumulative.Translation.X + "  Y" + e.Cumulative.Translation.Y);
            list.Items.Add("最后的线速度 X:" + e.Velocities.Linear.X + e.Velocities.Linear.Y + "  IsInerial" + e.IsInertial);
            list.Items.Add("---------------------------------------------------");
            doublevalues.Clear();
            doublevalues1.Clear();
            position.Clear();
        }

        private void TextBlock_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            list.Items.Add("TextBlockManipulationStarted表示你的手指刚刚接触到屏幕");
            list.Items.Add("接触点X:" + e.Position.X + "  Y:" + e.Position.Y);
            list.Items.Add("----------------------------------------------------");
            position.Add(e.Position.X);
            position.Add(e.Position.Y);
        }

        private void TextBlock_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            list.Items.Add("TextBlockManipulationDelta事件发生");
            list.Items.Add("变化 Translation X:" + e.Delta.Translation.X + "  Y:" + e.Delta.Translation.Y);
          //  singlevalues.Add(e.Delta.Translation.X, e.Delta.Translation.Y);
            doublevalues1.Add(e.Delta.Translation.Y);
            if (position.Count == 0 || position.Count == 4)
            {

                Debug.WriteLine("多指滑动");
            }
            else
            {
                Debug.WriteLine("单指滑动");
            }       
            //list.Items.Add("累增 Translation X:" + e.Cumulative.Translation.X + "  Y:" + e.Cumulative.Translation.Y);
            //list.Items.Add("线速度 Linear X:" + e.Velocities.Linear.X + "  Y:" + e.Velocities.Linear.Y);
            //list.Items.Add("----------------------------------------------------");
        }

        private void TextBlock_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            list.Items.Add("TextBlockManipulationCompleted手指离开了屏幕");
            list.Items.Add("总的变化 Translation X:" + e.Cumulative.Translation.X + "  Y" + e.Cumulative.Translation.Y);
            list.Items.Add("最后的线速度 X:" + e.Velocities.Linear.X + e.Velocities.Linear.Y + "  IsInerial" + e.IsInertial);
            list.Items.Add("---------------------------------------------------");
            if (position.Count == 0 || position.Count == 4)
            {
                Debug.WriteLine("多指滑动");
            }
            else
            {
                Debug.WriteLine("单指滑动");
            }
            doublevalues.Clear();
            doublevalues1.Clear();
            position.Clear();
        }
        */
        private void TextBlock_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
           
            if (e.Delta.Expansion == 0)
            {
                leftposition.Add(e.Delta.Translation.X);
                rightposition.Add(e.Delta.Translation.Y);
                Debug.WriteLine("单指滑动");
            }
            else
            {
                if (Math.Abs(e.Delta.Expansion) <= 100)
                {
                    Debug.WriteLine("双指滑动");
                    doublevalues.Add(e.Delta.Translation.Y);
                }
                else if (Math.Abs(e.Delta.Expansion) >100)
                    Debug.WriteLine("双指捏合");
                else if (e.Delta.Scale == 0)
                {
                    Debug.WriteLine("111");
                }
            }

            
        }

    }
}
