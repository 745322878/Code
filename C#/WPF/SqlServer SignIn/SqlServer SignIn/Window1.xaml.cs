using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

namespace SqlServer_SignIn
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件|*.txt";
            if (ofd.ShowDialog() != true)
            {
                return;
            }
            string filename = ofd.FileName;
            //File.ReadLines 是把文件一次读取到string集合
            IEnumerable<string> lines = File.ReadLines(filename,Encoding.Default);
            foreach (string line in lines)
            {
                string[] segs = line.Split('|');
                string name=segs[0];
                string password = segs[1];
                SqlHelper.ExecuteNonQuery("insert into T_User(UserName,Password,ErrorTimes) values(@name,@password,0)", new SqlParameter("@name", name), new SqlParameter("@password", password));
            }
            MessageBox.Show("导入成功,导入"+lines.Count()+"条数据!");
           
        }
    }
}
