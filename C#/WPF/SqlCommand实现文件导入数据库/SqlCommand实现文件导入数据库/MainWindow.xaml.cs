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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlCommand实现文件导入数据库
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog=MyTest; User ID=NULL;Password=wangyongzhi61011"))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("数据库连接成功!!!,等待数据录入");
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "文本文件|*.txt";
                    if (ofd.ShowDialog() != true)
                    {
                        return;
                    }
                    string filename = ofd.FileName;
                    string[] lines = File.ReadLines(filename, Encoding.Default).ToArray();
                    MessageBox.Show("开始录入数据!!!");
                    //IEnumerable<string> lines = File.ReadLines(filename,Encoding.Default).ToArray();
                    for (int i = 1; i < lines.Count(); i++)
                    {
                        string line = (string)lines[i];
                        string[] segs = line.Split('\t');
                        string startTelNum = segs[0];
                        string city = segs[1];
                        city = city.Trim('"');
                        string telType = segs[2];
                        telType = telType.Trim('"');
                        string areadigit = segs[3];
                        
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            /*cmd.CommandText = "insert into  T_TelPhone(StartTelNum,City,Type,AreaDigit) values(@starttelnum,@city,@type,@areadigit)";
                            cmd.Parameters.Add(new SqlParameter("@starttelnum",startTelNum));
                            cmd.Parameters.Add(new SqlParameter("@city", city));
                            cmd.Parameters.Add(new SqlParameter("@type", telType));
                            cmd.Parameters.Add(new SqlParameter("@areadigit", areadigit));*/
                            cmd.CommandText =@"insert into T_TelPhone(StartTelNum,City,Type,AreaDigit) values('1','1','1','1')";
                        }
                    }
                   
                    MessageBox.Show("录入完毕，共"+lines.Count()+"条信息");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
