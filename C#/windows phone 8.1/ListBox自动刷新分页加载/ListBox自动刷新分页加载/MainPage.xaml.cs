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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace ListBox自动刷新分页加载
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int pageCount =30;
        ScrollViewer scrollviewer;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            for (int i = 0; i < pageCount; i++)
            {
                listbox.Items.Add("编程小梦" + i); 
               
            }
            listbox.Loaded+=listbox_Loaded;
        }

        private void listbox_Loaded(object sender, RoutedEventArgs e)
        {
            scrollviewer =FindChildType<ScrollViewer >(listbox );
            scrollviewer.ViewChanged+=scrollviewer_ViewChanged;
        }

        private void scrollviewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollviewer == null)
            {
                throw new InvalidOperationException("error");
            }
            else
            {
                if (scrollviewer.VerticalOffset >= scrollviewer.Height)
                {
                    for (int i = 0; i < pageCount; i++)
                    {
                        int k = listbox.Items.Count;
                        listbox.Items.Add("编程小梦" + k);
                        k++;
                    }
                }
            }
        }
        static T FindChildType<T>(DependencyObject root) where T : class
        { 
            //创建一个队列来存放可视化数对象
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; i >= 0; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                        var typedChild=child  as T;
                    if(typedChild!=null)
                    {
                        return typedChild ;
                    }
                    queue.Enqueue (child);
                }
            }
            return  null;
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
