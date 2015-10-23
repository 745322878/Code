using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace 扫雷2
{
    
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
            DispatcherTimer timer = null;
            public int showNum = 0;
            int i = 0;
            public MainPage()
            {
                this.InitializeComponent();
                Init(9,9);
                
            }      
            /// <summary>
            ///  初始化雷区
            /// </summary>
            /// <param name="panLineNum"> 方格边长</param>
            /// <param name="mineCount"> 地雷数量</param>
            public void Init(int panLineNum, int mineCount)
            {
                
                //放置方格,布局方格位置
                for (int i = 0; i <panLineNum  ; i++)
                {
                    for (int j = 0; j < panLineNum ; j++)
                    {
                        UserButton userButton = new UserButton();
                        userButton.Click += new RoutedEventHandler(mouse_Press );
                        
                        //userButton.Style = (Style)Resources["ButtonStyle1"];
                        userButton.Height = 65;
                        userButton.IsShow = false ;
                        userButton.HasMine = false;
                        userButton.AroundMineCount = 0;
                        userButton.Background = new SolidColorBrush(Colors.Yellow);
                        MineFieldGrid.Children.Add(userButton);
                        Grid.SetRow(userButton, i);
                        Grid.SetColumn(userButton, j);
                        userButton.IsEnabled = true;
                        userButton.x = i;
                        userButton.y = j;
                        showNum = 0;
                    }
                }
                //放置地雷
                this.LayMine(mineCount );
                //设置方格点击后显示四周的地雷数
                foreach (UserButton button in MineFieldGrid.Children)
                {
                    button.AroundMineCount = this.GetAroundMineCount(button );
                }
            }
            //布雷
            private void LayMine(int mineCount)
            {
                Random ran = new Random();
                for (int i = 0; i < mineCount; i++)
                {

                    int mine_x = ran.Next(0, 9);
                    int mine_y = ran.Next(0, 9);
                    foreach (UserButton userButton in MineFieldGrid.Children)
                    {
                        if (userButton.x == mine_x && userButton.y == mine_y && userButton.HasMine == false)
                        {
                            userButton.HasMine = true;
                        }
                        else if (userButton.x == mine_x && userButton.y == mine_y && userButton.HasMine == true)
                        {
                            i--;
                            break;
                        }
                    }
                }
            }
            //得到userButton周围雷数
            private int GetAroundMineCount(UserButton userButton )
            {
                int minecount = 0;

                foreach (UserButton p in MineFieldGrid.Children)
                {
                    if ((Math.Abs(p.x - userButton.x) == 1 && p.y == userButton.y) 
                        || (Math.Abs(p.y - userButton.y) == 1 && p.x == userButton.x)
                        || (Math.Abs(p.x - userButton.x) == 1 && Math.Abs(p.y - userButton.y) == 1))
                    {
                        if (p.HasMine)
                            minecount++;
                    }
                }              
                return minecount;
                
            }         
            /// <summary>
            /// 点击鼠标事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>

            private  void mouse_Press(object sender, RoutedEventArgs e)
            {
                    
                    UserButton userButton = sender as UserButton;
                   
                    //userButton.IsEnabled = false;
                    userButton.IsShow = true  ;
                    
                    if (showNum == 0)
                    {
                        foreach (UserButton showButton in MineFieldGrid.Children)
                        {
                            if (showButton.IsShow == true)
                            {
                                showNum++;
                            }

                        }
                        if (showNum == 1)
                        {

                            this.showTime();
                        }
                    }
                  
                    if (this.IsAllMineSweeped())
                    {
                        this.DisplayAll();
                        this.MineField_MineSweeppedSuccessfully();
                       
                    }
                    else
                    {
                        if (userButton.HasMine)
                        {
                            
                            this.DisplayAll();
                            userButton.Open(userButton);
                            timer.Stop();
                           
                            this.MineField_MineSweeppedFaid();
                        }
                        else
                        {
                            this.DisplayAround(userButton);
                        }                    
                    }
            }
            /// <summary>
            /// 计时器
            /// </summary>
            private void showTime()
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
              
            }
            
            void timer_Tick(object sender, object e)
            {    
                i++;
                this .timeText.Text =i.ToString() ;
            }        
            /// <summary>
            /// 显示雷区全部内容
            /// </summary>
            public void DisplayAll()
            {             
                foreach (UserButton userButton in MineFieldGrid.Children)
                {
                    //if (userButton.IsShow)
                    //    continue;
                    userButton.Open(userButton);
                }
            }
            /// <summary>
            /// 递归调用显示周围没雷的方块
            /// </summary>
            /// <param name="currentUserButton"></param>
            /// 
            public void DisplayAround(UserButton userbutton)
            {
                if (userbutton.AroundMineCount != 0 )
                {
                    userbutton.Open(userbutton);
                    
                    return;
                }
                else
                {
                    userbutton.Open(userbutton);
                   
                    foreach (UserButton button in MineFieldGrid.Children)
                    {
                        if (button.IsShow  ==true )
                        {
                            continue;
                        }
                        else
                        {
                            if (Math.Abs(button.x - userbutton.x) == 1 && Math.Abs(button.y - userbutton.y) == 0 && button.IsShow==false )
                                DisplayAround(button);
                            if (Math.Abs(button.y - userbutton.y) == 1 && Math.Abs(button.x - userbutton.x) == 0 && button.IsShow==false )
                                DisplayAround(button);
                            if (Math.Abs(button.x - userbutton.x) == 1 && Math.Abs(button.y - userbutton.y) == 1 && button.IsShow==false )
                                DisplayAround(button);
                        }
                    }
                }
            }   
             /// <summary>
            /// 是否扫雷成功
            /// </summary>
            /// <returns></returns>
            private bool IsAllMineSweeped()
            {
                //int minecount = 0;
                int closecount=0;
                foreach (UserButton userButton in this.MineFieldGrid.Children)
                {                    
                     if (userButton.IsShow==false )
                    {
                        closecount++;
                    }
                }
                return 9==closecount ;
            }
            //开始按钮
            private  void BeginButton_Click(object sender, RoutedEventArgs e)
            {
               
                int openNum = 0;
                foreach (UserButton button in MineFieldGrid.Children)
                {
                    if (button.IsShow == true  )
                    {
                        openNum++;
                    }       
                }
                if (openNum == 0 || openNum == 81)
                {

                    this.Reset();
                }
                else
                {
                    timer.Stop();
                    timer = null;
                    this.DisplayAll();
                    
                }           
            }
            /// <summary>
            /// 重置雷与扫雷方格
            /// </summary>
            private void Reset()
            {
                foreach (UserButton allButton in MineFieldGrid.Children)
                {
                    allButton.IsShow = false;
                    allButton.HasMine = false;
                    allButton.AroundMineCount = 0;
                    allButton.Content = null;
                    allButton.Background = new SolidColorBrush(Colors.Yellow);
                    allButton.IsEnabled = true;


                }
                showNum = 0;
                timeText.Text = "Ready Go!";
                i = 0;
                this.LayMine(9);

                //设置方格点击后显示四周的地雷数
                foreach (UserButton button in MineFieldGrid.Children)
                {
                    button.AroundMineCount = this.GetAroundMineCount(button);
                }
            }
            //成功
            private async void MineField_MineSweeppedSuccessfully()
            {
                MessageDialog md = new MessageDialog("Victory,再来一局");
                UICommand yes = new UICommand("Yes");
                UICommand no = new UICommand("No");
                md.Commands.Add(yes);
                md.Commands.Add(no);
                var choice = await md.ShowAsync();
                if (choice == yes)
                {
                    this.Reset();
                }
                else
                {
                    App.Current.Exit(); ;
                }
                timer.Stop();
                timer = null;
            }
            //失败
            public  async void MineField_MineSweeppedFaid()
            {
                MessageDialog md = new MessageDialog("Sorry，侬失败了,是否继续!");
                UICommand yes = new UICommand("Yes");
                UICommand no = new UICommand("No");
                md.Commands.Add(yes);
                md.Commands.Add(no);
                var choice = await md.ShowAsync();
                if (choice == yes)
                {
                    this.Reset();
                }
                else
                {
                    App.Current.Exit();
                }
                timer.Stop();
                timer = null;

            }

            //private void next_Click(object sender, RoutedEventArgs e)
            //{
            //    this.Reset();
            //}

            //private void quit_Click(object sender, RoutedEventArgs e)
            //{
            //    this.Reset();
            //}    
        }
}      
