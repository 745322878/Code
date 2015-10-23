using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace 加载更多
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Item> items { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

         
            items = new ObservableCollection<Item>();
            for (int i = 0; i < 10; i++)
            {
                Item item = new Item { FirstName = "编程" + i, LastName = "小梦" + i };
                items.Add(item);
            }
            this.DataContext = this;
            //真正实现了数据绑定
            this.NavigationCacheMode = NavigationCacheMode.Required;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            int count = items.Count;
            for (int i = count; i < count + 5; i++)
            {
                Item item = new Item { FirstName = "编程" + i, LastName = "小梦" + i };
                items.Add(item);
            }
        }

        async private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectinfo = "";
            foreach (var item in e.AddedItems)
            {
                selectinfo += (item as Item).FirstName + (item as Item).LastName;
                await  new  MessageDialog(selectinfo ).ShowAsync ();
            }
        }
    }
}
