using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace _555
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Storage : Page
    {
        List<MusicList> list1 = new List<MusicList>();

        int icount = 0;

        public Storage()
        {
            GetMusicName();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            this.InitializeComponent();
        }



        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async private void GetMusicName()
        {
            //StorageFolder local = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync("Music", CreationCollisionOption.OpenIfExists);
            var local = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Music");
            if (local != null)
            {
                var conList = await local.GetFilesAsync();
                //IReadOnlyList<StorageFile> conList = await local.GetFilesAsync();

                foreach (StorageFile strfile in conList)
                {
                    list1.Add(new MusicList
                    {
                        Name = strfile.DisplayName,
                    });
                }
                conListBox.ItemsSource = list1;

            }
        }
        private void listplay_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int i = -1;
            foreach (var music in list1)
            {
                i++;

                if (button.Content.ToString() == music.Name)
                {
                    icount = i;

                    Frame.Navigate(typeof(Play), icount);
                }

            }
        }
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
            //Frame frame = Window.Current.Content as Frame;
            //if (frame == null)
            //{
            //    return;
            //}
            //if (frame.CanGoBack)
            //{
            //    e.Handled = true;
            //    frame.GoBack();
            //}
        }
    }
}
