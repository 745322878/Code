using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace _555
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Play : Page
    {
        //static SymbolIcon volumeIcon = new SymbolIcon(Symbol.Volume);
        //static SymbolIcon muteIcon = new SymbolIcon(Symbol.Mute);
        static SymbolIcon playIcon = new SymbolIcon(Symbol.Play);
        static SymbolIcon pauseIcon = new SymbolIcon(Symbol.Pause);
        static SymbolIcon stopIcon = new SymbolIcon(Symbol.Stop);
        static SymbolIcon previousIcon = new SymbolIcon(Symbol.Previous);
        static SymbolIcon nextIcon = new SymbolIcon(Symbol.Next);
        static SymbolIcon listIcon = new SymbolIcon(Symbol.List);

        System.Threading.Timer mTimer = null;
        //DispatcherTimer currentPosition = new DispatcherTimer();
        //DispatcherTimer timer = new DispatcherTimer();
        List<MusicList> list1 = new List<MusicList>();
        //int i = 0;
        int count = 0;
        int icount = 0;
        Point start = new Point();
        //默认为false
        // private bool _updatingMediaTimeline;


        public Play()
        {
            this.InitializeComponent();
            GetMusicName();

            //获取总时长
            this.tbCurrentTime.Text = musicplayWindow.NaturalDuration.TimeSpan.ToString("mm\\:ss");
            //开始启用计时器，获取实时进度
            this.mTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerCallback), null, TimeSpan.FromSeconds(0), TimeSpan.FromMilliseconds(50));


            image1.PointerEntered += image_PointerEntered;
            image1.PointerExited += image_PointerExited;

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            //var local=Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("MusicLrc");

            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += timer_Tick;
            //timer.Start();
            //currentPosition.Interval = TimeSpan.FromSeconds(1);
            //currentPosition.Tick += currentPosition_Tick;



        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            play.Icon = playIcon;
            stop.Icon = stopIcon;
            previous.Icon = previousIcon;
            next.Icon = nextIcon;
            list.Icon = listIcon;
            if (e.Parameter != null)
            {
                icount = Convert.ToInt32(e.Parameter);
                count = icount;
                Image imgItem = new Image();

                imgItem.Width = 350;
                imgItem.Height = 400;
                imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                image1.Children.Clear();
                this.image1.Children.Add(imgItem);


                musicplayWindow.Source = new Uri(App.musicPath[icount]);

                play.Icon = new SymbolIcon(Symbol.Pause);
                music_Name.Text = App.musicName[icount];

            }



        }

        /// <summary>
        /// 播放按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Click(object sender, RoutedEventArgs e)
        {
            //var storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///MusicLrc/" + App.musicLrc[0]));
            //IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
            //using (StreamReader streamReader = new StreamReader(accessStream.AsStreamForRead((int)accessStream.Size)))
            //{ 
            //    music_Lrc.Text = streamReader.ReadToEnd();
            //}
            var bar = sender as AppBarButton;

            if (musicplayWindow.Position.Seconds == 0)
            {

                //视频源
                musicplayWindow.Source = new Uri(App.musicPath[icount]);
                if (icount == 0)
                {
                    count = icount;
                    Image imgItem = new Image();

                    imgItem.Width = 350;
                    imgItem.Height = 400;
                    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                    image1.Children.Clear();
                    this.image1.Children.Add(imgItem);

                    music_Name.Text = App.musicName[icount];
                }
                musicplayWindow.Play();
                //currentPosition.Start();
                bar.Icon = new SymbolIcon(Symbol.Pause);

            }
            else
            {


                if (musicplayWindow.CurrentState == MediaElementState.Playing)
                {

                    musicplayWindow.Pause();
                    // currentPosition.Stop();

                    bar.Icon = new SymbolIcon(Symbol.Play);
                }

                else if (musicplayWindow.CurrentState == MediaElementState.Paused)
                {

                    musicplayWindow.Play();
                    //  currentPosition.Start();
                    bar.Icon = new SymbolIcon(Symbol.Pause);

                }
            }

        }

        // 进度条      
        //private void currentPosition_Tick(object sender, object e)
        //{
        //    musicTimeline1.Maximum = (int)musicplayWindow.NaturalDuration.TimeSpan.TotalSeconds;
        //    musicTimeline1.Value = (int)musicplayWindow.Position.TotalSeconds;

        //}

        /// 暂停按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            musicplayWindow.Stop();
            musicTimeline1.Value = 0;
            play.Icon = new SymbolIcon(Symbol.Play);
        }
        /// <summary>
        /// 上一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previous_Click(object sender, RoutedEventArgs e)
        {


            if (icount == 0)
            {
                previous.IsEnabled = false;
                next.IsEnabled = true;
            }
            else
            {
                icount--;
                count = icount;
                Image imgItem = new Image();

                imgItem.Width = 350;
                imgItem.Height = 400;
                imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                image1.Children.Clear();
                this.image1.Children.Add(imgItem);


                musicplayWindow.Source = new Uri(App.musicPath[icount]);


                music_Name.Text = App.musicName[icount];

                play.Icon = new SymbolIcon(Symbol.Pause);
                previous.IsEnabled = true;
                next.IsEnabled = true;
            }
        }
        /// <summary>
        /// 下一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, RoutedEventArgs e)
        {

            if (icount == App.musicPath.Count - 1)
            {
                previous.IsEnabled = true;
                next.IsEnabled = false;
            }
            else
            {
                icount++;
                count = icount;
                Image imgItem = new Image();

                imgItem.Width = 350;
                imgItem.Height = 400;
                imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                image1.Children.Clear();
                this.image1.Children.Add(imgItem);


                musicplayWindow.Source = new Uri(App.musicPath[icount]);


                music_Name.Text = App.musicName[icount];

                play.Icon = new SymbolIcon(Symbol.Pause);
                previous.IsEnabled = true;
                next.IsEnabled = true;
            }
        }

        //音量调节
        //private void volume_Click(object sender, RoutedEventArgs e)
        //{
        //    if (volume.Icon == volumeIcon)
        //    {
        //        musicplayWindow.IsMuted = true;
        //        volume.Icon = muteIcon;
        //    }
        //    else
        //    {
        //        musicplayWindow.IsMuted = false;
        //        volume.Icon = volumeIcon;
        //    }
        //}

        /// <summary>
        /// 列表AppbarButton事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_Click(object sender, RoutedEventArgs e)
        {

            if (conListBox.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                conListBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //image.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                image1.Opacity = 0;
            }
            else
            {
                conListBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                image1.Opacity = 1;
            }
        }
        /// <summary>
        /// 播放时间
        /// </summary>
        /// <param name="state"></param>
        private async void TimerCallback(object state)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    tbCurrentTime.Text = musicplayWindow.Position.ToString("mm\\:ss") ?? "00;00";

                    ////计算进度条值
                    var msecondsTotal = (double)musicplayWindow.NaturalDuration.TimeSpan.TotalSeconds;
                    var msecondsCurr = (double)musicplayWindow.Position.TotalSeconds;
                    if (msecondsTotal != 0)
                        musicTimeline1.Value = msecondsCurr * 100 / msecondsTotal;
                    TimeSpan duration = musicplayWindow.NaturalDuration.TimeSpan;
                    tbTotalTime.Text = duration.ToString("mm\\:ss") ?? "00:00";
                    if (tbCurrentTime.Text == tbTotalTime.Text && tbTotalTime.Text != "00:00" && icount < App.musicPath.Count - 1)
                    {
                        icount++;
                        musicplayWindow.Source = new Uri(App.musicPath[icount]);
                    }


                });

        }
        /// <summary>
        /// 获取音乐名另一种方法;
        /// </summary>
        async private void GetMusicName()
        {
            //StorageFolder local = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync("Music", CreationCollisionOption.OpenIfExists);
            var local = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Music");
            if (local != null)
            {
                var conList = await local.GetFilesAsync();
                //IReadOnlyList<StorageFile> conList = await local.GetFilesAsync();

                foreach (StorageFile strfile in conList)
                {
                    list1.Add(new MusicList
                    {
                        Name = strfile.DisplayName,
                    });
                }
                conListBox.ItemsSource = list1;

            }
        }

        //快进
        private void musicTimeline1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {


            //if (!_updatingMediaTimeline && musicplayWindow.CanSeek)
            //{
            //    //music总长度
            //    var msecondsTotal1 = musicplayWindow.NaturalDuration.TimeSpan.TotalSeconds;
            //    //计算视频拖动的位置
            //    int newPosition1 = (int)(msecondsTotal1 * musicTimeline1.Value / 100);

            //    //跳转至该时间点
            //    musicplayWindow.Position = new TimeSpan(0, 0, newPosition1);

            //}
        }

        /// <summary>
        /// 列表选择音乐播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listplay_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            int i = -1;
            foreach (var music in list1)
            {
                i++;

                if (button.Content.ToString() == music.Name)
                {
                    icount = i;
                    musicplayWindow.Source = new Uri(App.musicPath[icount]);
                    //button.Background = new SolidColorBrush(Colors.Pink);
                    play.Icon = new SymbolIcon(Symbol.Pause);

                    Image imgItem = new Image();

                    count = icount;
                    imgItem.Width = 350;
                    imgItem.Height = 400;
                    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                    image1.Children.Clear();
                    this.image1.Children.Add(imgItem);

                    music_Name.Text = App.musicName[icount];
                }

            }
        }

        /// 间隔10秒换图片       
        //private void timer_Tick(object sender, object e)
        //{

        //    i++;
        //    if (i % 10 == 0)
        //    {
        //        i = 0;
        //        count++;
        //        if (count >= App.musicPhoto.Count)
        //        {
        //            count = 1;
        //        }
        //        //if(count>0&&count<App.musicPhoto.Count )
        //        Image imgItem = new Image();

        //        imgItem.Width = 300;
        //        imgItem.Height = 370;
        //        imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
        //        image.Children.Clear();
        //        this.image.Children.Add(imgItem);
        //    }
        //}
        ///// <summary>

        private void image_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //获取点击处相对于image控件的坐标，作为开始点
            start = e.GetCurrentPoint(image1).Position;
        }
        private void image_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //获取结束的坐标点，也是相对于image控件的坐标点
            Point end = e.GetCurrentPoint(image1).Position;
            double angle = 0;
            if (Math.Abs(end.X - start.X) < 1 && Math.Abs(end.Y - start.Y) < 1)
            {
                angle = 0;
            }
            else if (end.X > start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 360 - Math.Atan((end.Y - start.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI);
                }
                else
                {
                    angle = Math.Atan((start.Y - end.Y) * 1.0 / (end.X - start.X) * 180 / Math.PI);
                }
            }
            else if (end.X < start.X)
            {
                if (end.Y > start.Y)
                {
                    angle = 180 + Math.Atan((end.Y - start.Y) * 1.0 / (start.X - end.X) * 180 / Math.PI);
                }
                else
                {
                    angle = 180 - Math.Atan((start.Y - end.Y) * 1.0 / (start.X - end.X) * 180 / Math.PI);
                }
            }
            //触摸从左向右
            if (angle <= 45 || angle > 315)
            {
                if (count == -1)
                {
                    count += 2;
                }
                else
                    count++;
                if (count < App.musicPhoto.Count)
                {

                    Image imgItem = new Image();

                    imgItem.Width = 350;
                    imgItem.Height = 400;
                    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                    image1.Children.Clear();
                    this.image1.Children.Add(imgItem);

                    icount = count;
                    musicplayWindow.Source = new Uri(App.musicPath[icount]);
                    play.Icon = new SymbolIcon(Symbol.Pause);
                    //if (musicplayWindow.CurrentState == MediaElementState.Playing)
                    //{
                    //    musicplayWindow.Play();
                    //}
                    //else if (musicplayWindow.CurrentState == MediaElementState.Paused)
                    //{
                    //    musicplayWindow.Pause();
                    //}
                    music_Name.Text = App.musicName[icount];
                }

                else
                {
                    count = App.musicPhoto.Count;
                }
            }
            //触摸从右向左
            else if (angle >= 135 && angle < 225)
            {
                if (count == App.musicPhoto.Count)
                {
                    count -= 2;
                }
                else
                    count--;
                if (count >= 0)
                {
                    Image imgItem = new Image();

                    imgItem.Width = 350;
                    imgItem.Height = 400;
                    imgItem.Source = (new BitmapImage(new Uri(App.musicPhoto[count])));
                    image1.Children.Clear();
                    this.image1.Children.Add(imgItem);

                    icount = count;
                    musicplayWindow.Source = new Uri(App.musicPath[icount]);

                    play.Icon = new SymbolIcon(Symbol.Pause);
                    music_Name.Text = App.musicName[icount];
                }

                else
                {
                    count = -1;
                }
            }
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}
