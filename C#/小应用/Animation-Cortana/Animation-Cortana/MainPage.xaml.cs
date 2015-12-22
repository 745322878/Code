using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Animation_Cortana
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int i = 0;
        int flag = 0;
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
            DAL.DAL.GetMusicPath();
            HardwareButtons.BackPressed+=HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          if(playstate.Content.ToString()=="播放"&&flag==0)
          {
              playstate.Content = "暂停";
              media.Source = new Uri(App.musicPath[i]);
              nametxt.Text = App.musicName[i];
              media.Play();
              myStoryboard.Begin();
                  
          }
          else if (playstate.Content.ToString() == "播放" && flag == 1)
          {
              media.Play();
              myStoryboard.Resume();
              playstate.Content = "暂停";
          }
          else if (playstate.Content.ToString() == "暂停")
          {
              playstate.Content = "播放";
              flag = 1;
              media.Pause();
              myStoryboard.Pause();
          }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myStoryboard.Stop();
            media.Stop();
            flag = 0;
            nametxt.Text = "";
            playstate.Content = "播放";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            i--;
            if (i <=-1)
                i = App.musicName.Count - 1;
            playstate.Content = "播放";
            flag = 0;
            Button_Click(null, null);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            i++;
            if (i >= App.musicName.Count )
                i = 0;
            playstate.Content = "播放";
            flag = 0;
            Button_Click(null, null);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(earPhonemode.Content.ToString()=="心动模式")
            {
                earPhonemode.Content = "FLY模式";
                media.IsMuted = true;
            }
            else if (earPhonemode.Content.ToString() == "FLY模式")
            {
                earPhonemode.Content = "心动模式";
                media.IsMuted = false;
            }
        
        }
    }
}
