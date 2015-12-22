using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace text
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
            SecondaryTile();
        }
        private async  void SecondaryTile()
        {
            Uri square71x71Logo = new Uri("ms-appx:///Assets/Square71x71Logo.scale-240.png");
            Uri square150x150Logo = new Uri("ms-appx:///Assets/Logo.scale-240.png");
            Uri wide310x150Logo = new Uri("ms-appx:///Assets/WideLogo.scale-240.png");
            string tileId = "App1";
            string tileArguments = "tileId" + " WasPinnedAt=" + DateTime.Now.ToLocalTime().ToString();
            SecondaryTile secondaryTile = new SecondaryTile(tileId, "TitleTest", tileArguments, square150x150Logo, TileSize.Square150x150);
      
            secondaryTile.VisualElements.Wide310x150Logo = wide310x150Logo;
            secondaryTile.VisualElements.Square150x150Logo = square150x150Logo;
            secondaryTile.VisualElements.Square71x71Logo = square71x71Logo;

            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            secondaryTile.VisualElements.ShowNameOnWide310x150Logo = true;

            bool isPinned = await secondaryTile.RequestCreateAsync();
        }
    }
}
