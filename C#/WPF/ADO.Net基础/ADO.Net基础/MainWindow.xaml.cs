using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ADO.Net基础
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
            //IDisposible,实现了此接口，方可使用using

            //SqlConnection 为建立和数据库连接的对象
            using (SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog=MyTest; User ID=sa;Password=wangyongzhi61011"))
            {
                try
                {

                    conn.Open();
                    MessageBox.Show("连接成功");
                    //通过连接创建一个向数据库发命令（Command）的对象SqlCommand
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        //CommandText 为要执行的SQL语句
                        //cmd.CommandText = "Insert into T_Student(Name,Class,Number,TelPhone,Score) values('李四','计科1506','04153019','13720517935','84')";
                        ////执行,  ExecuteNoQuery()一般用来执行Update,Delete,Insert语句
                        //cmd.ExecuteNonQuery();

                        //cmd.CommandText = "select Count(*) from T_Student";
                        //int i =(int )cmd.ExecuteScalar();
                        //MessageBox.Show(i + "个学生");

                        ///cmd.CommandText = "select * from T_Student where Class like '软件%'";
                        
                         cmd.CommandText = "select '王勇智'";
                        //ExecuteScalar一般用来执行有且只有一行一列返回值的SQL语句
                        string s = (string)cmd.ExecuteScalar();
                        MessageBox.Show("Name:" + s);

                       /* cmd.CommandText = "insert into T_Student(Name,Class,Number,TelPhone,Score) output inserted.Id values('张三','计科1501','04153101','13720517940','85')";
                        long i = (long)cmd.ExecuteScalar();
                        MessageBox.Show("Id:" + i);*/

                        /*cmd.CommandText = "select * from T_Student where Score>59";
                        //SqlDataReader 读取的内容存于数据库中，存在于一个指针指向存储数据的前一个位置
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //一个一个读，读到末尾时，reader.Read()为false
                            while (reader.Read())
                            {
                                //1,2,4，为列号,对应数据库中的Name,Class,TelPhone
                                string name = reader.GetString(1);
                                string class1 = reader.GetString(2);
                                string telphone = reader.GetString(4);
                                MessageBox.Show(name+","+class1+","+telphone);
                            }
                        }*/

                       // cmd.CommandText = "select score from T_Student where  ";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
               
            }
            MessageBox.Show("运行完毕");
        }
        /// <summary>
        /// 字符串连接查询,DataReader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source =.; Initial Catalog=MyTest; User ID=sa;Password=wangyongzhi61011"))
            {
                conn.Open();
                //MessageBox.Show("连接成功");
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.CommandText= "select Number from T_Student where Name=@Name or Score>="+"scoretextBox.Text";
                    //输入'1'  or '1=1' 会发生SQL注入漏洞
                    cmd.CommandText = "select Number from T_Student where Name=@Name or Score>=@score";
                    //insert into .....  values(@Name,@Score)
                    //delete ...... where ID=@id
                    //@参数不能用于替换表名，关键字，字段名
                    //select @age from @T  错误
                    cmd.Parameters.Add(new SqlParameter("@Name", textBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@score", Convert.ToInt32(scoretextBox.Text)));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            //GetInt32获得的是int类型
                            //GetInt64获得的是long类型（数据库是bigint）
                            string number = reader.GetString(0);
                            MessageBox.Show(number.ToString());
                        }
                    }
                }
            }
              


        }
        /// <summary>
        /// DataSet查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            //项目根目录添加一个"应用程序配置文件",名字为App.config(必须为这个，其他的名字不行)
            //App.config中添加结点，给add起一个name
            //引用添加System.Configuration
            //就能使用ConfigurationManager类了
            //wp，wpf,winform里都可以
            //asp.net里是web.config
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from T_Student where Score>@aaa";
                    cmd.Parameters.Add(new SqlParameter("@aaa", 59));

                    //SqlDataAdapter 是一个帮我们把SqlCommand查询结果填充到DataSet中的类
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //Dataset 相当于本地的一个复杂集合（eg：List<int>）
                    DataSet dataset = new DataSet();

                    adapter.Fill(dataset);//执行cmd并把SqlCommand查询结果填充到DataSet

                    DataTable table = dataset.Tables[0];
                    DataRowCollection rows = table.Rows;
                    for (int i = 0; i < rows.Count; i++)
                    {
                        DataRow row = rows[i];
                        string name = (string)row["Name"];
                        int score = (int)row["Score"];
                        MessageBox.Show(name+","+score);
                    }
                }
            }
        }
        /// <summary>
        /// 自定义SqlHelper类,封装一些方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           /* int i =(int )SqlHelper.ExecuteScalar("select Count(*) from T_Student");
            MessageBox.Show("Changes has" + i + "条");*/

            //SqlParameter[] s = new SqlParameter[1] {new SqlParameter("@score",70)};
            
            //固定参数
            //DataTable  dt=SqlHelper.ExecuteDataTable("select * from T_Student where Score>@score and Number>@number",new SqlParameter[]{new SqlParameter("@score",70),new SqlParameter("number",04143102)});
            //可变参数
            DataTable dt = SqlHelper.ExecuteDataTable("select * from T_Student where Score>@score and Number>@number",new SqlParameter("@score",70),new SqlParameter("number",04143102));
            //DataTable  dt=SqlHelper.ExecuteDataTable("select * from T_Student where Score>70");
            foreach (DataRow row in dt.Rows)
            { 
                 string name=(string)row["Name"];
                 MessageBox.Show(name);
            }
        }
    }
}