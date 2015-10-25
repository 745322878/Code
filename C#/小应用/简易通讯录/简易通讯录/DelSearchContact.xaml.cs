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
    public sealed partial class DelSearchContact : Page
    {
        private ContactStore conStore;
        ContactQueryResult conQueryResult;
        IReadOnlyList<StoredContact> conList;
        List<string> selected_ID = new List<string>();
        public DelSearchContact()
        {
            this.InitializeComponent();
        }
        class Item
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
        private async void GetContact(string p)
        {
            //创建联系人存储
            conStore = await ContactStore.CreateOrOpenAsync();
            //联系人查询结果
            ContactQueryResult conQueryResult = conStore.CreateContactQuery();
            //查询联系人
            IReadOnlyList<StoredContact> conList = await conQueryResult.GetContactsAsync();
            List<Item> list = new List<Item>();
            foreach (StoredContact strCon in conList)
            {
                if (strCon.GivenName.Contains(p))
                {
                    list.Add(new Item
                    {
                        Name = strCon.GivenName,
                        Id = strCon.Id,
                    });
                }
            }
            conListBox.ItemsSource = list;
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
                GetContact(e.Parameter.ToString());
            }           
        }

     

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
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).GoBack();
        }

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
        }

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

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            del_Message.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
