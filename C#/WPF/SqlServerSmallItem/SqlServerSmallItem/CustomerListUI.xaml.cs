using SqlServerSmallItem.DAL;
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

namespace SqlServerSmallItem
{
    /// <summary>
    /// CustomerListUI.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerListUI : Window
    {
        public CustomerListUI()
        {
            InitializeComponent();
           
        }

        private void LoadedData()
        {
            gridCustomers.ItemsSource = CustomerDAL.GetAll(); 
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadedData();  
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            Customer cs = (Customer)gridCustomers.SelectedItem;
            if (cs == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }
            //弹出消息框
            if (MessageBox.Show("确认删除此条数据吗？？？", "提醒", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CustomerDAL.DeleteById(cs.Id);

                //Window_Loaded(sender, e);
                LoadedData(); //刷新数据
                
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Customer cs = (Customer)gridCustomers.SelectedItem;
            if (cs == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            CustomerEditUI editUI = new CustomerEditUI();
            editUI.IsInsert = false;
            editUI.EditingId = cs.Id;
            if (editUI.ShowDialog() == true)
                LoadedData();

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            CustomerEditUI editUI = new CustomerEditUI();
            editUI.IsInsert = true;
            if (editUI.ShowDialog() == true)
                LoadedData();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            Customer[] customer = CustomerDAL.GetByName(txtSearch.Text);
            gridCustomers.ItemsSource = customer;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                LoadedData();  
            }
        }
    }
}
