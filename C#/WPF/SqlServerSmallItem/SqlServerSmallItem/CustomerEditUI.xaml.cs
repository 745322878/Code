using SqlServerSmallItem.DAL;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SqlServerSmallItem
{
    /// <summary>
    /// CustomerEditUI.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerEditUI : Window
    {
        public bool IsInsert { get; set; }
        public long EditingId { get; set; }
        public CustomerEditUI()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtClass.Text == "" || txtNumber.Text == "" || txtSchool.Text == "" || txtScore.Text == "" || txtTel.Text == "")
            {
                MessageBox.Show("输入不能为空!");
            }
            else
            {
                /*Customer customer=(Customer)grid.DataContext;*/
                Customer customer = new Customer();
                customer.Class = txtClass.Text;
                customer.Hobbies = txtHobbies.Text;
                customer.Name = txtName.Text;
                customer.Number = txtNumber.Text;
                customer.School = txtSchool.Text;
                customer.Score = Convert.ToInt32(txtScore.Text);
                customer.TelPhone = txtTel.Text;
                //增加保存
                if (IsInsert == true)
                {
                    
                    CustomerDAL.Insert(customer);
                }
                //修改
                else
                {
                   
                    customer.Id = EditingId;
                    CustomerDAL.Modify(customer);
                }
                DialogResult = true;
                
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //增加
            if (IsInsert == true)
            {
                Customer customer = new Customer();
                grid.DataContext = customer;
            }
            //修改
            else
            {
                Customer cs = new CustomerDAL().GetById(EditingId);
                /*txtName.Text = cs.Name;
                txtClass.Text = cs.Class;
                txtHobbies.Text =cs.Hobbies;
                txtNumber.Text  = cs.Number;
                txtSchool.Text =cs.School;
                txtScore.Text =cs.Score.ToString();
                txtTel.Text=cs.TelPhone;*/
                //使用数据绑定简化
                stackpanel.DataContext = cs;

            }
        }
    }
}
