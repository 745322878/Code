using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.PersonalInformation;
using Windows.UI.Popups;
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
    public sealed partial class AddContact : Page
    {
        public AddContact()
        {
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

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            if (name.Text != "" && telphone.Text != "")
            {
                try
                {
                    //创建一个联系人的信息对象
                    ContactInformation conInfo = new ContactInformation();
                    //获取联系人的属性字典
                    var properties = await conInfo.GetPropertiesAsync();
                    //添加联系人的属性
                    properties.Add(KnownContactProperties.GivenName, name.Text);
                    properties.Add(KnownContactProperties.Telephone, telphone.Text);
                    //创建或者打开联系人存储
                    ContactStore conStore = await ContactStore.CreateOrOpenAsync();
                    StoredContact storedContact = new StoredContact(conStore, conInfo);
                    //保存联系人
                    await storedContact.SaveAsync();
                    message = "保存成功";
                }
                catch (Exception ex)
                {
                    message = "保存失败,错误信息:"+ex.Message ;
                }
            }
            else
            {
                message = "名字或电话不能为空";
            }
            await new MessageDialog(message ).ShowAsync ();
            (Window.Current.Content as Frame).Navigate(typeof(ContactsList));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ContactsList));
        }
    }
}
