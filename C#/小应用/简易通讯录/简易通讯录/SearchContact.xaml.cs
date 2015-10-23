using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class SearchContact : Page
    {
        private ContactStore conStore;
        public SearchContact()
        {
            this.InitializeComponent();
        }
        public class Item
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter !=null)
            {
                GetContact(e.Parameter .ToString ());
            }           
        }
        private async void  GetContact(string p)
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
                            Name =strCon.GivenName ,
                            Id =strCon.Id ,
                        });
                }
            }
            conListBox.ItemsSource = list;
        }

       

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Item editItem = editButton.DataContext as Item;
            (Window.Current.Content as Frame).Navigate(typeof(EditContact),editItem.Id  );
        }
    }
}
