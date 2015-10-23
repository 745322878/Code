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
    public sealed partial class DelContact : Page
    {
        private ContactStore conStore;
        ContactQueryResult conQueryResult;
        IReadOnlyList<StoredContact> conList;
        List<string> selected_ID = new List<string>();
        public DelContact()
        {
            this.InitializeComponent();
            GetContacts();
        }
        class Item
        {
            public string Name { get; set; }
            public string Id { get; set;  }
        }
        /// <summary>
        /// 获取联系人
        /// </summary>
        private async void GetContacts()
        {
            //创建联系人存储
            conStore = await ContactStore.CreateOrOpenAsync();
            //联系人查询的结果
            conQueryResult = conStore.CreateContactQuery();
            //联系人集合
            conList = await conQueryResult.GetContactsAsync();
            //显示联系人的集合
           
            List<Item> list = new List<Item>();
            foreach (StoredContact storCon in conList)
            {
                var properties = await storCon.GetPropertiesAsync();
                list.Add(new Item
                    {
                        Name = storCon.GivenName,
                        Id =storCon .Id 
                    });
            }
            conListBox.ItemsSource = list;
        }    
       
        /// <summary>
        /// 点击CheckBox获取选择的CheckBox的ID集合的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            //获取该行id
            string Id = cb.Content.ToString();
            if ((bool)cb.IsChecked)
            {
                selected_ID.Add(Id);
            }
            else
            {
                selected_ID.Remove(Id);
            }
        }
        /// <summary>
        /// 删除操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selected_ID.Count > 0)
            {
                btn_Confirm.Content = "确定删除" + selected_ID.Count + "项";
                del_Message.Visibility = Windows.UI.Xaml.Visibility.Visible;
                button.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                await new MessageDialog("请先选择").ShowAsync();
            }
            // GetContacts();

        }
        /// <summary>
        /// 取消操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).GoBack();
        }
       
      
        /// <summary>
        /// 确认删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var id in selected_ID)
            {
                foreach (StoredContact strContact in conList)
                {
                    if (id == strContact.Id)
                    {
                        await conStore.DeleteContactAsync(id);
                    }
                }
            }
            del_Message.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button.Visibility = Windows.UI.Xaml.Visibility.Visible;           
            (Window.Current.Content as Frame).Navigate(typeof(DelContact));
            
        }
        //取消事件
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            del_Message.Visibility = Windows.UI.Xaml.Visibility.Collapsed ;
            button.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(DelSearchContact), search_Text.Text);
        }
    }
}
