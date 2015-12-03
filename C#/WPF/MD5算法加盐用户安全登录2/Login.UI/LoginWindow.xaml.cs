using Login.DAL;
using Login.Model;
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
using System.Windows.Shapes;

namespace Login.UI
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void loginClick(object sender, RoutedEventArgs e)
        {
            string username = txtName.Text;
            string password = txtPassword.Text;
            User u = LoginDAL.GetByName(username);
            if (u == null)
                MessageBox.Show("用户名或密码错误!!!");
            else
            {
                string dbmd5 = u.Password;
                string mymd5 = Common.GetMD5(password + "love");
                if (dbmd5 == mymd5)
                    MessageBox.Show("登录成功");
                else
                    MessageBox.Show("用户名或密码错误!!!");
            }
        }

        private void showoutClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
