using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
 
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlServer_SignIn
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int i;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(userTextBox.Text.Length<=0)
            {
                MessageBox.Show("请输入用户名!");
            }
            else  if(passwordTextBox.Text.Length<=0)
            {
                MessageBox.Show("请输入密码!");
            }
            DataTable dt = SqlHelper.ExecuteDataTable("select * from T_User where UserName=@name",new SqlParameter("@name",userTextBox.Text));
           //编程的时候要对“不可能发生的情况”做处理!
            //dt.Rows的数目基本不会为负
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("用户不存在!");
                return;
            }
            //防御型编程
            else if(dt.Rows.Count>1)
            {
                MessageBox.Show("用户名重复!");
                return;
            }
            DataRow row = dt.Rows[0];
            string password = (string)row["Password"];
            long  id=(long )row["Id"];
            int errortimes = (int)row["ErrorTimes"];
            if (passwordTextBox.Text == password)
            {
                MessageBox.Show("登陆成功!");
                Application.Current.Shutdown();

            }
            else
            {

                errortimes++;

                if (errortimes >= 3)
                {
                    MessageBox.Show("密码错误三次!!,程序退出");
                    errortimes = 0;
                    i = SqlHelper.ExecuteNonQuery("update T_User set ErrorTimes=@errortimes1 where Id=@id", new SqlParameter("@errortimes1",errortimes),new SqlParameter("@id",id));
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("密码错误");
                    i = SqlHelper.ExecuteNonQuery("update T_User set ErrorTimes=@errortimes1 where Id=@id", new SqlParameter("@errortimes1", errortimes), new SqlParameter("@id", id));
                }
            }
        }

    }
}
