using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace XML文件存储_购物清单
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Load;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        async void MainPage_Load(object sender, RoutedEventArgs e)
        {
            Files.Items.Clear();
           
            //获取应用程序的本地存储文件NoteList
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("NoteList", CreationCollisionOption.OpenIfExists);
            //获取当前文件夹的文件
            var files = await storage.GetFilesAsync();
            {
                //便利 获取所有文件
                foreach (StorageFile file in files)
                {
                    //初始化一个Grid类
                    Grid a = new Grid();
                    //列的属性
                    ColumnDefinition col = new ColumnDefinition();
                    //宽度200
                    GridLength gl = new GridLength(200);
                    col.Width = gl;
                    //添加col
                    a.ColumnDefinitions.Add(col);
                    //定义第二列
                    ColumnDefinition col2 = new ColumnDefinition();
                    GridLength gl2 = new GridLength(100);
                    col2.Width = gl2;//////////////
                    a.ColumnDefinitions.Add(col2);
                    //定义第三列
                    ColumnDefinition col3 = new ColumnDefinition();
                    GridLength g13 = new GridLength(100);
                    col3.Width = g13;
                    a.ColumnDefinitions.Add(col3);
                    //添加一个TextBlock显示记事本名到第一列
                    TextBlock txbx = new TextBlock();
                    txbx.Text = file.DisplayName;
                    txbx.FontSize = 20;
                    //把txbx设置到Grid 0号位置
                    Grid.SetColumn(txbx, 0);
                    //添加一个HyperlinkButton链接到记事本页面，这是第二列
                    //HyperlinkButton 可跳转Button
                    HyperlinkButton btn = new HyperlinkButton();
                    btn.Width = 80;
                    btn.Content = "删除";
                    btn.Foreground  = new SolidColorBrush(Colors.Blue );
                    btn.FontSize = 20;
                    btn.Name = file.DisplayName;
                    //Lambda表达式
                    //事件添加一个Lambda表达式
                    //等价于下面btn_Click方法
                    btn.Click += (s ,ea) =>
                        {
                            Frame.Navigate(typeof(DeleteItem ),file );
                        };
                    Grid.SetColumn(btn ,1);
                    //添加一个HyperlinkButton链接到记事本页面,这是第三列
                    HyperlinkButton btn1 = new HyperlinkButton();
                    btn1.Width = 80;
                    btn1.Content = "打开";
                    btn1.FontSize = 20;
                    btn1.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    btn1.Name = file.DisplayName;
                    btn1.Click += (s, ea) =>
                    {
                        Frame.Navigate(typeof(AddItem), file);
                    };
                    Grid.SetColumn(btn1, 2);
                    //给Grid a 添加子元素
                    a.Children.Add(txbx);
                    a.Children.Add(btn);
                    a.Children.Add(btn1);
                    //将 a 添加到Files中
                    Files.Items.Add(a);
                }
            }
        }
         //private void btn_Click(object s,RoutedEventArgs  ea) 
         //               {
         //                   Frame.Navigate(typeof(DeleteItem ));
         //               }
        //添加按钮
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItem));
        }
        //格式化按钮
        private  async void AppBarButton_Delete_Click(object sender, RoutedEventArgs e)
        {
            Files.Items.Clear();
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("NoteList", CreationCollisionOption.OpenIfExists);
            var files = await storage.GetFilesAsync();

            foreach (StorageFile file in files)
            {
                 
                await file.DeleteAsync();                          
            }
            await new MessageDialog("格式化成功").ShowAsync();
            
        }
        
    }

    /// <summary>
    /// 在此页将要在 Frame 中显示时进行调用。
    /// </summary>
    /// <param name="e">描述如何访问此页的事件数据。
    /// 此参数通常用于配置页。</param>
    //protected override void OnNavigatedTo(NavigationEventArgs e)
    //{
    //    // TODO: 准备此处显示的页面。

    //    // TODO: 如果您的应用程序包含多个页面，请确保
    //    // 通过注册以下事件来处理硬件“后退”按钮:
    //    // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
    //    // 如果使用由某些模板提供的 NavigationHelper，
    //    // 则系统会为您处理该事件。
    //}
}
