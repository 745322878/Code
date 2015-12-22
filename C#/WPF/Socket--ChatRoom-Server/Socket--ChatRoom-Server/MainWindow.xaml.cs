using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Socket__ChatRoom_Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public  static Socket server = null;            //

        public static  Socket[] clients = new Socket[40];       //连接上服务器的客户端

        public static  int socketnum;               //客户端的数目
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private    void Button_Click(object sender, RoutedEventArgs e)
        {
        
            Btn.Content = "取消侦听";
            Btn.IsEnabled = false;
            IPAddress ip = IPAddress.Any;
            IPEndPoint ipep = new IPEndPoint(ip, 11111);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(10);
            
            //Dispatcher.Invoke((Action)(() =>
            //{
               
            //}));         
           while(true )
            {
                //Thread th = new Thread(new ParameterizedThreadStart(Run1));
                //th.Start();
                Run1();
               
           }
            //Socket client = server.Accept();
            /*while (true)
            {
                Thread th = new Thread(new ParameterizedThreadStart (WaitConnect));
                th.Start();
             }*/
           // WaitConnect();
          
           
        }

        private void Run1(  )
        {
            server.BeginAccept(CallbackFile, server);
          
        }
        //private void WaitConnect()
        //{
        //    Socket client = server.Accept();
        //    App.socketServer = client;
          
        //    SocketData soc = new SocketData();
        //    soc.Show();
        //    this.Close();
        //}
        
        //和BeiginAccept一起使用,当BeiginAccept成功运行时执行CallbackFile
        private void CallbackFile(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            
            App.socketServer = s.EndAccept(ar);
           
            
            clients[socketnum++] =App.socketServer;
            if (socketnum == 1)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    SocketData soc = new SocketData();
                    soc.Show();
                    MessageBox.Show("连接成功");
                    this.Close();
                }));
            }
        }
    }
}
