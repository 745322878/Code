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

namespace DataBinding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Person p=new Person ();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {

            p.Name = "王勇智";
            p.Age = 19;

            nameTextBox.DataContext = p;
            ageTextBox.DataContext = p;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            p.Age++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(p.Age.ToString());
        }
    }
}
