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

namespace ComboBox下拉框
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<Man>datas=new List<Man> 
            {
                new Man {Name="张三",Age =20},
                new Man {Name="李四",Age=30},
                new Man  {Name ="王五",Age=40},
                new Man {Name ="刘德华",Age=50},
                new Man {Name="张学友",Age=60},
            };
            comboBox2.ItemsSource = datas;
            //获取ItemsControl的内容的对象源
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        //下拉框关闭时触发的事件处理逻辑
        private void comboBox2_DropDownClosed(object sender, object e)
        {
            //如果选中了某个选项就把选中选项信息显示到命名为Info控件上
            if (comboBox2.SelectedItem != null)
            { 
                Man man=comboBox2 .SelectedItem as Man ;
                Info.Text = "name" + man.Name + "age" + man.Age;
            }
        }
        //数据绑定的实体块
        public class Man
        {
            public string  Name { get; set; }
            public int  Age { get; set; }
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
