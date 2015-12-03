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
    public sealed partial class City : Page
    {
       
        List<Citys> cityNameList;
 
        private  static  Citys cityname,cityname1;
        public City()
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
            if (frame.CanGoBack )
            {
                frame.GoBack();
                //frame.Navigate (typeof(MainPage ),"");
                e.Handled = true;
            }
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (e.Parameter != null)
            {
                cityname = new Citys();

                cityname.city = e.Parameter.ToString();
                App.nearFindCity.Clear();
                //读取近期城市文件
                StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("NearCityList", CreationCollisionOption.OpenIfExists);
               //获取当前文件夹中的文件
                var files = await storage.GetFilesAsync();        
                //遍历每个文件 
                foreach (StorageFile file in files)
                 {
                     file.DateCreated.ToFileTime();
                    //XmlDocument doc=await XmlDocument.LoadFromUriAsync (new Uri(file.Path));
                    //cityname.city = doc.DocumentElement.Attributes.GetNamedItem("city").NodeValue.ToString();
                     cityname1 = new Citys();
                     cityname1.city = file.DisplayName;
                     if (cityname1.city == "City")
                         continue;
                     else
                     {
                         App.nearFindCity.Add(cityname1);
                     }
                 }
                 if (App.nearFindCity.Count == 0)
                     App.nearFindCity.Add(cityname);
                 else
                 {
                    //if(App.nearFindCity.Contains (cityname )==true )
                    //     return;
                     foreach (Citys  city5 in App.nearFindCity)
                     {
                         if (city5.city.ToString() == cityname.city.ToString()) 
                            goto loop;
                     }
                     
                         App.nearFindCity.Add(cityname);
                         if (App.nearFindCity.Count > 3)
                         {
                             do
                             {
                                 Random r = new Random();
                                 
                                 App.nearFindCity.RemoveAt(r.Next(2));
                             } while (App.nearFindCity.Count != 3);
                         }
                     
                 }
                
                  loop:
                  StorageFolder storage1 = await ApplicationData.Current.LocalFolder.CreateFolderAsync("NearCityList",CreationCollisionOption.ReplaceExisting);
                
                  foreach(Citys cityname2 in App.nearFindCity )
                 {
                     XmlDocument _doc = new XmlDocument();
                     XmlElement _item = _doc.CreateElement(cityname2.city.ToString ());
                     _doc.AppendChild(_item);
                     StorageFile file1 = await storage1.CreateFileAsync(cityname2.city.ToString() + ".xml", CreationCollisionOption.ReplaceExisting);
                     await _doc.SaveToFileAsync(file1);
                 }
               
              
            }
           
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
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
            nearCityFiles.ItemsSource = cityNameList;
        }
       

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
                FindCity();
        }

        private void textBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //nearCityFiles.Items.Clear();
            if ((textBox.Text).Length == 0)
            {
                nearCityFiles.ItemsSource = App.nearFindCity;
                nearCityFiles.Header = "近期搜索";
            }
            else
                nearCityFiles.Header = "";
        }



        //private void textBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    TextBlock t = sender as TextBlock ;
        //    (Window.Current.Content as Frame).Navigate(typeof(MainPage), t.Text);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            (Window.Current.Content as Frame).Navigate(typeof(MainPage ),btn.Content.ToString());
        }

      
    }
}
