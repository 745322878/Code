using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 读取安装包文件
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

       async  private void btGetFolder_Click(object sender, RoutedEventArgs e)
        {
            lbFolder.Items.Clear();
            StorageFolder localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            foreach (StorageFolder folder in await localFolder.GetFoldersAsync())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = "应用程序目录" + folder.Name;
                item.DataContext = folder;
                lbFolder.Items.Add(item);
            }
            lbFile.Items.Clear();
            foreach (StorageFile file in await localFolder.GetFilesAsync())
            {
                ListBoxItem item1 = new ListBoxItem();
                item1.Content = "文件" + file.Name;
                item1.DataContext = file;
                lbFolder.Items.Add(item1);
            }

        }

        async private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            if (lbFolder.SelectedItem == null)
            {
                await new MessageDialog("请选择一个文件夹").ShowAsync();
            }
            else 
            {
                ListBoxItem item = lbFolder.SelectedItem as ListBoxItem;
                //获取文件夹内容
                StorageFolder folder = item.DataContext as StorageFolder;
                lbFolder.Items.Clear();
                foreach (StorageFolder folder1 in await folder.GetFoldersAsync())
                {
                    ListBoxItem item1 = new ListBoxItem();
                    item1.Content = "文件夹：" + folder1.Name;
                    item1.DataContext = folder1;
                    lbFolder.Items.Add(item1);
                }
                lbFile.Items.Clear();
                foreach (StorageFile file in await folder.GetFilesAsync())
                {
                    ListBoxItem item2 = new ListBoxItem();
                    item2.Content = "文件：" + file.Name;
                    item2.DataContext = file;
                    lbFolder.Items.Add(item2);
                }
            }
        }

        private void createFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
