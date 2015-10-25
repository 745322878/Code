using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.PersonalInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace 简易通讯录
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TelContact : Page
    {
        //联系人数据存储
        private ContactStore conStore;
        //联系人对象
        private StoredContact storCon;
        //联系人属性字典
        private IDictionary<string, object> properties;
        string ID;
        public TelContact()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected   override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                GetContact(e.Parameter .ToString ());
            }
        }
        class Item
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
        private async void GetContact(string id)
        {

            //通过联系人的Id获取联系人的信息
         
            //创建联系人存储
            conStore = await ContactStore.CreateOrOpenAsync();
            //查找联系人
            storCon = await conStore.FindContactByIdAsync(id);
            //获取联系人信息
            properties = await storCon.GetPropertiesAsync();
            name.Text = storCon.GivenName;
            telphone.Content  = properties[KnownContactProperties.Telephone ].ToString();
            ID = id;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).GoBack();
        }

        private async void tel_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri ("tel:"+telphone.Content ));
        }

        private async void money_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("msg:"+telphone .Content ));
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Item editItem = telphone.DataContext as Item;
            (Window.Current.Content as Frame).Navigate(typeof (EditContact ),ID);
        }
    }
}
