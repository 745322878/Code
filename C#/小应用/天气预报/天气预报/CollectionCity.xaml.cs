using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
    public sealed partial class CollectionCity : Page
    {
        List<Citys> cityName = new List<Citys>();
        private static Citys cityname;
        
        public CollectionCity()
        {
            this.InitializeComponent();
         
            Windows.Phone.UI.Input.HardwareButtons.BackPressed +=HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame ;
            if (frame == null)
                return ;

            if (DeleteAppbar.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                frame.Navigate(typeof(MainPage), cityname);
            else if (DeleteAppbar.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                frame.Navigate(typeof(CollectionCity));
            }
            e.Handled = true;
            

        }

      

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EditAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            AddAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            FindAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DeleteAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //if (e.Parameter != null)
            //{
            //    cityname.city = e.Parameter.ToString();
            //    cityName.Add(cityname);
                
            //}
            //colCityName.ItemsSource = cityName;
            
             Files.Items.Clear();
            //创建文件夹
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("CityList", CreationCollisionOption.OpenIfExists);
            //获取当前文件夹中的文件
            var files = await storage.GetFilesAsync();
            //便利 每一个文件
            foreach (StorageFile file in files)
            {
                XmlDocument doc=await XmlDocument.LoadFromFileAsync (file );
                //cityname.city = doc.DocumentElement.Attributes.GetNamedItem ("city").NodeValue.ToString ();
                cityname = new Citys();
                cityname.city = file.DisplayName;
                if (cityname.city == "City")
                    continue;
                else
                    cityName.Add(cityname);

            }
            Files.ItemsSource = cityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            (Window.Current.Content as Frame).Navigate(typeof(MainPage),btn.Content.ToString () );
        }


        private void EditAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            EditAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AddAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            FindAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DeleteAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DeleteAppbar.IsEnabled = false;
            Files.SelectionMode = Files.SelectionMode == ListViewSelectionMode.Multiple ? ListViewSelectionMode.None : ListViewSelectionMode.Multiple;
            //if (Files.SelectionMode== ListViewSelectionMode.Multiple)
            //{
            //    DeleteAppbar.IsEnabled = true;
            //}
        }
        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(AddCollectionCity));
        }

     
        private void ZoomAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(City));
        }

        //private void Button_Holding(object sender, HoldingRoutedEventArgs e)
        //{
          
        //    Button btn  = (Button) sender;
        //    TextBlock tb = new TextBlock();
        //    tb.Text ="删除";
        //    Flyout flyout = new Flyout();
        //    flyout.Content = tb;
        //    flyout.ShowAt(btn);
        //}

        async private void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Citys city2 in Files.SelectedItems)
            {
                cityName.Remove(city2);
            }
            //获取记事本的文件夹对象NoteList
            StorageFolder storage1 = await ApplicationData.Current.LocalFolder.CreateFolderAsync("CityList",CreationCollisionOption.ReplaceExisting);

            foreach (Citys city3 in cityName)
            {
                //创建一个XML对象
                XmlDocument _doc = new XmlDocument();
                //使用记事本名字创建一个XML元素作为根节点
                XmlElement _item = _doc.CreateElement(city3.city.ToString ());

                //使用属性来作为信息的标识符，使用属性值来存储相关的信息
                //_item.SetAttribute("city", t.Text );
                _doc.AppendChild(_item);
                //创建一个应用文件
                StorageFile file = await storage1.CreateFileAsync(city3.city.ToString() + ".xml", CreationCollisionOption.ReplaceExisting);
                //把XML的信息保存到文件中去
                await _doc.SaveToFileAsync(file);
            }
            (Window.Current.Content as Frame).Navigate(typeof(CollectionCity));
        }

        private void Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Files.SelectedItems.Count != 0)
                DeleteAppbar.IsEnabled = true;
            else
                DeleteAppbar.IsEnabled = false;
        }

       

     

       
    }
}
