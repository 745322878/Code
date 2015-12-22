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

namespace Animation_SideBar
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point start = new Point();
        Point end = new Point();
        UIElement element;
        double prewidth=0;
        int flag = 0;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.SizeChanged += MainPage_SizeChanged;
            
           /* pointArea.PointerEntered += pointArea_PointerEntered;
            pointArea.PointerExited+=pointArea_PointerExited;*/
        }


        void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            element = new UIElement
            {

                Width = this.ActualWidth,
                Height = this.ActualHeight
            };
            stackpanel2.Width = element.Width;
        }

        private void stackpanel2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            start = e.GetCurrentPoint(grid ).Position;
        }
        private void CustomerAnimation(double  rangeWidth)
        {
            entranceanimation1.From = prewidth;
            entranceanimation1.To = rangeWidth;
            entranceanimation1.Duration = new Duration(TimeSpan.FromSeconds(0.1));
            prewidth = rangeWidth;
        
            entranceanimation.From = prewidth ;
            entranceanimation.To = rangeWidth;
            entranceanimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));

            entranceStoryboard.Begin();

        }
     
        private void stackpanel2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            end = e.GetCurrentPoint(grid  ).Position;
            //不是竖直滑动
            if ((end.Y - start.Y < 20) || (end.Y - start.Y) > -20)
            {
                //向右滑动
                if ((end.X - start.X) > 40)
                {
                    CustomerAnimation(end.X - start.X);

                }
                else
                    CustomerAnimation(start.X - end.X);
                
            }
        }

        private void stackpanel2_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
        //    if (stackpanel2.Width >= e.Delta.Translation.X)
        //    {
        //        if (stackpanel2.Width - e.Delta.Translation.X >= 0)
        //            stackpanel2.Width -= e.Delta.Translation.X;
        //    }
        }


        //Two

        //void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    element=new UIElement 
        //    {

        //         Width=this.ActualWidth,
        //         Height=this.ActualHeight
        //    };
        //    stackpanel1.Width = element.Width - 50;
        //}
        //private void stackpanel2_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    start=e.GetCurrentPoint(grid).Position;
        //}

        //private void stackpanel2_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    end = e.GetCurrentPoint(grid).Position;
        //    //不是竖直滑动
        //    if ((end.Y - start.Y < 20) || (end.Y - start.Y) > -20)
        //    {
        //        //向右滑动
        //        if ((end.X - start.X) > 40&&flag==0)
        //        {
        //            entranceStoryboard.Begin();
        //            flag = 1;
        //        }
        //        //向左滑动
        //        else if((end.X-start.X)<-40&&flag==1)
        //        {
        //            exitStoryboard.Begin();
        //            flag = 0;
        //        }    
        //    }
        //}

       
        //One

       /* private void pointArea_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            end = e.GetCurrentPoint(pointArea).Position;
            double angle = 0;
            if (Math.Abs(end.X - start.X) < 1 && Math.Abs(end.Y - start.Y) < 1)
            {
                angle = 0;
            }
            else if (end.X > start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 360 - Math.Atan(end.Y - start.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI;
                }
                else
                {
                    angle = Math.Atan(start.Y - end.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI;
                }
            }
            else if (end.X < start.X)
            {
                if (end.Y > start.Y)
                {
                    angle =Math.Atan(end.Y - start.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI+180;
                }
                else
                {
                    angle = 180-Math.Atan(start.Y - end.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI;
                }
            }
            if ((angle <= 45 || angle > 315 )&&flag==0)
            {
                flag = 1;
                myStoryboard.Begin();
                
            }
            if ((angle >= 135 && angle < 225)&&flag==1)
            {
                flag = 0;
                exitStoryboard.Begin();
            }
        }

        private void pointArea_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            start = e.GetCurrentPoint(pointArea).Position;
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

        private void CustomAnimation(double endheight)
        {
            double height = stackpanelDown.Height;
            doubleanmation.From = height;
            doubleanmation.To = endheight;

            doubleanmation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
       
            storyboard.Begin();


         
            
        }
        private void stackpanelDown_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            double i = e.Cumulative.Translation.Y;
            if (stackpanelDown.Height >= (int)e.Delta.Translation.Y)
            {
                if (stackpanelDown.Height - (int)e.Delta.Translation.Y >60)
                {
                    stackpanelDown.Height -= (int)e.Delta.Translation.Y;
                  
                }
             
            }
        }

        private void stackpanelDown_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            start = e.GetCurrentPoint(null).Position;
        }

        private void stackpanelDown_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            end = e.GetCurrentPoint(null).Position;
           if (stackpanelDown.Height >= 400)
            {
                CustomAnimation(600);

            }
            else if(stackpanelDown.Height<400)
            {
                CustomAnimation(60);
            }
        
            //判断相对向左还是向右
            if (end.Y - start.Y < 20 || end.Y - start.Y > -20)
            {
                if ((end.X - start.X) > 40)
                {
                    // ComeVoice.GetVoice("向右滑动");
                    VoiceHandleAsync("下一页ppt");
                    handleModel.Handle = "Cmd";
                    handleModel.Message = App.Office["下一页PPT"];
                    App.MessageHandle(JsonConvert.SerializeObject(handleModel), App.socketCmd);
                }
                else if ((end.X - start.X) < -40)
                {
                    // ComeVoice.GetVoice("向左滑动");
                    VoiceHandleAsync("上一页ppt");
                    handleModel.Handle = "Cmd";
                    handleModel.Message = App.Office["上一页PPT"];
                    App.MessageHandle(JsonConvert.SerializeObject(handleModel), App.socketCmd);
                }
            }
        }*/
    }
}
