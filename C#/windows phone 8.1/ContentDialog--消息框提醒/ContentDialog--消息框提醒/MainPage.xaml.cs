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

namespace ContentDialog__消息框提醒
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

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title="编程之道",
                Content="专注与windows phone开发",
                FullSizeDesired=false ,
                PrimaryButtonText="赞一个",
                SecondaryButtonText="顶一个"
            };
            dialog.PrimaryButtonClick+=dialog_PrimaryButtonClick;
            dialog.SecondaryButtonClick+=dialog_SecondaryButtonClick;
            ContentDialogResult result = await dialog.ShowAsync();
        }

        async private void dialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            await new MessageDialog("顶成功").ShowAsync();
        }

        async private void dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            await new MessageDialog("赞成功").ShowAsync();
        }

        async private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "编程小梦";
            dialog.Content = "专注开发Windows phone应用";
            dialog.PrimaryButtonText = "赞一个";
            dialog.SecondaryButtonText="顶一个";
            dialog.FullSizeDesired = true;
            await dialog.ShowAsync();
            
        }

        async private void Button2_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog1 contentDialog1 = new ContentDialog1();
            await contentDialog1.ShowAsync();
        }

        async private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog2 contentDialog2 = new ContentDialog2();
            await contentDialog2.ShowAsync();
        }
    }
}
