using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace 扫雷2
{
    public class UserButton : Button 
    {
        /// <summary>
        /// 获取或设置当前方格是否埋有地雷
        /// </summary>
        public bool HasMine { get; set; }
        /// <summary>
        /// 获取或设置当前方块周围的地雷数量
        /// </summary>
        public int AroundMineCount { get; set; }
        /// <summary>
        /// 获取或设置当前方块的状态（打开或关闭）
        /// </summary>
        public  bool IsShow { get; set; }
        //方格坐标
        public int x;
        public int y;
        //游戏状态（已开始或结束）
        public bool IsBegin { get; set; }
        //方格打开后的样子
        public void Open(UserButton u)
        {
            //u.IsEnabled = false;
            u.IsShow =true ;
            if (u.HasMine)
            {
                 //u.Background = new SolidColorBrush(Colors.Red);
                 //Image imgItem = new Image();
                 
                 //imgItem.Source = (new BitmapImage(new Uri("ms-appx:///Assest/1.jpg",UriKind.RelativeOrAbsolute)));
                 //u.Content = imgItem;
                 u.IsEnabled = false;
            }
            else
            {
                switch (u.AroundMineCount)
                {
                    case 0:
                        u.Background = new SolidColorBrush(Colors.PapayaWhip);
                        break;
                    case 1:       
                        u.Content = "1    111";                     
                        u.FontSize = 30;
                    
                        u.Foreground = new SolidColorBrush(Color.FromArgb(255,0,0,253));
                        u.Background = new SolidColorBrush(Color.FromArgb(255,0,255,255));
                       
                        break;
                    case 2:
                        

                        u.Content = "2    222";
                        u.FontSize = 30;
                        u.Foreground = new SolidColorBrush(Color.FromArgb (255,0,128,0) );
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                        break;
                    case 3:
                       
                       

                        u.Content = "3   333";
                       
                        u.FontSize = 30;
                        u.Foreground = new SolidColorBrush(Colors.Black   );
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));                  
                        break;
                    case 4:
                        this.Content = "4   444" ;
                        this.FontSize = 30;
                        this.Foreground = new SolidColorBrush(Color.FromArgb(255,1,1,127));
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                        break;
                    case 5:
                        this.Content = "5   555";
                        this.FontSize = 30;
                        
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                        break;
                    case 6:
                        this.Content = "6   666";
                        this.FontSize = 30;
                       
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                        break;
                    case 7:
                        this.Content = "7   777";
                        this.FontSize = 30;
                       
                        u.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                        break;
                    case 8:
                        this.Content = "8   888";
                        this.FontSize = 30;
                       
                        u.Background = new SolidColorBrush(Colors.LightGreen);
                        break;


                }
            }

        }
    }
}

