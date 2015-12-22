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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace FilmManagerment
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FileInfornation_User : Page
    {
        public FileInfornation_User()
        {
            this.InitializeComponent();
            Windows.Phone.UI .Input.HardwareButtons.BackPressed+=HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainView));
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            { 
                int i=Convert.ToInt32(e.Parameter );
                Film film = App.filmList[i];
                ImageBrush imageBrush = new ImageBrush();
                Uri uri = new Uri(film.PhotoSource);
                BitmapImage bitmap = new BitmapImage(uri);
                imageBrush.ImageSource = bitmap;
                photoBtn.Background=imageBrush ;
                scrollViewer.DataContext = film;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainView));
        }
      
    }
}
