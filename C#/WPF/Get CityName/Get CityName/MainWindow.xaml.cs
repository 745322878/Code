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

namespace Get_CityName
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Citycs > provinceNames=new List<Citycs> ();
        public static List<Citycs> cityNames = new List<Citycs>();
        Citycs cityname;
        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
        }
        private void Window_Loaded()
        {
            DataTable dt = SqlHelper.ExecuteDataTable("select * from AreaFull where AreaPid=0");
            foreach (DataRow datarow in dt.Rows)
            { 
                string name=(string)datarow["AreaName"];
                int id = (int)datarow["AreaId"];
                cityname = new Citycs();
                cityname.AreaName = name;
                cityname.AreaId = id;
                provinceNames.Add(cityname);
            }
            lbprovince.ItemsSource = provinceNames;

        }

        private void lbprovince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //获得所选择的省的信息
            Citycs areaCity = (Citycs)lbprovince.SelectedItem;
            //获得所选省的全部城市
            DataTable dt = SqlHelper.ExecuteDataTable("select * from AreaFull where AreaId=@id", new SqlParameter("@id", areaCity.AreaId));
            foreach (DataRow datarow in dt.Rows)
            {
                //获得城市名字
                string name = (string)datarow["AreaName"];
                //获得城市id
                int id = (int)datarow["AreaId"];
                cityname = new Citycs();
                cityname.AreaName = name;
                cityname.AreaId = id;
                cityNames.Add(cityname);
            }
            //数据绑定,ListBox前端用DisplayMemberPath属性
            lbcity.ItemsSource = cityNames;
        }

      
        
    }
}
