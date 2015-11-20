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

namespace DataBing1
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
            List<Person> p = new List<Person>();
            Person p1 = new Person();
            Person p2 = new Person();
            p1.Name = "熊大";
            p1.Age = 19;
            p2.Name = "熊二";
            p2.Age = 18;
            Person p3 = new Person { Name = "熊儿", Age = 17 };
            p.Add(p1);
            p.Add(p2);
            p.Add(p3);
            listBox.ItemsSource = p;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = listBox.SelectedItem;
            object selectedValue = listBox.SelectedValue;
        }
    }
}
