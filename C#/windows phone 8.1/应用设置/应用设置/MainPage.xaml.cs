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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 应用设置
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appSettings;

        public MainPage()
        {
            this.InitializeComponent();
            //获取本地应用设置容器
            _appSettings = ApplicationData.Current.LocalSettings;
            //将所有的key绑定到List上
            BindKeyList();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        //把当前的所有的key值绑定到List上
        private void BindKeyList()
        {
            //先清空LIST控件的绑定值
            lstKeys.Items.Clear();
            //获取当前程序的所有的key
            foreach (string key in _appSettings.Values.Keys)
            {
                //添加到List控件上
                lstKeys.Items.Add(key);
            }
            //键值初始化
            txtKey.Text = "";
            txtValue.Text = "";
            
        }
        //清空所有
        private void deleteall_Click(object sender, RoutedEventArgs e)
        {
            _appSettings.Values.Clear();
            BindKeyList();
        }
        //保存
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //键不能为空
            if (!String.IsNullOrEmpty(txtKey.Text))
            {
                //增加一个txtKey
                _appSettings.Values[txtKey.Text] = txtValue.Text;

                BindKeyList();
            }
            
            else
            {
                //异步提示
                await new MessageDialog("请输入key值").ShowAsync();
            }
        }
        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //如果选中了List中的某项
            if (lstKeys.SelectedIndex >=0)
            {
                //移除选中的项目
                _appSettings.Values.Remove(lstKeys.SelectedItem.ToString());
                BindKeyList();
            }
        }
        //List控件选中项的事件，将选中的键和值显示在上面的文本框中
        private void lstKeys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //获取List中选择的Key
                string key = e.AddedItems[0].ToString();
                //检查设置是否存在这个Key
                if (_appSettings.Values.ContainsKey(key))
                {
                    txtKey.Text = key;
                    txtValue.Text = _appSettings.Values[key].ToString();
                }
            }
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
