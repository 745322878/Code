using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 天气预报
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private HttpClient httpClient;
        private static WeatherData weatherData;
        private static Btn bt;
        List<Btn> btn = new List<Btn>();
        public static string cityName="西安";
       
        public MainPage()
        {
            
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
           
             httpClient = new HttpClient();
             Show(cityName );
             this.Loaded += MainPage_Loaded;
             Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

             SyncAppBarButton_Click(null, null);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SyncAppBarButton_Click(null, null);
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
                frame.GoBack();
                e.Handled = true;
            }

        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.ToString() == "天气预报.Citys")             
            {
                cityName = "西安"; 
            }
            else if (e.Parameter.ToString() == "")
            {
                if (textCity.Text == "西安" || textCity.Text == "")
                    cityName = "西安";
                else  if (textCity.Text != "西安")
                    cityName = textCity.Text;
                
            }
            else  if (e.Parameter.ToString() != "")
            {
                cityName = e.Parameter.ToString();
            }
          
      
        }
        
        public static WeatherData JsonAnalytical(string jsonString)
        {
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(WeatherData));
             weatherData = (WeatherData )obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonString)));
            return weatherData;
        }
        public static  WeatherData XmlAnalytical(string xmlString)
        {
            XmlDocument document = new XmlDocument( );
            document.CreateElement("Employees");
            // XmlRootAttribute Root = new XmlRootAttribute();
            //   Root.ElementName = "weatherDatas";
            XmlSerializer sr = new XmlSerializer(typeof(WeatherData));
            ///StringReader strReader = new StringReader(xmlString);
            //System.Xml.XmlReader  r = new System.Xml();
            //weatherData = (WeatherData)sr.Deserialize(r);
            //return weatherData;
            DataContractSerializer obj = new DataContractSerializer(typeof(WeatherData));
             weatherData = (WeatherData)obj.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            return weatherData;
        }
        
           
        
         async public void Show(string  cityName)
        {

            string Address = "http://api.map.baidu.com/telematics/v3/weather?location=" + cityName + "&output=json&ak=Gi27P5bmIinr86htrjU4ESnY";


            try
            {

                //var strinfo = await httpClient.GetStringAsync(Address);
                var strinfo = await httpClient.GetStringAsync(Address);
                //json解析
                weatherData = JsonAnalytical(strinfo);
                //xml解析
                //weatherData = XmlAnalytical(strinfo);
              
            }
            catch (Exception ex)
            {
                Debug.WriteLine("网络请求失败!" + ex.Message.ToString());

            }

        
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Visible;
            listWeather.ItemsSource = weatherData.results[0].weather_data;
            //listIndex.ItemsSource = weatherData.results[0].index;
            textPmName.Text = "PM2.5";
            dayPicture.Source = (new BitmapImage(new Uri(weatherData.results[0].weather_data[0].dayPictureUrl.ToString())));
            nightPicture.Source = (new BitmapImage(new Uri(weatherData.results[0].weather_data[0].nightPictureUrl.ToString())));
            date.Text = weatherData.results[0].weather_data[0].date;
            weather.Text = weatherData.results[0].weather_data[0].weather;
            wind.Text = weatherData.results[0].weather_data[0].wind;
            temperature.Text = weatherData.results[0].weather_data[0].temperature;
            textCity.Text = weatherData.results[0].currentCity;
            textPm.Text = weatherData.results[0].pm25;     
            showCity.Text = cityName;
             


            bt = new Btn();
            bt.zero = "天气情况";
            bt.one = "穿衣指数";
            bt.two = "洗车指数";
            bt.three = "旅游指数";
            bt.four = "感冒指数";
            bt.five = "运动指数";
            bt.six = "紫外线强度指数";

            btn.Add(bt);

            listbtn.ItemsSource = btn;
           
           
          

        }
         private void Button_Click_0(object sender, RoutedEventArgs e)
         {
             listWeather.Visibility = Windows.UI.Xaml.Visibility.Visible;
             listIndex.Visibility = Windows.UI.Xaml.Visibility.Collapsed ;
         }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add( weatherData.results[0].index[0]);
            listIndex.ItemsSource =list;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add(weatherData.results[0].index[1]);
            listIndex.ItemsSource = list;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add(weatherData.results[0].index[2]);
            listIndex.ItemsSource = list;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add(weatherData.results[0].index[3]);
            listIndex.ItemsSource = list;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add(weatherData.results[0].index[4]);
            listIndex.ItemsSource = list;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            listWeather.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listIndex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<天气预报.WeatherData.indexList> list = new List<WeatherData.indexList>();
            list.Add(weatherData.results[0].index[5]);
            listIndex.ItemsSource = list;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(City),cityName);
        }
        private void SyncAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Show(cityName);
        }

        private void CollectAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(CollectionCity));
        }

    

     
    }
}
