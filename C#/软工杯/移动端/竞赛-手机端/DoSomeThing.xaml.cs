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
using System.Threading;
using Windows.Storage.Streams;
using System.Text;
using System.Diagnostics;
using Windows.UI.Popups;
using System.Threading.Tasks;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace 竞赛_手机端
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DoSomeThing : Page
    {

        //List<double> leftposition = new List<double>();
        //List<double> rightposition;
        //List<double> doublevalues = new List<double>();
        //List<double> expansion;
        //List<double> scale ;
        //double pre_expansion = 0;
  
        string data;
        public DoSomeThing()
        {
            this.InitializeComponent();
        }



       
        private async void Send(double  setptX,double setptY)
        {
            byte[] buffer;           
            data = setptX.ToString() + ',' + setptY.ToString() + ',' + "MOVE";
            DataWriter writer=new DataWriter(MainPage.clientSocket.OutputStream);
            buffer = Encoding.UTF8.GetBytes(data);
            writer.WriteBytes(buffer);
            await writer.StoreAsync();
            writer.DetachStream();
            writer.Dispose();
        }
        private async void SendDirection(double setptX,double setptY,string direction)
        {
            byte[] buffer;
            data = setptX.ToString() + ',' + setptY.ToString() + ',' + direction;
            DataWriter writer = new DataWriter(MainPage.clientSocket.OutputStream);
            buffer = Encoding.UTF8.GetBytes(data);
            writer.WriteBytes(buffer);
            await writer.StoreAsync();
            writer.DetachStream();
            writer.Dispose();
        }
        private async void DisPose()
        {
            MainPage.clientSocket.Dispose();
            await new MessageDialog("已经和PC端断开连接！").ShowAsync();           
            Frame.Navigate(typeof(MainPage));
            
        }



        private void ellipsel_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.Delta.Expansion == 0)
            {
                Send(e.Delta.Translation.X, e.Delta.Translation.Y);
                Debug.WriteLine("单指滑动");
            }
            else
            {
              
            
                if(e.Delta.Scale>0.85&&e.Delta.Scale<1.15&&Math.Abs(e.Delta.Expansion)<=20)
                {
                    Debug.WriteLine("双指滑动");
                    Debug.WriteLine(e.Delta.Translation.X);
                    Debug.WriteLine(e.Delta.Translation.Y);
                    //doublevalues.Add(e.Delta.Translation.Y);
                    if (e.Delta.Translation.Y > 0)
                        SendDirection(0, 0,"WHEELUP");
                    else if(e.Delta.Translation.Y<0)
                        SendDirection(0,0,"WHEELDOWN");
                }
                else if (e.Delta.Scale > 1.15)
                {
                    SendDirection(0, 0, "BIG");
                    Debug.WriteLine(e.Delta.Translation.X);
                    Debug.WriteLine(e.Delta.Translation.Y);
                }
                else if (e.Delta.Scale < 0.85)
                {
                    SendDirection(0, 0, "SMALL");
                    Debug.WriteLine(e.Delta.Translation.X);
                    Debug.WriteLine(e.Delta.Translation.Y);
                }     
            }
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendDirection(0,0,"RIGHT");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendDirection(0,0,"LEFT");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DisPose();
        }

       


      
    }
}
