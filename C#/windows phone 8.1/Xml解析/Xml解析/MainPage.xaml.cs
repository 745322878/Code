using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Xml解析
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            readXmlFile();
        }
         async public void  readXmlFile()
        {
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml("ms-appx:///NewFolder/XMLFile.xml");
            //XmlElement rootElem = doc.DocumentElement;
            //XmlNodeList personNodes = rootElem.GetElementsByTagName("person"); 
            //StorageFolder folder1 = null;
            
           // StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/city.plist"));
            //
            ////StorageFolder localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            //foreach (StorageFolder folder in await localFolder.GetFoldersAsync())
            //{
            //    if (folder.DisplayName == "Citys")
            //    {
            //        folder1 = folder;
            //        break;
            //    }
            //}
            //foreach (StorageFile file in await folder1.GetFilesAsync())
            //{
            //    if (file.DisplayName == "city.plist")
            //    {
            //        file1 = file;
            //        break;
            //    }
            //}
            StorageFile file1 = null;
            string text;
            Citys city;
            List<Citys> citys = new List<Citys>();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("NewFolder");
            foreach (StorageFile file in await folder.GetFilesAsync())
            {
                if (file.Name == "XMLFile.xml")
                {
                    file1 = file;
                    break;
                }
            }
        
            if (file1 == null)
                return;
            IRandomAccessStream accesssStream = await file1.OpenReadAsync();
            //string  fileContent =await FileIO.ReadTextAsync(file1);
            using (StreamReader stream = new StreamReader(accesssStream.AsStreamForRead((int)accesssStream.Size))) 
            {
                text = stream.ReadToEnd();
             //   DataContractSerializer serializer = new DataContractSerializer(typeof(Citys));
                //city = (Citys)serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(text)) );
               // city = (Citys)serializer.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(text)));
            }
            string a = "<string>";
            string b = "</string>";
             int indexa=1;
             int indexb=1;
             for (int i = 0; i < text.Length; i++)
            {
                indexa = text.IndexOf(a, indexa + 1);
                indexb = text.IndexOf(b, indexb + 1);
                if (indexa == -1 && indexb == -1)
                    break;
                string s = text.Substring(indexa+a.Length , indexb - indexa-a.Length );
                city = new Citys();
                city.name = s;
                citys.Add(city);
            }
             citysName.ItemsSource = citys;
           
            //using (IInputStream inStream = await file1.OpenSequentialReadAsync())
            //{
            //   DataContractSerializer serializer = new DataContractSerializer(typeof(Citys));
            //    city = (Citys)serializer.ReadObject(inStream.AsStreamForRead());
            //}

        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
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
