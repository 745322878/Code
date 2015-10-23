using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 录音机
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaCapture _mediaCaptureManager;
        private StorageFile _recordStorageFile;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            InitializeAudioRecording();
        }

        private async void InitializeAudioRecording()//初始化
        {
            _mediaCaptureManager = new MediaCapture();
            var settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;//设置为音频
            settings.MediaCategory = MediaCategory.Other;//设置音频种类
            await _mediaCaptureManager.InitializeAsync(settings);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)//开始录音
        {
            try
            {
                //控制控件状态和动画
                storyboard.Begin();
                record.IsEnabled = false;
                stop.IsEnabled = true;
                String fileName = "1.aac";

                //创建文件
                _recordStorageFile = await KnownFolders.VideosLibrary.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                //关键就是这俩句
                MediaEncodingProfile recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);//录音

                await _mediaCaptureManager.StartRecordToStorageFileAsync(recordProfile, this._recordStorageFile);//将录音保存到视频库

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //控制控件状态和动画
            record.IsEnabled = true;
            stop.IsEnabled = false;
            storyboard.Stop();

            await _mediaCaptureManager.StopRecordAsync();//停止录音

            //将录制的语音设置为MediaElement控件的源
            var stream = await _recordStorageFile.OpenAsync(FileAccessMode.Read);
            playbackElement1.AutoPlay = true;
            playbackElement1.SetSource(stream, _recordStorageFile.FileType);
            playbackElement1.Play();

        }
    }
}
