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

namespace DataGrid
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            List<Student> students=new List<Student> ();
            List<string> classes = new List<string>();

            //Student stu = new Student();
            //stu.Name = "熊大";
            //stu.Age = 18;
            //stu.Gender = true;
            //stu.Class = "一班";
            //Student stu1 = new Student { Name = "熊二", Age = 19 };
            //Student stu2 = new Student { Name = "熊三", Age = 20 };
            students.Add(new Student { Name = "熊大", Age = 18 ,Score=60});
            students.Add(new Student { Name = "熊二", Age = 19 ,Score=70});
            students.Add(new Student { Name = "熊三", Age = 20 ,Score=80});
            students.Add(new Student { Name = "熊五", Age = 21 ,Score =90});

            dataGrid.ItemsSource = students;

            classes.Add("一班");
            classes.Add("二班");
            classes.Add("三班");
            comboBox.ItemsSource = classes;
        }
    }
}
