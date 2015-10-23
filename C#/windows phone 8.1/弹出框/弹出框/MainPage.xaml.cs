using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 弹出框
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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed+=HardwareButtons_BackPressed;
        }

        private async void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            //获取当前的激活的窗口的框架
            Frame frame = Window.Current.Content as Frame;
            //判断是否为空，是否能返回，其实就是主页面了，主页面肯定不能返回嘛
            if (null != frame && (!frame.CanGoBack))
            {
                //设置事件已经处理
                e.Handled = true;
                //设置在最后一个界面跳出弹出窗口
                var messageDig = new MessageDialog("确定退出吗？");

                var btn_OK = new UICommand("确定");
                var btn_NO = new UICommand("取消");

                messageDig.Commands.Add(btn_OK);
                messageDig.Commands.Add(btn_NO);

                //展示窗口，获取按钮是否退出
                var result = await messageDig.ShowAsync();
                //如果是确定退出就直接让应用程序退出
                if (null != result && result.Label == "确定")
                {
                    Application.Current.Exit();
                }


            }
            //如果可以返回，就返回上一个界面
            else if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
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
