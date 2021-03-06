﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
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

namespace _555
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
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            bool taskRegistered = BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name == "TaskDemo");
            if (!taskRegistered)
            {
                BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                if (status != BackgroundAccessStatus.Denied)
                {
                    var builder = new BackgroundTaskBuilder();
                    builder.Name = "TaskDemo";
                    builder.TaskEntryPoint = "MyTask.TaskDemo";
                    builder.SetTrigger(new SystemTrigger(SystemTriggerType.BackgroundWorkCostChange, false));
                    builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                    BackgroundTaskRegistration task = builder.Register();
                }

            }
        }

        private void music_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Play));
        }

        private void musicStorage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Storage));
        }

    }
}
