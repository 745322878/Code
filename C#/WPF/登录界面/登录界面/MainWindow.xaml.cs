using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 登录界面
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //事件
        public  event RoutedEventHandler btnClick;
        public MainWindow()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Content = "注册";
            btn.Width=100;
            btn.Height=50;
            //给事件添加方法
            btnClick += Button_Click1;
            //Lambda表达式
            //btn.Click+=(s,ex) =>
            //    {
            //        MessageBox.Show("WPF,Hello World!");
            //    };
            //给button添加事件
            btn.Click +=btnClick;
            grid.Children.Add(btn);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WPF  Hello World!");
            //MessageBoxButton.YesNoCancel.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World!");
        }
    }
}
