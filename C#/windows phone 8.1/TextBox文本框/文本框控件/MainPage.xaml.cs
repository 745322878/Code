using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 文本框控件
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //文本的信息
        string text = "";
        //选择文本的信息
        string selectedText = "";
        //是否发生粘贴
        string pasteTest = "";
        //文本变化的事件
        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = TextBox1.Text;
            ShowInformation();
        }
        //文本选择的事件
        private void TextBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            selectedText = TextBox1.SelectedText;
            ShowInformation();

        }
        //粘贴事件
        private void TextBox1_Paste(object sender, TextControlPasteEventArgs e)
        { 
            text =TextBox1.Text;
            selectedText =TextBox1.SelectedText;
            pasteTest ="产生了粘贴操作";
            ShowInformation();
        }
        //操作信息展示
        private  void ShowInformation()
        {
            textBlock1.Text = "文本信息:"+text +"选择的信息:"+selectedText  +"粘贴的信息:"+ pasteTest +"";
        }
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
