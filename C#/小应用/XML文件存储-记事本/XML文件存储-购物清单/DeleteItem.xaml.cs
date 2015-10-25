using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace XML文件存储_购物清单
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DeleteItem : Page
    {
        public DeleteItem()
        {
            this.InitializeComponent();
        }
      

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile nowfile = e.Parameter as StorageFile;
            if (nowfile == null)
            {
                return;
            }
            //获取记事本名字
            string itemname = nowfile.DisplayName;
            //nameTxt.Text = itemname;
            ////把应用文件作为一个xml文档加载进来
            //XmlDocument doc = await XmlDocument.LoadFromFileAsync(nowfile);
            ////获取xml文档的信息
            //NotePad.Text = doc.DocumentElement.Attributes.GetNamedItem("word").NodeValue.ToString();
            //获取NoteList中的信息
            StorageFolder storage = await ApplicationData.Current.LocalFolder.CreateFolderAsync("NoteList", CreationCollisionOption.OpenIfExists);
            //获取当前文件夹中的文件
            var files = await storage.GetFilesAsync();
            //便利 每一个文件
            foreach (StorageFile file in files)
            {
                if (file.DisplayName == itemname)
                {
                    await nowfile.DeleteAsync();
                    await new MessageDialog("删除成功").ShowAsync();
                   
                }
                Frame.Navigate(typeof(MainPage));

            }
         
        }
    }
}
