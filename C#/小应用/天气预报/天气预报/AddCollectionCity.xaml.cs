using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace 天气预报
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AddCollectionCity : Page
    {
    
        List<Citys> cityNameList;
       
      
        public AddCollectionCity()
        {
            this.InitializeComponent();
            

            
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }
            if (frame.CanGoBack)
            {
                //frame.Navigate(typeof(CollectionCity));
                frame.GoBack();
                e.Handled = true;
            }

        }
         private void FindCity()
        {
         
            cityNameList = new List<Citys>();
            foreach (Citys cityname in App.citynameList)
            {
                if (cityname.city.Contains(textBox.Text))
                {
                    cityNameList.Add(new Citys
                    {
                        city = cityname.city,
                    });
                }

            }
            cityName.ItemsSource = cityNameList;
        }

      
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            FindCity();
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //获取记事本的文件夹对象NoteList
            StorageFolder storage = await ApplicationData.Current.LocalFolder.GetFolderAsync("CityList");


            //创建一个XML对象
            XmlDocument _doc = new XmlDocument();
            //使用记事本名字创建一个XML元素作为根节点
            XmlElement _item = _doc.CreateElement(btn.Content.ToString());

            //使用属性来作为信息的标识符，使用属性值来存储相关的信息
            //_item.SetAttribute("city", t.Text );
            _doc.AppendChild(_item);
            //创建一个应用文件
            StorageFile file = await storage.CreateFileAsync(btn.Content.ToString() + ".xml", CreationCollisionOption.ReplaceExisting);
            //把XML的信息保存到文件中去
            await _doc.SaveToFileAsync(file);




            (Window.Current.Content as Frame).Navigate(typeof(CollectionCity));
        }
    }
}
