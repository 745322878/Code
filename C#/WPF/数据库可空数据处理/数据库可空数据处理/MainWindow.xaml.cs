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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 数据库可空数据处理
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
            string name1 = name.Text;
            
            object  objname;
            if (name1.Length<=0)
            {
                //向数据库中存NULL时，程序中应该是DBNull.Value
                objname = DBNull.Value;
            }
            else
            {
                objname=name1;
            }
            object objage;
            if (age.Text.ToString().Length<=0)
            {
                objage  = DBNull.Value;
            }
            else
            {
                objage = Convert.ToInt32(age.Text);
            }
            decimal height1;
            if (height.Text.ToString().Length <= 0)
            {
                MessageBox.Show("请输入height,height不能为空!!!");
            }
            else 
            {
                height1 = decimal.Parse(height.Text);
                Helper.ExecuteNonQuery(@"insert into T_DBNULL(Name,Age,Height) values(@name,@age,@height)", new SqlParameter("@name", objname), new SqlParameter("@age", objage), new SqlParameter("@height", height1));
                MessageBox.Show("录入成功");
            }
         
           



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataTable dt = Helper.ExecuteDataTable(@"select * from T_DBNULL where Id=@id",new SqlParameter("@id",id.Text));
            DataRow row = dt.Rows[0];
            string  name;
            //读取的值如果在数据库中是NULL，则返回为DBNull.Value
            if (row["Name"] == DBNull.Value)
            {
                name = null;
            }
            else 
            {
                name = (string)row["Name"];
            }
            //对于可空列，要注意int?的问题
            //int?为可空类型
            int? age;
            if (row["Age"] == DBNull.Value)
            {
                age = null;
            }
            else
            { 
                age=(int)row["Age"];
            }
            decimal  height;
          
            height =(decimal  )(row["Height"])  ;
            MessageBox.Show(name+age+height);
        }
    }
}
