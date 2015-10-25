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
    public sealed partial class AddItem : Page
    {
        public AddItem()
        {
            this.InitializeComponent();
        }
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //获取记事本的文件夹对象NoteList
            StorageFolder storage = await ApplicationData.Current.LocalFolder.GetFolderAsync("NoteList" );
           
           
            if (nameTxt.Text =="")
            {
                await new MessageDialog("文件名不能为空").ShowAsync();

            }
            else
            {
                //创建一个XML对象
                XmlDocument _doc = new XmlDocument();
                //使用记事本名字创建一个XML元素作为根节点
                XmlElement _item = _doc.CreateElement(nameTxt.Text);
               
                //使用属性来作为信息的标识符，使用属性值来存储相关的信息
                _item.SetAttribute("word", NotePad.Text);
                _doc.AppendChild(_item);
                //创建一个应用文件
                StorageFile file = await storage.CreateFileAsync(nameTxt.Text + ".xml", CreationCollisionOption.ReplaceExisting);
                //把XML的信息保存到文件中去
                await _doc.SaveToFileAsync(file);
               
            }
            //调回清单主页，返回上一步
                
            Frame.GoBack();
           
        }
        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void  OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile file = e.Parameter as StorageFile;
            if (file == null)
                return;
            //获取记事本名字
            String itemName = file.DisplayName;
            nameTxt.Text = itemName;
            //把应用文件作为一个XML文档加载进来
            XmlDocument doc = await XmlDocument.LoadFromFileAsync(file);
            //获取XML文档的信息
            
            NotePad.Text = doc.DocumentElement.Attributes.GetNamedItem("word").NodeValue.ToString();
        }

    }
}
