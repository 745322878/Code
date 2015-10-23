using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 拍照
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaCapture captureManager=null;
        ImageEncodingProperties imgFormat;
        BitmapImage bmpImage;
        StorageFile file;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
           
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (captureManager == null)
            {
                captureManager = new MediaCapture();
                await captureManager.InitializeAsync();

              
                
            }
            capturePreview.Source = captureManager;
            await captureManager.StartPreviewAsync();
             
        }

   
        async private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
          
           imgFormat = ImageEncodingProperties.CreateJpeg();
            
         
           file = await ApplicationData.Current.LocalFolder.CreateFileAsync
               ("1.jpg", CreationCollisionOption.GenerateUniqueName);
            
            //using (IsolatedStorage store = IsolatedStorageFile.GetUserStoreForApplication())
            await captureManager.CapturePhotoToStorageFileAsync(imgFormat, file);
            //bmpImage = new BitmapImage(new Uri(file.Path));
            
            //imagePreview.Source = bmpImage;
        }

        private void SearchCapturePreview_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchCapturePhoto), file.Path );
        }

        //private void SearchCapturePreview_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //async private void StopCapturePreview_Click(object sender, RoutedEventArgs e)
        //{
        //    await captureManager.StopPreviewAsync();
        //}

     
    }
}
