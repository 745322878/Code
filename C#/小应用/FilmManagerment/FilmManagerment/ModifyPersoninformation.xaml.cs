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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace FilmManagerment
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ModifyPersoninformation : Page
    {
        string fileName;
        public ModifyPersoninformation()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(MainView));
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
            if (e.Parameter != null)
            {
                User.Content = e.Parameter.ToString();
                if (SignIn.IsSignUp_User(e.Parameter.ToString()))
                {

                    fileName = "UserPassword";

                }
                else if (SignIn.IsSignUp_Admin(e.Parameter.ToString()))
                {
                    fileName = "AdminPassword";
                }

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainView));
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Text == "" || PasswordAgain.Text == "")
            {
                await new MessageDialog("输入不能为空").ShowAsync();
            }

            else if (Password.Text != PasswordAgain.Text)
            {
                await new MessageDialog("两次密码不同，请重新输入").ShowAsync();
            }
            else
            {
                SignIn.Registered(fileName, User.Content.ToString(), Password.Text);
                (Window.Current.Content as Frame).Navigate(typeof(MainView), User.Content.ToString());
            }
        }
    }
}
