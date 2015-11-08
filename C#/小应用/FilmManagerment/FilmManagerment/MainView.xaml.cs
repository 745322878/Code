using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace FilmManagerment
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainView : Page
    {

        List<Film> filmName;
        //Point start = new Point();
        int count = 0;
        int time = 0;
        DispatcherTimer timer = new DispatcherTimer();
        public MainView()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Init();
           
            
            
           // btn.PointerEntered += btn_PointerEntered;
           // btn.PointerExited += btn_PointerExited;
           
        }
       
        public async  void Init()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
      
            string str = await Film.ReadFile("FilmInformationFile");
            if (str.Contains("文件读取错误"))
            {
                ;
            }
            else
            {
                App.filmList = Film.DataSerializer(str);
                
            }
            App.filmPhoto = new List<string>();
            for (int i = 0; i < App.filmList.Count; i++)
            {
              
                App.filmPhoto.Add(App.filmList[i].PhotoSource);
            }
            if (App.filmPhoto.Count > 0)
            {
                Uri uri = new Uri(App.filmPhoto[0]);
                BitmapImage bitmap = new BitmapImage(uri);
                imagebrush.ImageSource = bitmap;
            }
            //listBox.ItemsSource = App.filmList;
        }
       /* private void btn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            start = e.GetCurrentPoint(btn).Position;
        }
        private  void btn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Point end = e.GetCurrentPoint(btn).Position;
            double angle = 0;
            if (Math.Abs(end.X - start.X) < 1 && Math.Abs(end.Y - start.Y) < 1)
            {
                angle = 0;
            }
            else if (end.X > start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 360 - Math.Atan((end.Y - start.Y) * 1.0 / (end.X - start.X)) * 180 / Math.PI;
                }
                else
                {
                    angle = Math.Atan((start.Y - end.Y) * 1.0 / (end.X - start.X)) * 180 / Math.PI;
                }
            }
            else if (end.X < start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 180 + Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X)) * 180 / Math.PI;
                }
                else
                {
                    angle = 180 - Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X)) * 180 / Math.PI;
                }
            }
            if (angle == 0)
                Btn_Click(sender, e);
            else if (angle <= 45 || angle > 315)            //从左到右
            {
                count++;
                if (count == App.filmPhoto.Count)
                    count = 0;    
            }
            else if (angle >= 135 && angle < 225)           //从右到左
            {
                count--;
                if (count == -1)
                    count = App.filmPhoto.Count - 1;
            }
            else if(angle >=45&&angle<135)      //从下往上
            {
                count--;
                if (count == -1)
                    count = App.filmPhoto.Count - 1;
            }
            else if(angle>=225 && angle<315)
                //从上往下
            {
                 count++;
                if (count == App.filmPhoto.Count)
                    count = 0;   
            }
            Uri uri = new Uri(App.filmPhoto[count]);
            BitmapImage bitmap = new BitmapImage(uri);
            imagebrush.ImageSource = bitmap;
        }*/
        private void timer_Tick(object sender, object e)
        {

            if (App.filmPhoto.Count>0)
            {
                //if (App.filmPhoto.Count <=3)
                //{
                //    for (int j = 0; j < App.filmPhoto.Count; j++)
                //    {
                //        RadioButton radioBtn = new RadioButton();
                //        radioBtn.Style = (Style)Resources["RadioButtonStyle1"];
                //        radioBtn.IsEnabled = false ;
                //        stackpanel.Children.Add(radioBtn );
                //    }
                //}
                //time++;
                if (time % 5 == 0)
                {
                    time = 0;
                    count++;
                    if (count >= App.filmPhoto.Count)
                    //if(count>=3)
                    {
                        count = 0;
                    }
                    //switch (count)
                    //{
                    //    case 0:
                    //        radioBtn1.IsChecked = true;
                    //        radioBtn2.IsChecked = false;
                    //        radioBtn3.IsChecked = false;
                    //        break;
                    //    case 1:
                    //         radioBtn1.IsChecked = false;
                    //        radioBtn2.IsChecked = true ;
                    //        radioBtn3.IsChecked = false  ;
                    //        break; 
                    //    case 2:
                    //         radioBtn1.IsChecked = false ;
                    //        radioBtn2.IsChecked = false;
                    //        radioBtn3.IsChecked = true ;
                    //        break;
                    //}
                   
                    Uri uri = new Uri(App.filmPhoto[count]);
                    BitmapImage bitmap = new BitmapImage(uri);
                    imagebrush.ImageSource = bitmap;
                }
            }
        }
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(MainPage));
                e.Handled = true;
            }
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
                App.userId = e.Parameter.ToString();

            }
            if (SignIn.IsSignUp_User(App.userId))
            {
                Add.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Create.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            }
            else if (SignIn.IsSignUp_Admin(App.userId))
            {
                Add.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Create.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            Title.Text = App.userId;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(FileInformation),"Add");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(ModifyPersoninformation), Title.Text);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(Register), "AdminPassword");
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
           
            Button btn = sender as Button;
            ImageBrush imageBrush=(ImageBrush)btn.Background;
            BitmapImage bitmap =(BitmapImage)imageBrush.ImageSource;
            for (int i = 0; i < App.filmList.Count; i++)
            {
                string photoPath=App.filmList[i].PhotoSource;
                Uri uri = new Uri(photoPath);
                BitmapImage bit = new BitmapImage(uri);
                if (bit.UriSource.ToString() == bitmap.UriSource.ToString())
                {
                    if (Add.Visibility == Windows.UI.Xaml.Visibility.Visible)
                        (Window.Current.Content as Frame).Navigate(typeof(FileInformation),i);
                    else
                        (Window.Current.Content as Frame).Navigate(typeof(FileInfornation_User),i);
                }

            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            filmName = new List<Film>();
            foreach (Film f in App.filmList)
            {
                if (f.Name.Contains(search_textBox.Text))
                {
                    filmName.Add(f);
                }
            }
            listBox.ItemsSource = filmName;
        }
    }
}
