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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace UIFilmView
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> photo = new List<string>();
        //Point start = new Point();
        int count = 0;
        DispatcherTimer timer = new DispatcherTimer();
        int time=0;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            GetPhotoSource();
            Init();
            //btn.PointerEntered += btn_PointerEntered;
            //btn.PointerExited += btn_PointerExited;
        }
        public  void Init()
        {
            if(photo.Count>0)
            {
                Uri uri = new Uri(photo[count]);
                BitmapImage bitmap = new BitmapImage(uri);
                imageBrush.ImageSource = bitmap;
                radioBtn1.IsChecked = true;
                radioBtn2.IsChecked = false;
                radioBtn3.IsChecked = false;
                radioBtn4.IsChecked = false;
                radioBtn5.IsChecked = false;
            }
            
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick+=timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, object e)
        {
            if (photo.Count > 0)
            {
                time++;
                if (time % 3 == 0)
                {
                    time = 0;
                    count++;
                    if (count >= photo.Count)
                    {
                        count = 0;
                    }
                    switch (count)
                    { 
                        case 0:
                            radioBtn1.IsChecked = true;
                            radioBtn2.IsChecked = false;
                            radioBtn3.IsChecked = false;
                            radioBtn4.IsChecked = false;
                            radioBtn5.IsChecked = false;
                            break;
                        case 1:
                            radioBtn1.IsChecked = false;
                            radioBtn2.IsChecked = true;
                            radioBtn3.IsChecked = false;
                            radioBtn4.IsChecked = false;
                            radioBtn5.IsChecked = false;
                            break;
                        case 2:
                            radioBtn1.IsChecked = false;
                            radioBtn2.IsChecked = false;
                            radioBtn3.IsChecked = true;
                             radioBtn4.IsChecked = false;
                            radioBtn5.IsChecked = false;
                            break;
                        case 3:
                            radioBtn1.IsChecked = false;
                            radioBtn2.IsChecked = false;
                            radioBtn3.IsChecked = false ;
                            radioBtn4.IsChecked = true ;
                            radioBtn5.IsChecked = false;
                            break;
                        case 4:
                            radioBtn1.IsChecked = false;
                            radioBtn2.IsChecked = false;
                            radioBtn3.IsChecked = false ;
                            radioBtn4.IsChecked = false;
                            radioBtn5.IsChecked = true ;
                            break;
                    }
                    Uri uri = new Uri(photo[count]);
                    BitmapImage bitmap = new BitmapImage(uri);
                    imageBrush.ImageSource = bitmap;
                }
            }
        }


        //private void btn_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    start = e.GetCurrentPoint(btn).Position;
        //}
        //private async void btn_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    Point end = e.GetCurrentPoint(btn).Position;
        //    double angle = 0;
        //    if (Math.Abs(end.X - start.X) < 1 && Math.Abs(end.Y - start.Y) < 1)
        //    {
        //        angle = 0;
        //    }
        //    else if (end.X > start.X)
        //    {
        //        if (end.Y > start.Y)
        //        {
        //            angle = 360 - Math.Atan((end.Y - start.Y) * 1.0 / (end.X - start.X)) * 180 / Math.PI;
        //        }
        //        else
        //        {
        //            angle = Math.Atan((start.Y - end.Y) * 1.0 / (end.X - start.X)) * 180 / Math.PI;
        //        }
        //    }
        //    else if (end.X < start.X)
        //    {
        //        if (end.Y > start.Y)
        //        {
        //            angle = 180 + Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X)) * 180 / Math.PI;
        //        }
        //        else
        //        {
        //            angle = 180 - Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X)) * 180 / Math.PI;
        //        }
        //    }
        //    if (angle == 0)
        //        await new MessageDialog("点击操作").ShowAsync();
        //    else if (angle <= 45 || angle > 315)            //从左到右
        //    {
        //        count++;
        //        if (count == photo.Count)
        //            count = 0;
        //        Uri uri = new Uri(photo[count]);
        //        BitmapImage bitmap = new BitmapImage(uri);
        //        imageBrush.ImageSource = bitmap;
        //    }
        //    else if (angle >= 135 && angle < 225)           //从右到左
        //    {
        //        count--;
        //        if (count == -1)
        //            count = photo.Count - 1;

        //        Uri uri = new Uri(photo[count]);
        //        BitmapImage bitmap = new BitmapImage(uri);
        //        imageBrush.ImageSource = bitmap;
        //    }
        //}
        public async void GetPhotoSource()
        {
            StorageFolder storage = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var folder = await storage.GetFolderAsync("FilmPhoto");
            foreach (var file in await folder.GetFilesAsync())
            {
                photo.Add(file.Path);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
        }
    }
}
