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
    public sealed partial class ContactsList : Page
    {
        //联系人存储
        private ContactStore conStore;

        public ContactsList()
        {
            this.InitializeComponent();
            GetContacts();
        }
         class Item
        {
            public string Name { get; set; }
            public string Id { get; set; }
          
        }
       
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        private async void GetContacts()
        {
            conStore = await ContactStore.CreateOrOpenAsync();
            ContactQueryResult conQueryResult = conStore.CreateContactQuery();
            //查询联系人
            IReadOnlyList<StoredContact> conList = await conQueryResult.GetContactsAsync();
            List<Item> list = new List<Item>();
            foreach (StoredContact storCon in conList)
            {
                var properties = await storCon.GetPropertiesAsync();
                list.Add(new Item 
                {
                    Name=storCon .GivenName ,
                    Id =storCon .Id ,
                });
            }
            conListBox.ItemsSource = list;
        }
       
        /// <summary>
        /// 编辑联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Item editItem = editButton.DataContext as Item;
            (Window.Current.Content as Frame).Navigate(typeof(TelContact), editItem.Id);
           // (Window.Current.Content as Frame ).Navigate(typeof (TelContact));
        }

        /// <summary>
        /// 新建联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AddContact));
        }

        private void DelContact_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DelContact));
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(SearchContact),search_Text.Text);
        }
        
    }
}
