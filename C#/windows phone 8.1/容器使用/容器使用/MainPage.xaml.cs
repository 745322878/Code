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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 容器使用
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localSettings = null;
        const string containerName = "exampleContainer";
        const string settingName = "exampleSetting";
        public MainPage()
        {
            this.InitializeComponent();
            localSettings = ApplicationData.Current.LocalSettings;
            DisplayOutPut();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        void CreateContainer_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer container = localSettings.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
            DisplayOutPut();
        }
        void DeleteContainer_Click(object sender, RoutedEventArgs e)
        {
            localSettings.DeleteContainer(containerName);
            DisplayOutPut();
        }
        void WriteSetting_Click(object sender, RoutedEventArgs e)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values[settingName] = "Hello World!";
        
            }
            DisplayOutPut();
        }
        void DeleteSetting_Click(object sender, RoutedEventArgs e)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values.Remove(settingName);
            }
            DisplayOutPut();
        }
        //展示创建的容器和应用设置的信息
        void DisplayOutPut()
        { 
            //判断容器是否存在
            bool hasContainer = localSettings.Containers.ContainsKey(containerName);
            //判断容器里面的键值是否存在
            bool hasSetting = hasContainer ? localSettings.Containers[containerName].Values.ContainsKey(settingName ) : false;
            string output=String.Format("Container Exists:{0}\n"+"Setting Exist:{1}",hasContainer ?"trur":"false",hasSetting?"true":"false");
            OutputTextBlock.Text=output ;
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
