﻿using System;
using System.IO;
using Windows.Phone.PersonalInformation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 联系人存储
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

        private async  void bt_add_Click(object sender, RoutedEventArgs e)
        {
            //创建一个系统通信可以读写和其他程序制度的联系人存储
            ContactStore conStore = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            //新增联系人
            ContactInformation conInfo = new ContactInformation();
            //获取ContacInformation类的属性map表
            var properties = await conInfo.GetPropertiesAsync();
            //添加电话属性
            properties.Add(KnownContactProperties.FamilyName, "test");
            properties.Add(KnownContactProperties.Telephone, "123456789");
         
            //创建联系人对象
            StoredContact storedContact = new StoredContact(conStore, conInfo);
            //获取安装包的一张图片文件用作联系人的头像
            StorageFile imagefile = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Assets/1.jpeg");
                
            
            ///设置头像，将图片数据转化成Stream对象，在转化成IInputStream对象。
            //打开文件的可读数据流
            Stream stream = await imagefile.OpenStreamForReadAsync();
            //用Stream对象转化成为IIputStream对象
            IInputStream inputStream = stream.AsInputStream();
            //用IInputStream 对象设置为联系人头像
            await storedContact.SetDisplayPictureAsync(inputStream);
            //保存联系人
            await storedContact.SaveAsync();
            
            ///获取头像，接收到的图片数据为IRandomAccessStream类型，如果要展示出来，需要创建一个BitmapImage对象,
            IRandomAccessStream raStream = await storedContact.GetDisplayPictureAsync();
            BitmapImage bi = new BitmapImage();
            bi.SetSource(raStream);
            image.Source = bi;
        }
    }
}
