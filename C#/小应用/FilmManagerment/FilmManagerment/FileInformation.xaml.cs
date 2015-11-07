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
    public sealed partial class FileInformation : Page
    {
        List<Film> film = new List<Film>();
        int i;
        string photoPath="";
        ListBoxItem item_folder;
        ListBoxItem pre_itemfolder = new ListBoxItem();
        public FileInformation()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(MainView));
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
                i = Convert.ToInt32(e.Parameter);
                Film f = (Film)App.filmList[i];
               // Image image = new Image();
                ImageBrush imageBrush = new ImageBrush();
                
                Uri uri = new Uri(f.PhotoSource);
                BitmapImage bitmap = new BitmapImage(uri);
                //image.Source = bitmap;
                imageBrush.ImageSource = bitmap;
                photoPath = f.PhotoSource;
                photoBtn.Background = imageBrush;
                name.Text = f.Name;
                name.IsEnabled = false;
                actor.Text = f.Actor;
                director.Text = f.Dictor;
                type.Text = f.Type;
                score.Text = f.Score;
                introduction.Text = f.Introduction;
  
            }
            accpet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            cancel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            edit.Visibility = Windows.UI.Xaml.Visibility.Visible;
            delete.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || actor.Text == "" || director.Text == "" || type.Text == "" || score.Text == "" || introduction.Text == ""||photoPath=="")
            {
                await new MessageDialog("信息输入不能为空").ShowAsync();
            }
            else
            {
                Film f = new Film()
                {
                    Name = name.Text,
                    Actor = actor.Text,
                    Dictor = director.Text,
                    Type = type.Text,
                    Score =score.Text,
                    Introduction = introduction.Text,
                    PhotoSource=photoPath
                };
                //Film f = new Film (name.Text, actor.Text, director.Text, type.Text, score.Text, introduction.Text);
                string str = await Film.ReadFile("FilmInformationFile");
                if (str.Contains("文件读取错误"))
                {
                    ;
                }
                else
                {
                    film = Film.DataSerializer(str);
                       
                }
                for (int i = 0; i < film.Count; i++)
                {
                    if (film[i].Name == name.Text)
                        film.RemoveAt(i);
                }
                film.Add(f);


               
                //把Film类序列化为字符串
                string jsonstring = Film.ToJsonData(film);
                await Film.WriteFile("FilmInformationFile", jsonstring);
 
                (Window.Current.Content as Frame).Navigate(typeof(MainView));
            }     
        }
        private async  void Delete_Click(object sender, RoutedEventArgs e)
        {
            App.filmList.RemoveAt(i);
            string jsonstring = Film.ToJsonData(App.filmList);  
            await Film.WriteFile("FilmInformationFile", jsonstring);
            (Window.Current.Content as Frame).Navigate(typeof(MainView));

        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = lbFile.SelectedItem as ListBoxItem;
            StorageFile file = item.DataContext as StorageFile;
            photoPath = file.Path;
            ImageBrush imageBrush = new ImageBrush();
            Uri uri = new Uri(photoPath);
            BitmapImage bitmap = new BitmapImage(uri);
            imageBrush.ImageSource = bitmap;
            photoBtn.Background  = imageBrush;

            scrollviewer.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            accpet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            cancel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            edit.Visibility = Windows.UI.Xaml.Visibility.Visible;
            delete.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }     
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            scrollviewer.Visibility= Windows.UI.Xaml.Visibility.Collapsed;
            accpet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            cancel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            edit.Visibility = Windows.UI.Xaml.Visibility.Visible;
            delete.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            scrollviewer.Visibility = Windows.UI.Xaml.Visibility.Visible;
            accpet.Visibility = Windows.UI.Xaml.Visibility.Visible;
            cancel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            edit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            delete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            lbFolder.Items.Clear();
            StorageFolder storage = Windows.ApplicationModel.Package.Current.InstalledLocation;
            foreach (var folder in await storage.GetFoldersAsync())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = folder.Name;
                item.DataContext = folder;
                lbFolder.Items.Add(item);
            }
        }
        private async void lbFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_folder = lbFolder.SelectedItem as ListBoxItem;
            //获取选中的文件夹
            StorageFolder folder1 = item_folder.DataContext as StorageFolder;
            lbFile.Items.Clear();
            foreach (var file in await folder1.GetFilesAsync())
            {
                ListBoxItem item1 = new ListBoxItem();
                item1.Content = file.Name;
                item1.DataContext = file;
                lbFile.Items.Add(item1);
            }
            image.Source = null;
        }
        private void lbFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pre_itemfolder.Content==null )
            {
              
                ListBoxItem item = lbFile.SelectedItem as ListBoxItem;
                StorageFile file = item.DataContext as StorageFile;
                photoPath = file.Path;
                Uri uri = new Uri(photoPath);
                BitmapImage bitmap = new BitmapImage(uri);
                image.Source = bitmap;
                image.Width = grid.Width;
            }
            else if (item_folder.Content.ToString() == pre_itemfolder.Content.ToString())
            {
                
                ListBoxItem item = lbFile.SelectedItem as ListBoxItem;
                StorageFile file = item.DataContext as StorageFile;
                photoPath = file.Path;
                Uri uri = new Uri(photoPath);
                BitmapImage bitmap = new BitmapImage(uri);
                image.Source = bitmap;
                image.Width = grid.Width;
            }
            pre_itemfolder = item_folder;       
        }   
    }
}
