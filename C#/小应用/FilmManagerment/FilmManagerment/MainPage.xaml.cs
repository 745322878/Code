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

namespace FilmManagerment
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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

        }
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            //App.Current.Exit();
            e.Handled = true;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Password.Password = "";
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Accept.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Add.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void Adminstrator_Click(object sender, RoutedEventArgs e)
        {
            Accept.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Add.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(Register), "UserPassword");
        }

        async private void Accept_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            if (Client.IsChecked == true)
            {
                if (User.Text == "" || Password.Password == "")
                {
                    await new MessageDialog("输入不能为空").ShowAsync();
                }
                else
                {
                    foreach (var d in App.userpassworddictionary)
                    {
                        if (d.Key == User.Text && d.Value == Password.Password)
                        {
                            flag = 1;
                            break;
                        }

                    }
                    if (flag == 1)
                        (Window.Current.Content as Frame).Navigate(typeof(MainView), User.Text);
                    else if (flag == 0)
                        await new MessageDialog("输入有误").ShowAsync();

                }
            }
            else if (Adminstrator.IsChecked == true)
            {
                foreach (var d in App.adminpassworddictionary)
                {
                    if (d.Key == User.Text && d.Value == Password.Password)
                    {
                        flag = 1;
                        break;

                    }
                }
                if (flag == 1)
                    (Window.Current.Content as Frame).Navigate(typeof(MainView), User.Text);
                else if (flag == 0)
                    await new MessageDialog("输入有误").ShowAsync();

            }


        }
    }
}
